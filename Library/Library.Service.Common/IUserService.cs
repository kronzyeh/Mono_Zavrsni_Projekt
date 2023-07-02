using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Service.Common
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetSpecificUserAsync(Guid id);
        Task<int> UpdateUserAsync(Guid id, [FromBody] User user);
        Task<int> DeleteUserAsync(Guid id);
    }
}
