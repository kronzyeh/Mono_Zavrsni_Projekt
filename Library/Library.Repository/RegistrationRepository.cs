using Library.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Repository.Common;
using System.Web.Http.ModelBinding;
using MongoDB.Driver.Core.Configuration;
using System.Runtime.Remoting.Contexts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Web.Security;
using Library.Common;

namespace Library.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        //private readonly string connectionString = "Server=localhost;Port=5432;Database=library;User Id = postgres; Password=tomo;";
        private readonly IConfiguration configuration;
        public RegistrationRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public RegistrationRepository()
        {
        }


        public async Task<string> RegisterAsync([FromBody] User user)
        {
            if (user == null)
            {
                return "User not found";
            }
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return "Email and password are required.";
            }
            
            int rowsAffected;
            try
            {
                StringBuilder query = new StringBuilder("INSERT INTO \"User\" (\"Id\", \"FirstName\", \"LastName\", \"DateOfBirth\", \"PhoneNumber\", \"Email\", \"Password\", \"RoleId\", \"IsActive\", \"CreatedByUserId\", \"UpdatedByUserId\", \"DateCreated\", \"DateUpdated\") " +
    "VALUES (@Id, @FirstName, @LastName, @DateOfBirth, @PhoneNumber, @Email, @Password, @RoleId, @IsActive, @CreatedByUserId, @UpdatedByUserId, @DateCreated, @DateUpdated)");

                using (var connection = new NpgsqlConnection(Helper.connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query.ToString(), connection))
                    {
                       
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                        command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@RoleId", user.RoleId);
                        command.Parameters.AddWithValue("@IsActive", user.IsActive);
                        command.Parameters.AddWithValue("@CreatedByUserId", user.CreatedByUserId);
                        command.Parameters.AddWithValue("@UpdatedByUserId", user.UpdatedByUserId);
                        command.Parameters.AddWithValue("@DateCreated", user.DateCreated);
                        command.Parameters.AddWithValue("@DateUpdated", user.DateUpdated);

                        rowsAffected = await command.ExecuteNonQueryAsync();
                        return "User registered successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            try
            {
                using (var connection = new NpgsqlConnection(Helper.connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT * FROM \"User\" WHERE \"Email\" = @Email";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                string hashedPassword = reader.GetString(reader.GetOrdinal("Password"));

                                // Verify the password
                                if (VerifyPassword(password, hashedPassword))
                                {
                                    // Create and return the User object
                                    var user = new User
                                    {
                                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                        Email = reader.GetString(reader.GetOrdinal("Email")),
                                        RoleId = (Guid)reader["RoleId"]

                                    };

                                    return user;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }

            return null;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                string[] passwordParts = hashedPassword.Split(':');
                if (passwordParts.Length == 2)
                {
                    byte[] salt = Convert.FromBase64String(passwordParts[0]);
                    string storedHashedPassword = passwordParts[1];

                    string hashedPasswordInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA512,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                    return storedHashedPassword.Equals(hashedPasswordInput);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}
