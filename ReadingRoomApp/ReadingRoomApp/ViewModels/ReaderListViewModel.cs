// ReaderListViewModel.cs (обновленный)
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
    public class ReaderListViewModel : INotifyPropertyChanged
    {
        private readonly IReaderService _readerService;
        private ObservableCollection<Reader> _readers;
        private Reader _selectedReader;
        private object _currentView;

        public ObservableCollection<Reader> Readers
        {
            get => _readers;
            set
            {
                _readers = value;
                OnPropertyChanged();
            }
        }

        public Reader SelectedReader
        {
            get => _selectedReader;
            set
            {
                _selectedReader = value;
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

        public ICommand AddReaderCommand { get; }
        public ICommand EditReaderCommand { get; }
        public ICommand DeleteReaderCommand { get; }
        public ICommand ViewReaderDetailsCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ReaderListViewModel(IReaderService readerService = null)
        {
            _readerService = readerService ?? new FileReaderService();
            Readers = new ObservableCollection<Reader>();

            AddReaderCommand = new RelayCommand(AddReader);
            EditReaderCommand = new RelayCommand(EditReader, CanEditReader);
            DeleteReaderCommand = new RelayCommand(DeleteReader, CanDeleteReader);
            ViewReaderDetailsCommand = new RelayCommand(ViewReaderDetails, CanViewReaderDetails);

            LoadReaders();
        }

        private async void LoadReaders()
        {
            var readers = await _readerService.GetAllAsync();
            Readers.Clear();
            foreach (var reader in readers)
            {
                Readers.Add(reader);
            }
        }

        private void AddReader(object obj)
        {
            var addReaderViewModel = new AddReaderViewModel(_readerService);
            addReaderViewModel.ReaderAdded += (s, e) => {
                LoadReaders();
                CurrentView = null;
            };
            addReaderViewModel.CancelRequested += (s, e) => CurrentView = null;
            CurrentView = addReaderViewModel;
        }

        private bool CanEditReader(object obj) => SelectedReader != null;

        private void EditReader(object obj)
        {
            if (SelectedReader != null)
            {
                // Здесь будет инициализация EditReaderViewModel
                // Пока заглушка
                CurrentView = null;
            }
        }

        private bool CanDeleteReader(object obj) => SelectedReader != null;

        private async void DeleteReader(object obj)
        {
            if (SelectedReader != null)
            {
                await _readerService.DeleteAsync(SelectedReader.Id);
                Readers.Remove(SelectedReader);
            }
        }

        private bool CanViewReaderDetails(object obj) => SelectedReader != null;

        private void ViewReaderDetails(object obj)
        {
            if (SelectedReader != null)
            {
                var readerDetailsViewModel = new ReaderDetailsViewModel(SelectedReader);
                readerDetailsViewModel.CloseRequested += (s, e) => CurrentView = null;
                readerDetailsViewModel.EditRequested += (s, reader) => EditReader(reader);
                CurrentView = readerDetailsViewModel;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}