// User.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReadingRoomApp.Models
{
    public enum UserRole
    {
        Reader = 0,     // Обычный пользователь (читатель)
        Author = 1,     // Писатель
        Admin = 2       // Администратор
    }

    public class User : INotifyPropertyChanged
    {
        private int _id;
        private string _username;
        private string _email;
        private string _firstName;
        private string _lastName;
        private UserRole _role;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public UserRole Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged();
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public bool IsReader => Role == UserRole.Reader;
        public bool IsAuthor => Role == UserRole.Author;
        public bool IsAdmin => Role == UserRole.Admin;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}