using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ReadingRoomApp.Models;

namespace ReadingRoomApp.Services
{
    public interface ILoginService
    {
        Task<Librarian> AuthenticateAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string email, string password, bool isAdmin = false);
    }

    public class LoginService : ILoginService
    {
        private readonly List<Librarian> _librarians;

        public LoginService()
        {
            // В реальном приложении данные будут храниться в базе данных
            _librarians = new List<Librarian>
            {
                new Librarian
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@library.com",
                    IsAdmin = true
                }
            };
        }

        public async Task<Librarian> AuthenticateAsync(string username, string password)
        {
            // В реальном приложении используйте хеширование и соль
            var librarian = _librarians.FirstOrDefault(l =>
                l.Username == username);

            // Простая проверка (в реальном приложении - более безопасная)
            if (librarian != null && IsPasswordValid(username, password))
            {
                return await Task.FromResult(librarian);
            }

            return null;
        }

        public async Task<bool> RegisterAsync(string username, string email, string password, bool isAdmin = false)
        {
            // Проверка существования пользователя
            if (_librarians.Any(l => l.Username == username || l.Email == email))
            {
                return false;
            }

            var newLibrarian = new Librarian
            {
                Id = _librarians.Any() ? _librarians.Max(l => l.Id) + 1 : 1,
                Username = username,
                Email = email,
                IsAdmin = isAdmin
            };

            _librarians.Add(newLibrarian);
            return await Task.FromResult(true);
        }

        private bool IsPasswordValid(string username, string password)
        {
            // Простая проверка (В реальном приложении - хеширование)
            return password == username + "123";
        }

        // Пример метода хеширования пароля (для более безопасной реализации)
        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}