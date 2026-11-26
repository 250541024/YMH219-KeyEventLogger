namespace KeyEventLogger.Models
{
    /// <summary>
    /// Yakalanan klavye olayının türü.
    /// Windows Forms'taki üç klavye olayına karşılık gelir.
    /// </summary>
    public enum KeyEventType
    {
        /// <summary>Tuşa basıldığı an (her tuş için oluşur).</summary>
        KeyDown,

        /// <summary>Tuş bırakıldığı an.</summary>
        KeyUp,

        /// <summary>Basılan tuş bir karakter ürettiğinde (ör. 'a', 'ş', '?').</summary>
        KeyPress
    }
}
