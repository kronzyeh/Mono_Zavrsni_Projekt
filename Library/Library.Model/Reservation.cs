using Library.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Reservation : IReservation
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsReturned { get; set; }
        public Guid PublicationId { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTime DateUpdated { get; set; }
        public string PublicationTitle { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}
