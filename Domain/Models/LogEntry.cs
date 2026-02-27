using diPasswords.Domain.Enums;

namespace diPasswords.Domain.Models
{
    public class LogEntry
    {
        public LogLevel level { get; set; }
        public string message { get; set; }

        public LogEntry(LogLevel level, string message)
        {
            this.level = level;
            this.message = message;
        }
    }
}
