// AddBookViewModel.cs (обновленный)
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ReadingRoomApp.Commands;
using ReadingRoomApp.Models;
using ReadingRoomApp.Services;

namespace ReadingRoomApp.ViewModels
{
    public class AddBookViewModel : INotifyPropertyChanged
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private Book _newBook;
        private ObservableCollection<Author> _authors;
        private ObservableCollection<Genre> _genres;

        public Book NewBook
        {
            get => _newBook;
            set
            {
                _newBook = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Genre> Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler BookAdded;
        public event EventHandler CancelRequested;

        public AddBookViewModel(
            IBookService bookService,
            IAuthorService authorService = null,
            IGenreService genreService = null)
        {
            _bookService = bookService;
            _authorService = authorService ?? new FileAuthorService();
            _genreService = genreService ?? new FileGenreService();

            NewBook = new Book { IsAvailable = true };
            Authors = new ObservableCollection<Author>();
            Genres = new ObservableCollection<Genre>();

            SaveCommand = new RelayCommand(SaveBook, CanSaveBook);
            CancelCommand = new RelayCommand(Cancel);

            LoadAuthorsAndGenres();
        }

        private async void LoadAuthorsAndGenres()
        {
            var authors = await _authorService.GetAllAsync();
            Authors.Clear();
            foreach (var author in authors)
            {
                Authors.Add(author);
            }

            var genres = await _genreService.GetAllAsync();
            Genres.Clear();
            foreach (var genre in genres)
            {
                Genres.Add(genre);
            }
        }

        private bool CanSaveBook(object obj)
        {
            return !string.IsNullOrWhiteSpace(NewBook.Title) &&
                   NewBook.Author != null &&
                   NewBook.Genre != null &&
                   NewBook.PublicationYear > 0;
        }

        private async void SaveBook(object obj)
        {
            await _bookService.AddAsync(NewBook);
            BookAdded?.Invoke(this, EventArgs.Empty);
        }

        private void Cancel(object obj)
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}