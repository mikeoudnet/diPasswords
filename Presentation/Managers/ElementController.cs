using diPasswords.Application.Interfaces;

namespace diPasswords.Presentation.Managers
{
    /// <inheritdoc cref="IElementController{T}"/>
    // Element enabling by neccessary and their parameters editting
    // Objects linking to appropriate element
    public class ElementController<T> : IElementController<T> where T : Control
    {
        private T _element; // Object linking to element
        private bool? _startStatus; // Primary element status (null - set current element status)
        private string _primaryText; // Primary text for a button
        private string _secondaryText; // Extra text for a button

        public ElementController(T element, bool? startStatus, string? secondaryText = null)
        {
            _element = element;
            _primaryText = _element.Text;
            _startStatus = startStatus;

            if (secondaryText != null) _secondaryText = secondaryText;
            else _secondaryText = _primaryText;
        }
        
        /// <inheritdoc cref="IElementController{T}.Switch(bool?)"/>
        // Switching on/off an element
        public void Switch(bool? flag = null)
        {
            if (flag == null) _element.Enabled = !_element.Enabled;
            else if (_startStatus != null) _element.Enabled = (bool)_startStatus ? !(bool)flag : (bool)flag;
        }
        /// <inheritdoc cref="IElementController{T}.Retext(bool?)"/>
        // Element text switching
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
