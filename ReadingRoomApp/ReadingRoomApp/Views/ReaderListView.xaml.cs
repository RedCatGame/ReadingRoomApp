using System.Windows.Controls;
using System.Windows.Input;
using ReadingRoomApp.ViewModels;

namespace ReadingRoomApp.Views
{
    public partial class ReaderListView : UserControl
    {
        public ReaderListView()
        {
            InitializeComponent();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ReaderListViewModel viewModel && viewModel.SelectedReader != null)
            {
                viewModel.ViewReaderDetailsCommand.Execute(null);
            }
        }
    }
}