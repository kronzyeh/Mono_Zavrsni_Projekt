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
    public class PublisherService : IPublisherService
    {
        protected IPublisherRepository PublisherRepository { get; set; }
        public PublisherService(IPublisherRepository publisherRepository)
        {
            PublisherRepository = publisherRepository;
        }

        public async Task<bool> CreatePublisherAsync(string publisherName, string publisherContactNumber)
        {
            Publisher publisher = new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = publisherName,
                ContactNumber = publisherContactNumber,
                IsActive = true,
                //CreatedByUserId = User.Id,
                DateCreated = DateTime.Now,
                UpdatedByUserId = null,
                DateUpdated = null
            };

            return await PublisherRepository.CreatePublisherAsync(publisher);
        }

    }
}
