using System;
using System.IO;

namespace SIDISTraceChecker.Models
{
    internal class TraceFileModel
    {
        /// <summary>
        /// The folder name.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// The last modifided timestamp of files in this folder.
        /// </summary>
        public DateTime LastModifiedTime
        {
            get => Directory.GetLastWriteTime(FileName);
        }
        /// <summary>
        /// The earliest timestamp of files in this folder.
        /// </summary>
        public string StartTime
        {
            get
            {
                try
                {
                    using (StreamReader sr = new StreamReader(FileName))
                    {
                        DateTime dateTime = DateTime.MinValue;
                        string res = string.Empty;

                        //"22-05-27 14:10:02,642"
                        res = sr.ReadLine().Substring(0, 21);
                        dateTime = DateTime.ParseExact(res, @"yy-MM-dd HH:mm:ss,fff", null);

                        return dateTime == DateTime.MinValue ? "NotFomattedFile" : res;
                    }
                }
                catch (Exception ex) { return "NotFomattedFile"; }
            }
        }
    }
}
