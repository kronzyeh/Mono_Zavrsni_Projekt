using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.Common
{
    public interface IPublisherRepository
    {
        Task<bool> CreatePublisherAsync(Publisher publisher);
    }
}
