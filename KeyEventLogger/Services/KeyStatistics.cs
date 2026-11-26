using System;
using System.Collections.Generic;
using System.Linq;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services
{
    /// <summary>
    /// Bir kayıt listesi üzerinden istatistik hesaplar:
    /// toplam olay sayısı, toplam tuş basımı ve en sık kullanılan tuşlar.
    /// Tuş basımı olarak yalnızca KeyDown olayları sayılır; böylece bir tuş için
    /// oluşan KeyUp ve KeyPress olayları sayıyı şişirmez.
    /// </summary>
    public class KeyStatistics
    {
        private readonly Dictionary<string, int> keyCounts = new Dictionary<string, int>();

        public int TotalEvents { get; }
        public int KeyPressCount { get; }
        public int UniqueKeyCount => keyCounts.Count;

        public KeyStatistics(IEnumerable<KeyEventRecord> records)
        {
            if (records == null) throw new ArgumentNullException(nameof(records));

            foreach (KeyEventRecord record in records)
            {
                TotalEvents++;
                if (record.EventType != KeyEventType.KeyDown) continue;

                KeyPressCount++;
                keyCounts.TryGetValue(record.KeyName, out int count);
                keyCounts[record.KeyName] = count + 1;
            }
        }

        /// <summary>En çok basılan ilk <paramref name="count"/> tuşu, çoktan aza sıralı döndürür.</summary>
        public IList<KeyValuePair<string, int>> GetTopKeys(int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

            return keyCounts
                .OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key)
                .Take(count)
                .ToList();
        }
    }
}
