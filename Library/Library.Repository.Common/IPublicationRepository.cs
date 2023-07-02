﻿using Library.Common;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Repository.Common
{
    public interface IPublicationRepository
    {
        
        Task<PublicationWithAuthorIds> GetPublicationByIdAsync(Guid id);

        Task<bool> AddPublicationAsync([FromBody] Publication publication, List<Guid> listOfAuthorIds);

        Task<bool> RemovePublicationAsync(Guid id);

        Task<PublicationWithAuthorIds> UpdatePublicationAsync(Guid id, Publication updatedPublication);

        Task<PagedList<PublicationWithAuthorIds>> GetAllPublicationsAsync(PublicationFiltering filtering, Sorting sorting, Paging paging);

        /*
        Task<list<Publication>> GetNewestPublications();
        
       */
    }
}
