namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// Element enabling by neccessary and their parameter edditing
    /// Objects linking to appropriate element
    /// </summary>
    /// <typeparam name="T"></typeparam>    
    public interface IElementController<T>
    {
        /// <summary>
        /// Switching on/off an element
        /// </summary>
        /// <param name="flag"></param>
        void Switch(bool? flag = null);
        /// <summary>
        /// Element text switching
        /// </summary>
        /// <param name="flag"></param>
        void Retext(bool? flag = null);
    }
}
