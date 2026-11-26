using System.Collections.Generic;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Kayıtları kalıcı bir hedefe yazan sınıfların sözleşmesi (arayüz).
    /// Form bu arayüze bağımlıdır; farklı bir biçim (ör. CSV) eklemek için
    /// yeni bir sınıf yazmak yeterlidir, form kodu değişmez.
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>Kaydedici dosya iletişim kutusunda kullanılacak filtre metni.</summary>
        string FileFilter { get; }

        /// <summary>Kayıtları verilen dosyaya yazar. Hata durumunda LogFileException fırlatır.</summary>
        void Write(string path, IReadOnlyCollection<KeyEventRecord> records);
    }
}
