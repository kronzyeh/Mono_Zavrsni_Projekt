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
    public class TypeRepository : ITypeRepository
    {
        public async Task<bool> CreateTypeAsync(Model.Type type)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("insert into \"Type\" (\"Id\", \"Name\",  \"IsActive\", \"CreatedByUserId\", \"DateCreated\", \"UpdatedByUserId\", \"DateUpdated\" ) " +
                        "values (@Id, @Name, @IsActive, @CreatedByUserId, @DateCreated, @UpdatedByUserId, @DateUpdated);", conn);
                    if (type != null)
                    {
                        Guid id = Guid.NewGuid();
                        cmd.Parameters.AddWithValue("@Id", type.Id);
                        cmd.Parameters.AddWithValue("@Name", type.Name);
                        cmd.Parameters.AddWithValue("@IsActive", type.IsActive);
                        cmd.Parameters.AddWithValue("@CreatedByUserId", type.CreatedByUserId);
                        cmd.Parameters.AddWithValue("@DateCreated", type.DateCreated);
                        cmd.Parameters.AddWithValue("@UpdatedByUserId", type.UpdatedByUserId);
                        cmd.Parameters.AddWithValue("@DateUpdated", type.DateUpdated);

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
