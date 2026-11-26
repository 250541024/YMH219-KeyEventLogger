using System;
using System.Windows.Forms;
using KeyEventLogger.Core;

namespace KeyEventLogger.Models
{
    /// <summary>
    /// Tek bir klavye olayının kaydı. Oluşturulduktan sonra değiştirilemez (immutable):
    /// tüm özellikler yalnızca okunabilir, değerler yapıcı metotta atanır.
    /// </summary>
    public sealed class KeyEventRecord
    {
        public DateTime Timestamp { get; }
        public KeyEventType EventType { get; }

        /// <summary>Kullanıcıya gösterilen tuş adı (ör. "A", "Enter", "Ctrl", "'ş'").</summary>
        public string KeyName { get; }

        /// <summary>KeyDown/KeyUp için sanal tuş kodu; KeyPress için karakterin Unicode değeri.</summary>
        public int Code { get; }

        /// <summary>Olay anında basılı olan değiştirici tuşlar (Ctrl, Shift, Alt).</summary>
        public Keys Modifiers { get; }

        private KeyEventRecord(DateTime timestamp, KeyEventType eventType, string keyName, int code, Keys modifiers)
        {
            if (string.IsNullOrWhiteSpace(keyName))
                throw new ArgumentException("Tuş adı boş olamaz.", nameof(keyName));

            Timestamp = timestamp;
            EventType = eventType;
            KeyName = keyName;
            Code = code;
            Modifiers = modifiers;
        }

        /// <summary>KeyDown veya KeyUp olayından kayıt oluşturur (Factory Method).</summary>
        public static KeyEventRecord FromKeyEvent(KeyEventType eventType, KeyEventArgs e, DateTime timestamp)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            if (eventType == KeyEventType.KeyPress)
                throw new ArgumentException("KeyPress olayı için FromKeyPress metodunu kullanın.", nameof(eventType));

            return new KeyEventRecord(timestamp, eventType,
                KeyNameFormatter.GetKeyName(e.KeyCode), (int)e.KeyCode, e.Modifiers);
        }

        /// <summary>KeyPress olayından kayıt oluşturur (Factory Method).</summary>
        public static KeyEventRecord FromKeyPress(KeyPressEventArgs e, DateTime timestamp)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            return new KeyEventRecord(timestamp, KeyEventType.KeyPress,
                KeyNameFormatter.GetCharName(e.KeyChar), e.KeyChar, Control.ModifierKeys);
        }

        /// <summary>Değiştirici tuşları okunabilir metne çevirir, ör. "Ctrl+Shift".</summary>
        public string ModifiersText => KeyNameFormatter.GetModifiersText(Modifiers);

        /// <summary>Ekranda (ListBox) gösterilecek kısa biçim.</summary>
        public override string ToString()
        {
            string mods = ModifiersText.Length > 0 ? "[" + ModifiersText + "]" : "";
            return string.Format("{0:HH:mm:ss.fff}  {1,-8}  {2,-18} {3,-6} {4}",
                Timestamp, EventType, KeyName, CodeText, mods).TrimEnd();
        }

        /// <summary>Dosyaya yazılacak ayrıntılı biçim (tarih dahil).</summary>
        public string ToLogLine()
        {
            string mods = ModifiersText.Length > 0 ? ModifiersText : "-";
            return string.Format("[{0:dd.MM.yyyy HH:mm:ss.fff}] {1,-8} | Tuş: {2,-16} | Kod: {3,-6} | Değiştirici: {4}",
                Timestamp, EventType, KeyName, CodeText, mods);
        }

        private string CodeText => EventType == KeyEventType.KeyPress ? "U+" + Code.ToString("X4") : Code.ToString();
    }
}
