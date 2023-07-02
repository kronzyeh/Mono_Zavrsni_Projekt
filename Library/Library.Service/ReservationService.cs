using Library.Common;
using Library.Model;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Service
{
    public class ReservationService : IReservationService
    {
        private IReservationRepository reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }
        public async Task<int> AddReservationAsync([FromBody] Reservation reservation)
        {
            try
            {
                reservation = SetReservationData(reservation);
                return await reservationRepository.AddReservationAsync(reservation);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<int> DeleteReservationAsync(Guid id)
        {
            try
            {
                return await reservationRepository.DeleteReservationAsync(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<List<Reservation>> GetAllReservationsAsync(Filter filter)
        {
            try
            {
                ClaimsIdentity identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
                string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string role = identity.FindFirst(ClaimTypes.Role)?.Value;
                filter.ReservationUserRole = role;

                if (role == "User")
                {
                    filter.ReservationUserId = Guid.Parse(userId);
                }

                return await reservationRepository.GetAllReservationsAsync(filter);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<Reservation> GetSpecificReservationAsync(Guid id)
        {
            try
            {
                return await reservationRepository.GetSpecificReservationAsync(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<int> UpdateReservationAsync(Guid id)
        {
            try
            {
                return await reservationRepository.UpdateReservationAsync(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        private Reservation SetReservationData(Reservation reservation)
        {
            ClaimsIdentity identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            reservation.Id = Guid.NewGuid();
            reservation.StartDate = DateTime.Now;
            reservation.EndDate = DateTime.Now + TimeSpan.FromDays(14);
            reservation.IsActive = true;
            reservation.DateCreated = DateTime.Now;
            reservation.DateUpdated = DateTime.Now;
            reservation.CreatedByUserId = Guid.Parse(userId);
            reservation.UpdatedByUserId = Guid.Parse(userId);
            reservation.IsReturned = false;

            return reservation;
        }
    }
}
