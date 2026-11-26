using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KeyEventLogger.Models;

namespace KeyEventLogger.Core
{
    /// <summary>
    /// Kayıt oturumunu yöneten sınıf: kaydın açık/kapalı durumu, hangi olay türlerinin
    /// yakalanacağı ve toplanan kayıtların listesi burada tutulur.
    /// Arayüzden bağımsızdır; form yalnızca olaylara (event) abone olarak ekranı günceller.
    /// </summary>
    public class KeyEventRecorder
    {
        private readonly List<KeyEventRecord> records = new List<KeyEventRecord>();
        private readonly HashSet<KeyEventType> enabledTypes = new HashSet<KeyEventType>
        {
            KeyEventType.KeyDown, KeyEventType.KeyUp, KeyEventType.KeyPress
        };

        /// <summary>Kayıt açıkken true.</summary>
        public bool IsRecording { get; private set; }

        /// <summary>Son "Başlat" işleminin zamanı; hiç başlatılmadıysa null.</summary>
        public DateTime? StartedAt { get; private set; }

        /// <summary>Son kaydetme işleminden sonra yeni kayıt eklendiyse true.</summary>
        public bool HasUnsavedRecords { get; private set; }

        /// <summary>Kayıtlar dışarıya salt okunur olarak verilir (kapsülleme).</summary>
        public ReadOnlyCollection<KeyEventRecord> Records => records.AsReadOnly();

        public int Count => records.Count;

        /// <summary>Yeni bir kayıt eklendiğinde tetiklenir.</summary>
        public event EventHandler<KeyEventRecordedEventArgs> RecordAdded;

        /// <summary>Kayıt başlatıldığında veya durdurulduğunda tetiklenir.</summary>
        public event EventHandler RecordingStateChanged;

        /// <summary>Tüm kayıtlar temizlendiğinde tetiklenir.</summary>
        public event EventHandler RecordsCleared;

        public void Start()
        {
            if (IsRecording) return;
            IsRecording = true;
            StartedAt = DateTime.Now;
            RecordingStateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            if (!IsRecording) return;
            IsRecording = false;
            RecordingStateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Clear()
        {
            records.Clear();
            HasUnsavedRecords = false;
            RecordsCleared?.Invoke(this, EventArgs.Empty);
        }

        public bool IsEnabled(KeyEventType type) => enabledTypes.Contains(type);

        /// <summary>Belirli bir olay türünün yakalanıp yakalanmayacağını ayarlar.</summary>
        public void SetEnabled(KeyEventType type, bool enabled)
        {
            if (enabled) enabledTypes.Add(type);
            else enabledTypes.Remove(type);
        }

        /// <summary>
        /// Kayıt açıksa ve olay türü seçiliyse kaydı listeye ekler.
        /// Eklendiyse true, kayıt kapalı ya da tür devre dışıysa false döner.
        /// </summary>
        public bool TryRecord(KeyEventRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));
            if (!IsRecording || !IsEnabled(record.EventType)) return false;

            records.Add(record);
            HasUnsavedRecords = true;
            RecordAdded?.Invoke(this, new KeyEventRecordedEventArgs(record));
            return true;
        }

        /// <summary>Kayıtlar dosyaya yazıldıktan sonra çağrılır.</summary>
        public void MarkSaved()
        {
            HasUnsavedRecords = false;
        }
    }
}
