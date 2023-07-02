using Library.Model;
using Library.Service.Common;
using Library.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET api/<controller>
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            List<UserRest> usersRest = new List<UserRest>();
            try
            {

                users = await userService.GetUsersAsync();
                usersRest = SetModelToRest(users);
                return Request.CreateResponse(HttpStatusCode.OK, usersRest);
            }
            catch (Exception ex)
            {

                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
        }

        // GET api/<controller>/5
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> GetSpecificUser(Guid id)
        {
            User user = new User();
            UserRest userRest = new UserRest();
            try
            {
                user = await userService.GetSpecificUserAsync(id);
                List<User> users = new List<User>{
                    user
                };
                List<UserRest> usersRest = new List<UserRest>();

                usersRest = SetModelToRest(users);
                return Request.CreateResponse(HttpStatusCode.OK, usersRest);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        // PUT api/<controller>/5
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> UpdateUserAsync(Guid id, [FromBody] UserRest userRest)
        {
            User user = new User();
            int rowsAffected;
            try
            {
                user = SetModelFromRest(userRest);
                rowsAffected = await userService.UpdateUserAsync(id, user);
                return Request.CreateResponse(HttpStatusCode.OK, rowsAffected);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> DeleteUserAsync (Guid id)
        {
            int rowsAffected;
            try
            {
                rowsAffected = await userService.DeleteUserAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, rowsAffected);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private List<UserRest> SetModelToRest(List<User> users)
        {
            List<UserRest> restUsers = new List<UserRest>();

            foreach (User user in users)
            {
                
                UserRest userRest = new UserRest();
                userRest.Id = user.Id;
                userRest.FirstName = user.FirstName;
                userRest.LastName = user.LastName;
                userRest.Email = user.Email;
                userRest.PhoneNumber = user.PhoneNumber;
                userRest.DateOfBirth = user.DateOfBirth;
                userRest.StartDate = user.StartDate;
                userRest.EndDate = user.EndDate;
                restUsers.Add(userRest);
            }
            return restUsers;

        }
        private User SetModelFromRest(UserRest userRest)
        {
            User user = new User
            {
                Id = userRest.Id,
                FirstName = userRest.FirstName,
                LastName = userRest.LastName,
                Email = userRest.Email,
                PhoneNumber = userRest.PhoneNumber,
                DateOfBirth = userRest.DateOfBirth,

            };

            return user;
        }
    }
}