using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KeyEventLogger.Core;
using KeyEventLogger.Models;
using KeyEventLogger.Services;

namespace KeyEventLogger
{
    /// <summary>
    /// Ana pencere. Klavye olaylarını yakalayıp KeyEventRecorder nesnesine iletir ve
    /// recorder'ın olaylarına (event) abone olarak arayüzü günceller.
    /// Kayıt mantığı formda değil, KeyEventRecorder sınıfındadır.
    ///
    /// GÜVENLİK SINIRI: Olaylar yalnızca formun kendi KeyDown/KeyUp/KeyPress olaylarından
    /// alınır (KeyPreview). Global klavye hook'u, arka planda çalışma, otomatik başlatma
    /// veya ağ üzerinden gönderme yoktur. Pencere odakta değilken Windows bu olayları
    /// uygulamaya hiç göndermez.
    /// </summary>
    public partial class MainForm : Form
    {
        private static readonly Color RecordingColor = Color.FromArgb(220, 38, 38);
        private static readonly Color StoppedColor = Color.FromArgb(100, 116, 139);

        private readonly KeyEventRecorder recorder = new KeyEventRecorder();
        // Desteklenen kayıt biçimleri. Yeni bir biçim için yalnızca FileLogWriterBase'den
        // türeyen bir sınıf yazıp bu listeye eklemek yeterlidir (form kodu değişmez).
        private readonly FileLogWriterBase[] logWriters = { new TextFileLogWriter(), new CsvLogWriter() };

        /// <summary>Uygulanan zaman filtresi; null ise tüm kayıtlar gösterilir.</summary>
        private TimeRangeFilter activeFilter;

        public MainForm()
        {
            InitializeComponent();

            // Recorder olaylarına abone ol (Observer yaklaşımı)
            recorder.RecordAdded += Recorder_RecordAdded;
            recorder.RecordingStateChanged += Recorder_RecordingStateChanged;
            recorder.RecordsCleared += Recorder_RecordsCleared;

            // Filtre varsayılanı: bugünün tamamı
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today.AddDays(1).AddSeconds(-1);

            UpdateRecordingUi();
            UpdateStatistics();
        }

        // ---------------------------------------------------------------
        //  Klavye olayları (yalnızca bu form aktifken gelir)
        // ---------------------------------------------------------------

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            HandleRecord(KeyEventRecord.FromKeyEvent(KeyEventType.KeyDown, e, DateTime.Now));
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            HandleRecord(KeyEventRecord.FromKeyEvent(KeyEventType.KeyUp, e, DateTime.Now));
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            HandleRecord(KeyEventRecord.FromKeyPress(e, DateTime.Now));
        }

        private void HandleRecord(KeyEventRecord record)
        {
            try
            {
                recorder.TryRecord(record);
            }
            catch (Exception ex)
            {
                // Bir olayın işlenememesi uygulamayı kapatmamalı
                tsslState.Text = "Olay işlenemedi: " + ex.Message;
            }
        }

        // ---------------------------------------------------------------
        //  Recorder olaylarına verilen tepkiler
        // ---------------------------------------------------------------

        private void Recorder_RecordAdded(object sender, KeyEventRecordedEventArgs e)
        {
            // Filtre açıksa yalnızca aralığa uyan yeni kayıtlar listeye eklenir
            if (activeFilter == null || activeFilter.IsMatch(e.Record))
                AddToList(e.Record);

            if (e.Record.EventType != KeyEventType.KeyUp)
                captureBox.LastKeyText = e.Record.KeyName;
            UpdateCount();
            UpdateStatistics();
        }

        private void Recorder_RecordingStateChanged(object sender, EventArgs e)
        {
            UpdateRecordingUi();
        }

        private void Recorder_RecordsCleared(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
            captureBox.LastKeyText = string.Empty;
            UpdateCount();
            UpdateStatistics();
        }

        // ---------------------------------------------------------------
        //  Düğmeler
        // ---------------------------------------------------------------

        private void BtnStart_Click(object sender, EventArgs e)
        {
            recorder.Start();
            captureBox.Focus();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            recorder.Stop();
            captureBox.Focus();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (recorder.Count == 0)
            {
                MessageBox.Show(this, "Temizlenecek kayıt yok.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult answer = MessageBox.Show(this,
                recorder.Count + " kayıt ekrandan silinecek. Emin misiniz?", "Temizle",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
                recorder.Clear();

            captureBox.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveRecords();
            captureBox.Focus();
        }

        /// <summary>
        /// Kayıtları kullanıcının seçtiği .txt dosyasına yazar.
        /// Başarılıysa true, iptal edildiyse veya hata oluştuysa false döner.
        /// </summary>
        private bool SaveRecords()
        {
            // Filtre açıksa ekranda görünen (süzülmüş) kayıtlar kaydedilir
            IReadOnlyCollection<KeyEventRecord> toSave = GetVisibleRecords();
            if (toSave.Count == 0)
            {
                string msg = recorder.Count == 0
                    ? "Kaydedilecek kayıt yok. Önce \"Başlat\" ile kayıt alın."
                    : "Seçilen zaman aralığında kayıt yok. Filtreyi değiştirin veya kaldırın.";
                MessageBox.Show(this, msg, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Kayıtları dosyaya kaydet";
                dialog.Filter = string.Join("|", logWriters.Select(w => w.FileFilter));
                dialog.FileName = "KeyEventLog_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                dialog.AddExtension = true;
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.OverwritePrompt = true;

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return false;

                try
                {
                    // Biçim, dosya uzantısına göre seçilir (uzantı yazılmadıysa seçilen dosya türü)
                    string extension = System.IO.Path.GetExtension(dialog.FileName);
                    FileLogWriterBase writer = logWriters.FirstOrDefault(w =>
                        string.Equals(w.Extension, extension, StringComparison.OrdinalIgnoreCase))
                        ?? logWriters[Math.Clamp(dialog.FilterIndex - 1, 0, logWriters.Length - 1)];
                    writer.Write(dialog.FileName, toSave);
                    if (activeFilter == null)
                        recorder.MarkSaved(); // Tüm kayıtlar yazıldı
                    tsslState.Text = toSave.Count + " kayıt kaydedildi: " + System.IO.Path.GetFileName(dialog.FileName);
                    MessageBox.Show(this, toSave.Count + " kayıt başarıyla kaydedildi.\n\n" + dialog.FileName,
                        "Kaydedildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (LogFileException ex)
                {
                    MessageBox.Show(this, ex.Message + "\n\nAyrıntı: " + ex.InnerException?.Message,
                        "Kaydetme hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                activeFilter = new TimeRangeFilter(dtpFrom.Value, dtpTo.Value);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Geçersiz aralık",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefreshList();
            lblFilterInfo.Text = "Filtre açık:\n" + activeFilter + "\n" +
                                 lstLog.Items.Count + " / " + recorder.Count + " kayıt gösteriliyor.";
            captureBox.Focus();
        }

        private void BtnClearFilter_Click(object sender, EventArgs e)
        {
            activeFilter = null;
            RefreshList();
            lblFilterInfo.Text = "Filtre yok: tüm kayıtlar gösteriliyor.";
            captureBox.Focus();
        }

        private void EventTypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            recorder.SetEnabled(KeyEventType.KeyDown, chkKeyDown.Checked);
            recorder.SetEnabled(KeyEventType.KeyUp, chkKeyUp.Checked);
            recorder.SetEnabled(KeyEventType.KeyPress, chkKeyPress.Checked);
        }

        // ---------------------------------------------------------------
        //  Pencere odağı
        // ---------------------------------------------------------------

        private void MainForm_Activated(object sender, EventArgs e)
        {
            captureBox.WindowActive = true;
            tsslFocus.Text = "Pencere aktif: olaylar yakalanabilir";
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            captureBox.WindowActive = false;
            tsslFocus.Text = "Pencere arka planda: olay yakalanmıyor";
        }

        /// <summary>Kaydedilmemiş kayıt varsa kapatmadan önce kullanıcıya sorar.</summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!recorder.HasUnsavedRecords || recorder.Count == 0) return;

            DialogResult answer = MessageBox.Show(this,
                recorder.Count + " kayıt henüz dosyaya kaydedilmedi. Çıkmadan önce kaydetmek ister misiniz?",
                "Kaydedilmemiş kayıtlar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if (answer == DialogResult.Cancel)
                e.Cancel = true;
            else if (answer == DialogResult.Yes && !SaveRecords())
                e.Cancel = true;
        }

        // ---------------------------------------------------------------
        //  Arayüz yardımcıları
        // ---------------------------------------------------------------

        private void AddToList(KeyEventRecord record)
        {
            lstLog.Items.Add(record);
            lstLog.TopIndex = lstLog.Items.Count - 1; // Son kayda kaydır
        }

        private void UpdateCount()
        {
            tsslCount.Text = activeFilter == null
                ? "Kayıt sayısı: " + recorder.Count
                : "Kayıt sayısı: " + recorder.Count + " (gösterilen " + lstLog.Items.Count + ")";
        }

        /// <summary>Ekranda gösterilen kayıtlar: filtre varsa süzülmüş liste, yoksa tümü.</summary>
        private IReadOnlyCollection<KeyEventRecord> GetVisibleRecords()
        {
            if (activeFilter == null)
                return recorder.Records;
            return activeFilter.Apply(recorder.Records);
        }

        /// <summary>Listeyi aktif filtreye göre baştan doldurur.</summary>
        private void RefreshList()
        {
            lstLog.BeginUpdate();
            try
            {
                lstLog.Items.Clear();
                foreach (KeyEventRecord record in GetVisibleRecords())
                    lstLog.Items.Add(record);
                if (lstLog.Items.Count > 0)
                    lstLog.TopIndex = lstLog.Items.Count - 1;
            }
            finally
            {
                lstLog.EndUpdate();
            }
            UpdateCount();
        }

        /// <summary>İstatistik panelini tüm kayıtlara göre yeniler.</summary>
        private void UpdateStatistics()
        {
            var stats = new KeyStatistics(recorder.Records);
            lblTotalEvents.Text = "Toplam olay: " + stats.TotalEvents;
            lblKeyPressCount.Text = "Toplam tuş basımı (KeyDown): " + stats.KeyPressCount;
            lblUniqueKeys.Text = "Farklı tuş sayısı: " + stats.UniqueKeyCount;

            lstTopKeys.BeginUpdate();
            lstTopKeys.Items.Clear();
            int rank = 1;
            foreach (KeyValuePair<string, int> pair in stats.GetTopKeys(5))
            {
                lstTopKeys.Items.Add(string.Format("{0}. {1,-14} {2,4} kez", rank, pair.Key, pair.Value));
                rank++;
            }
            if (lstTopKeys.Items.Count == 0)
                lstTopKeys.Items.Add("Henüz tuş basılmadı.");
            lstTopKeys.EndUpdate();
        }

        /// <summary>Kayıt durum göstergesini, düğmeleri ve başlığı günceller.</summary>
        private void UpdateRecordingUi()
        {
            bool on = recorder.IsRecording;

            lblRecordingIndicator.Text = on ? "●  KAYIT AÇIK" : "■  KAYIT DURDU";
            lblRecordingIndicator.BackColor = on ? RecordingColor : StoppedColor;

            btnStart.Enabled = !on;
            btnStop.Enabled = on;
            captureBox.Recording = on;

            Text = on ? "● KAYITTA - Key Event Logger" : "Key Event Logger";
            tsslState.Text = on
                ? "Durum: Kayıt açık (başlangıç " + recorder.StartedAt.Value.ToString("HH:mm:ss") + ")"
                : "Durum: Kayıt kapalı";
        }

        /// <summary>ListBox satırlarını olay türüne göre renklendirir.</summary>
        private void LstLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0 || e.Index >= lstLog.Items.Count) return;

            var record = lstLog.Items[e.Index] as KeyEventRecord;
            Color color = Color.Black;
            if (record != null)
            {
                switch (record.EventType)
                {
                    case KeyEventType.KeyDown: color = Color.FromArgb(21, 128, 61); break;
                    case KeyEventType.KeyUp: color = Color.FromArgb(100, 116, 139); break;
                    case KeyEventType.KeyPress: color = Color.FromArgb(29, 78, 216); break;
                }
            }

            if (e.Index % 2 == 1)
            {
                using (var stripe = new SolidBrush(Color.FromArgb(248, 250, 252)))
                    e.Graphics.FillRectangle(stripe, e.Bounds);
            }

            TextRenderer.DrawText(e.Graphics, lstLog.Items[e.Index].ToString(), e.Font,
                new Point(e.Bounds.X + 4, e.Bounds.Y + 1), color, TextFormatFlags.NoPrefix);
        }
    }
}
