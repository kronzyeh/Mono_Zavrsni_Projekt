using Library.Common;
using Library.Model;
using Library.Model.Common;
using Library.Repository.Common;
using MongoDB.Driver.Core.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        public async Task<int> AddReservationAsync([FromBody] Reservation reservation)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new NpgsqlConnection(Helper.connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Reservation\" (\"Id\", \"StartDate\", \"EndDate\", \"IsReturned\", \"PublicationId\", \"UserId\", \"IsActive\", \"DateUpdated\", \"DateCreated\", \"CreatedByUserId\", \"UpdatedByUserId\") VALUES (@Id, @StartDate, @EndDate, @IsReturned, @PublicationId, @UserId, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId)", connection))
                    {
                        command.Parameters.AddWithValue("@Id", reservation.Id);
                        command.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                        command.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                        command.Parameters.AddWithValue("@IsReturned", reservation.IsReturned);
                        command.Parameters.AddWithValue("@PublicationId", reservation.PublicationId);
                        command.Parameters.AddWithValue("@UserId", reservation.UserId);
                        command.Parameters.AddWithValue("@IsActive", reservation.IsActive);
                        command.Parameters.AddWithValue("@DateUpdated", reservation.DateUpdated);
                        command.Parameters.AddWithValue("@DateCreated", reservation.DateCreated);
                        command.Parameters.AddWithValue("@UpdatedByUserId", reservation.UpdatedByUserId);
                        command.Parameters.AddWithValue("@CreatedByUserId", reservation.CreatedByUserId);


                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }

            return rowsAffected;
        }

        public async Task<int> DeleteReservationAsync(Guid id)
        {
            int rowsAffected = 0;
            try
            {
                Reservation reservation = await GetReservationById(id);
                using (NpgsqlConnection connection = new NpgsqlConnection(Helper.connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("Delete From \"Reservation\" WHERE \"Id\" = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
            return rowsAffected;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync(Filter filter)
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(Helper.connectionString))
                {
                    connection.Open();

                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append("SELECT r.*, u.\"FirstName\", u.\"LastName\", p.\"Title\" FROM \"Reservation\" AS r ");
                    queryBuilder.Append("LEFT JOIN \"User\" AS u ON r.\"UserId\" = u.\"Id\" ");
                    queryBuilder.Append("LEFT JOIN \"Publication\" AS p ON r.\"PublicationId\" = p.\"Id\" ");

                    if (filter.ReservationUserRole == "User")
                    {
                        queryBuilder.Append("WHERE r.\"UserId\" = @UserId");
                    }

                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryBuilder.ToString(), connection))
                    {
                        if (filter.ReservationUserRole == "User")
                        {
                            cmd.Parameters.AddWithValue("@UserId", filter.ReservationUserId);
                        }

                        using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Reservation reservation = new Reservation();
                                reservation.Id = (Guid)reader["Id"];
                                reservation.StartDate = (DateTime)reader["StartDate"];
                                reservation.EndDate = (DateTime)reader["EndDate"];
                                reservation.IsReturned = (bool)reader["IsReturned"];
                                reservation.PublicationId = (Guid)reader["PublicationId"];
                                reservation.UserId = (Guid)reader["UserId"];
                                reservation.DateCreated = (DateTime)reader["DateCreated"];
                                reservation.DateUpdated = (DateTime)reader["DateUpdated"];
                                reservation.CreatedByUserId = (Guid)reader["CreatedByUserId"];
                                reservation.UpdatedByUserId = (Guid)reader["UpdatedByUserId"];
                                reservation.IsActive = (bool)reader["IsActive"];
                                reservation.PublicationTitle = (string)reader["Title"];
                                reservation.UserFirstName = (string)reader["FirstName"];
                                reservation.UserLastName = (string)reader["LastName"];

                                reservations.Add(reservation);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
            return reservations;
        }



        public async Task<Reservation> GetSpecificReservationAsync(Guid id)
        {
            Reservation reservation = new Reservation();
            try
            {
                reservation = await GetReservationById(id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
                throw;
            }
            return reservation;
        }

        public async Task<int> UpdateReservationAsync(Guid id)
        {
            int rowsAffected = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(Helper.connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {

                        StringBuilder updateQuery = new StringBuilder("UPDATE \"Reservation\"\r\n SET \"EndDate\" = \"EndDate\" + INTERVAL '7 days', \"DateUpdated\" = @DateUpdated, \"UpdatedByUserId\" = @UpdatedByUserId");
                        ClaimsIdentity identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
                        string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        command.Parameters.AddWithValue("DateUpdated", DateTime.Now);
                        command.Parameters.AddWithValue("UpdatedByUserId", Guid.Parse(userId));
                        updateQuery.Append(" WHERE \"Id\" = @Id");

                        string query = updateQuery.ToString();
                        command.CommandText = query;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("Id", @id);
                        rowsAffected = await command.ExecuteNonQueryAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw;
            }
            return rowsAffected;
        }

        public async Task<Reservation> GetReservationById(Guid id)
        {
            Reservation reservation = new Reservation();
            using (NpgsqlConnection connection = new NpgsqlConnection(Helper.connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("Select * From \"Reservation\" WHERE \"Id\" = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        reader.Read();

                        reservation.Id = (Guid)reader["Id"];
                        reservation.StartDate = (DateTime)reader["StartDate"];
                        reservation.EndDate = (DateTime)reader["EndDate"];
                        reservation.IsReturned = (bool)reader["IsReturned"];
                        reservation.PublicationId = (Guid)reader["PublicationId"];
                        reservation.UserId= (Guid)reader["UserId"];
                        reservation.DateCreated = (DateTime)reader["DateCreated"];
                        reservation.DateUpdated = (DateTime)reader["DateUpdated"];
                        reservation.CreatedByUserId = (Guid)reader["CreatedByUserId"];
                        reservation.UpdatedByUserId = (Guid)reader["UpdatedByUserId"];
                        reservation.IsActive = (bool)reader["IsActive"];
                        reservation.UserFirstName = (string)reader["FirstName"];
                        reservation.UserLastName = (string)reader["LastName"];
                    }
                }
            }
            return reservation;
        }
    }
}
