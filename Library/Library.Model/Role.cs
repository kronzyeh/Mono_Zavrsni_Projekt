using Library.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Role : IRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
