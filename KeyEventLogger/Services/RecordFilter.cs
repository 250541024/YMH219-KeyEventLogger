using System;
using System.Collections.Generic;
using System.Linq;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Kayıtları başlangıç-bitiş zamanına ve isteğe bağlı olarak olay türüne
    /// (KeyDown / KeyUp / KeyPress) göre süzer.
    /// Geçersiz aralık (başlangıç &gt; bitiş) yapıcı metotta reddedilir.
    /// </summary>
    public class RecordFilter
    {
        public DateTime From { get; }
        public DateTime To { get; }

        /// <summary>Gösterilecek olay türü; null ise tüm türler.</summary>
        public KeyEventType? EventType { get; }

        public RecordFilter(DateTime from, DateTime to, KeyEventType? eventType = null)
        {
            if (from > to)
                throw new ArgumentException("Başlangıç zamanı bitiş zamanından sonra olamaz.");

            From = from;
            To = to;
            EventType = eventType;
        }

        public bool IsMatch(KeyEventRecord record)
        {
            return record != null
                && record.Timestamp >= From && record.Timestamp <= To
                && (EventType == null || record.EventType == EventType.Value);
        }

        public List<KeyEventRecord> Apply(IEnumerable<KeyEventRecord> records)
        {
            return records.Where(IsMatch).ToList();
        }

        public override string ToString()
        {
            return From.ToString("dd.MM.yyyy HH:mm:ss") + " - " + To.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
