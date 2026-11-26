using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Kayıtları UTF-8 kodlamalı düz metin (.txt) dosyasına StreamWriter ile yazar.
    /// Yalnızca kullanıcının seçtiği yerel dosyaya yazar; ağ üzerinden gönderim yoktur.
    /// </summary>
    public class TextFileLogWriter : ILogWriter
    {
        public string FileFilter => "Metin dosyası (*.txt)|*.txt";

        public void Write(string path, IReadOnlyCollection<KeyEventRecord> records)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Dosya yolu boş olamaz.", nameof(path));
            if (records == null)
                throw new ArgumentNullException(nameof(records));

            try
            {
                // using bloğu: hata olsa bile dosya kapatılır (kaynak yönetimi)
                using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine("# Key Event Logger - Klavye Olay Kayıtları");
                    writer.WriteLine("# Oluşturulma : " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                    writer.WriteLine("# Kayıt sayısı: " + records.Count);
                    writer.WriteLine("# Not: Kayıtlar yalnızca uygulama penceresi odaktayken alınmıştır.");
                    writer.WriteLine(new string('-', 90));

                    foreach (KeyEventRecord record in records)
                        writer.WriteLine(record.ToLogLine());
                }
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
    }
}
