using diPasswords.Application.Interfaces;

namespace diPasswords.Infrastructure
{
    // Отображение данных для данного пользователя
    public class DataListShower : IDataListShower
    {
        public event Action<List<Domain.Models.Data>> OnList; // Связывание объекта с ListBox
        public event Action<string> OnListCursor; // Установка курсора на выбранные данные

        // Обновить вывод данных
        public void UpdateList(List<Domain.Models.Data> dataList) => OnList?.Invoke(dataList);
        // Курсор на новые данные
        public void SetDataCursor(string name) => OnListCursor?.Invoke(name);
    }
}
