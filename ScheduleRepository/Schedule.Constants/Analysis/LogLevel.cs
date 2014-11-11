namespace Schedule.Constants.Analysis
{
    public class LogLevel
    {
        public int Level { get; set; }
        public string Description { get; set; }
        
        public static LogLevel ErrorsOnly = new LogLevel { Level = 1, Description = "Только ошибки" };
        public static LogLevel ErrorsAndWarnings = new LogLevel { Level = 2, Description = "Ошибки и предупреждения" };
        public static LogLevel Normal = new LogLevel { Level = 3, Description = "Нормальный" };
        public static LogLevel Max = new LogLevel { Level = 4, Description = "Максимальный" }; 
    }
}
