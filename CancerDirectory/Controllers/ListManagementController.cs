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

namespace CancerDirectory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ListManagementController : Controller
    {
        private readonly IPhysicianService _physicianService;
        public ListManagementController()
        {
            _physicianService = new PhysicianService();
        }
        public ListManagementController(IPhysicianService _physicianService)
        {
            this._physicianService = _physicianService;
        }



        // GET: ListManagement/Details/5
        public async Task<ActionResult> PhysicianListManagement()
        {

            var ddlSpecialties = await _physicianService.GetAllSpecialties();
            ViewBag.ddlSpecialties = ddlSpecialties.Body;

            var ddlDisciplines = await _physicianService.GetAllDisciplines();
            ViewBag.ddlDisciplines = ddlDisciplines.Body;

            var ddlTreatmentTeams = await _physicianService.GetAllTreatmentTeams();
            ViewBag.ddlTreatmentTeams = ddlTreatmentTeams.Body;

            var ddlCredentials = await _physicianService.GetAllCredentials();
            ViewBag.ddlCredentials = ddlCredentials.Body;         

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> addNewSpecialty()
        {
            return View();
        }


        [HttpPost]
        public async Task<string> addNewSpecialty(Specialties specialty)
        {
            try
            {
                           
                Specialties s = new Specialties
                {
                    SpecialtyTitle = specialty.SpecialtyTitle,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                var data = await _physicianService.AddSpecialty(s);
             
                if (data.Code == "200")
                {
                    return JsonConvert.SerializeObject(data);
                }

             //   var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = data.AccessToken };
                return JsonConvert.SerializeObject(data);
                //return null;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
                //throw;
            }         
        }




        [HttpGet]
        public async Task<ActionResult> addNewDiscipline()
        {
            return View();
        }


        [HttpPost]
        public async Task<string> addNewDiscipline(Discipline discipline)
        {
            try
            {
                Discipline d = new Discipline
                {
                    DisciplineTitle = discipline.DisciplineTitle,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                var data = await _physicianService.AddDisciplines(d);

                if (data.Code == "200")
                {
                    return JsonConvert.SerializeObject(data);
                }

                // return null;
                return JsonConvert.SerializeObject(data);
                //return null;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
                //throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> addNewTreatmentTeam()
        {
            return View();
        }


        [HttpPost]
        public async Task<string> addNewTreatmentTeam(TreatmentTeam treatment)
        {
            try
            {
                TreatmentTeam t = new TreatmentTeam
                {
                    DiseaseTitle = treatment.DiseaseTitle,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                var data = await _physicianService.AddTreatmentTeam(t);

                if (data.Code == "200")
                {
                    return JsonConvert.SerializeObject(data);
                }

                return JsonConvert.SerializeObject(data);
                //return null;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
                //throw;
            }
        }




        [HttpGet]
        public async Task<ActionResult> addNewCredential()
        {
            return View();
        }


        [HttpPost]
        public async Task<string> addNewCredential(Credentials credentials)
        {
            try
            {
                Credentials c = new Credentials
                {
                    CredentialsTitle = credentials.CredentialsTitle,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                var data = await _physicianService.AddCredential(c);

                if (data.Code == "200")
                {
                    return JsonConvert.SerializeObject(data);
                }

                return JsonConvert.SerializeObject(data);
                //return null;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
                //throw;
            }
        }





        // GET: ListManagement
        public ActionResult Index()
        {
            return View();
        }

        // GET: ListManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListManagement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    

        [HttpPost]
        public async Task<string> deleteSpecialty(Guid SpecialtyId)
        {
            try
            {
                var data = await _physicianService.DeleteSpecialty(SpecialtyId);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = data.AccessToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }


        [HttpPost]
        public async Task<string> deleteDiscipline(Guid DisciplineId)
        {
            try
            {
                var data = await _physicianService.DeleteDiscipline(DisciplineId);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = data.AccessToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        public async Task<string> deleteTreatment(Guid TreatmentId)
        {
            try
            {
                var data = await _physicianService.DeleteTreatment(TreatmentId);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = data.AccessToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

        [HttpPost]
        public async Task<string> deleteCredential(Guid CredentialId)
        {
            try
            {
                var data = await _physicianService.DeleteCredential(CredentialId);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = data.AccessToken };
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }


        // GET: ListManagement/Edit/5
        public async Task<ActionResult> Edit(Guid? SpecialtyId,Guid? DisciplineId,Guid? TreatmentTeamId, Guid? CredentialsId)
        {
            var Result = new ResponseModel();

            if (SpecialtyId != null)
            {
                Guid id = Guid.Parse(SpecialtyId.ToString());
                 Result = await _physicianService.GetSpecialties(id);
               ViewBag.GetSpecialties = Result.Body;
                ViewBag.PageTitle = "Edit Specialty";
            }
            else if(DisciplineId != null)
            {
                Guid id = Guid.Parse(DisciplineId.ToString());
                 Result = await _physicianService.GetDiscipline(id);
                ViewBag.GetDiscipline = Result.Body;
                ViewBag.PageTitle = "Edit Discipline";
            }
            else if (TreatmentTeamId != null)
            {
                Guid id = Guid.Parse(TreatmentTeamId.ToString());
                 Result = await _physicianService.GetTreatmentTeams(id);
                ViewBag.GetTreatmentTeams = Result.Body;
                ViewBag.PageTitle = "Edit TreatmentTeam";
            }
            else if (CredentialsId != null)
            {
                Guid id = Guid.Parse(CredentialsId.ToString());
                 Result = await _physicianService.GetCredentials(id);
                ViewBag.GetCredentials = Result.Body;
                ViewBag.PageTitle = "Edit Credentials";
            }

         //   ViewBag.GetModelValues = Result.Body;
            return View();
        }

        // POST: ListManagement/Edit/5
        [HttpPost]
        public async Task<string> Edit(Specialties specialties, Discipline discipline, TreatmentTeam treatmentTeam, Credentials credentials)
        {
            try
            {

                if (specialties.SpecialtyTitle != null)
                {

                    var result = await _physicianService.EditSpecialty(specialties);

                    if (result.Code == "200")
                    {
                        return JsonConvert.SerializeObject(result);
                    }
                    return JsonConvert.SerializeObject(result);
                }
                else if(discipline.DisciplineTitle != null)
                {

                    var result = await _physicianService.EditDiscipline(discipline);

                    if (result.Code == "200")
                    {
                        return JsonConvert.SerializeObject(result);
                    }
                    return JsonConvert.SerializeObject(result);
                }
                else if (treatmentTeam.DiseaseTitle != null)
                {

                    var result = await _physicianService.EditTreatmentTeams(treatmentTeam);

                    if (result.Code == "200")
                    {
                        return JsonConvert.SerializeObject(result);
                    }
                    return JsonConvert.SerializeObject(result);
                }
                else if (credentials.CredentialsTitle != null)
                {

                    var result = await _physicianService.EditCredentials(credentials);

                    if (result.Code == "200")
                    {
                        return JsonConvert.SerializeObject(result);
                    }
                    return JsonConvert.SerializeObject(result);
                }

                //  return null;
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("No data found", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }

    }
}
