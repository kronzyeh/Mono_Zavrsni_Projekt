using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebApi.Models
{
    public class PublicationRest
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
        
    }
}