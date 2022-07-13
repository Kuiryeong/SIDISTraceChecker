using System;

namespace SIDISTraceChecker.Models
{
    internal class TraceDetailModel
    {
        /// <summary>
        /// The full file path of original trace file.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// The line no from the original trace file.
        /// </summary>
        public int LineNo { get; set; }
        /// <summary>
        /// The timestamp generated this trace set.
        /// </summary>
        public string IniTime { get; set; }
        /// <summary>
        /// The timestamp of last message from this trace set.
        /// </summary>
        public string FullMessages { get; set; }
        /// <summary>
        /// If it is true, this trace set include "ERROR" message.
        /// </summary>
    }
}
