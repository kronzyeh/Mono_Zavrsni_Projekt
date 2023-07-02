using Auth0.ManagementApi.Models.Rules;
using Library.Model;
using Library.Service.Common;
using Library.WebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;
using Library.Common;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using System.Linq.Expressions;

namespace Library.WebApi.Controllers
{
    public class RegistrationController : ApiController
    {
        private readonly IRegistrationService registrationService;
        public RegistrationController()
        {

        }
        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        //[Authorize(Roles = "User")]
        public async Task<HttpResponseMessage> RegisterAsync([FromBody] UserRest userRest)
        {
            User user = new User();
            try
            {
                user = SetUserFromRest(userRest);
                await registrationService.RegisterUserAsync(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }
        
        public async Task<HttpResponseMessage> LoginAsync(string email, string password)
        {
            try
            {
                User user = await registrationService.LoginUserAsync(email, password);
               

                if (user != null)
                {
                    string role = GetRoleNameFromId(user.RoleId);

                    var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    identity.AddClaim(new Claim("Email", user.Email));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    // Create an authentication ticket with the identity
                    var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());

                    // Generate the access token
                    var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
                    // Return the access token
                    LoginReturnRest returnRest = new LoginReturnRest();
                    returnRest.Role = role;
                    returnRest.AccessToken = accessToken;
                    return Request.CreateResponse(HttpStatusCode.OK, returnRest);
                }
                else
                {
                
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid email or password.");
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred during login.");
            }
        }


        private User SetUserFromRest(UserRest userRest)
        {
            User user = new User
            {
                FirstName = userRest.FirstName,
                LastName = userRest.LastName,
                PhoneNumber = userRest.PhoneNumber,
                DateOfBirth = userRest.DateOfBirth,
                Email = userRest.Email,
                Password = userRest.Password,
            };
           

            return user;
        }
        public string GetRoleNameFromId(Guid roleId)
        {

            var roles = new Dictionary<Guid, string>
            {
                { new Guid("c72d4c65-4d08-49ab-84e1-6cb3341f8bb6"), "User" },
                { new Guid("9ea84fc7-89c2-4067-b8b6-5df728d5f64c"), "Admin" }
            };

            // Return the role name associated with the role ID, or an empty string if not found
            return roles.ContainsKey(roleId) ? roles[roleId] : string.Empty;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/logout")]
        public HttpResponseMessage Logout()
        {
            try
            {
HttpContext.Current.GetOwinContext().Authentication.SignOut();
            return Request.CreateResponse(HttpStatusCode.OK, "Logged out successfully.");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
            
                
        }

    }
}