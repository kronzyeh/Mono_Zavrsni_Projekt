﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IGenreService
    {
        Task<bool> CreateGenreAsync(string genreName);
    }
}
