using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebApi.Models
{
    public class LoginReturnRest
    {
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}