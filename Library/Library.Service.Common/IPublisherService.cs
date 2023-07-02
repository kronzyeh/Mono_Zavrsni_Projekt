using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IPublisherService
    {
        Task<bool> CreatePublisherAsync(string publisherName, string publisherContactNumber);
    }
}
