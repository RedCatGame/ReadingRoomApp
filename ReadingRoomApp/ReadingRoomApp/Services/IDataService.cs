using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingRoomApp.Models;

namespace ReadingRoomApp.Services
{
    public interface IDataService<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
    }

    public interface IBookService : IDataService<Book>
    {
        Task<List<Book>> GetBooksByAuthorAsync(int authorId);
        Task<List<Book>> GetBooksByGenreAsync(int genreId);
    }

    public interface IReaderService : IDataService<Reader>
    {
        Task<Reader> GetReaderByEmailAsync(string email);
    }
    public interface IAuthorService : IDataService<Author>
    {
    }

    public interface IGenreService : IDataService<Genre>
    {
    }
}