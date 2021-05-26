using CancerDirectoryAPI.Attributes;
using Newtonsoft.Json;
using OA.Data.Common;
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
    public class PhysicianController : BaseController
    {
        private readonly IPhysicianService _physicianService;
        public PhysicianController()
        {
            _physicianService = new PhysicianService();
        }
        public PhysicianController(IPhysicianService _physicianService)
        {
            this._physicianService = _physicianService;
        }


        [HttpGet]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetAllLanguages()
        {
            try
            {
                var data = await _physicianService.GetAllLanguages();
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetAllCredentials()
        {
            try
            {
                var data = await _physicianService.GetAllCredentials();
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetAllDisciplines()
        {
            try
            {
                var data = await _physicianService.GetAllDisciplines();
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetAllSpecialties()
        {
            try
            {
                var data = await _physicianService.GetAllSpecialties();
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetAllTreatmentTeams()
        {
            try
            {
                var data = await _physicianService.GetAllTreatmentTeams();
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpGet]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetPhysicianList()
        {
            try
            {
                var data = await _physicianService.GetPhysicianList();
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };

                return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                //return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetPhysicianDetails(Guid Id)
        {
            try
            {
                var data = await _physicianService.GetPhysicianDetails(Id);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetPhysicianByName(string name)
        {
            try
            {
                var data = await _physicianService.GetPhysicianByName(name);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetPhysicianBySpecialty(Guid Id)
        {
            try
            {
                var data = await _physicianService.GetPhysiciansBySpecialty(Id);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetPhysiciansByDiscipline(Guid Id)
        {
            try
            {
                var data = await _physicianService.GetPhysiciansByDiscipline(Id);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        [UserAuthorization]
        [CacheFilter(120, 120, false)]
        public async Task<string> GetPhysiciansByTreatmentTeam(Guid Id)
        {
            try
            {
                var data = await _physicianService.GetPhysiciansByTreatmentTeam(Id);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = UserToken };
                return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }
    }
}