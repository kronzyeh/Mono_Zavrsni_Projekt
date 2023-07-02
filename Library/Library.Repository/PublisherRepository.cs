using Library.Common;
using Library.Model;
using Library.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        public async Task<bool> CreatePublisherAsync(Publisher publisher)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("insert into \"Type\" (\"Id\", \"Name\", \"ContactNumber\",  \"IsActive\", \"CreatedByUserId\", \"DateCreated\", \"UpdatedByUserId\", \"DateUpdated\" ) " +
                        "values (@Id, @Name, @ContactNumber, @IsActive, @CreatedByUserId, @DateCreated, @UpdatedByUserId, @DateUpdated);", conn);
                    if (publisher != null)
                    {
                        Guid id = Guid.NewGuid();
                        cmd.Parameters.AddWithValue("@Id", publisher.Id);
                        cmd.Parameters.AddWithValue("@Name", publisher.Name);
                        cmd.Parameters.AddWithValue("@ContactNumber", publisher.ContactNumber);
                        cmd.Parameters.AddWithValue("@IsActive", publisher.IsActive);
                        cmd.Parameters.AddWithValue("@CreatedByUserId", publisher.CreatedByUserId);
                        cmd.Parameters.AddWithValue("@DateCreated", publisher.DateCreated);
                        cmd.Parameters.AddWithValue("@UpdatedByUserId", publisher.UpdatedByUserId);
                        cmd.Parameters.AddWithValue("@DateUpdated", publisher.DateUpdated);

                        conn.Open();

                        int numberOfAffectedRows = await cmd.ExecuteNonQueryAsync();

                        if (numberOfAffectedRows > 0)
                        {
                            return true;
                        }
                    }
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
