// BookDetailsViewModel.cs
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReadingRoomApp.Commands;
using ReadingRoomApp.Models;

namespace ReadingRoomApp.ViewModels
{
    public class BookDetailsViewModel : INotifyPropertyChanged
    {
        private Book _book;

        public Book Book
        {
            get => _book;
            set
            {
                _book = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand EditCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CloseRequested;
        public event EventHandler<Book> EditRequested;

        public BookDetailsViewModel(Book book)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));

            CloseCommand = new RelayCommand(Close);
            EditCommand = new RelayCommand(Edit);
        }

        private void Close(object obj)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Edit(object obj)
        {
            EditRequested?.Invoke(this, Book);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}