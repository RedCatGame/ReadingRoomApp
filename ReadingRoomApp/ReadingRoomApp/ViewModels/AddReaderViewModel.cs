// AddReaderViewModel.cs
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReadingRoomApp.Commands;
using ReadingRoomApp.Models;
using ReadingRoomApp.Services;

namespace ReadingRoomApp.ViewModels
{
    public class AddReaderViewModel : INotifyPropertyChanged
    {
        private readonly IReaderService _readerService;
        private Reader _newReader;

        public Reader NewReader
        {
            get => _newReader;
            set
            {
                _newReader = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ReaderAdded;
        public event EventHandler CancelRequested;

        public AddReaderViewModel(IReaderService readerService)
        {
            _readerService = readerService;
            NewReader = new Reader();

            SaveCommand = new RelayCommand(SaveReader, CanSaveReader);
            CancelCommand = new RelayCommand(Cancel);
        }

        private bool CanSaveReader(object obj)
        {
            return !string.IsNullOrWhiteSpace(NewReader.FirstName) &&
                   !string.IsNullOrWhiteSpace(NewReader.LastName) &&
                   !string.IsNullOrWhiteSpace(NewReader.Email);
        }

        private async void SaveReader(object obj)
        {
            await _readerService.AddAsync(NewReader);
            ReaderAdded?.Invoke(this, EventArgs.Empty);
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