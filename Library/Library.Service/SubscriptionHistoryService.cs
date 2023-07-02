using Library.Model;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore;
using System.Threading.Tasks;
using System.Web.Http;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Owin.Security.OAuth;
using System.Threading;

namespace Library.Service
{
    public class SubscriptionHistoryService : ISubscriptionHistoryService
    {
        
        private readonly ISubscriptionHistoryRepository subscriptionHistoryRepository;
   
        public SubscriptionHistoryService(ISubscriptionHistoryRepository subscriptionHistoryRepository)
        {
            this.subscriptionHistoryRepository = subscriptionHistoryRepository;
        }

        public async Task<int> AddSubscriptionAsync([FromBody] SubscriptionHistory subscription)
        {
            try
            {
                subscription = SetSubscriptionData(subscription);
                return await subscriptionHistoryRepository.AddSubscriptionAsync(subscription);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }



        private SubscriptionHistory SetSubscriptionData(SubscriptionHistory subscriptionHistory)
        {
            ClaimsIdentity identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            subscriptionHistory.Id = Guid.NewGuid();
            subscriptionHistory.EndDate = DateTime.Now + TimeSpan.FromDays(30);
            subscriptionHistory.IsActive = true;
            subscriptionHistory.DateCreated = DateTime.Now;
            subscriptionHistory.DateUpdated = DateTime.Now;
            subscriptionHistory.CreatedByUserId = Guid.Parse(userId);
            subscriptionHistory.UpdatedByUserId = Guid.Parse(userId);

            return subscriptionHistory;

        }

        public async Task<int> UpdateSubscriptionAsync(Guid id)
        {
            try
            {
                return await subscriptionHistoryRepository.UpdateSubscriptionAsync(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

    }
}
