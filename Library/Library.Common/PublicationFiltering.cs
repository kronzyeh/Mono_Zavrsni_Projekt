using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class PublicationFiltering
    {
       public string SearchQuery { get; set; }
       public DateTime? MinDatePublished { get; set; }
       public DateTime? MaxDatePublished { get; set; }
       public int? MinNumberOfPages { get; set; }
       public int? MaxNumberOfPages { get; set; }
       public string Language { get; set; }
       public Guid? TypeId { get; set; }
       public Guid? GenreId { get; set; }
    }
}
