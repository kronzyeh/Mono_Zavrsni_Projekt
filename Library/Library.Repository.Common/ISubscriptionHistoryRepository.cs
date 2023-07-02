using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Repository.Common
{
    public interface ISubscriptionHistoryRepository
    {
        Task<int> AddSubscriptionAsync([FromBody] SubscriptionHistory history);
        Task<int> UpdateSubscriptionAsync(Guid id);
    }
}
