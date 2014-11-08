using System;
using UchOtd.Schedule.Analysis;

namespace UchOtd.Analysis
{
    public class LogMessage
    {
        public DateTime Time { get; set; }
        public LogLevel Level { get; set; }
        public string Text { get; set; }
    }
}
