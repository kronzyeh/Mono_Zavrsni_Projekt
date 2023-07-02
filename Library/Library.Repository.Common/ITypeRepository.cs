using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Repository.Common
{
    public interface ITypeRepository
    {
        Task<bool> CreateTypeAsync(Model.Type type);
    }
}
