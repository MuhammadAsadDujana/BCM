using OA.Data.Common;
using OA.Data.Helper;
using OA.Repository.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationContext _dContext;
        private DbSet<TEntity> entites;

        public Repository()
        {
        }

        [ImportingConstructor]
        public Repository(ApplicationContext dContext)
        {
            this._dContext = dContext;
            entites = dContext.Set<TEntity>();
        }

        public async Task<ResponseModel> DeleteAsync(TEntity entity, TEntity Log)
        {
            if (entity == null)
            {
                return ResponseHandler.GetResponse("Faild", "Entity is null", "404", entity, null);
            }
            Log.ModifiedDate = DateTime.Now;
            entites.Add(Log);
            entites.Remove(entity);
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Succss", "Delete Record", "200", entity, null);
        }

        public TEntity Get(Guid Id)
        {
            return entites.SingleOrDefault(x => x.Id == Id);
        }

        public  IQueryable<TEntity> GetAll()
        {

            return  entites.AsQueryable();
        }

        public async Task<ResponseModel> InsertAsync(TEntity entity, TEntity Log)
        {
            if (entity == null)
            {
                return ResponseHandler.GetResponse("Faild", "Entity is null", "404", entity, null);
            }
            Log.CreatedDate = DateTime.Now;
            entites.Add(Log);
            entites.Add(entity);
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Succss", "Inserted", "200", entity, null);
        }

        public void SaveChanges()
        {
            _dContext.SaveChanges();
        }

       
        public async Task<ResponseModel> UpdateAsync(TEntity entity, TEntity Log)
        {
            if (entity == null)
            {
                return ResponseHandler.GetResponse("Faild", "Entity is null", "404", entity, null);
            }
            Log.ModifiedDate = DateTime.Now;
            entites.Add(Log);
            entites.Add(entity);
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Succss", "Updated Record", "200", entity, null);
        }
    }
}
