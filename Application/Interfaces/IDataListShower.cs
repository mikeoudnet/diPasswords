using diPasswords.Domain.Models;

namespace diPasswords.Application.Interfaces
{
    /// <summary>
    /// User data visualization
    /// </summary>
    public interface IDataListShower
    {
        /// <summary>
        /// Object linking to the ListBox
        /// </summary>
        public event Action<List<Data>> OnList;
        /// <summary>
        /// Cursor setting to choosed data
        /// </summary>
        public event Action<string> OnListCursor;
        /// <summary>
        /// Data outputting updating
        /// </summary>
        /// <param name="dataList"></param>
        void UpdateList(List<Data> dataList);
        /// <summary>
        /// Cursor setting to new data
        /// </summary>
        /// <param name="name"></param>
        public void SetDataCursor(string name);
    }
}
