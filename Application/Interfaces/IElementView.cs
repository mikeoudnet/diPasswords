using diPasswords.Domain.Enums;

namespace diPasswords.Application.Interfaces
{
    // Класс, содержащий наборы данных, одновременно необходимых к изменению
    public interface IElementView
    {
        void Switch(ElementMode mode, bool status); // Переключить свойство Enabled у группы элементов
        void Switch(bool status); // Переключить свойство Enabled у текущего набора элементов
        void AddPool(ElementMode mode, params IElementController<Control>[] controllers); // Добавить набор элементов
        void CurrentPool(ElementMode mode = ElementMode.None); // Изменить текущий набор данных
    }
}
