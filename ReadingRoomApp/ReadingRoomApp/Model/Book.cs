using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReadingRoomApp.Models
{
    public class Book : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private Author _author;
        private Genre _genre;
        private int _publicationYear;
        private bool _isAvailable;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public Author Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public Genre Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged();
            }
        }

        public int PublicationYear
        {
            get => _publicationYear;
            set
            {
                _publicationYear = value;
                OnPropertyChanged();
            }
        }

        public bool IsAvailable
        {
            get => _isAvailable;
            set
            {
                _isAvailable = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}