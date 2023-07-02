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
    public class GenreController : ApiController
    {
        protected IGenreService GenreService { get; set; }

        public GenreController(IGenreService genreService)
        {
            GenreService = genreService;
        }

        // POST api/genre
        [HttpPost]
        public async Task<HttpResponseMessage> CreateGenreAsync([FromBody] GenreRest genreRest)
        {
            bool createdGenre = await GenreService.CreateGenreAsync(genreRest.Name);
            if (createdGenre)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

    }
}