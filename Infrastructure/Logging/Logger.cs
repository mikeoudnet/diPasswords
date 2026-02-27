using diPasswords.Application.Interfaces;
using diPasswords.Domain.Models;
using diPasswords.Domain.Enums;

namespace diPasswords.Infrastructure.Logging
{
    // Логгирование работы программы в отдельный элемент
    // (в данном случае ListView)
    public class Logger : ILogger
    {
        public event Action<LogEntry> OnLog; // Событие, связывающее логи и логгер

        // Информирующее сообщение
        public void Info(string message) => OnLog?.Invoke(new LogEntry(LogLevel.Info, message));
        // Предупреждающее сообщение
        public void Warn(string message) => OnLog?.Invoke(new LogEntry(LogLevel.Warn, message));
        // Сообщение об ошибке
        public void Error(string message) => OnLog?.Invoke(new LogEntry(LogLevel.Error, message));
        // Сообщение об ошибке с выводом окна с ошибкой
        public void CurrentError(string message) => OnLog?.Invoke(new LogEntry(LogLevel.CurrentError, message));
        // Фатальная ошибка, обрывающая работы программы
        public void Fatal(string message) => OnLog?.Invoke(new LogEntry(LogLevel.FatalError, message));
    }
}
