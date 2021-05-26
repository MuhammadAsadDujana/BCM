using CancerDirectoryAPI.Attributes;
using Newtonsoft.Json;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Service.UserServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CancerDirectoryAPI.Controllers
{
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;
        public LocationController()
        {
            _locationService = new LocationService();
        }
        public LocationController(ILocationService _locationService)
        {
            this._locationService = _locationService;
        }

        // GET: Location

        [HttpGet]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetAllCountries()
        {
            try
            {
                var Result = await _locationService.GetAllCountries();
                return JsonConvert.SerializeObject(Result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        public async Task<string> GetAllStates()
        {
            try
            {
                var Result = await _locationService.GetAllStates();
                return JsonConvert.SerializeObject(Result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }
    }
}