using Library.Model;
using Library.Repository.Common;
using MongoDB.Driver.Core.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString = "Server=localhost;Port=5432;Database=library;User Id = postgres; Password=tomo;";

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT u.*, sh.\"StartDate\", sh.\"EndDate\" FROM \"User\" AS u LEFT JOIN \"SubscriptionHistory\" AS sh ON u.\"Id\" = sh.\"UserId\"", connection))
                    {
                        using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                User user = new User();

                                user.Id = (Guid)reader["Id"];
                                user.FirstName = (string)reader["FirstName"];
                                user.LastName = (string)reader["LastName"];
                                user.DateOfBirth = (DateTime)reader["DateOfBirth"];
                                user.Email = (string)reader["Email"];
                                user.PhoneNumber = (string)reader["PhoneNumber"];
                                user.Password = (string)reader["Password"];
                                user.DateCreated = (DateTime)reader["DateCreated"];
                                user.DateUpdated = (DateTime)reader["DateUpdated"];
                                user.CreatedByUserId = (Guid)reader["CreatedByUserId"];
                                user.UpdatedByUserId = (Guid)reader["UpdatedByUserId"];
                                user.IsActive = (bool)reader["IsActive"];
                                user.RoleId = (Guid)reader["RoleId"];
                                //user.StartDate = (DateTime)reader["StartDate"];
                                //user.EndDate = (DateTime)reader["EndDate"];
                                 

                                users.Add(user);
                            }

                        }
                    }
                }
                }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
            return users;
        }

        public async Task<User> GetUserByIdAsync (Guid id)
        {
            User user = new User();
            try
            {
                user = await GetUserById(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
            return user;
        }
        [HttpPost]
        public async Task<int> EditUserAsync (Guid id, [FromBody] User user)
        {
            int rowsAffected = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {

                        StringBuilder updateQuery = new StringBuilder("UPDATE \"User\" SET ");

                        if (!string.IsNullOrEmpty(user.FirstName))
                        {
                            updateQuery.Append("\"FirstName\" = @FirstName, ");
                            command.Parameters.AddWithValue("@FirstName", user.FirstName);

                        }

                        if (!string.IsNullOrEmpty(user.LastName))
                        {
                            updateQuery.Append("\"LastName\" = @LastName, ");
                            command.Parameters.AddWithValue("@LastName", user.LastName);

                        }

                        if (!string.IsNullOrEmpty(user.Email))
                        {
                            updateQuery.Append("\"Email\" = @Email, ");
                            command.Parameters.AddWithValue("@Email", user.Email);
                        }

                        if (!string.IsNullOrEmpty(user.PhoneNumber))
                        {
                            updateQuery.Append("\"PhoneNumber\" = @PhoneNumber, ");
                            command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        }
                        if (user.DateOfBirth != null)
                        {
                            updateQuery.Append("\"DateOfBirth\" = @DateOfBirth, ");
                            command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                        }

                        if (updateQuery.Length > 0)
                        {
                            updateQuery.Length -= 2;
                        }

                        updateQuery.Append(" WHERE \"Id\" = @Id");

                        string query = updateQuery.ToString();
                        command.CommandText = query;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("Id", @id);
                        rowsAffected = await command.ExecuteNonQueryAsync();

                    }
                }
            }catch(Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw;
            }

            return rowsAffected;
        }
        public async Task<int> DeleteUserAsync (Guid id)
        {
            int rowsAffected = 0;
            try
            {
                User user = await GetUserById(id);
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("Delete From \"User\" WHERE \"Id\" = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
            return rowsAffected;
        }
        public async Task<User> GetUserById(Guid id)
        {
            User user = new User();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("Select * From \"User\" WHERE \"Id\" = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        reader.Read();

                        user.Id = (Guid)reader["Id"];
                        user.FirstName = (string)reader["FirstName"];
                        user.LastName = (string)reader["LastName"];
                        user.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        user.Email = (string)reader["Email"];
                        user.PhoneNumber = (string)reader["PhoneNumber"];
                        user.Password = (string)reader["Password"];
                        user.DateCreated = (DateTime)reader["DateCreated"];
                        user.DateUpdated = (DateTime)reader["DateUpdated"];
                        user.CreatedByUserId = (Guid)reader["CreatedByUserId"];
                        user.UpdatedByUserId = (Guid)reader["UpdatedByUserId"];
                        user.IsActive = (bool)reader["IsActive"];
                        user.RoleId = (Guid)reader["RoleId"];
                    }
                }
            }
            return user;
        }
    }
}
