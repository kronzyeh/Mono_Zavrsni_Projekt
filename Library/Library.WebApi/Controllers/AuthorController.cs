using Auth0.ManagementApi.Paging;
using Library.Common;
using Library.Model;
using Library.Service;
using Library.Service.Common;
using Library.WebApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Razor.Generator;

namespace Library.WebApi.Controllers
{
    [RoutePrefix("api/library")]
    public class AuthorController : ApiController
    {
        protected IAuthorService AuthorService { get; set; }

        public AuthorController(IAuthorService authorService)
        {
            AuthorService = authorService;
        }

        private async Task<Author> GetAuthorAsync(Guid id)
        {
            Author author = await AuthorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return null;
            }

            return author;
        }



        [HttpGet]
        [Route("author/{id}")]
       public async Task<HttpResponseMessage> GetAuthorByIdAsync(Guid id)
        {
            //Svi

            Author author = await AuthorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Passenger with Id {id} not found.");
            }

            AuthorRestId authorRestId = new AuthorRestId()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Nationality = author.Nationality,
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath
            };

            return Request.CreateResponse(HttpStatusCode.OK, authorRestId);
        }

        [HttpPost]

        [Route("add/author")]
        public async Task<HttpResponseMessage> AddAuthorAsync([FromBody]AuthorRest authorRest)
        {
            //Admin
            try
            {
                if (authorRest == null)
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Invalid author.");

                            }

                            string FirstName = authorRest.FirstName;
                            string LastName = authorRest.LastName;
                            string Nationality = authorRest.Nationality;
                            DateTime DateOfBirth = authorRest.DateOfBirth;
                            DateTime? DateOfDeath = authorRest.DateOfDeath;

                            if (DateOfDeath == DateTime.MinValue)
                            {
                                DateOfDeath = null;
                            }


                            return Request.CreateResponse(HttpStatusCode.OK, await AuthorService.AddAuthorAsync(FirstName, LastName, Nationality, DateOfBirth, DateOfDeath));
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            

        }

        
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveAuthorAsync(Guid id)
        {
            //Admin

            return  Request.CreateResponse(HttpStatusCode.OK, await AuthorService.RemoveAuthorAsync(id));

        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAuthorAsync(Guid id, AuthorRest updatedAuthorRest)
        {
           
            if (updatedAuthorRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Invalid author.");
            }


            Author updatedAuthor = new Author()
            {
                Id = Guid.NewGuid(),
                FirstName = updatedAuthorRest.FirstName,
                LastName = updatedAuthorRest.LastName,
                Nationality = updatedAuthorRest.Nationality,
                DateOfBirth = updatedAuthorRest.DateOfBirth,
                DateOfDeath = updatedAuthorRest.DateOfDeath,
                IsActive = false,
                CreatedByUserId = Guid.Parse("9c34cf9a-c6a7-427b-abd3-b54a279b0d78"),
                DateCreated = DateTime.Now

            };
            
            return Request.CreateResponse(HttpStatusCode.OK, await AuthorService.UpdateAuthorAsync(id, updatedAuthor));


        }



        
        [HttpGet]
        [Route("authors")]
        public async Task<HttpResponseMessage> GetAllAuthorsAsync(int pageSize = 10, int pageNumber = 1, string orderBy = "FirstName", string sortOrder = "asc",
            string searchQuery = null, string nationality = "", DateTime? minDateOfBirth = null, DateTime? maxDateOfBirth = null)
        {

            Paging paging = new Paging()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            Sorting sorting = new Sorting()
            {
                OrderBy = orderBy,
                SortOrder = sortOrder
            };

            AuthorFiltering filtering = new AuthorFiltering()
            {
                SearchQuery = searchQuery,
                MinDateOfBirth = minDateOfBirth,
                MaxDateOfBirth = maxDateOfBirth,
                Nationality= nationality
                
            };

            Common.PagedList<Author> listOfAuthors = await AuthorService.GetAllAuthorsAsync(filtering, paging, sorting);

            List<AuthorRestId> listOfMappedAuthors = MapToAuthorRestList(listOfAuthors);

            if (listOfMappedAuthors != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, listOfMappedAuthors);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);


        }

        public List<AuthorRestId> MapToAuthorRestList(List<Author> listOfAuthors)
        {
            if (listOfAuthors.Count > 0)
            {
                List<AuthorRestId> mappedAuthors = new List<AuthorRestId>();
                foreach (Author author in listOfAuthors)
                {
                    AuthorRestId authorRest = new AuthorRestId()
                    {
                        Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        Nationality = author.Nationality,
                        DateOfBirth = author.DateOfBirth,
                        DateOfDeath = author.DateOfDeath
                    };
                    mappedAuthors.Add(authorRest);
                }
                return mappedAuthors;
            }
            return null;
        }




    }
}