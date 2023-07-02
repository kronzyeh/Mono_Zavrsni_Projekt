using Library.Common;
using Library.Model;
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
    public class SubscriptionHistoryRepository : ISubscriptionHistoryRepository
    {
        public async Task<int> AddSubscriptionAsync([FromBody] SubscriptionHistory subscriptionHistory)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new NpgsqlConnection(Helper.connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"SubscriptionHistory\" (\"Id\", \"StartDate\", \"EndDate\", \"UserId\", \"IsActive\", \"DateUpdated\", \"DateCreated\", \"CreatedByUserId\", \"UpdatedByUserId\") VALUES (@Id, @StartDate, @EndDate, @UserId, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId)", connection))
                    {
                        command.Parameters.AddWithValue("@Id", subscriptionHistory.Id);
                        command.Parameters.AddWithValue("@StartDate", subscriptionHistory.StartDate);
                        command.Parameters.AddWithValue("@EndDate", subscriptionHistory.EndDate);
                        command.Parameters.AddWithValue("@UserId", subscriptionHistory.UserId);
                        command.Parameters.AddWithValue("@IsActive", subscriptionHistory.IsActive);
                        command.Parameters.AddWithValue("@DateUpdated", subscriptionHistory.DateUpdated);
                        command.Parameters.AddWithValue("@DateCreated", subscriptionHistory.DateCreated);
                        command.Parameters.AddWithValue("@UpdatedByUserId", subscriptionHistory.UpdatedByUserId);
                        command.Parameters.AddWithValue("@CreatedByUserId", subscriptionHistory.CreatedByUserId);


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

        public async Task<int> UpdateSubscriptionAsync(Guid id)
        {
            int rowsAffected = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(Helper.connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {

                        StringBuilder updateQuery = new StringBuilder("UPDATE \"SubscriptionHistory\"\r\n SET \"EndDate\" = \"EndDate\" + INTERVAL '30 days', \"DateUpdated\" = @DateUpdated, \"UpdatedByUserId\" = @UpdatedByUserId");
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
    }
}
