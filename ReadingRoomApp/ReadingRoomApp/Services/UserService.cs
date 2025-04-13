// UserService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ReadingRoomApp.Models;

namespace ReadingRoomApp.Services
{
    public class FileUserService : IUserService
    {
        private const string FILE_PATH = @"Data\users.json";
        private List<UserWithPassword> _users;

        public FileUserService()
        {
            LoadUsers();

            // Добавление администратора по умолчанию, если его нет
            if (!_users.Any(u => u.Username == "RedKotik"))
            {
                _users.Add(new UserWithPassword
                {
                    Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1,
                    Username = "RedKotik",
                    Email = "admin@readingroom.com",
                    FirstName = "Admin",
                    LastName = "User",
                    Role = UserRole.Admin,
                    PasswordHash = HashPassword("1234", "salt_for_admin")
                });
                SaveUsers();
            }
        }

        private void LoadUsers()
        {
            _users = JsonHelper.LoadFromJsonFile<UserWithPassword>(FILE_PATH);
        }

        private void SaveUsers()
        {
            JsonHelper.SaveToJsonFile(_users, FILE_PATH);
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return await Task.FromResult(ConvertToUser(user));
            }
            return null;
        }

        public async Task<bool> RegisterAsync(User user, string password)
        {
            if (_users.Any(u => u.Username == user.Username || u.Email == user.Email))
            {
                return false;
            }

            var userWithPassword = new UserWithPassword
            {
                Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                PasswordHash = HashPassword(password, $"salt_for_{user.Username}")
            };

            _users.Add(userWithPassword);
            SaveUsers();
            return await Task.FromResult(true);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            return await Task.FromResult(user != null ? ConvertToUser(user) : null);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            return await Task.FromResult(user != null ? ConvertToUser(user) : null);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
                return false;

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Role = user.Role;

            SaveUsers();
            return await Task.FromResult(true);
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null || !VerifyPassword(oldPassword, user.PasswordHash))
                return false;

            user.PasswordHash = HashPassword(newPassword, $"salt_for_{user.Username}");
            SaveUsers();
            return await Task.FromResult(true);
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            return await Task.FromResult(!_users.Any(u => u.Username == username));
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await Task.FromResult(!_users.Any(u => u.Email == email));
        }

        private User ConvertToUser(UserWithPassword userWithPassword)
        {
            return new User
            {
                Id = userWithPassword.Id,
                Username = userWithPassword.Username,
                Email = userWithPassword.Email,
                FirstName = userWithPassword.FirstName,
                LastName = userWithPassword.LastName,
                Role = userWithPassword.Role
            };
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = $"{password}_{salt}";
                var bytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Для упрощения в этом примере мы используем тот же соляной шаблон и алгоритм хеширования
            // В реальном приложении соли и хеши следует сохранять отдельно
            var salt = "salt_for_" + password; // Упрощенная версия, в реальном приложении соль должна быть уникальной для каждого пользователя
            var hash = HashPassword(password, salt);
            return hash == storedHash;
        }

        // Вспомогательный класс для хранения пароля
        private class UserWithPassword : User
        {
            public string PasswordHash { get; set; }
        }
    }
}