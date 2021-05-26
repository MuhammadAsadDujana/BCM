using OA.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Get(Guid Id);
        Task<ResponseModel> InsertAsync(T entity, T Log);
        Task<ResponseModel> UpdateAsync(T entity, T Log);
        Task<ResponseModel> DeleteAsync(T entity, T Log);
        void SaveChanges();
    }
}
