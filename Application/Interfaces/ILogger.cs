using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// Logging to separate element
    /// (for this time it is ListView)
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// An event linking logs and logger
        /// </summary>
        public event Action<LogEntry> OnLog;

        /// <summary>
        /// Informational message
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);
        /// <summary>
        /// Warning message
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
        /// <summary>
        /// Error message
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// Error message with MessageBox containing error information
        /// </summary>
        /// <param name="message"></param>
        void CurrentError(string message);
        /// <summary>
        /// Fatal error message to finish whole program
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);
    }
}
