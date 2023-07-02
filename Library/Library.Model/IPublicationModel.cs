using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class IPublicationModel
    {
        Guid Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        int Edition { get; set; }
        DateTime DatePublished { get; set; }
        int Quantity { get; set; }
        int NumberOfPages { get; set; }
        string Language { get; set; }
        Guid TypeId { get; set; }
        Guid GenreId { get; set; }
        Guid PublisherId { get; set; }
        bool IsActive { get; set; }
        Guid CreatedByUserId { get; set; }
        DateTime DateCreated { get; set; }
        Guid? UpdatedByUserId { get; set; } = null;
        DateTime? DateUpdated { get; set; } = null;
    }
}
