using System;
using KeyEventLogger.Models;

namespace KeyEventLogger.Core
{
    /// <summary>
    /// KeyEventRecorder.RecordAdded olayı ile birlikte gönderilen bilgi.
    /// </summary>
    public class KeyEventRecordedEventArgs : EventArgs
    {
        public KeyEventRecord Record { get; }

        public KeyEventRecordedEventArgs(KeyEventRecord record)
        {
            Record = record ?? throw new ArgumentNullException(nameof(record));
        }
    }
}
