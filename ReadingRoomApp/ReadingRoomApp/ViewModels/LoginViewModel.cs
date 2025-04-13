// LoginViewModel.cs (обновленный)
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReadingRoomApp.Commands;
using ReadingRoomApp.Models;
using ReadingRoomApp.Services;

namespace ReadingRoomApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private string _username;
        private string _password;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public event EventHandler<AuthenticatedUserEventArgs> LoginSuccessful;
        public event EventHandler RegisterRequested;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new RelayCommand(LoginAsync, param => CanLogin);
            NavigateToRegisterCommand = new RelayCommand(param => RegisterRequested?.Invoke(this, EventArgs.Empty));
        }

        private async void LoginAsync(object param)
        {
            var user = await _userService.AuthenticateAsync(Username, Password);
            if (user != null)
            {
                LoginSuccessful?.Invoke(this, new AuthenticatedUserEventArgs(user));
                ErrorMessage = string.Empty;
            }
            else
            {
                ErrorMessage = "Неверное имя пользователя или пароль";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AuthenticatedUserEventArgs : EventArgs
    {
        public User User { get; }

        public AuthenticatedUserEventArgs(User user)
        {
            User = user;
        }
    }
}