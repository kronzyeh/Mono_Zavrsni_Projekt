using Library.Model;
using Library.Repository.Common;
using Library.Service.Common;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository registrationRepository;
        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            this.registrationRepository = registrationRepository;
        }
        [HttpPost]
        public async Task<string> RegisterUserAsync([FromBody] User user)
        {
            try
            {
                user = SetUserData(user);
                return await registrationRepository.RegisterAsync(user);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            try
            {
                
                return await registrationRepository.LoginAsync(email, password);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        private User SetUserData(User user)
        {
            string hashedPassword = HashPassword(user.Password);
            user.Password = hashedPassword;
            user.Id = Guid.NewGuid();
            user.UpdatedByUserId = user.Id;
            user.CreatedByUserId = user.Id;
            user.DateCreated = DateTime.Now; 
            user.DateUpdated = DateTime.Now;
            user.IsActive = true;
            user.RoleId = Guid.Parse("c72d4c65-4d08-49ab-84e1-6cb3341f8bb6");

            return user;
        }
        private string HashPassword(string password)
        {
            // Generate a unique salt for each user
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password using PBKDF2
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Combine the salt and hashed password for storage
            string combinedHash = $"{Convert.ToBase64String(salt)}:{hashedPassword}";

            return combinedHash;
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
