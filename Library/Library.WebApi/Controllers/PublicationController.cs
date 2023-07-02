using Library.Common;
using Library.Model;
using Library.Service;
using Library.Service.Common;
using Library.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.WebApi.Controllers
{
    [RoutePrefix("api/library")]
    public class PublicationController : ApiController
    {
        protected IPublicationService PublicationService { get; set; }

        public PublicationController(IPublicationService publicationService)
        {
            PublicationService = publicationService;

        }
        [Route("publication/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetPublicationByIdAsync(Guid id)
        {
            PublicationWithAuthorIds publication = await PublicationService.GetPublicationByIdAsync(id);

            if (publication == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Publication with Id {id} not found.");
            }

        

            return Request.CreateResponse(HttpStatusCode.OK, publication);
        }

        [Route("add/publication")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddPublicationAsync([FromBody] PublicationStringAuthorIdsRest publicationRest)
        {
            if (publicationRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Invalid publication.");
            }

            Publication publication = new Publication()
            {
                Id = Guid.NewGuid(),
                Title = publicationRest.Title,
                Description = publicationRest.Title,
                Edition = publicationRest.Edition,
                DatePublished = publicationRest.DatePublished,
                Quantity = publicationRest.Quantity,
                NumberOfPages = publicationRest.NumberOfPages,
                Language = publicationRest.Language,
                TypeId = publicationRest.TypeId,
                GenreId = publicationRest.GenreId,
                PublisherId = publicationRest.PublisherId,
                IsActive = true,
                CreatedByUserId = Guid.Parse("9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f"),
                //CreatedByUserId = User.Id,
                DateCreated = DateTime.Now
            };
            string joinedAuthorIds = publicationRest.ListOfAuthorIds;

            List<Guid> listOfAuthorIds = !string.IsNullOrEmpty(joinedAuthorIds) ? Helper.ToGuidList(joinedAuthorIds) : null;
            return Request.CreateResponse(HttpStatusCode.Created, await PublicationService.AddPublicationAsync(publication, listOfAuthorIds));
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> RemovePublicationAsync(Guid id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await PublicationService.RemovePublicationAsync(id));
        }


        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePublicationAsync(Guid id, [FromBody] PublicationRestId updatedPublicationRestId)
        {
            if (updatedPublicationRestId == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Invalid Publication.");
            }


            Publication updatedPublication = new Publication()
            {
                Id = updatedPublicationRestId.Id,
                Title = updatedPublicationRestId.Title,
                Description = updatedPublicationRestId.Description,
                Edition = updatedPublicationRestId.Edition,
                DatePublished = updatedPublicationRestId.DatePublished,
                Quantity = updatedPublicationRestId.Quantity,
                NumberOfPages = updatedPublicationRestId.NumberOfPages,
                Language = updatedPublicationRestId.Language,
                TypeId = updatedPublicationRestId.TypeId,
                GenreId = updatedPublicationRestId.GenreId,
                PublisherId = updatedPublicationRestId.PublisherId,
                IsActive = true,
                CreatedByUserId = Guid.Parse("9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f"),
                //CreatedByUserId = User.Id,
                DateCreated = DateTime.Now
               
            };

            PublicationWithAuthorIds newPublication =await PublicationService.UpdatePublicationAsync(id, updatedPublication);

            PublicationWithAuthorIdsRest publicationRest = new PublicationWithAuthorIdsRest()
            {
                Title = newPublication.Title,
                Description = newPublication.Description,
                Edition = newPublication.Edition,
                DatePublished = newPublication.DatePublished,
                Quantity = newPublication.Quantity,
                NumberOfPages = newPublication.NumberOfPages,
                Language = newPublication.Language,
                TypeId = newPublication.TypeId,
                GenreId = newPublication.GenreId,
                PublisherId = newPublication.PublisherId,
                ListOfAuthorIds = newPublication.ListOfAuthorIds
            };

            return Request.CreateResponse(HttpStatusCode.OK, publicationRest) ;

        }


        [HttpGet]

        [Route("home")]
        public async Task<HttpResponseMessage> GetAllPublicationsAsync(int pageSize = 10, int pageNumber = 1, string orderBy = "", string sortOrder = "asc",
            string searchQuery = null, string language = "", DateTime? minDatePublished = null, DateTime? maxDatePublished = null, int? minNumberOfPages = null,
            int? maxNumberOfPages = null,  Guid? typeId = null , Guid? genreId = null)
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

            PublicationFiltering filtering = new PublicationFiltering()
            {
                SearchQuery = searchQuery,
                MinDatePublished = minDatePublished,
                MaxDatePublished = maxDatePublished,
                MinNumberOfPages = minNumberOfPages,
                MaxNumberOfPages = maxNumberOfPages,
                Language = language,
                TypeId = typeId,
                GenreId = genreId
            };

            PagedList<PublicationWithAuthorIds> listOfPublications = await PublicationService.GetAllPublicationsAsync(filtering, sorting, paging);

            

            List<PublicationWithAuthorIdsRest> listOfMappedPublications = MapToPublicationRestList(listOfPublications);

        


            if (listOfMappedPublications != null)
            {
                var response = new
                {
                    NumberOfPages = listOfPublications.TotalPages,
                    ItemsPerPage = listOfPublications.PageSize,
                    Publications = listOfMappedPublications
                };
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);



        }



        public List<PublicationWithAuthorIdsRest> MapToPublicationRestList(List<PublicationWithAuthorIds> listOfPublications)
        {
            if (listOfPublications.Count > 0)
            {
                List<PublicationWithAuthorIdsRest> mappedPublications = new List<PublicationWithAuthorIdsRest>();
                foreach (PublicationWithAuthorIds publication in listOfPublications)
                {
                    PublicationWithAuthorIdsRest publicationRest = new PublicationWithAuthorIdsRest()
                    {
                        Id = publication.Id,
                        Title = publication.Title,
                        Description = publication.Description,
                        Edition = publication.Edition,
                        DatePublished = publication.DatePublished,
                        Quantity = publication.Quantity,
                        NumberOfPages = publication.NumberOfPages,
                        Language = publication.Language,
                        TypeId = publication.TypeId,
                        GenreId = publication.GenreId,
                        PublisherId = publication.PublisherId,
                        ListOfAuthorIds= publication.ListOfAuthorIds

                    };

                    mappedPublications.Add(publicationRest);
                }
                return mappedPublications;
            }
            return null;
        }

        
        [HttpGet]
          public async Task<HttpResponseMessage> GetNewestPublicationsAsync()
           {
            Paging paging = new Paging()
            {
                PageSize = 10,
                PageNumber = 1
            };

            Sorting sorting = new Sorting()
            {
                OrderBy = "DatePublished",
                SortOrder = "desc"
            };

            PublicationFiltering filtering = new PublicationFiltering()
            {
     
            };

            PagedList<PublicationWithAuthorIds> listOfPublications = await PublicationService.GetAllPublicationsAsync(filtering, sorting, paging);

            List<PublicationWithAuthorIdsRest> listOfMappedPublications = MapToPublicationRestList(listOfPublications);

            if (listOfMappedPublications != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, listOfMappedPublications);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }




       
    }
}