using Library.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Service.Common
{
    public interface IRegistrationService
    {
        Task<User> LoginUserAsync(string email, string password);
        Task<string> RegisterUserAsync([FromBody] User user);
    }
}
