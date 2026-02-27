using diPasswords.Application.Interfaces;

namespace diPasswords.Infrastructure
{
    /// <inheritdoc cref="IDataBaseManager"/>
    // Collapsed databases requests
    // (no neccessary to write same code)
    public class DataListShower : IDataListShower
    {
        /// <inheritdoc cref="IDataListShower.OnList"/>
        // Object linking to the ListBox
        public event Action<List<Domain.Models.Data>> OnList;
        /// <inheritdoc cref="IDataListShower.OnListCursor"/>
        // Cursor setting to choosed data
        public event Action<string> OnListCursor;

        /// <inheritdoc cref="IDataListShower.UpdateList(List{Domain.Models.Data})"/>
        // Data outputting updating
        public void UpdateList(List<Domain.Models.Data> dataList) => OnList?.Invoke(dataList);
        /// <inheritdoc cref="IDataListShower.SetDataCursor(string)"/>
        // Cursor setting to new data
        public void SetDataCursor(string name) => OnListCursor?.Invoke(name);
    }
}
