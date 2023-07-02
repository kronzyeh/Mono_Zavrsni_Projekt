using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebApi.Models
{
    public class SubscriptionHistoryRest
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
    }
}