using Library.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Author : IAuthorModel
    {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Nationality { get; set; }
            public DateTime DateOfBirth { get; set; }
            public DateTime? DateOfDeath { get; set; } = null;
            public bool IsActive { get; set; }
            public Guid CreatedByUserId { get; set; }
            public DateTime DateCreated { get; set; }
            public Guid? UpdatedByUserId { get; set; } = null;
            public DateTime? DateUpdated { get; set; } = null;

    }
}
