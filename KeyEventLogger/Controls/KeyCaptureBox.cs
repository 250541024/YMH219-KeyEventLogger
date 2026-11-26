using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KeyEventLogger.Controls
{
    /// <summary>
    /// Tuş girişlerinin yapılacağı alan. Control sınıfından türetilmiştir (kalıtım).
    /// Normalde Tab, ok tuşları ve Enter gibi tuşlar form tarafından "gezinme" için
    /// kullanılır ve KeyDown olayı oluşmaz. IsInputKey metodu ezilerek (override)
    /// bu alan odaktayken tüm tuşların olay üretmesi sağlanır.
    /// </summary>
    public class KeyCaptureBox : Control
    {
        private bool recording;
        private bool windowActive = true;
        private string lastKeyText = string.Empty;

        public KeyCaptureBox()
        {
            SetStyle(ControlStyles.Selectable | ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            TabStop = true;
            Cursor = Cursors.Hand;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 11F);
        }

        /// <summary>Kayıt açık mı? Görünümü değiştirir.</summary>
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Recording
        {
            get => recording;
            set { recording = value; Invalidate(); }
        }

        /// <summary>Uygulama penceresi aktif mi? Değilse uyarı gösterilir.</summary>
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool WindowActive
        {
            get => windowActive;
            set { windowActive = value; Invalidate(); }
        }

        /// <summary>En son basılan tuşun metni (büyük puntoyla gösterilir).</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LastKeyText
        {
            get => lastKeyText;
            set { lastKeyText = value ?? string.Empty; Invalidate(); }
        }

        protected override bool IsInputKey(Keys keyData) => true;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true; // Karakter başka bir yere işlenmesin
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            base.OnMouseDown(e);
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color borderColor;
            string message;
            if (!recording)
            {
                borderColor = Color.FromArgb(148, 163, 184);
                message = "Kayıt kapalı. Kaydı açmak için \"Başlat\" düğmesine basın.";
            }
            else if (!windowActive)
            {
                borderColor = Color.FromArgb(234, 88, 12);
                message = "Pencere arka planda: olay yakalanmıyor.";
            }
            else if (!Focused)
            {
                borderColor = Color.FromArgb(234, 179, 8);
                message = "Kayıt açık. Bu alana tıklayıp tuşlara basın.";
            }
            else
            {
                borderColor = Color.FromArgb(22, 163, 74);
                message = "Kayıt açık: tuşlara basın, olaylar aşağıda listelenir.";
            }

            Rectangle r = ClientRectangle;
            r.Width -= 1;
            r.Height -= 1;
            using (var pen = new Pen(borderColor, Focused ? 3F : 2F))
            using (GraphicsPath path = RoundedRect(Rectangle.Inflate(r, -1, -1), 8))
            {
                g.DrawPath(pen, path);
            }

            var msgRect = new Rectangle(16, 0, Width - 220, Height);
            TextRenderer.DrawText(g, message, Font, msgRect, Color.FromArgb(51, 65, 85),
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);

            if (recording && lastKeyText.Length > 0)
            {
                using (var big = new Font("Segoe UI", 18F, FontStyle.Bold))
                {
                    var keyRect = new Rectangle(Width - 210, 0, 194, Height);
                    TextRenderer.DrawText(g, lastKeyText, big, keyRect, borderColor,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Right | TextFormatFlags.EndEllipsis);
                }
            }
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
