using diPasswords.Application.Interfaces;

namespace diPasswords.Presentation.Managers
{
    // Отключение элементов при необходимости и изменение их параметров
    // в зависимости от текущего состояния других элементов
    public class ElementController<T> : IElementController<T> where T : Control
    {
        private T _element; // Связывание объекта с элементом
        private bool? _startStatus; // Изначальный статус элемента (null - установить текущий статус элемента)
        private string _primaryText; // Исходный текст для кнопки
        private string _secondaryText; // Дополнительный текст для кнопки

        public ElementController(T element, bool? startStatus, string? secondaryText = null)
        {
            _element = element;
            _primaryText = _element.Text;
            _startStatus = startStatus;

            if (secondaryText != null) _secondaryText = secondaryText;
            else _secondaryText = _primaryText;
        }

        // Включить/отключить элемент
        public void Switch(bool? flag = null)
        {
            if (flag == null) _element.Enabled = !_element.Enabled;
            else if (_startStatus != null) _element.Enabled = (bool)_startStatus ? !(bool)flag : (bool)flag;
        }
        // Изменить текст элемента
        public void Retext(bool? flag = null)
        {
            if (_element.GetType() != typeof(TextBox))
            {
                if (flag == null)
                {
                    if (_element.Text == _primaryText) _element.Text = _secondaryText;
                    else if (_element.Text == _secondaryText) _element.Text = _primaryText;
                }
                else if (_startStatus != null)
                {
                    _element.Text = (bool)flag ? (bool)_startStatus ? _primaryText : _secondaryText : (bool)_startStatus ? _secondaryText : _primaryText;
                }
                else
                {
                    _element.Text = (bool)flag ? _secondaryText : _primaryText;
                }
            }
        }
    }
}
