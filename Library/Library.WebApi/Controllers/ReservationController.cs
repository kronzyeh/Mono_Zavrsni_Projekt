using Library.Common;
using Library.Model;
using Library.Service;
using Library.Service.Common;
using Library.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.WebApi.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly IReservationService reservationService;
        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllReservationsAsync(Guid? Id = null)
        {
            Filter filter = new Filter();
            

            List<Reservation> reservations = new List<Reservation>();
            List<ReservationRestShow> reservationsRest = new List<ReservationRestShow>();
            try
            {

                reservations = await reservationService.GetAllReservationsAsync(filter);
                reservationsRest = SetModelToRest(reservations);
                return Request.CreateResponse(HttpStatusCode.OK, reservationsRest);
            }
            catch (Exception ex)
            {

                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
        }

        
        [HttpGet]
        public async Task<HttpResponseMessage> GetSpecificReservationAsync(Guid id)
        {
            Reservation reservation = new Reservation();
            ReservationRestShow reservationRestShow = new ReservationRestShow();
            try
            {
                reservation = await reservationService.GetSpecificReservationAsync(id);
                List<Reservation> reservations = new List<Reservation>{
                    reservation
                };
                List<ReservationRestShow> restsShow = new List<ReservationRestShow>();

                restsShow = SetModelToRest(reservations);
                return Request.CreateResponse(HttpStatusCode.OK, restsShow);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        
        [HttpPost]
        public async Task<HttpResponseMessage> AddReservationAsync([FromBody] ReservationRestAdd reservationAdd)
        {
            Reservation reservation = new Reservation();
            int rowsAffected;
            try
            {
                reservation = SetModelFromRest(reservationAdd);
                rowsAffected = await reservationService.AddReservationAsync(reservation);
                return Request.CreateResponse(HttpStatusCode.OK, rowsAffected);

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateReservationAsync(Guid id)
        {
            Reservation reservation = new Reservation();
            int rowsAffected;
            try
            {
                rowsAffected = await reservationService.UpdateReservationAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, rowsAffected);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteReservationAsync(Guid id)
        {
            int rowsAffected;
            try
            {
                rowsAffected = await reservationService.DeleteReservationAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, rowsAffected);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private List<ReservationRestShow> SetModelToRest(List<Reservation> reservations)
        {
            List<ReservationRestShow> restReservations = new List<ReservationRestShow>();

            foreach (Reservation reservation in reservations)
            {
                ReservationRestShow reservationRestShow = new ReservationRestShow();
                reservationRestShow.Id = reservation.Id;
                reservationRestShow.StartDate = reservation.StartDate;
                reservationRestShow.EndDate = reservation.EndDate;
                reservationRestShow.IsReturned = reservation.IsReturned;
                reservationRestShow.UserFirstName = reservation.UserFirstName;
                reservationRestShow.UserLastName = reservation.UserLastName;
                reservationRestShow.PublicationTitle = reservation.PublicationTitle;

                restReservations.Add(reservationRestShow);
                
            }
            return restReservations;

        }

        private Reservation SetModelFromRest(ReservationRestAdd reservationAdd)
        {
            Reservation reservation = new Reservation
            {
                Id = reservationAdd.Id,
                UserId = reservationAdd.UserId,
                PublicationId = reservationAdd.PublicationId,
            };

            return reservation;
        }
    }
}