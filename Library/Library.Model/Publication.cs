using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Publication : IPublicationModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Edition { get; set; }
        public DateTime DatePublished { get; set; }
        public int Quantity { get; set; }
        public int NumberOfPages { get; set; }
        public string Language { get; set; }
        public Guid TypeId { get; set; }
        public Guid GenreId { get; set; }
        public Guid PublisherId { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid? UpdatedByUserId { get; set; } = null;
        public DateTime? DateUpdated { get; set; } = null;
    }
}
