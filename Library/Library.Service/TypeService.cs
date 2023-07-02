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
    public class TypeService : ITypeService
    {
        protected ITypeRepository TypeRepository { get; set; }
        public TypeService(ITypeRepository typeRepository)
        {
            TypeRepository = typeRepository;
        }

        public async Task<bool> CreateTypeAsync(string typeName)
        {
           Model.Type type = new Model.Type()
            {
                Id = Guid.NewGuid(),
                Name = typeName,
                IsActive = true,
                //CreatedByUserId = User.Id,
                DateCreated = DateTime.Now,
                UpdatedByUserId = null,
                DateUpdated = null
            };

            return await TypeRepository.CreateTypeAsync(type);
        }

    }
}
