using Library.Model;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService (IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public async Task<List<User>> GetUsersAsync ()
        {
            try
            {
                return await userRepository.GetAllUsersAsync();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }
        [HttpGet]
        public async Task<User> GetSpecificUserAsync(Guid id)
        {
            try
            {
                return await userRepository.GetUserByIdAsync(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }
        [HttpPut]
        public async Task<int> UpdateUserAsync(Guid id, [FromBody] User user)
        {
            try
            {
                return await userRepository.EditUserAsync(id, user);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }
        [HttpDelete]
        public async Task<int> DeleteUserAsync(Guid id)
        {
            try
            {
                return await userRepository.DeleteUserAsync(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }
    }
}
