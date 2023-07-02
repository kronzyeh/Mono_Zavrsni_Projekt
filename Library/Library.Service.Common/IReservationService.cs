using Library.Common;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Service.Common
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllReservationsAsync(Filter filter);
        Task<Reservation> GetSpecificReservationAsync(Guid id);
        Task<int> AddReservationAsync([FromBody] Reservation reservation);
        Task<int> UpdateReservationAsync(Guid id);
        Task<int> DeleteReservationAsync(Guid id);
    }
}
