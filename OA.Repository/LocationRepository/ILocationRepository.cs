using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public interface ILocationRepository
    {
        Task<IQueryable<Country>> GetAllCountries();
        Task<IQueryable<State>> GetAllStates();
    }
}
