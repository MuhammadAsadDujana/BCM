using OA.Data.Common;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public interface ILogRepository
    {
        IQueryable<Logs> GetAll();
        Logs Get(Guid Id);
        Task<ResponseModel> InsertAsync(Logs entity);
        Task<ResponseModel> UpdateAsync(Logs entity);
        Task<ResponseModel> DeleteAsync(Logs entity);
        void SaveChanges();
    }
}
