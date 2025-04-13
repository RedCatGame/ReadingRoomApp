// ReaderDetailsViewModel.cs
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReadingRoomApp.Commands;
using ReadingRoomApp.Models;

namespace ReadingRoomApp.ViewModels
{
    public class ReaderDetailsViewModel : INotifyPropertyChanged
    {
        private Reader _reader;

        public Reader Reader
        {
            get => _reader;
            set
            {
                _reader = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand EditCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CloseRequested;
        public event EventHandler<Reader> EditRequested;

        public ReaderDetailsViewModel(Reader reader)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));

            CloseCommand = new RelayCommand(Close);
            EditCommand = new RelayCommand(Edit);
        }

        private void Close(object obj)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Edit(object obj)
        {
            EditRequested?.Invoke(this, Reader);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}