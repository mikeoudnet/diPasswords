using diPasswords.Application.Interfaces;
using diPasswords.Domain.Enums;

namespace diPasswords.Presentation.Managers
{
    /// <inheritdoc cref="IElementView"/>
    // Class containing elements sets, neccessary to editting at the same time
    public class ElementView : IElementView
    {
        private Dictionary<ElementMode, IElementController<Control>[]> controllerPools = new Dictionary<ElementMode, IElementController<Control>[]>(); // All elements sets
        private ElementMode _mode; // Current element mode

        /// <inheritdoc cref="IElementView.Switch(ElementMode, bool)"/>
        // Switching .Enable-propery of elements set to choosed status value
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
        /// <inheritdoc cref="IElementView.Switch(bool))"/>
        // Switching .Enable-property of current elements set
        public void Switch(bool status)
        {
            if (_mode != ElementMode.None) Switch(_mode, status);
        }
        /// <inheritdoc cref="IElementView.AddPool(ElementMode, IElementController{Control}[])"/>
        // Elements set adding
        public void AddPool(ElementMode mode, params IElementController<Control>[] controllers) => controllerPools.Add(mode, controllers);
        /// <inheritdoc cref="IElementView.CurrentPool(ElementMode)"/>
        // Current elements set editting
        public void CurrentPool(ElementMode mode = ElementMode.None) => _mode = mode;
    }
}
