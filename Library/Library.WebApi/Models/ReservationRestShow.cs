using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebApi.Models
{
    public class ReservationRestShow
    {
        public Guid Id { get; set; }
        public string PublicationTitle { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public bool IsReturned { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}