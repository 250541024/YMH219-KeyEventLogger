using System;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Kayıt dosyası yazılamadığında fırlatılan özel istisna.
    /// Asıl hata (IOException, UnauthorizedAccessException vb.) InnerException içinde saklanır.
    /// </summary>
    public class LogFileException : Exception
    {
        public string FilePath { get; }

        public LogFileException(string message, string filePath, Exception innerException)
            : base(message, innerException)
        {
            FilePath = filePath;
        }
    }
}
