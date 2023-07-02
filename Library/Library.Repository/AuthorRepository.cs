using Library.Common;
using Library.Model;
using Library.Repository.Common;
using MongoDB.Driver.Core.Configuration;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("select * from \"Author\" where \"Id\"=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Author author = new Author()
                        {
                            Id = reader.GetGuid(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Nationality = reader.GetString(3),
                            DateOfBirth = reader.GetDateTime(4),
                            DateOfDeath = reader.GetDateTime(5),
                            IsActive = reader.GetBoolean(6),
                            CreatedByUserId = reader.GetGuid(7),
                            DateCreated = reader.GetDateTime(8)
                        };
                        return author;
                    }
                }
                return null;
            }

        }

        public async Task<bool> AddAuthorAsync(Author author)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("insert into \"Author\" (\"Id\", \"FirstName\", \"LastName\", \"Nationality\", \"DateOfBirth\"," +
                    " \"DateOfDeath\", \"IsActive\", \"CreatedByUserId\", \"DateCreated\") values " +
                    "(@Id, @FirstName, @LastName, @Nationality, @DateOfBirth, @DateOfDeath, @IsActive, @CreatedByUserId, @DateCreated); ", conn);

                DateTime? dateOfDeathValue = author.DateOfDeath;
                object dateOfDeathParameter = dateOfDeathValue != null ? (object)dateOfDeathValue : DBNull.Value;


                cmd.Parameters.AddWithValue("@Id", author.Id);
                cmd.Parameters.AddWithValue("@FirstName", author.FirstName);
                cmd.Parameters.AddWithValue("@LastName", author.LastName);
                cmd.Parameters.AddWithValue("@Nationality", author.Nationality);
                cmd.Parameters.AddWithValue("@DateOfBirth", author.DateOfBirth);
                cmd.Parameters.AddWithValue("@DateOfDeath", dateOfDeathParameter);
                cmd.Parameters.AddWithValue("@IsActive", author.IsActive);
                cmd.Parameters.AddWithValue("@CreatedByUserId", author.CreatedByUserId);
                cmd.Parameters.AddWithValue("@DateCreated", author.DateCreated);


                conn.Open();

                int numberOfAffectedRows = await cmd.ExecuteNonQueryAsync();

                if (numberOfAffectedRows > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RemoveAuthorAsync(Guid id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("update \"Author\" set \"IsActive\" = @IsActive  where \"Id\" = @Id", conn);
                //deaktivirati i authorpublication unos
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@IsActive", false);

                conn.Open();

                int numberOfAffectedRows = await cmd.ExecuteNonQueryAsync();

                if (numberOfAffectedRows > 0)
                {
                    return true;
                }

                return false;
            }

        }

        public async Task<Author> UpdateAuthorAsync(Guid id, Author updatedAuthor)
        {
            try
            {

                Author currentAuthor = await GetAuthorByIdAsync(id);


                if (currentAuthor != null)
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
                    {
                        StringBuilder sb = new StringBuilder();

                        NpgsqlCommand cmd = new NpgsqlCommand();

                        cmd.Connection = conn;

                        sb.Append("update \"Author\" set ");
                        if (updatedAuthor.FirstName != null & currentAuthor.FirstName != updatedAuthor.FirstName)
                        {
                            sb.Append("\"FirstName\" = @FirstName, ");
                            cmd.Parameters.AddWithValue("@FirstName", updatedAuthor.FirstName);
                        }
                        if (updatedAuthor.LastName != null & currentAuthor.LastName != updatedAuthor.LastName)
                        {
                            sb.Append("\"LastName\" = @LastName, ");
                            cmd.Parameters.AddWithValue("@LastName", updatedAuthor.LastName);
                        }
                        if (updatedAuthor.Nationality != null & currentAuthor.Nationality != updatedAuthor.Nationality)
                        {
                            sb.Append("\"Nationality\" = @Nationality, ");
                            cmd.Parameters.AddWithValue("@Nationality", updatedAuthor.Nationality);
                        }
                        if (updatedAuthor.DateOfBirth != DateTime.MinValue & currentAuthor.DateOfBirth != updatedAuthor.DateOfBirth)
                        {
                            sb.Append("\"DateOfBirth\" = @DateOfBirth, ");
                            cmd.Parameters.AddWithValue("@DateOfBirth", updatedAuthor.DateOfBirth);
                        }

                        if (updatedAuthor.DateOfDeath != null & currentAuthor.DateOfDeath != updatedAuthor.DateOfDeath)
                        {
                            sb.Append("\"DateOfDeath\" = @DateOfDeath, ");
                            cmd.Parameters.AddWithValue("@DateOfDeath", updatedAuthor.DateOfDeath);
                        }

                        sb.Append("\"UpdatedByUserId\" = @UpdatedByUserId, ");
                        cmd.Parameters.AddWithValue("@UpdatedByUserId", updatedAuthor.UpdatedByUserId);
                        sb.Append("\"DateUpdated\" = @DateUpdated, ");
                        cmd.Parameters.AddWithValue("@DateUpdated", updatedAuthor.DateUpdated);

                        sb.Remove(sb.Length - 2, 2);

                        sb.Append(" where \"Id\" = @Id;");
                        cmd.Parameters.AddWithValue("@Id", id);

                        conn.Open();

                        cmd.CommandText = sb.ToString();

                        int numberOfAffectedRows = await cmd.ExecuteNonQueryAsync();
                        if (numberOfAffectedRows > 0)
                        {
                            return await GetAuthorByIdAsync(id);
                        }
                    }
                }
                return null;

            }
            catch (Exception)
            {
                return null;
            }

        }


        public async Task<PagedList<Author>> GetAllAuthorsAsync(AuthorFiltering filtering, Paging paging, Sorting sorting)
        {
            int numberOfRows = 0;

            List<Author> listOfAuthors = new List<Author>();

            List<string> allowedSortColumns = new List<string> { "firstname", "lastname", "nationality", "dateofbirth", "dateofdeath" };
            try
            {
                
                using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
                { 
                    
                    StringBuilder queryStringBuilder = new StringBuilder("select * from \"Author\" ");
                    StringBuilder countStringBuilder = new StringBuilder("select count (*) from \"Author\" ");

                    queryStringBuilder = AuthorFilterResults(queryStringBuilder, filtering);
                    countStringBuilder = AuthorFilterResults(countStringBuilder, filtering);


                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryStringBuilder.ToString(), conn))
                    {
                        if (sorting != null)
                        {
                            if (allowedSortColumns.Contains(sorting.OrderBy.ToLower()))
                            {
                                queryStringBuilder.Append($" order by \"{sorting.OrderBy}\" ");
                            }
                            if ((sorting.SortOrder.ToLower() == "asc") || (sorting.SortOrder.ToLower() == "desc"))
                            {
                                queryStringBuilder.Append($" {sorting.SortOrder} ");
                            }
                        }
                        if (paging != null)
                        {
                            queryStringBuilder.Append(" offset @Offset rows \r\n fetch next @PageSize rows only");

                            cmd.Parameters.AddWithValue("@Offset", (paging.PageNumber - 1) * paging.PageSize);
                            cmd.Parameters.AddWithValue("@PageSize", paging.PageSize);

                        }

                        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Author author = new Author
                                {
                                    Id = reader.GetGuid(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Nationality = reader.GetString(3),
                                    DateOfBirth = reader.GetDateTime(4),
                                    DateOfDeath = reader.GetDateTime(5),
                                    IsActive = reader.GetBoolean(6),
                                    CreatedByUserId = reader.GetGuid(7),
                                    DateCreated = reader.GetDateTime(8)
                                };

                                listOfAuthors.Add(author);
                            }
                        }
                    }
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(countStringBuilder.ToString(), conn))
                    {
                        numberOfRows = Convert.ToInt16(await cmd.ExecuteScalarAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }

            return new PagedList<Author>(listOfAuthors, numberOfRows, paging.PageNumber, paging.PageSize);
        }

        private StringBuilder AuthorFilterResults(StringBuilder filteringStringBuilder, AuthorFiltering filtering)
        {

            List<string> listOfNationalities = new List<string>{"english", "british", "nigerian", "colombian", "czech", "american", "scottish",
                "french", "russian", "bosnian"};

            filteringStringBuilder.Append("WHERE 1 = 1");

            if (filtering != null)
            {

                if (filtering.SearchQuery != null)
                {
                    filteringStringBuilder.Append($" and ((\"FirstName\" like {filtering.SearchQuery}) or (\"LastName\" like {filtering.SearchQuery}) or " +
                        $"(\"DateOfBirth\" like {filtering.SearchQuery}) or (\"DateOfDeath\" like {filtering.SearchQuery}) or (\"Nationality\" like {filtering.SearchQuery})) ");
                }
                if (filtering.MinDateOfBirth != null)
                {
                    filteringStringBuilder.Append($" and \"MinDateOfBirth\" >= {filtering.MinDateOfBirth} ");
                }
                if (filtering.MaxDateOfBirth != null)
                {
                    filteringStringBuilder.Append($" and \"MaxDateOfBirth\" <= {filtering.MaxDateOfBirth} ");
                }
                if (listOfNationalities.Contains(filtering.Nationality.ToLower()) && filtering.Nationality!=null)
                {
                    filteringStringBuilder.Append($" and \"Nationality\" = \'{filtering.Nationality}\' ");
                }

            }
            return filteringStringBuilder;
        }
    }
}
