using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Common
{
    public interface ISubscriptionHistory
    {
        Guid Id { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        Guid UserId { get; set; }
        bool IsActive { get; set; }
        Guid CreatedByUserId { get; set; }
        DateTime DateCreated { get; set; }
        Guid UpdatedByUserId { get; set; }
        DateTime DateUpdated { get; set; }
    }
}
