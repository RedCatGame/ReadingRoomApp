// DbUserService.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ReadingRoomApp.Models;

namespace ReadingRoomApp.Services
{
    public class DbUserService : IUserService
    {
        private readonly string _connectionString;

        public DbUserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                var query = @"
                    SELECT UserId, Username, Email, FirstName, LastName, PasswordHash, PasswordSalt, Role
                    FROM Users
                    WHERE Username = @Username";

                using (var command = new SqlCommand(query, context.Connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var userId = reader.GetInt32(0);
                            var userName = reader.GetString(1);
                            var email = reader.GetString(2);
                            var firstName = reader.GetString(3);
                            var lastName = reader.GetString(4);
                            var passwordHash = reader.GetString(5);
                            var passwordSalt = reader.GetString(6);
                            var role = (UserRole)reader.GetInt32(7);

                            // Проверка пароля
                            var hashedPassword = HashPassword(password, passwordSalt);
                            if (hashedPassword == passwordHash)
                            {
                                return new User
                                {
                                    Id = userId,
                                    Username = userName,
                                    Email = email,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    Role = role
                                };
                            }
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> RegisterAsync(User user, string password)
        {
            // Генерация соли для пароля
            var salt = $"salt_for_{user.Username}_{Guid.NewGuid().ToString().Substring(0, 8)}";
            var passwordHash = HashPassword(password, salt);

            using (var context = new DatabaseContext(_connectionString))
            {
                var query = @"
                    INSERT INTO Users (Username, Email, FirstName, LastName, PasswordHash, PasswordSalt, Role)
                    VALUES (@Username, @Email, @FirstName, @LastName, @PasswordHash, @PasswordSalt, @Role)";

                using (var command = new SqlCommand(query, context.Connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@PasswordSalt", salt);
                    command.Parameters.AddWithValue("@Role", (int)user.Role);

                    try
                    {
                        var result = await command.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                var query = @"
                    SELECT UserId, Username, Email, FirstName, LastName, Role
                    FROM Users
                    WHERE Username = @Username";

                using (var command = new SqlCommand(query, context.Connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Email = reader.GetString(2),
                                FirstName = reader.GetString(3),
                                LastName = reader.GetString(4),
                                Role = (UserRole)reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                var query = @"
                    SELECT UserId, Username, Email, FirstName, LastName, Role
                    FROM Users
                    WHERE Email = @Email";

                using (var command = new SqlCommand(query, context.Connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Email = reader.GetString(2),
                                FirstName = reader.GetString(3),
                                LastName = reader.GetString(4),
                                Role = (UserRole)reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                var query = @"
                    UPDATE Users
                    SET Username = @Username, Email = @Email, FirstName = @FirstName, 
                        LastName = @LastName, Role = @Role, UpdatedAt = GETDATE()
                    WHERE UserId = @UserId";

                using (var command = new SqlCommand(query, context.Connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Role", (int)user.Role);

                    try
                    {
                        var result = await command.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            // Сначала проверяем старый пароль
            using (var context = new DatabaseContext(_connectionString))
            {
                var query = @"
                    SELECT PasswordHash, PasswordSalt
                    FROM Users
                    WHERE UserId = @UserId";

                using (var command = new SqlCommand(query, context.Connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var passwordHash = reader.GetString(0);
                            var passwordSalt = reader.GetString(1);

                            var hashedOldPassword = HashPassword(oldPassword, passwordSalt);
                            if (hashedOldPassword != passwordHash)
                            {
                                return false; // Старый пароль неверный
                            }

                            // Если старый пароль правильный, обновляем пароль
                            var newSalt = $"salt_for_user_{userId}_{Guid.NewGuid().ToString().Substring(0, 8)}";
                            var newPasswordHash = HashPassword(newPassword, newSalt);

                            var updateQuery = @"
                                UPDATE Users
                                SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, UpdatedAt = GETDATE()
                                WHERE UserId = @UserId";

                            using (var updateCommand = new SqlCommand(updateQuery, context.Connection))
                            {
                                updateCommand.Parameters.AddWithValue("@UserId", userId);
                                updateCommand.Parameters.AddWithValue("@PasswordHash", newPasswordHash);
                                updateCommand.Parameters.AddWithValue("@PasswordSalt", newSalt);

                                var result = await updateCommand.ExecuteNonQueryAsync();
                                return result > 0;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                var query = @"
                    SELECT COUNT(*)
                    FROM Users
                    WHERE Username = @Username";

                using (var command = new SqlCommand(query, context.Connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var count = (int)await command.ExecuteScalarAsync();
                    return count == 0;
                }
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                var query = @"
                    SELECT COUNT(*)
                    FROM Users
                    WHERE Email = @Email";

                using (var command = new SqlCommand(query, context.Connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    var count = (int)await command.ExecuteScalarAsync();
                    return count == 0;
                }
            }
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
    }
}