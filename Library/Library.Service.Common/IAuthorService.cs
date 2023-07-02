using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Library.Common;

namespace Library.Service.Common
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorByIdAsync(Guid id);

        Task<bool> AddAuthorAsync(string FirstName, string LastName, string Nationality, DateTime DateOfBirth, DateTime? DateOfDeath);

        Task<bool> RemoveAuthorAsync(Guid id);

        Task<Author> UpdateAuthorAsync(Guid id, Author updatedAuthor);
        
        Task<PagedList<Author>> GetAllAuthorsAsync(AuthorFiltering filtering, Paging paging, Sorting sorting);
       
    }
}
