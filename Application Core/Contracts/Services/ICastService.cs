using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Models;

namespace Application_Core.Contracts.Services
{
    public interface ICastService
    {
        Task<CastDetailsResponseModel> GetCastDetails(int id);
    }
}
