using System.Windows;
using ReadingRoomApp.ViewModels;

namespace ReadingRoomApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Получаем сервисы из глобального класса App
            _viewModel = new MainWindowViewModel(
                App.UserService,
                App.BookService,
                App.ReaderService);

            DataContext = _viewModel;
        }
    }
}