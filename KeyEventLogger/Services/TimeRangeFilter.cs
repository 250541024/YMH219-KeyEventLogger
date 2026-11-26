using System;
using System.Collections.Generic;
using System.Linq;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Kayıtları başlangıç ve bitiş zamanına göre süzer.
    /// Geçersiz aralık (başlangıç &gt; bitiş) yapıcı metotta reddedilir.
    /// </summary>
    public class TimeRangeFilter
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public TimeRangeFilter(DateTime from, DateTime to)
        {
            if (from > to)
                throw new ArgumentException("Başlangıç zamanı bitiş zamanından sonra olamaz.");

            From = from;
            To = to;
        }

        public bool IsMatch(KeyEventRecord record)
        {
            return record != null && record.Timestamp >= From && record.Timestamp <= To;
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
