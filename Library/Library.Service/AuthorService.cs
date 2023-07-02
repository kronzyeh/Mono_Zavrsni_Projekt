using Library.Model;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Library.Common;

namespace Library.Service
{
    public class AuthorService : IAuthorService
    {
        protected IAuthorRepository AuthorRepository { get; set; }

        public AuthorService(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
        }
        
        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            return await AuthorRepository.GetAuthorByIdAsync(id);

        }

        public async Task<bool> AddAuthorAsync(string FirstName, string LastName, string Nationality, DateTime DateOfBirth, DateTime? DateOfDeath)
        {
            Author author = new Author()
            {
                Id = Guid.NewGuid(),
                FirstName = FirstName,
                LastName = LastName,
                Nationality = Nationality,
                DateOfBirth = DateOfBirth,
                DateOfDeath = DateOfDeath,
                IsActive = true,
                CreatedByUserId = Guid.Parse("9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f"),
                //CreatedByUserId = User.Id,
                DateCreated = DateTime.Now
            };

            return await AuthorRepository.AddAuthorAsync(author);

        }

        public async Task<bool> RemoveAuthorAsync(Guid id)
        {
            return await AuthorRepository.RemoveAuthorAsync(id);
        }

        public async Task<Author> UpdateAuthorAsync(Guid id, Author updatedAuthor)
        {
            updatedAuthor.UpdatedByUserId = Guid.Parse("9c34cf9a-c6a7-427b-abd3-b54a279b0d78");
            updatedAuthor.DateUpdated = DateTime.Now;

            return await AuthorRepository.UpdateAuthorAsync(id, updatedAuthor);

        }

        
        public async Task<PagedList<Author>> GetAllAuthorsAsync(AuthorFiltering filtering, Paging paging, Sorting sorting)
        {
          
            return await AuthorRepository.GetAllAuthorsAsync(filtering, paging, sorting);

        }

        
       
        
    }
}
