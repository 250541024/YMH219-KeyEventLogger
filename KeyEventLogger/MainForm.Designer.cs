namespace KeyEventLogger
{
    partial class MainForm
    {
        /// <summary>
        /// Tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanılan kaynakları temizler.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new System.Windows.Forms.Panel();
            lblRecordingIndicator = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            lblTitle = new System.Windows.Forms.Label();
            btnStart = new System.Windows.Forms.Button();
            btnStop = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            lblEventTypes = new System.Windows.Forms.Label();
            chkKeyDown = new System.Windows.Forms.CheckBox();
            chkKeyUp = new System.Windows.Forms.CheckBox();
            chkKeyPress = new System.Windows.Forms.CheckBox();
            captureBox = new KeyEventLogger.Controls.KeyCaptureBox();
            lstLog = new System.Windows.Forms.ListBox();
            grpStats = new System.Windows.Forms.GroupBox();
            lblTotalEvents = new System.Windows.Forms.Label();
            lblKeyPressCount = new System.Windows.Forms.Label();
            lblUniqueKeys = new System.Windows.Forms.Label();
            lblTopKeysTitle = new System.Windows.Forms.Label();
            lstTopKeys = new System.Windows.Forms.ListBox();
            grpFilter = new System.Windows.Forms.GroupBox();
            lblFrom = new System.Windows.Forms.Label();
            dtpFrom = new System.Windows.Forms.DateTimePicker();
            lblTo = new System.Windows.Forms.Label();
            dtpTo = new System.Windows.Forms.DateTimePicker();
            btnApplyFilter = new System.Windows.Forms.Button();
            btnClearFilter = new System.Windows.Forms.Button();
            lblFilterInfo = new System.Windows.Forms.Label();
            lblEventType = new System.Windows.Forms.Label();
            cmbEventType = new System.Windows.Forms.ComboBox();
            statusStrip = new System.Windows.Forms.StatusStrip();
            tsslState = new System.Windows.Forms.ToolStripStatusLabel();
            tsslCount = new System.Windows.Forms.ToolStripStatusLabel();
            tsslFocus = new System.Windows.Forms.ToolStripStatusLabel();
            pnlHeader.SuspendLayout();
            grpStats.SuspendLayout();
            grpFilter.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            //
            // pnlHeader
            //
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            pnlHeader.Controls.Add(lblRecordingIndicator);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(1000, 66);
            pnlHeader.TabIndex = 0;
            //
            // lblRecordingIndicator
            //
            lblRecordingIndicator.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblRecordingIndicator.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblRecordingIndicator.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            lblRecordingIndicator.ForeColor = System.Drawing.Color.White;
            lblRecordingIndicator.Location = new System.Drawing.Point(768, 13);
            lblRecordingIndicator.Name = "lblRecordingIndicator";
            lblRecordingIndicator.Size = new System.Drawing.Size(220, 40);
            lblRecordingIndicator.TabIndex = 2;
            lblRecordingIndicator.Text = "■  KAYIT DURDU";
            lblRecordingIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            lblSubtitle.Location = new System.Drawing.Point(14, 40);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(520, 15);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Eğitsel amaçlıdır: klavye olayları yalnızca bu pencere odaktayken yakalanır. Global hook kullanılmaz.";
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location = new System.Drawing.Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(170, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Key Event Logger";
            //
            // btnStart
            //
            btnStart.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            btnStart.ForeColor = System.Drawing.Color.White;
            btnStart.Location = new System.Drawing.Point(12, 78);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(110, 34);
            btnStart.TabIndex = 1;
            btnStart.Text = "▶  Başlat";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += BtnStart_Click;
            //
            // btnStop
            //
            btnStop.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            btnStop.Enabled = false;
            btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            btnStop.ForeColor = System.Drawing.Color.White;
            btnStop.Location = new System.Drawing.Point(128, 78);
            btnStop.Name = "btnStop";
            btnStop.Size = new System.Drawing.Size(110, 34);
            btnStop.TabIndex = 2;
            btnStop.Text = "■  Durdur";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += BtnStop_Click;
            //
            // btnClear
            //
            btnClear.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            btnClear.Location = new System.Drawing.Point(244, 78);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(110, 34);
            btnClear.TabIndex = 3;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += BtnClear_Click;
            //
            // btnSave
            //
            btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            btnSave.Location = new System.Drawing.Point(360, 78);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(140, 34);
            btnSave.TabIndex = 4;
            btnSave.Text = "Dosyaya Kaydet…";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            //
            // lblEventTypes
            //
            lblEventTypes.AutoSize = true;
            lblEventTypes.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblEventTypes.Location = new System.Drawing.Point(522, 87);
            lblEventTypes.Name = "lblEventTypes";
            lblEventTypes.Size = new System.Drawing.Size(117, 15);
            lblEventTypes.TabIndex = 5;
            lblEventTypes.Text = "Yakalanacak olaylar:";
            //
            // chkKeyDown
            //
            chkKeyDown.AutoSize = true;
            chkKeyDown.Checked = true;
            chkKeyDown.CheckState = System.Windows.Forms.CheckState.Checked;
            chkKeyDown.Location = new System.Drawing.Point(650, 86);
            chkKeyDown.Name = "chkKeyDown";
            chkKeyDown.Size = new System.Drawing.Size(76, 19);
            chkKeyDown.TabIndex = 6;
            chkKeyDown.Text = "KeyDown";
            chkKeyDown.UseVisualStyleBackColor = true;
            chkKeyDown.CheckedChanged += EventTypeCheckBox_CheckedChanged;
            //
            // chkKeyUp
            //
            chkKeyUp.AutoSize = true;
            chkKeyUp.Checked = true;
            chkKeyUp.CheckState = System.Windows.Forms.CheckState.Checked;
            chkKeyUp.Location = new System.Drawing.Point(740, 86);
            chkKeyUp.Name = "chkKeyUp";
            chkKeyUp.Size = new System.Drawing.Size(61, 19);
            chkKeyUp.TabIndex = 7;
            chkKeyUp.Text = "KeyUp";
            chkKeyUp.UseVisualStyleBackColor = true;
            chkKeyUp.CheckedChanged += EventTypeCheckBox_CheckedChanged;
            //
            // chkKeyPress
            //
            chkKeyPress.AutoSize = true;
            chkKeyPress.Checked = true;
            chkKeyPress.CheckState = System.Windows.Forms.CheckState.Checked;
            chkKeyPress.Location = new System.Drawing.Point(815, 86);
            chkKeyPress.Name = "chkKeyPress";
            chkKeyPress.Size = new System.Drawing.Size(71, 19);
            chkKeyPress.TabIndex = 8;
            chkKeyPress.Text = "KeyPress";
            chkKeyPress.UseVisualStyleBackColor = true;
            chkKeyPress.CheckedChanged += EventTypeCheckBox_CheckedChanged;
            //
            // captureBox
            //
            captureBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            captureBox.Location = new System.Drawing.Point(12, 122);
            captureBox.Name = "captureBox";
            captureBox.Size = new System.Drawing.Size(976, 66);
            captureBox.TabIndex = 0;
            //
            // lstLog
            //
            lstLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lstLog.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            lstLog.Font = new System.Drawing.Font("Consolas", 10F);
            lstLog.HorizontalScrollbar = true;
            lstLog.IntegralHeight = false;
            lstLog.ItemHeight = 18;
            lstLog.Location = new System.Drawing.Point(12, 198);
            lstLog.Name = "lstLog";
            lstLog.SelectionMode = System.Windows.Forms.SelectionMode.None;
            lstLog.Size = new System.Drawing.Size(690, 420);
            lstLog.TabIndex = 9;
            lstLog.TabStop = false;
            lstLog.DrawItem += LstLog_DrawItem;
            //
            // grpStats
            //
            grpStats.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            grpStats.Controls.Add(lstTopKeys);
            grpStats.Controls.Add(lblTopKeysTitle);
            grpStats.Controls.Add(lblUniqueKeys);
            grpStats.Controls.Add(lblKeyPressCount);
            grpStats.Controls.Add(lblTotalEvents);
            grpStats.Location = new System.Drawing.Point(712, 194);
            grpStats.Name = "grpStats";
            grpStats.Size = new System.Drawing.Size(276, 214);
            grpStats.TabIndex = 11;
            grpStats.TabStop = false;
            grpStats.Text = "İstatistikler";
            //
            // lblTotalEvents
            //
            lblTotalEvents.AutoSize = true;
            lblTotalEvents.Location = new System.Drawing.Point(12, 24);
            lblTotalEvents.Name = "lblTotalEvents";
            lblTotalEvents.Size = new System.Drawing.Size(110, 15);
            lblTotalEvents.TabIndex = 0;
            lblTotalEvents.Text = "Toplam olay: 0";
            //
            // lblKeyPressCount
            //
            lblKeyPressCount.AutoSize = true;
            lblKeyPressCount.Location = new System.Drawing.Point(12, 45);
            lblKeyPressCount.Name = "lblKeyPressCount";
            lblKeyPressCount.Size = new System.Drawing.Size(150, 15);
            lblKeyPressCount.TabIndex = 1;
            lblKeyPressCount.Text = "Toplam tuş basımı: 0";
            //
            // lblUniqueKeys
            //
            lblUniqueKeys.AutoSize = true;
            lblUniqueKeys.Location = new System.Drawing.Point(12, 66);
            lblUniqueKeys.Name = "lblUniqueKeys";
            lblUniqueKeys.Size = new System.Drawing.Size(120, 15);
            lblUniqueKeys.TabIndex = 2;
            lblUniqueKeys.Text = "Farklı tuş sayısı: 0";
            //
            // lblTopKeysTitle
            //
            lblTopKeysTitle.AutoSize = true;
            lblTopKeysTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblTopKeysTitle.Location = new System.Drawing.Point(12, 92);
            lblTopKeysTitle.Name = "lblTopKeysTitle";
            lblTopKeysTitle.Size = new System.Drawing.Size(150, 15);
            lblTopKeysTitle.TabIndex = 3;
            lblTopKeysTitle.Text = "En sık kullanılan 5 tuş:";
            //
            // lstTopKeys
            //
            lstTopKeys.Font = new System.Drawing.Font("Consolas", 9.75F);
            lstTopKeys.IntegralHeight = false;
            lstTopKeys.ItemHeight = 15;
            lstTopKeys.Location = new System.Drawing.Point(12, 112);
            lstTopKeys.Name = "lstTopKeys";
            lstTopKeys.SelectionMode = System.Windows.Forms.SelectionMode.None;
            lstTopKeys.Size = new System.Drawing.Size(252, 90);
            lstTopKeys.TabIndex = 4;
            lstTopKeys.TabStop = false;
            //
            // grpFilter
            //
            grpFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            grpFilter.Controls.Add(lblFilterInfo);
            grpFilter.Controls.Add(cmbEventType);
            grpFilter.Controls.Add(lblEventType);
            grpFilter.Controls.Add(btnClearFilter);
            grpFilter.Controls.Add(btnApplyFilter);
            grpFilter.Controls.Add(dtpTo);
            grpFilter.Controls.Add(lblTo);
            grpFilter.Controls.Add(dtpFrom);
            grpFilter.Controls.Add(lblFrom);
            grpFilter.Location = new System.Drawing.Point(712, 414);
            grpFilter.Name = "grpFilter";
            grpFilter.Size = new System.Drawing.Size(276, 204);
            grpFilter.TabIndex = 12;
            grpFilter.TabStop = false;
            grpFilter.Text = "Zamana ve Olay Türüne Göre Filtrele";
            //
            // lblFrom
            //
            lblFrom.AutoSize = true;
            lblFrom.Location = new System.Drawing.Point(12, 28);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new System.Drawing.Size(62, 15);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "Başlangıç:";
            //
            // dtpFrom
            //
            dtpFrom.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpFrom.Location = new System.Drawing.Point(84, 24);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.ShowUpDown = true;
            dtpFrom.Size = new System.Drawing.Size(180, 23);
            dtpFrom.TabIndex = 1;
            //
            // lblTo
            //
            lblTo.AutoSize = true;
            lblTo.Location = new System.Drawing.Point(12, 62);
            lblTo.Name = "lblTo";
            lblTo.Size = new System.Drawing.Size(34, 15);
            lblTo.TabIndex = 2;
            lblTo.Text = "Bitiş:";
            //
            // dtpTo
            //
            dtpTo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpTo.Location = new System.Drawing.Point(84, 58);
            dtpTo.Name = "dtpTo";
            dtpTo.ShowUpDown = true;
            dtpTo.Size = new System.Drawing.Size(180, 23);
            dtpTo.TabIndex = 3;
            //
            // btnApplyFilter
            //
            btnApplyFilter.Location = new System.Drawing.Point(12, 122);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new System.Drawing.Size(120, 30);
            btnApplyFilter.TabIndex = 4;
            btnApplyFilter.Text = "Filtrele";
            btnApplyFilter.UseVisualStyleBackColor = true;
            btnApplyFilter.Click += BtnApplyFilter_Click;
            // 
            // lblEventType
            // 
            lblEventType.AutoSize = true;
            lblEventType.Location = new System.Drawing.Point(12, 94);
            lblEventType.Name = "lblEventType";
            lblEventType.Size = new System.Drawing.Size(60, 15);
            lblEventType.TabIndex = 7;
            lblEventType.Text = "Olay türü:";
            // 
            // cmbEventType
            // 
            cmbEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbEventType.Location = new System.Drawing.Point(84, 90);
            cmbEventType.Name = "cmbEventType";
            cmbEventType.Size = new System.Drawing.Size(180, 23);
            cmbEventType.TabIndex = 8;
            //
            // btnClearFilter
            //
            btnClearFilter.Location = new System.Drawing.Point(144, 122);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new System.Drawing.Size(120, 30);
            btnClearFilter.TabIndex = 5;
            btnClearFilter.Text = "Filtreyi Kaldır";
            btnClearFilter.UseVisualStyleBackColor = true;
            btnClearFilter.Click += BtnClearFilter_Click;
            //
            // lblFilterInfo
            //
            lblFilterInfo.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblFilterInfo.Location = new System.Drawing.Point(12, 156);
            lblFilterInfo.Name = "lblFilterInfo";
            lblFilterInfo.Size = new System.Drawing.Size(252, 46);
            lblFilterInfo.TabIndex = 6;
            lblFilterInfo.Text = "Filtre yok: tüm kayıtlar gösteriliyor.";
            //
            // statusStrip
            //
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsslState, tsslCount, tsslFocus });
            statusStrip.Location = new System.Drawing.Point(0, 628);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(1000, 22);
            statusStrip.TabIndex = 10;
            //
            // tsslState
            //
            tsslState.Name = "tsslState";
            tsslState.Size = new System.Drawing.Size(110, 17);
            tsslState.Text = "Durum: Kayıt kapalı";
            //
            // tsslCount
            //
            tsslCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            tsslCount.Name = "tsslCount";
            tsslCount.Size = new System.Drawing.Size(80, 17);
            tsslCount.Text = "Kayıt sayısı: 0";
            //
            // tsslFocus
            //
            tsslFocus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            tsslFocus.Name = "tsslFocus";
            tsslFocus.Size = new System.Drawing.Size(120, 17);
            tsslFocus.Spring = true;
            tsslFocus.Text = "Pencere aktif";
            tsslFocus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // MainForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            ClientSize = new System.Drawing.Size(1000, 650);
            Controls.Add(grpFilter);
            Controls.Add(grpStats);
            Controls.Add(lstLog);
            Controls.Add(captureBox);
            Controls.Add(chkKeyPress);
            Controls.Add(chkKeyUp);
            Controls.Add(chkKeyDown);
            Controls.Add(lblEventTypes);
            Controls.Add(btnSave);
            Controls.Add(btnClear);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(statusStrip);
            Controls.Add(pnlHeader);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            KeyPreview = true;
            MinimumSize = new System.Drawing.Size(900, 560);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Key Event Logger";
            Activated += MainForm_Activated;
            Deactivate += MainForm_Deactivate;
            FormClosing += MainForm_FormClosing;
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            KeyPress += MainForm_KeyPress;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpStats.ResumeLayout(false);
            grpStats.PerformLayout();
            grpFilter.ResumeLayout(false);
            grpFilter.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblRecordingIndicator;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblEventTypes;
        private System.Windows.Forms.CheckBox chkKeyDown;
        private System.Windows.Forms.CheckBox chkKeyUp;
        private System.Windows.Forms.CheckBox chkKeyPress;
        private KeyEventLogger.Controls.KeyCaptureBox captureBox;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.GroupBox grpStats;
        private System.Windows.Forms.Label lblTotalEvents;
        private System.Windows.Forms.Label lblKeyPressCount;
        private System.Windows.Forms.Label lblUniqueKeys;
        private System.Windows.Forms.Label lblTopKeysTitle;
        private System.Windows.Forms.ListBox lstTopKeys;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Label lblFilterInfo;
        private System.Windows.Forms.Label lblEventType;
        private System.Windows.Forms.ComboBox cmbEventType;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslState;
        private System.Windows.Forms.ToolStripStatusLabel tsslCount;
        private System.Windows.Forms.ToolStripStatusLabel tsslFocus;
    }
}
