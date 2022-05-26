﻿using Application_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Core.Contracts.Services
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllGenres();
    }
}
