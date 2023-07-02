using Library.Model;
using Library.Repository.Common;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.WebApi.Providers
{
    public class RegistrationServerProvider : OAuthAuthorizationServerProvider
    {
       
        private readonly IRegistrationRepository _repo;

        public RegistrationServerProvider(IRegistrationRepository repo)
        {
            _repo = repo;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            User user = await _repo.LoginAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, GetRoleNameFromId(user.RoleId)));
            identity.AddClaim(new Claim("Email", user.Email));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            context.Validated(ticket);
        }

        public string GetRoleNameFromId(Guid roleId)
        {
            
            var roles = new Dictionary<Guid, string>
            {
                { new Guid("c72d4c65-4d08-49ab-84e1-6cb3341f8bb6"), "User" },
                { new Guid("9ea84fc7-89c2-4067-b8b6-5df728d5f64c"), "Admin" }
            };

                
                return roles.ContainsKey(roleId) ? roles[roleId] : string.Empty;
        }

    }
}
