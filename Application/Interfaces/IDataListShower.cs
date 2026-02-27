using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    // Отображение данных для данного пользователя
    public interface IDataListShower
    {
        public event Action<List<Data>> OnList; // Связывание объекта с ListBox
        public event Action<string> OnListCursor; // Установка курсора на выбранные данные

        void UpdateList(List<Data> dataList); // Обновить вывод данных
        public void SetDataCursor(string name); // Курсор на новые данные
    }
}
