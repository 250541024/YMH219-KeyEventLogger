using System;
using System.Collections.Generic;
using System.IO;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Kayıtları okunabilir düz metin (.txt) olarak yazar: açıklama başlığı ve her olay için bir satır.
    /// </summary>
    public class TextFileLogWriter : FileLogWriterBase
    {
        public override string FileFilter => "Metin dosyası (*.txt)|*.txt";
        public override string Extension => ".txt";

        protected override void WriteContent(StreamWriter writer, IReadOnlyCollection<KeyEventRecord> records)
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
}
