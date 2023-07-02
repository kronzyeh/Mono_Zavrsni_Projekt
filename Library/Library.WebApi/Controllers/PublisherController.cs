using Library.Service.Common;
using Library.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.WebApi.Controllers
{
    public class PublisherController : ApiController
    {
        protected IPublisherService PublisherService { get; set; }

        public PublisherController(IPublisherService publisherService)
        {
            PublisherService = publisherService;
        }

        // POST api/publisher
        [HttpPost]
        public async Task<HttpResponseMessage> CreateGenreAsync([FromBody] PublisherRest publisherRest)
        {
            bool createdPublisher = await PublisherService.CreatePublisherAsync(publisherRest.Name, publisherRest.ContactNumber);
            if (createdPublisher)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

    }
}
