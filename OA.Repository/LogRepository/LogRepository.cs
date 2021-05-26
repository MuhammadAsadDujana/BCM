using OA.Data.Common;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationContext _dContext;
        private DbSet<Logs> entites;

        public LogRepository()
        {
            _dContext = new ApplicationContext();
        }

        public LogRepository(ApplicationContext context)
        {
            this._dContext = context;
            entites = context.Set<Logs>();
        }
        public Task<ResponseModel> DeleteAsync(Logs entity)
        {
            throw new NotImplementedException();
        }

        public Logs Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Logs> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> InsertAsync(Logs entity)
        {
            if (entity == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(entity).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", entity, null);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> UpdateAsync(Logs entity)
        {
            throw new NotImplementedException();
        }
    }
}
