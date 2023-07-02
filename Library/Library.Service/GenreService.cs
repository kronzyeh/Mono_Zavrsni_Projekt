using Library.Model;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public  class GenreService : IGenreService
    {
        protected IGenreRepository GenreRepository { get; set; }
        public GenreService(IGenreRepository genreRepository)
        {
            GenreRepository = genreRepository;
        }

        public async Task<bool> CreateGenreAsync(string genreName)
        {
            Genre genre = new Genre()
            {
                Id= Guid.NewGuid(),
                Name = genreName,
                IsActive = true,
                //CreatedByUserId = User.Id,
                DateCreated = DateTime.Now,
                UpdatedByUserId = null,
                DateUpdated = null
            };

            return await GenreRepository.CreateGenreAsync(genre);
        }
    }
}
