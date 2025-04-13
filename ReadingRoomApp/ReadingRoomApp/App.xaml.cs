using System;
using System.Configuration;
using System.IO;
using System.Windows;
using ReadingRoomApp.Services;

namespace ReadingRoomApp
{
    public partial class App : Application
    {
        // Строка подключения к базе данных
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ReadingRoomDB"]?.ConnectionString;

        // Сервисы для использования в приложении
        public static IUserService UserService { get; private set; }
        public static IBookService BookService { get; private set; }
        public static IReaderService ReaderService { get; private set; }
        public static IAuthorService AuthorService { get; private set; }
        public static IGenreService GenreService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создаем директорию для хранения данных, если её нет
            EnsureDataDirectoryExists();

            // Инициализация сервисов в зависимости от наличия строки подключения к БД
            InitializeServices();
        }

        private void EnsureDataDirectoryExists()
        {
            string dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
        }

        private void InitializeServices()
        {
            // Если есть строка подключения, используем сервисы для базы данных
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                try
                {
                    UserService = new DbUserService(ConnectionString);
                    BookService = new DbBookService(ConnectionString);
                    ReaderService = new DbReaderService(ConnectionString);
                    AuthorService = new DbAuthorService(ConnectionString);
                    GenreService = new DbGenreService(ConnectionString);

                    // Пока используем файловые сервисы, пока БД не будет готова
                    //UserService = new FileUserService();
                    //BookService = new FileBookService();
                   //ReaderService = new FileReaderService();
                    //AuthorService = new FileAuthorService();
                    //GenreService = new FileGenreService();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при подключении к базе данных: {ex.Message}. Будут использованы файловые хранилища.",
                                   "Ошибка подключения",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);

                    // Если произошла ошибка, используем файловые хранилища
                    InitializeFileServices();
                }
            }
            else
            {
                // Если строки подключения нет, используем файловые хранилища
                InitializeFileServices();
            }
        }

        private void InitializeFileServices()
        {
            UserService = new FileUserService();
            BookService = new FileBookService();
            ReaderService = new FileReaderService();
            AuthorService = new FileAuthorService();
            GenreService = new FileGenreService();
        }
    }
}