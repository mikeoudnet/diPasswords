using diPasswords.Application.Interfaces;
using diPasswords.Domain.Models;
using diPasswords.Domain.Enums;

namespace diPasswords.Infrastructure.Logging
{
    /// <inheritdoc cref="ILogger"/>
    // Logging to separate element
    // (for this time it is ListView)
    public class Logger : ILogger
    {
        /// <inheritdoc cref="ILogger.OnLog"/>
        // An event linking logs and logger
        public event Action<LogEntry> OnLog;

        /// <inheritdoc cref="ILogger.Info(string)"/>
        // Informational message
        public void Info(string message) => OnLog?.Invoke(new LogEntry(LogLevel.Info, message));
        /// <inheritdoc cref="ILogger.Warn(string)"/>
        // Warning message
        public void Warn(string message) => OnLog?.Invoke(new LogEntry(LogLevel.Warn, message));
        /// <inheritdoc cref="ILogger.Error(string)"/>
        // Error message
        public void Error(string message) => OnLog?.Invoke(new LogEntry(LogLevel.Error, message));
        /// <inheritdoc cref="ILogger.CurrentError(string)"/>
        // Error message with MessageBox containing error information
        public void CurrentError(string message) => OnLog?.Invoke(new LogEntry(LogLevel.CurrentError, message));
        /// <inheritdoc cref="ILogger.Fatal(string)"/>
        // Fatal error message to finish whole program
        public void Fatal(string message) => OnLog?.Invoke(new LogEntry(LogLevel.FatalError, message));
    }
}
