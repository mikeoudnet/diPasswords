using diPasswords.Application.Interfaces;
using diPasswords.Domain.Enums;

namespace diPasswords.Presentation.Managers
{
    // Класс, содержащий наборы данных, одновременно необходимых к изменению
    public class ElementView : IElementView
    {
        private Dictionary<ElementMode, IElementController<Control>[]> controllerPools = new Dictionary<ElementMode, IElementController<Control>[]>(); // Все наборы элементов
        private ElementMode _mode; // Текущее название набора элементов

        // Переключить свойство Enabled у группы элементов
        public void Switch(ElementMode mode, bool status)
        {
            if (controllerPools.ContainsKey(mode))
            {
                IElementController<Control>[] controllers = controllerPools[mode];
                foreach (var controller in controllers)
                {
                    controller.Switch(status);
                    controller.Retext(status);
                }
            }
        }
        // Переключить свойство Enabled у текущего набора элементов
        public void Switch(bool status)
        {
            if (_mode != ElementMode.None) Switch(_mode, status);
        }
        // Добавить набор элементов
        public void AddPool(ElementMode mode, params IElementController<Control>[] controllers) => controllerPools.Add(mode, controllers);
        // Изменить текущий набор данных
        public void CurrentPool(ElementMode mode = ElementMode.None) => _mode = mode;
    }
}
