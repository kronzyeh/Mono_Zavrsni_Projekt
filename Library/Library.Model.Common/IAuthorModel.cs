using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Common
{
    public interface IAuthorModel
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Nationality { get; set; }
        DateTime DateOfBirth { get; set; }
        DateTime? DateOfDeath { get; set; }
        bool IsActive { get; set; }
        Guid CreatedByUserId { get; set; }
        DateTime DateCreated { get; set; }
        Guid? UpdatedByUserId { get; set; }
        DateTime? DateUpdated { get; set; }
    }
}
