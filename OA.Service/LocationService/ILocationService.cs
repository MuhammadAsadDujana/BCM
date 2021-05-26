using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.UserServices
{
    public interface ILocationService
    {
        Task<ResponseModel> GetAllCountries();
        Task<ResponseModel> GetAllStates();
    }
}
