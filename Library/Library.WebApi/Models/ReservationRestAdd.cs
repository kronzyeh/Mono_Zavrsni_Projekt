using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebApi.Models
{
    public class ReservationRestAdd
    {
        public Guid Id { get; set; }
        public Guid PublicationId { get; set; }
        public Guid UserId { get; set; }
    }
}