using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    // Логгирование работы программы в отдельный элемент
    // (в данном случае ListView)
    public interface ILogger
    {
        public event Action<LogEntry> OnLog; // Событие, связывающее логи и логгер

        void Info(string message); // Информирующее сообщение
        void Warn(string message); // Предупреждающее сообщение
        void Error(string message); // Сообщение об ошибке
        void CurrentError(string message); // Сообщение об ошибке с выводом окна с ошибкой
        void Fatal(string message); // Фатальная ошибка, обрывающая работы программы
    }
}
