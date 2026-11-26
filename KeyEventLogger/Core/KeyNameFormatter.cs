using System.Collections.Generic;
using System.Windows.Forms;

namespace KeyEventLogger.Core
{
    /// <summary>
    /// Keys değerlerini ve karakterleri kullanıcının anlayacağı adlara çevirir.
    /// Durum tutmadığı için statik bir yardımcı sınıftır.
    /// </summary>
    public static class KeyNameFormatter
    {
        // Keys enum'undaki adı anlaşılır olmayan tuşlar için Türkçe/kısa adlar
        private static readonly Dictionary<Keys, string> FriendlyNames = new Dictionary<Keys, string>
        {
            { Keys.Return, "Enter" },
            { Keys.Back, "Backspace" },
            { Keys.Space, "Boşluk" },
            { Keys.Escape, "Esc" },
            { Keys.ControlKey, "Ctrl" },
            { Keys.LControlKey, "Sol Ctrl" },
            { Keys.RControlKey, "Sağ Ctrl" },
            { Keys.ShiftKey, "Shift" },
            { Keys.LShiftKey, "Sol Shift" },
            { Keys.RShiftKey, "Sağ Shift" },
            { Keys.Menu, "Alt" },
            { Keys.LMenu, "Sol Alt" },
            { Keys.RMenu, "Sağ Alt" },
            { Keys.LWin, "Sol Windows" },
            { Keys.RWin, "Sağ Windows" },
            { Keys.Capital, "Caps Lock" },
            { Keys.NumLock, "Num Lock" },
            { Keys.Scroll, "Scroll Lock" },
            { Keys.Prior, "Page Up" },
            { Keys.Next, "Page Down" },
            { Keys.Up, "Yukarı Ok" },
            { Keys.Down, "Aşağı Ok" },
            { Keys.Left, "Sol Ok" },
            { Keys.Right, "Sağ Ok" },
            { Keys.Delete, "Delete" },
            { Keys.Insert, "Insert" },
            { Keys.Snapshot, "Print Screen" },
            { Keys.Apps, "Menü" },
            { Keys.Decimal, "Num ." },
            { Keys.Add, "Num +" },
            { Keys.Subtract, "Num -" },
            { Keys.Multiply, "Num *" },
            { Keys.Divide, "Num /" },
        };

        /// <summary>KeyDown/KeyUp olayındaki tuş kodunun adını döndürür.</summary>
        public static string GetKeyName(Keys keyCode)
        {
            if (FriendlyNames.TryGetValue(keyCode, out string name))
                return name;

            // Üst sıradaki rakam tuşları: D0..D9 -> "0".."9"
            if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
                return ((char)('0' + (keyCode - Keys.D0))).ToString();

            // Sayısal tuş takımı: NumPad0..NumPad9 -> "Num 0".."Num 9"
            if (keyCode >= Keys.NumPad0 && keyCode <= Keys.NumPad9)
                return "Num " + (keyCode - Keys.NumPad0);

            return keyCode.ToString();
        }

        /// <summary>KeyPress olayındaki karakterin gösterilecek adını döndürür.</summary>
        public static string GetCharName(char c)
        {
            switch (c)
            {
                case '\r': return "'\\r' (Enter)";
                case '\b': return "'\\b' (Backspace)";
                case '\t': return "'\\t' (Tab)";
                case (char)27: return "(Esc)";
                case ' ': return "' ' (Boşluk)";
            }

            // Ctrl+harf gibi kombinasyonlar görünmeyen kontrol karakterleri üretir
            if (char.IsControl(c))
                return "(Kontrol " + (int)c + ")";

            return "'" + c + "'";
        }

        /// <summary>Değiştirici tuşları "Ctrl+Shift+Alt" biçiminde metne çevirir.</summary>
        public static string GetModifiersText(Keys modifiers)
        {
            var parts = new List<string>();
            if ((modifiers & Keys.Control) == Keys.Control) parts.Add("Ctrl");
            if ((modifiers & Keys.Shift) == Keys.Shift) parts.Add("Shift");
            if ((modifiers & Keys.Alt) == Keys.Alt) parts.Add("Alt");
            return string.Join("+", parts);
        }
    }
}
