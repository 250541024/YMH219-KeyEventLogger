using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Dosyaya kayıt yazan sınıfların ortak soyut temel sınıfı (Template Method deseni).
    /// Dosyayı açma, kapatma ve dosya hatalarını LogFileException'a çevirme işi burada
    /// bir kez yazılır; alt sınıflar yalnızca dosyanın içeriğini (WriteContent) belirler.
    /// Yalnızca kullanıcının seçtiği yerel dosyaya yazılır; ağ üzerinden gönderim yoktur.
    /// </summary>
    public abstract class FileLogWriterBase : ILogWriter
    {
        public abstract string FileFilter { get; }

        /// <summary>Bu yazıcının ürettiği dosya uzantısı (ör. ".txt").</summary>
        public abstract string Extension { get; }

        public void Write(string path, IReadOnlyCollection<KeyEventRecord> records)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Dosya yolu boş olamaz.", nameof(path));
            if (records == null)
                throw new ArgumentNullException(nameof(records));

            try
            {
                // UTF-8 + BOM: Türkçe karakterler Not Defteri'nde ve Excel'de doğru görünür.
                // using bloğu: hata olsa bile dosya kapatılır (kaynak yönetimi).
                using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
                    WriteContent(writer, records);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new LogFileException("Bu konuma yazma izniniz yok.", path, ex);
            }
            catch (SecurityException ex)
            {
                throw new LogFileException("Güvenlik ayarları dosyaya yazmayı engelledi.", path, ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new LogFileException("Seçilen klasör bulunamadı.", path, ex);
            }
            catch (PathTooLongException ex)
            {
                throw new LogFileException("Dosya yolu çok uzun.", path, ex);
            }
            catch (IOException ex)
            {
                throw new LogFileException("Dosya yazılamadı (dosya başka bir programda açık olabilir).", path, ex);
            }
        }

        /// <summary>Dosyanın içeriğini yazar. Alt sınıflar biçimi kendisi belirler.</summary>
        protected abstract void WriteContent(StreamWriter writer, IReadOnlyCollection<KeyEventRecord> records);
    }
}
