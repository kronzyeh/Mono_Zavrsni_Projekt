using Library.Repository.Common;
using MongoDB.Driver.Core.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.Common;
using Library.Model;

namespace Library.Repository
{
    public class GenreRepository : IGenreRepository
    {
        public async Task<bool> CreateGenreAsync(Genre genre)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("insert into \"Genre\" (\"Id\", \"Name\",  \"IsActive\", \"CreatedByUserId\", \"DateCreated\", \"UpdatedByUserId\", \"DateUpdated\" ) " +
                        "values (@Id, @Name, @IsActive, @CreatedByUserId, @DateCreated, @UpdatedByUserId, @DateUpdated);", conn);
                    if (genre != null)
                    {
                        Guid id = Guid.NewGuid();
                        cmd.Parameters.AddWithValue("@Id", genre.Id);
                        cmd.Parameters.AddWithValue("@Name", genre.Name);
                        cmd.Parameters.AddWithValue("@IsActive", genre.IsActive);
                        cmd.Parameters.AddWithValue("@CreatedByUserId", genre.CreatedByUserId);
                        cmd.Parameters.AddWithValue("@DateCreated", genre.DateCreated);
                        cmd.Parameters.AddWithValue("@UpdatedByUserId", genre.UpdatedByUserId);
                        cmd.Parameters.AddWithValue("@DateUpdated", genre.DateUpdated);

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
