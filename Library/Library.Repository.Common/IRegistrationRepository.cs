using Library.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Repository.Common
{
    public interface IRegistrationRepository
    {
        Task<string> RegisterAsync([FromBody] User user);
        Task<User> LoginAsync(string email, string password);
    }
}
