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
    public class TypeController : ApiController
    {
        protected ITypeService TypeService { get; set; }

        public TypeController(ITypeService typeService)
        {
            TypeService = typeService;
        }

        // POST api/type
        [HttpPost]
        public async Task<HttpResponseMessage> CreateTypeAsync([FromBody] TypeRest typeRest)
        {
            bool createdType = await TypeService.CreateTypeAsync(typeRest.Name);
            if (createdType)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}