// BookListView.xaml.cs (обновленный)
using System.Windows.Controls;
using System.Windows.Input;
using ReadingRoomApp.ViewModels;

namespace ReadingRoomApp.Views
{
    public partial class BookListView : UserControl
    {
        public BookListView()
        {
            InitializeComponent();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is BookListViewModel viewModel && viewModel.SelectedBook != null)
            {
                viewModel.ViewBookDetailsCommand.Execute(null);
            }
        }
    }
}