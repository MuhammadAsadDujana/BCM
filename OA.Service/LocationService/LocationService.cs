using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.UserServices
{
    [Export(typeof(ILocationService))]
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILogRepository _logRepository;


        public LocationService() 
        {
            _locationRepository = new LocationRepository();
            _logRepository = new LogRepository();
        }
        public LocationService(ILocationRepository locationRepository , ILogRepository logRepository)
        {
            this._locationRepository = locationRepository;
            this._logRepository = logRepository;
        }

        public async Task<ResponseModel> GetAllCountries()
        {
            try
            {
                var data = await _locationRepository.GetAllCountries();
                if (data == null)
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllCountries", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllCountries", Description = "Successfully get country list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllCountries", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetAllStates()
        {
            try
            {
                var data = await _locationRepository.GetAllStates();
                if (data == null)
                {
                    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllStates", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllStates", Description = "Successfully get state list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllStates", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
    }
}
