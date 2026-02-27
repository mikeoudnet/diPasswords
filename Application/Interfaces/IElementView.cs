using diPasswords.Domain.Enums;

namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// Class containging elements sets, neccessary to editting at the same time
    /// </summary>
    public interface IElementView
    {
        /// <summary>
        /// Switching .Enable-property of elements set to choosed status value
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="status"></param>
        void Switch(ElementMode mode, bool status);
        /// <summary>
        /// Switching .Enable-property of current elements set
        /// </summary>
        /// <param name="status"></param>
        void Switch(bool status);
        /// <summary>
        /// Elements set adding
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="controllers"></param>
        void AddPool(ElementMode mode, params IElementController<Control>[] controllers);
        /// <summary>
        /// Current elements set editting
        /// </summary>
        /// <param name="mode"></param>
        void CurrentPool(ElementMode mode = ElementMode.None);
    }
}
