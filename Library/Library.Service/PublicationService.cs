using Library.Common;
using Library.Model;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Service
{
    public class PublicationService : IPublicationService
    {
        protected IPublicationRepository PublicationRepository { get; set; }

        public PublicationService(IPublicationRepository publicationRepository)
        {
            PublicationRepository = publicationRepository;
        }


        public async Task<PublicationWithAuthorIds> GetPublicationByIdAsync(Guid id)
        {
                return await PublicationRepository.GetPublicationByIdAsync(id);
        }

        public async Task<bool> AddPublicationAsync(Publication publication, List<Guid> listOfAuthorIds)
        {
            return await PublicationRepository.AddPublicationAsync(publication, listOfAuthorIds);
        }

        public async Task<bool> RemovePublicationAsync(Guid id)
        {
            return await PublicationRepository.RemovePublicationAsync(id);
        }

        public async Task<PublicationWithAuthorIds> UpdatePublicationAsync(Guid id, Publication updatedPublication)
        {
            updatedPublication.UpdatedByUserId = Guid.Parse("9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f");
            updatedPublication.DateUpdated = DateTime.Now;

            return await PublicationRepository.UpdatePublicationAsync(id, updatedPublication);
        }

        public async Task<PagedList<PublicationWithAuthorIds>> GetAllPublicationsAsync(PublicationFiltering filtering, Sorting sorting, Paging paging)
        {
            return await PublicationRepository.GetAllPublicationsAsync(filtering, sorting, paging);
        }

    }
}
