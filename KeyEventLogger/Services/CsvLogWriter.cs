using System.Collections.Generic;
using System.IO;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Kayıtları CSV (virgülle/noktalı virgülle ayrılmış değerler) olarak yazar; dosya Excel'de
    /// tablo olarak açılır. Türkçe Windows'ta Excel liste ayırıcısı ";" olduğu için ";" kullanılır.
    /// </summary>
    public class CsvLogWriter : FileLogWriterBase
    {
        private const char Separator = ';';

        public override string FileFilter => "CSV dosyası - Excel (*.csv)|*.csv";
        public override string Extension => ".csv";

        protected override void WriteContent(StreamWriter writer, IReadOnlyCollection<KeyEventRecord> records)
        {
            writer.WriteLine(string.Join(Separator, "Tarih", "Saat", "Olay", "Tuş", "Kod", "Değiştirici"));

            foreach (KeyEventRecord record in records)
            {
                writer.WriteLine(string.Join(Separator,
                    record.Timestamp.ToString("dd.MM.yyyy"),
                    record.Timestamp.ToString("HH:mm:ss.fff"),
                    record.EventType.ToString(),
                    Escape(record.KeyName),
                    record.CodeText,
                    Escape(record.ModifiersText)));
            }
        }

        /// <summary>
        /// Ayırıcı, tırnak veya satır sonu içeren alanlar tırnak içine alınır ve içteki tırnaklar
        /// ikilenir (CSV standardı). Örn. ';' tuşunun adı veya "'\"'" karakteri tabloyu bozmaz.
        /// </summary>
        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.IndexOfAny(new[] { Separator, '"', '\n', '\r' }) < 0) return value;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
