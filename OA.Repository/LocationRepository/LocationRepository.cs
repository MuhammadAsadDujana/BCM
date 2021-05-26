using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationContext _dContext;
        public LocationRepository()
        {
            _dContext = new ApplicationContext();
        }

        public async Task<IQueryable<Country>> GetAllCountries()
        {
                var data = await _dContext.Country.Where(x => x.IsActive == true).ToListAsync();
                return data.AsQueryable();
        }

        public async Task<IQueryable<State>> GetAllStates()
        {
            var data = await _dContext.State.Where(x => x.IsActive == true).ToListAsync();
            return data.AsQueryable();
        }
    }
}
