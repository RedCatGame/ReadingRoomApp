// BookListViewModel.cs (обновленный)
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReadingRoomApp.Commands;
using ReadingRoomApp.Models;
using ReadingRoomApp.Services;

namespace ReadingRoomApp.ViewModels
{
    public class BookListViewModel : INotifyPropertyChanged
    {
        private readonly IBookService _bookService;
        private ObservableCollection<Book> _books;
        private Book _selectedBook;
        private object _currentView;

        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
            }
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsListViewVisible));
            }
        }

        public bool IsListViewVisible => CurrentView == null;

        public ICommand AddBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand ViewBookDetailsCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public BookListViewModel(IBookService bookService = null)
        {
            _bookService = bookService ?? new FileBookService();
            Books = new ObservableCollection<Book>();

            AddBookCommand = new RelayCommand(AddBook);
            EditBookCommand = new RelayCommand(EditBook, CanEditBook);
            DeleteBookCommand = new RelayCommand(DeleteBook, CanDeleteBook);
            ViewBookDetailsCommand = new RelayCommand(ViewBookDetails, CanViewBookDetails);

            LoadBooks();
        }

        private async void LoadBooks()
        {
            var books = await _bookService.GetAllAsync();
            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        private void AddBook(object obj)
        {
            var addBookViewModel = new AddBookViewModel(_bookService);
            addBookViewModel.BookAdded += (s, e) => {
                LoadBooks();
                CurrentView = null;
            };
            addBookViewModel.CancelRequested += (s, e) => CurrentView = null;
            CurrentView = addBookViewModel;
        }

        private bool CanEditBook(object obj) => SelectedBook != null;

        private void EditBook(object obj)
        {
            if (SelectedBook != null)
            {
                var editBookViewModel = new EditBookViewModel(_bookService, SelectedBook);
                editBookViewModel.BookUpdated += (s, e) => {
                    LoadBooks();
                    CurrentView = null;
                };
                editBookViewModel.CancelRequested += (s, e) => CurrentView = null;
                CurrentView = editBookViewModel;
            }
        }

        private bool CanDeleteBook(object obj) => SelectedBook != null;

        private async void DeleteBook(object obj)
        {
            if (SelectedBook != null)
            {
                await _bookService.DeleteAsync(SelectedBook.Id);
                Books.Remove(SelectedBook);
            }
        }

        private bool CanViewBookDetails(object obj) => SelectedBook != null;

        private void ViewBookDetails(object obj)
        {
            if (SelectedBook != null)
            {
                var bookDetailsViewModel = new BookDetailsViewModel(SelectedBook);
                bookDetailsViewModel.CloseRequested += (s, e) => CurrentView = null;
                bookDetailsViewModel.EditRequested += (s, book) => EditBook(book);
                CurrentView = bookDetailsViewModel;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}