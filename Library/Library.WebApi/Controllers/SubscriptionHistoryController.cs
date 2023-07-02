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
    
    public class SubscriptionHistoryController : ApiController
    {
        private readonly ISubscriptionHistoryService subscriptionHistoryService;
        public SubscriptionHistoryController(ISubscriptionHistoryService subscriptionHistoryService)
        {
            this.subscriptionHistoryService = subscriptionHistoryService;
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody] SubscriptionHistoryRest subscriptionRest)
        {
            SubscriptionHistory subscription = new SubscriptionHistory();
            int rowsAffected;
            try
            {
                subscription = SetModelFromRest(subscriptionRest);
                rowsAffected = await subscriptionHistoryService.AddSubscriptionAsync(subscription);
                return Request.CreateResponse(HttpStatusCode.OK, rowsAffected);

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateSubscriptionAsync(Guid id)
        {
            SubscriptionHistory subscription = new SubscriptionHistory();
            int rowsAffected;
            try
            {
                rowsAffected = await subscriptionHistoryService.UpdateSubscriptionAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, rowsAffected);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private SubscriptionHistory SetModelFromRest(SubscriptionHistoryRest subscriptionRest)
        {
            SubscriptionHistory subscription = new SubscriptionHistory
            {
                StartDate = subscriptionRest.StartDate,
                UserId = subscriptionRest.UserId,
            };

            return subscription;
        }
    }
}