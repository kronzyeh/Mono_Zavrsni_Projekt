using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Service.Common
{
    public interface ISubscriptionHistoryService
    {
        Task<int> AddSubscriptionAsync([FromBody] SubscriptionHistory subscription);
        Task<int> UpdateSubscriptionAsync(Guid id);

    }
}
