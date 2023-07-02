using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Common
{
    public interface IRole
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
