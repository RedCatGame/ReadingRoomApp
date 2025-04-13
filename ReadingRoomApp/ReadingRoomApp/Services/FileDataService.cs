using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingRoomApp.Models;

namespace ReadingRoomApp.Services
{
    public class FileBookService : IBookService
    {
        private const string FILE_PATH = @"Data\books.json";
        private List<Book> _books;

        public FileBookService()
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            _books = JsonHelper.LoadFromJsonFile<Book>(FILE_PATH);
        }

        private void SaveBooks()
        {
            JsonHelper.SaveToJsonFile(_books, FILE_PATH);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await Task.FromResult(_books);
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await Task.FromResult(_books.FirstOrDefault(b => b.Id == id));
        }

        public async Task<Book> AddAsync(Book book)
        {
            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
            SaveBooks();
            return await Task.FromResult(book);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null)
                return null;

            int index = _books.IndexOf(existingBook);
            _books[index] = book;
            SaveBooks();
            return await Task.FromResult(book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return false;

            _books.Remove(book);
            SaveBooks();
            return await Task.FromResult(true);
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await Task.FromResult(_books.Where(b => b.Author?.Id == authorId).ToList());
        }

        public async Task<List<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await Task.FromResult(_books.Where(b => b.Genre?.Id == genreId).ToList());
        }
    }

    public class FileReaderService : IReaderService
    {
        private const string FILE_PATH = @"Data\readers.json";
        private List<Reader> _readers;

        public FileReaderService()
        {
            LoadReaders();
        }

        private void LoadReaders()
        {
            _readers = JsonHelper.LoadFromJsonFile<Reader>(FILE_PATH);
        }

        private void SaveReaders()
        {
            JsonHelper.SaveToJsonFile(_readers, FILE_PATH);
        }

        public async Task<List<Reader>> GetAllAsync()
        {
            return await Task.FromResult(_readers);
        }

        public async Task<Reader> GetByIdAsync(int id)
        {
            return await Task.FromResult(_readers.FirstOrDefault(r => r.Id == id));
        }

        public async Task<Reader> GetReaderByEmailAsync(string email)
        {
            return await Task.FromResult(_readers.FirstOrDefault(r => r.Email == email));
        }

        public async Task<Reader> AddAsync(Reader reader)
        {
            reader.Id = _readers.Any() ? _readers.Max(r => r.Id) + 1 : 1;
            _readers.Add(reader);
            SaveReaders();
            return await Task.FromResult(reader);
        }

        public async Task<Reader> UpdateAsync(Reader reader)
        {
            var existingReader = _readers.FirstOrDefault(r => r.Id == reader.Id);
            if (existingReader == null)
                return null;

            int index = _readers.IndexOf(existingReader);
            _readers[index] = reader;
            SaveReaders();
            return await Task.FromResult(reader);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reader = _readers.FirstOrDefault(r => r.Id == id);
            if (reader == null)
                return false;

            _readers.Remove(reader);
            SaveReaders();
            return await Task.FromResult(true);
        }
    }
    public class FileAuthorService : IAuthorService
    {
        private const string FILE_PATH = @"Data\authors.json";
        private List<Author> _authors;

        public FileAuthorService()
        {
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            _authors = JsonHelper.LoadFromJsonFile<Author>(FILE_PATH);
        }

        private void SaveAuthors()
        {
            JsonHelper.SaveToJsonFile(_authors, FILE_PATH);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await Task.FromResult(_authors);
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await Task.FromResult(_authors.FirstOrDefault(a => a.Id == id));
        }

        public async Task<Author> AddAsync(Author author)
        {
            author.Id = _authors.Any() ? _authors.Max(a => a.Id) + 1 : 1;
            _authors.Add(author);
            SaveAuthors();
            return await Task.FromResult(author);
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id);
            if (existingAuthor == null)
                return null;

            int index = _authors.IndexOf(existingAuthor);
            _authors[index] = author;
            SaveAuthors();
            return await Task.FromResult(author);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return false;

            _authors.Remove(author);
            SaveAuthors();
            return await Task.FromResult(true);
        }
    }

    public class FileGenreService : IGenreService
    {
        private const string FILE_PATH = @"Data\genres.json";
        private List<Genre> _genres;

        public FileGenreService()
        {
            LoadGenres();
        }

        private void LoadGenres()
        {
            _genres = JsonHelper.LoadFromJsonFile<Genre>(FILE_PATH);
        }

        private void SaveGenres()
        {
            JsonHelper.SaveToJsonFile(_genres, FILE_PATH);
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await Task.FromResult(_genres);
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await Task.FromResult(_genres.FirstOrDefault(g => g.Id == id));
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            genre.Id = _genres.Any() ? _genres.Max(g => g.Id) + 1 : 1;
            _genres.Add(genre);
            SaveGenres();
            return await Task.FromResult(genre);
        }

        public async Task<Genre> UpdateAsync(Genre genre)
        {
            var existingGenre = _genres.FirstOrDefault(g => g.Id == genre.Id);
            if (existingGenre == null)
                return null;

            int index = _genres.IndexOf(existingGenre);
            _genres[index] = genre;
            SaveGenres();
            return await Task.FromResult(genre);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            if (genre == null)
                return false;

            _genres.Remove(genre);
            SaveGenres();
            return await Task.FromResult(true);
        }
    }
}