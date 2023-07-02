using Library.Common;
using Library.Model;
using Library.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Library.Repository
{
    public class PublicationRepository : IPublicationRepository
    {

        public async Task<PublicationWithAuthorIds> GetPublicationByIdAsync(Guid id)
       {
            using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("select \"Title\",\"Description\",\"Edition\",\"DatePublished\",\"Quantity\",\"NumberOfPages\"," +
                        "\"Language\",\"TypeId\",\"GenreId\",\"PublisherId\" from \"Publication\"   where \"Id\" = @Id  ", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        PublicationWithAuthorIds publication = new PublicationWithAuthorIds
                        {
                            Id = id,
                            Title = reader.GetString(0),
                            Description = reader.GetString(1),
                            Edition = reader.GetInt16(2),
                            DatePublished = reader.GetDateTime(3),
                            Quantity = reader.GetInt16(4),
                            NumberOfPages = reader.GetInt16(5),
                            Language = reader.GetString(6),
                            TypeId = reader.GetGuid(7),
                            GenreId = reader.GetGuid(8),
                            PublisherId = reader.GetGuid(9),
                            ListOfAuthorIds = await GetPublicationAuthorIds(id)

                        };

                        return publication;
                    }
                }
                return null;
            }

        }

        public async Task<Publication> GetFullPublicationByIdAsync(Guid id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("select * from \"Publication\"  where \"Id\" = @Id ", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Publication publication = new Publication()
                        {
                            Id = reader.GetGuid(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Edition = reader.GetInt16(3),
                            DatePublished = reader.GetDateTime(4),
                            Quantity = reader.GetInt16(5),
                            NumberOfPages = reader.GetInt16(6),
                            Language = reader.GetString(7),
                            TypeId = reader.GetGuid(8),
                            GenreId = reader.GetGuid(9),
                            PublisherId = reader.GetGuid(10),
                            IsActive = reader.GetBoolean(11),
                            CreatedByUserId = reader.GetGuid(12),
                            DateCreated = reader.GetDateTime(13),
                            UpdatedByUserId = reader.GetGuid(14),
                            DateUpdated = reader.GetDateTime(15)
                            
                        };
                        return publication;
                    }
                }
                return null;
            }

        }


        public async Task<bool> AddPublicationAsync(Publication publication, List<Guid> listOfAuthorIds)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
            {

                NpgsqlCommand publicationCmd = new NpgsqlCommand("insert into \"Publication\" (\"Id\", \"Title\", \"Description\", \"Edition\", \"DatePublished\"," +
                    " \"Quantity\", \"NumberOfPages\", \"Language\", \"TypeId\", \"GenreId\",\"PublisherId\", \"IsActive\", \"CreatedByUserId\", \"DateCreated\") values " +
                    "(@Id, @Title, @Description, @Edition, @DatePublished, @Quantity, @NumberOfPages, @Language, @TypeId, @GenreId, @PublisherId, @IsActive, @CreatedByUserId," +
                    "@DateCreated); ", conn);

                publicationCmd.Parameters.AddWithValue("@Id", publication.Id);
                publicationCmd.Parameters.AddWithValue("@Title", publication.Title);
                publicationCmd.Parameters.AddWithValue("@Description", publication.Description);
                publicationCmd.Parameters.AddWithValue("@Edition", publication.Edition);
                publicationCmd.Parameters.AddWithValue("@DatePublished", publication.DatePublished);
                publicationCmd.Parameters.AddWithValue("@Quantity", publication.Quantity);
                publicationCmd.Parameters.AddWithValue("@NumberOfPages", publication.NumberOfPages);
                publicationCmd.Parameters.AddWithValue("@Language", publication.Language);
                publicationCmd.Parameters.AddWithValue("@TypeId", publication.TypeId);
                publicationCmd.Parameters.AddWithValue("@GenreId", publication.GenreId);
                publicationCmd.Parameters.AddWithValue("@PublisherId", publication.PublisherId);
                publicationCmd.Parameters.AddWithValue("@IsActive", publication.IsActive);
                publicationCmd.Parameters.AddWithValue("@CreatedByUserId", publication.CreatedByUserId);
                publicationCmd.Parameters.AddWithValue("@DateCreated", publication.DateCreated);


                conn.Open();

                int numberOfAffectedRows = await publicationCmd.ExecuteNonQueryAsync();

                if (numberOfAffectedRows > 0)
                {
                    foreach (Guid authorId in listOfAuthorIds)
                    {
                        NpgsqlCommand publicationauthorCmd = new NpgsqlCommand("insert into \"PublicationAuthor\" (\"Id\", \"PublicationId\", \"AuthorId\"," +
                            " \"IsActive\", \"CreatedByUserId\", \"DateCreated\") values (@Id, @PublicationId, @AuthorId, @IsActive, @CreatedByUserId," +
                            " @DateCreated) ", conn);


                        publicationauthorCmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        publicationauthorCmd.Parameters.AddWithValue("@PublicationId", publication.Id);
                        publicationauthorCmd.Parameters.AddWithValue("@AuthorId", authorId);
                        publicationauthorCmd.Parameters.AddWithValue("@IsActive", publication.IsActive);
                        publicationauthorCmd.Parameters.AddWithValue("@CreatedByUserId", publication.CreatedByUserId);
                        publicationauthorCmd.Parameters.AddWithValue("@DateCreated", publication.DateCreated);

                        int numberOfAffectedRows2 = await publicationauthorCmd.ExecuteNonQueryAsync();
                        if (numberOfAffectedRows2 == 0)
                        {
                           return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RemovePublicationAsync(Guid id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("update \"Publication\" set \"IsActive\" = @IsActive  where \"Id\" = @Id", conn);
                //deaktivirati i authorpublication unos
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@IsActive", false);

                conn.Open();

                int numberOfAffectedRows = await cmd.ExecuteNonQueryAsync();

                if (numberOfAffectedRows > 0)
                {
                    NpgsqlCommand cmd2 = new NpgsqlCommand("update \"PublicationAuthor\" set \"IsActive\" = @IsActive  where \"PublicationId\" = @Id", conn);
                    //deaktivirati i authorpublication unos
                    cmd2.Parameters.AddWithValue("@Id", id);
                    cmd2.Parameters.AddWithValue("@IsActive", false);

                    return true;
                }

                return false;
            }
        }

        public async Task<PublicationWithAuthorIds> UpdatePublicationAsync(Guid id, Publication updatedPublication)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;


            try
            {
                int numberOfAffectedRows = 0;
                Publication currentPublication = await GetFullPublicationByIdAsync(id);

                if (currentPublication != null)
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
                    {
                        StringBuilder sb = new StringBuilder();

                        NpgsqlCommand cmd = new NpgsqlCommand();

                        cmd.Connection = conn;

                        sb.Append("update \"Publication\" set ");
                        if (updatedPublication.Title != null & currentPublication.Title != updatedPublication.Title)
                        {
                            sb.Append("\"Title\" = @Title, ");
                            cmd.Parameters.AddWithValue("@Title", updatedPublication.Title);
                        }
                        if (updatedPublication.Description != null & currentPublication.Description != updatedPublication.Description)
                        {
                            sb.Append("\"Description\" = @Description, ");
                            cmd.Parameters.AddWithValue("@Description", updatedPublication.Description);
                        }
                        if (updatedPublication.Edition != 0 & currentPublication.Edition != updatedPublication.Edition)
                        {
                            sb.Append("\"Edition\" = @Edition, ");
                            cmd.Parameters.AddWithValue("@Edition", updatedPublication.Edition);
                        }
                        if (updatedPublication.DatePublished != DateTime.MinValue & currentPublication.DatePublished != updatedPublication.DatePublished)
                        {
                            sb.Append("\"DatePublished\" = @DatePublished, ");
                            cmd.Parameters.AddWithValue("@DatePublished", updatedPublication.DatePublished);
                        }
                        if (updatedPublication.NumberOfPages != 0 & currentPublication.NumberOfPages != updatedPublication.NumberOfPages)
                        {
                            sb.Append("\"NumberOfPages\" = @NumberOfPages, ");
                            cmd.Parameters.AddWithValue("@NumberOfPages", updatedPublication.NumberOfPages);
                        }
                        if (updatedPublication.Language != null & currentPublication.Language != updatedPublication.Language)
                        {
                            sb.Append("\"Language\" = @Language, ");
                            cmd.Parameters.AddWithValue("@Language", updatedPublication.Language);
                        }
                        if (updatedPublication.TypeId != Guid.Empty & currentPublication.TypeId != updatedPublication.TypeId)
                        {
                            NpgsqlCommand checkTypeCmd= new NpgsqlCommand("select * from \"Type\" where \"Id\" = @TypeId",conn);
                            checkTypeCmd.Parameters.AddWithValue("@TypeId", updatedPublication.TypeId);

                            numberOfAffectedRows = await checkTypeCmd.ExecuteNonQueryAsync();
                            if (numberOfAffectedRows > 0)
                            {
                                sb.Append("\"TypeId\" = @TypeId, ");
                                cmd.Parameters.AddWithValue("@TypeId", updatedPublication.TypeId);
                            }
                        }
                        if (updatedPublication.GenreId != Guid.Empty & currentPublication.GenreId != updatedPublication.GenreId)
                        {
                            NpgsqlCommand checkGenreCmd = new NpgsqlCommand("select * from \"Genre\" where \"Id\" = @GenreId", conn);
                            checkGenreCmd.Parameters.AddWithValue("@GenreId", updatedPublication.GenreId);

                            numberOfAffectedRows = await checkGenreCmd.ExecuteNonQueryAsync();
                            if (numberOfAffectedRows > 0)
                            {
                                sb.Append("\"GenreId\" = @GenreId, ");
                                cmd.Parameters.AddWithValue("@GenreId", updatedPublication.GenreId);
                            }
                        }
                        if (updatedPublication.PublisherId != Guid.Empty & currentPublication.PublisherId != updatedPublication.PublisherId )
                        {
                            NpgsqlCommand checkPublisherCmd = new NpgsqlCommand("select * from \"Publisher\" where \"Id\" = @PublisherId", conn);
                            checkPublisherCmd.Parameters.AddWithValue("@PublisherId", updatedPublication.PublisherId);

                            numberOfAffectedRows = await checkPublisherCmd.ExecuteNonQueryAsync();
                            if (numberOfAffectedRows > 0)
                            {
                                sb.Append("\"PublisherId\" = @PublisherId, ");
                                cmd.Parameters.AddWithValue("@PublisherId", updatedPublication.PublisherId);
                            }
                        }
                        if (updatedPublication.IsActive == false)
                        {
                            sb.Append("\"IsActive\" = @IsActive, ");
                            cmd.Parameters.AddWithValue("@IsActive", false);
                        }

                        sb.Append("\"UpdatedByUserId\" = @UpdatedByUserId, ");
                        cmd.Parameters.AddWithValue("@UpdatedByUserId", updatedPublication.UpdatedByUserId);
                        sb.Append("\"DateUpdated\" = @DateUpdated, ");
                        cmd.Parameters.AddWithValue("@DateUpdated", updatedPublication.DateUpdated);

                        sb.Remove(sb.Length - 2, 2);

                        sb.Append(" where \"Id\" = @Id;");
                        cmd.Parameters.AddWithValue("@Id", id);

                        conn.Open();

                        cmd.CommandText = sb.ToString();

                        numberOfAffectedRows = await cmd.ExecuteNonQueryAsync();
                        if (numberOfAffectedRows > 0)
                        {
                            return await GetPublicationByIdAsync(id);
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

        public async Task<PagedList<PublicationWithAuthorIds>> GetAllPublicationsAsync(PublicationFiltering filtering, Sorting sorting, Paging paging)
        {
            int numberOfRows = 0;

            List<PublicationWithAuthorIds> listOfPublications = new List<PublicationWithAuthorIds>();

            List<string> allowedSortColumns = new List<string> { "title", "edition", "datepublished", "quantity", "numberofpages",
        "Language", "typeid", "genreid" };

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = conn;

                    StringBuilder queryStringBuilder = new StringBuilder("select \"Id\", \"Title\",\"Description\",\"Edition\",\"DatePublished\",\"Quantity\",\"NumberOfPages\"," +
                            "\"Language\",\"TypeId\",\"GenreId\",\"PublisherId\" from \"Publication\" ");

                    StringBuilder countStringBuilder = new StringBuilder("select count (*) from \"Publication\" ");

                    queryStringBuilder = PublicationFilterResults(queryStringBuilder, filtering, cmd);
                    countStringBuilder = PublicationFilterResults(countStringBuilder, filtering, cmd);

                    cmd.CommandText = queryStringBuilder.ToString();

                    conn.Open();

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
                            PublicationWithAuthorIds publication = new PublicationWithAuthorIds()
                            {
                                Id = reader.GetGuid(0),
                                Title = reader.GetString(1),
                                Description = reader.GetString(2),
                                Edition = reader.GetInt16(3),
                                DatePublished = reader.GetDateTime(4),
                                Quantity = reader.GetInt16(5),
                                NumberOfPages = reader.GetInt16(6),
                                Language = reader.GetString(7),
                                TypeId = reader.GetGuid(8),
                                GenreId = reader.GetGuid(9),
                                PublisherId = reader.GetGuid(10),
                                ListOfAuthorIds = await GetPublicationAuthorIds(reader.GetGuid(0))
                            };

                            listOfPublications.Add(publication);
                        }
                    }

                    await reader.CloseAsync();

                    using (NpgsqlCommand cmd2 = new NpgsqlCommand(countStringBuilder.ToString(), conn))
                    {
                        numberOfRows = Convert.ToInt32(await cmd2.ExecuteScalarAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }

            return new PagedList<PublicationWithAuthorIds>(listOfPublications, paging.PageNumber, paging.PageSize, numberOfRows);
        }


        private StringBuilder PublicationFilterResults(StringBuilder filteringStringBuilder, PublicationFiltering filtering, NpgsqlCommand cmd ) 
        {
            List<string> listOfLanguages = new List<string>{"english"};

            filteringStringBuilder.Append("WHERE 1 = 1");

            if (filtering != null)
            {
                if (filtering.SearchQuery != null)
                {
                    filteringStringBuilder.Append(" and ((\"Title\" like @SearchQuery) or (\"Description\" like @SearchQuery) or " +
                        $"(\"DatePublished\" like @SearchQuery) or (\"Language\" like @SearchQuery)");
                    cmd.Parameters.AddWithValue("@SearchQuery", filtering.SearchQuery);
                }
                if (filtering.MinDatePublished != null)
                {
                    filteringStringBuilder.Append(" and \"MinDatePublished\" >= @MinDatePublished ");
                    cmd.Parameters.AddWithValue("@MinDatePublished", filtering.MinDatePublished);
                }
                if (filtering.MaxDatePublished != null)
                {
                    filteringStringBuilder.Append(" and \"MaxDatePublished\" <= @MaxDatePublished ");
                    cmd.Parameters.AddWithValue("@MaxDatePublished", filtering.MaxDatePublished);

                }

                if (filtering.MinNumberOfPages != null)
                {
                    filteringStringBuilder.Append(" and \"MinNumberOfPages\" >= @MinNumberOfPages ");
                    cmd.Parameters.AddWithValue("@MinNumberOfPages", filtering.MinNumberOfPages);

                }
                if (filtering.MaxNumberOfPages != null)
                {
                    filteringStringBuilder.Append(" and \"MaxNumberOfPages\" <= @MaxNumberOfPages ");
                    cmd.Parameters.AddWithValue("@MaxNumberOfPages", filtering.MaxNumberOfPages);

                }
                if (listOfLanguages.Contains(filtering.Language?.ToLower()))
                {
                    filteringStringBuilder.Append(" and \"Language\" = @Language ");
                    cmd.Parameters.AddWithValue("@Language", filtering.Language);
                }

                if (filtering.TypeId != null)
                {
                    filteringStringBuilder.Append(" and \"TypeId\" = @TypeId ");
                    cmd.Parameters.AddWithValue("@TypeId", filtering.TypeId);

                }
                if (filtering.GenreId != null)
                {
                    filteringStringBuilder.Append(" and \"GenreId\" = @GenreId ");
                    cmd.Parameters.AddWithValue("@GenreId", filtering.GenreId);

                }

            }
            return filteringStringBuilder;
        }

        private async Task<List<Guid>> GetPublicationAuthorIds(Guid publicationId)
        {

            List<Guid> listOfAuthorIds = new List<Guid>();

            using (NpgsqlConnection conn = new NpgsqlConnection(Helper.connectionString))
            {
                NpgsqlCommand cmd= new NpgsqlCommand("select \"AuthorId\" from \"PublicationAuthor\"  where \"PublicationId\" = @publicationId ", conn);
                cmd.Parameters.AddWithValue("@publicationId", publicationId);

                conn.Open();

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listOfAuthorIds.Add(reader.GetGuid(0));
                    }
                    return listOfAuthorIds;
                }
                return null;
            }
            

        }

    }

   
}
