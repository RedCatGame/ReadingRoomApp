using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReadingRoomApp.Commands;
using ReadingRoomApp.Models;
using ReadingRoomApp.Services;
using ReadingRoomApp.ViewModels;

namespace ReadingRoomApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        private readonly IReaderService _readerService;

        private User _currentUser;
        private object _currentView;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(IsReader));
                OnPropertyChanged(nameof(IsAuthor));
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoggedIn => CurrentUser != null;
        public bool IsReader => CurrentUser?.IsReader ?? false;
        public bool IsAuthor => CurrentUser?.IsAuthor ?? false;
        public bool IsAdmin => CurrentUser?.IsAdmin ?? false;

        public ICommand LogoutCommand { get; }
        public ICommand NavigateToBookListCommand { get; }
        public ICommand NavigateToReaderListCommand { get; }
        public ICommand NavigateToMyBooksCommand { get; }
        public ICommand NavigateToAddBookCommand { get; }
        public ICommand NavigateToAddReaderCommand { get; }
        public ICommand NavigateToAuthorsCommand { get; }
        public ICommand NavigateToGenresCommand { get; }
        public ICommand NavigateToUsersCommand { get; }
        public ICommand NavigateToProfileCommand { get; }
        public ICommand NavigateToMyLibraryCommand { get; }

        public MainWindowViewModel(
            IUserService userService,
            IBookService bookService = null,
            IReaderService readerService = null)
        {
            _userService = userService;
            _bookService = bookService ?? new FileBookService();
            _readerService = readerService ?? new FileReaderService();

            LogoutCommand = new RelayCommand(Logout, CanLogout);
            NavigateToBookListCommand = new RelayCommand(NavigateToBookList, CanNavigate);
            NavigateToReaderListCommand = new RelayCommand(NavigateToReaderList, CanNavigate);
            NavigateToMyBooksCommand = new RelayCommand(NavigateToMyBooks, CanNavigateAsAuthor);
            NavigateToAddBookCommand = new RelayCommand(NavigateToAddBook, CanNavigateAsAuthorOrAdmin);
            NavigateToAddReaderCommand = new RelayCommand(NavigateToAddReader, CanNavigateAsAdmin);
            NavigateToAuthorsCommand = new RelayCommand(NavigateToAuthors, CanNavigateAsAdmin);
            NavigateToGenresCommand = new RelayCommand(NavigateToGenres, CanNavigateAsAdmin);
            NavigateToUsersCommand = new RelayCommand(NavigateToUsers, CanNavigateAsAdmin);
            NavigateToProfileCommand = new RelayCommand(NavigateToProfile, CanNavigate);
            NavigateToMyLibraryCommand = new RelayCommand(NavigateToMyLibrary, CanNavigateAsReader);

            // Показываем экран логина при запуске
            ShowLoginScreen();
        }

        private void ShowLoginScreen()
        {
            var loginViewModel = new LoginViewModel(_userService);
            loginViewModel.LoginSuccessful += (s, e) => {
                CurrentUser = e.User;
                NavigateToBookList(null);
            };
            loginViewModel.RegisterRequested += (s, e) => ShowRegisterScreen();
            CurrentView = loginViewModel;
        }

        private void ShowRegisterScreen()
        {
            var registerViewModel = new RegisterViewModel(_userService, CurrentUser?.IsAdmin == true);
            registerViewModel.RegistrationSuccessful += (s, e) => ShowLoginScreen();
            registerViewModel.CancelRequested += (s, e) => ShowLoginScreen();
            registerViewModel.LoginRequested += (s, e) => ShowLoginScreen(); // Эта строка должна быть
            CurrentView = registerViewModel;
        }

        private bool CanLogout(object obj)
        {
            return CurrentUser != null;
        }

        private void Logout(object obj)
        {
            CurrentUser = null;
            ShowLoginScreen();
        }

        private bool CanNavigate(object obj)
        {
            return CurrentUser != null;
        }

        private bool CanNavigateAsAuthor(object obj)
        {
            return (CurrentUser?.IsAuthor == true) || (CurrentUser?.IsAdmin == true);
        }

        private bool CanNavigateAsAuthorOrAdmin(object obj)
        {
            return (CurrentUser?.IsAuthor == true) || (CurrentUser?.IsAdmin == true);
        }

        private bool CanNavigateAsAdmin(object obj)
        {
            return CurrentUser?.IsAdmin == true;
        }

        private bool CanNavigateAsReader(object obj)
        {
            return (CurrentUser?.IsReader == true) || (CurrentUser?.IsAdmin == true);
        }

        private void NavigateToBookList(object obj)
        {
            CurrentView = new BookListViewModel(_bookService);
        }

        private void NavigateToReaderList(object obj)
        {
            CurrentView = new ReaderListViewModel(_readerService);
        }

        private void NavigateToMyBooks(object obj)
        {
            // Здесь будет реализация навигации к списку книг автора
            // CurrentView = new AuthorBooksViewModel(_bookService, CurrentUser.Id);
        }

        private void NavigateToAddBook(object obj)
        {
            // Здесь будет реализация навигации к добавлению книги
            // Например, можно использовать существующую AddBookViewModel
            // var addBookViewModel = new AddBookViewModel(_bookService);
            // CurrentView = addBookViewModel;
        }

        private void NavigateToAddReader(object obj)
        {
            // Здесь будет реализация навигации к добавлению читателя
            // var addReaderViewModel = new AddReaderViewModel(_readerService);
            // CurrentView = addReaderViewModel;
        }

        private void NavigateToAuthors(object obj)
        {
            // Здесь будет реализация навигации к управлению авторами
            // CurrentView = new AuthorsManagementViewModel(...);
        }

        private void NavigateToGenres(object obj)
        {
            // Здесь будет реализация навигации к управлению жанрами
            // CurrentView = new GenresManagementViewModel(...);
        }

        private void NavigateToUsers(object obj)
        {
            // Здесь будет реализация навигации к управлению пользователями
            // CurrentView = new UsersManagementViewModel(_userService);
        }

        private void NavigateToProfile(object obj)
        {
            // Здесь будет реализация навигации к профилю пользователя
            // CurrentView = new UserProfileViewModel(_userService, CurrentUser);
        }

        private void NavigateToMyLibrary(object obj)
        {
            // Здесь будет реализация навигации к библиотеке пользователя
            // CurrentView = new UserLibraryViewModel(_bookService, CurrentUser.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}