// RegisterViewModel.cs
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReadingRoomApp.Commands;
using ReadingRoomApp.Models;
using ReadingRoomApp.Services;

namespace ReadingRoomApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _firstName;
        private string _lastName;
        private UserRole _selectedRole = UserRole.Reader;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        public UserRole SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAdminOption { get; private set; }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool CanRegister { get; private set; }

        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RegistrationSuccessful;
        public event EventHandler CancelRequested;
        public event EventHandler LoginRequested;

        public RegisterViewModel(IUserService userService, bool isAdmin = false)
        {
            _userService = userService;
            ShowAdminOption = isAdmin;

            RegisterCommand = new RelayCommand(RegisterAsync, (param) => CanRegister);
            CancelCommand = new RelayCommand((param) => CancelRequested?.Invoke(this, EventArgs.Empty));
        }

        private async void RegisterAsync(object obj)
        {
            // Проверка уникальности имени пользователя
            if (!await _userService.IsUsernameUniqueAsync(Username))
            {
                ErrorMessage = "Имя пользователя уже занято";
                return;
            }

            // Проверка уникальности email
            if (!await _userService.IsEmailUniqueAsync(Email))
            {
                ErrorMessage = "Email уже используется";
                return;
            }

            var user = new User
            {
                Username = Username,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Role = SelectedRole
            };

            bool success = await _userService.RegisterAsync(user, Password);
            if (success)
            {
                RegistrationSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = "Ошибка при регистрации. Пожалуйста, попробуйте снова.";
            }
        }

        private void ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Введите имя пользователя";
                CanRegister = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Введите пароль";
                CanRegister = false;
                return;
            }

            if (Password.Length < 4)
            {
                ErrorMessage = "Пароль должен содержать минимум 4 символа";
                CanRegister = false;
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Пароли не совпадают";
                CanRegister = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Введите email";
                CanRegister = false;
                return;
            }

            if (!Email.Contains("@") || !Email.Contains("."))
            {
                ErrorMessage = "Введите корректный email";
                CanRegister = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                ErrorMessage = "Введите имя";
                CanRegister = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                ErrorMessage = "Введите фамилию";
                CanRegister = false;
                return;
            }

            ErrorMessage = string.Empty;
            CanRegister = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}