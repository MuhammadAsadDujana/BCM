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
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Transactions;
using System.IO;
using System.Configuration;
using System.Net;
using System.Text;

namespace CancerDirectory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhysicianController : Controller
    {

        private readonly IPhysicianService _physicianService;

        public PhysicianController()
        {
            _physicianService = new PhysicianService();
        }
        public PhysicianController(IPhysicianService physicianService)
        {
            this._physicianService = physicianService;
        }


        [HttpGet]
        public async Task<ActionResult> physiciansManagementList()
        {
            try
            {
              //  var data = (Physician)(await _physicianService.GetPhysicianList()).Body;

                var data = await _physicianService.GetPhysicianListSimple();
                var physician =  data.Body;
             //   var physicianList = (Physician) physician;
                


                return View(physician);

             //   Json(new { data = data.Body }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
            return View();
         //     Json(new { data = data.Body }, JsonRequestBehavior.AllowGet);
        }

        
        public async Task<ActionResult> addNewPhysician()
        {
            var ddlCredentials = await _physicianService.GetAllCredentials();   
            ViewBag.ddlCredentials = ddlCredentials.Body;

            var ddlLanguages = await _physicianService.GetAllLanguages();
            ViewBag.ddlLanguages = ddlLanguages.Body;

            var ddlDisciplines = await _physicianService.GetAllDisciplines();
            ViewBag.ddlDisciplines = ddlDisciplines.Body;

            var ddlSpecialties = await _physicianService.GetAllSpecialties();
            ViewBag.ddlSpecialties = ddlSpecialties.Body;

            var ddlTreatmentTeams = await _physicianService.GetAllTreatmentTeams();
            ViewBag.ddlTreatmentTeams = ddlTreatmentTeams.Body;

            return View();
        }

        [HttpPost]
        public async Task<string> AddNewPhysician(Physician physician)
        {

          

            try
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                ModelState.Remove("imageUpload");
                if (ModelState.IsValid)
                { 
                    var js = new JavaScriptSerializer();
                    var deserializedCredentials = (object[])js.DeserializeObject(physician.physicianCredentials);
                    var deserializedDiscipline = (object[])js.DeserializeObject(physician.physicianDiscipline);
                    var deserializedSpecialties = (object[])js.DeserializeObject(physician.physicianSpecialties);
                    var deserializedTreatmentTeam = (object[])js.DeserializeObject(physician.physicianTreatmentTeam);
                    var deserializedLanguage = (object[])js.DeserializeObject(physician.physicianLanguage);
                    string encImage = "";
                    if (physician.imageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(physician.imageUpload.FileName);
                        string extension = Path.GetExtension(physician.imageUpload.FileName);
                        fileName = "content/physician_profile/" + fileName + DateTime.Now.ToString("yymmssff") + extension;
                        ///physician.ProfileImage = "~/content/physician_profile_pictures/" + fileName;
                        ///
                        string path = ConfigurationManager.AppSettings["liveUrl"] + fileName;

                        fileName = Path.Combine(Server.MapPath("~/"), fileName);
                        physician.imageUpload.SaveAs(fileName);
                       encImage = TripleDESCryptography.Encrypt(path);
                    }
                    else
                    {
                        encImage = "";
                    }

                    

                Physician Newphysician = new Physician
                {
                    FirstName = physician.FirstName,
                    LastName = physician.LastName,
                    Status = physician.Status == 0 ? PhysicianStatus.UnPublished : PhysicianStatus.Published,
                    DirectPhone = physician.DirectPhone,
                    OfficePhone = physician.OfficePhone,
                    Email = physician.Email,
                    JoiningDate = physician.JoiningDate == null || String.IsNullOrEmpty(physician.JoiningDate.ToString()) ? DateTime.Now : physician.JoiningDate,
                    //  ProfileImage = physician.ProfileImage == "" ? null : Images.Base64ToImage(physician.ProfileImage, Server),
                    ProfileImage = encImage == "" ? null : encImage,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
                var Result = await _physicianService.AddPhysician(Newphysician);
                
                Guid _PhysicianId = Newphysician.PhysicianId;

                if (Result.Code == "200")
                {


                    foreach (var item in deserializedCredentials)
                    {

                        PhysicianCredentials NewPhysicianCredentials = new PhysicianCredentials
                        {
                            PhysicianId = _PhysicianId,
                            CredentialsId = Guid.Parse(item.ToString()),
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };

                        var result = await _physicianService.AddPhysicianCredentials(NewPhysicianCredentials);
                    }


                    foreach (var item in deserializedDiscipline)
                    {
                        PhysicianDiscipline NewPhysicianDiscipline = new PhysicianDiscipline
                        {
                            PhysicianId = _PhysicianId,
                            DisciplineId = Guid.Parse(item.ToString()),
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        var result = await _physicianService.AddPhysicianDisciplines(NewPhysicianDiscipline);
                    }


                    foreach (var item in deserializedLanguage)
                    {
                        PhysicianLanguage NewphysicianLanguage = new PhysicianLanguage
                        {
                            PhysicianId = _PhysicianId,
                            LanguageId = Guid.Parse(item.ToString()),
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        var result = await _physicianService.AddPhysicianLanguages(NewphysicianLanguage);

                    }

                    foreach (var item in deserializedSpecialties)
                    {
                        PhysicianSpecialties NewPhysicianSpecialties = new PhysicianSpecialties
                        {
                            PhysicianId = _PhysicianId,
                            SpecialtyId = Guid.Parse(item.ToString()),
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        var result = await _physicianService.AddPhysicianSpecialties(NewPhysicianSpecialties);

                    }

                    foreach (var item in deserializedSpecialties)
                    {
                        PhysicianSpecialties NewPhysicianSpecialties = new PhysicianSpecialties
                        {
                            PhysicianId = _PhysicianId,
                            SpecialtyId = Guid.Parse(item.ToString()),
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        var result = await _physicianService.AddPhysicianSpecialties(NewPhysicianSpecialties);

                    }

                    foreach (var item in deserializedTreatmentTeam)
                    {
                        PhysicianTreatmentTeam NewPhysicianSpecialties = new PhysicianTreatmentTeam
                        {
                            PhyicianId = _PhysicianId,
                            TreatmentTeamId = Guid.Parse(item.ToString()),
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        var result = await _physicianService.AddPhysicianTreatmentTeams(NewPhysicianSpecialties);

                    }
                    //}
                    //    scope.Complete();
                    //}
                    //  
                 
                  }
                //  return RedirectToAction("physiciansManagementList");
                //return Json(resultStatus, JsonRequestBehavior.AllowGet);
         //       return JsonConvert.SerializeObject(Result.Code);

                var data = new ResponseModel { Code = Result.Code, Status = Result.Status, Message = Result.Message, Body = Result.Body, AccessToken = Result.AccessToken };
                return JsonConvert.SerializeObject(data, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                }
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                // return Json(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, null));
                //return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));
            }

        
        }


        [HttpGet]
        public async Task<ActionResult> ViewPhysician(Guid? physicianId)
        {
            if (physicianId != null)
            {
                Guid _physicianId = Guid.Parse(physicianId.ToString());
                var _Physician = await _physicianService.GetPhysicianDetailsSimple(_physicianId);
                var data = (Physician) _Physician.Body;

                //if (data.ProfileImage == null)
                //{
                //    data.ProfileImage = "~/Content/theme/img/profile.png";
                //}

                var _CredentialsList = await _physicianService.GetCredentialsListById(_physicianId);
                ViewBag.CredentialsEditList = _CredentialsList.Body;
                var _dbCredentialsList = await _physicianService.GetAllCredentials();
                ViewBag.dbCredentialsList = _dbCredentialsList.Body;

                var _DisciplineList = await _physicianService.GetDisciplineListById(_physicianId);
                ViewBag.DisciplineEditList = _DisciplineList.Body;
                var _dbDisciplineList = await _physicianService.GetAllDisciplines();
                ViewBag.dbDisciplineList = _dbDisciplineList.Body;

                var _TreatmentTeamsList = await _physicianService.GetTreatmentTeamsListById(_physicianId);
                ViewBag.TreatmentTeamsEditList = _TreatmentTeamsList.Body;
                var _dbTreatmentTeamsList = await _physicianService.GetAllTreatmentTeams();
                ViewBag.dbTreatmentTeamsList = _dbTreatmentTeamsList.Body;

                var _SpecialtiesList = await _physicianService.GetSpecialtiesListById(_physicianId);
                ViewBag.SpecialtiesEditList = _SpecialtiesList.Body;
                var _dbSpecialtiesList = await _physicianService.GetAllSpecialties();
                ViewBag.dbSpecialtiesList = _dbSpecialtiesList.Body;

                var _LanguagesList = await _physicianService.GetLanguagesListById(_physicianId);
                ViewBag.LanguagesEditList = _LanguagesList.Body;
                var _dbLanguagesList = await _physicianService.GetAllLanguages();
                ViewBag.dbLanguagesList = _dbLanguagesList.Body;

                return View(data);

            }
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> EditPhysician(Guid? physicianId)
        {
            if (physicianId != null)
            {
                Guid _physicianId = Guid.Parse(physicianId.ToString());
                var _Physician = await _physicianService.GetPhysicianDetailsSimple(_physicianId);
                var data = (Physician) _Physician.Body;

                
                //if (data.ProfileImage == null || data.ProfileImage == "" || string.IsNullOrEmpty(data.ProfileImage))
                //{
                //    data.ProfileImage = "~/Content/theme/img/profile.png";
                //}

                var _CredentialsList = await _physicianService.GetCredentialsListById(_physicianId);
                ViewBag.CredentialsEditList = _CredentialsList.Body;
                var _dbCredentialsList = await _physicianService.GetAllCredentials();
                ViewBag.dbCredentialsList = _dbCredentialsList.Body;

                var _DisciplineList = await _physicianService.GetDisciplineListById(_physicianId);
                ViewBag.DisciplineEditList = _DisciplineList.Body;
                var _dbDisciplineList = await _physicianService.GetAllDisciplines();
                ViewBag.dbDisciplineList = _dbDisciplineList.Body;

                var _TreatmentTeamsList = await _physicianService.GetTreatmentTeamsListById(_physicianId);
                ViewBag.TreatmentTeamsEditList = _TreatmentTeamsList.Body;
                var _dbTreatmentTeamsList = await _physicianService.GetAllTreatmentTeams();
                ViewBag.dbTreatmentTeamsList = _dbTreatmentTeamsList.Body;

                var _SpecialtiesList = await _physicianService.GetSpecialtiesListById(_physicianId);
                ViewBag.SpecialtiesEditList = _SpecialtiesList.Body;
                var _dbSpecialtiesList = await _physicianService.GetAllSpecialties();
                ViewBag.dbSpecialtiesList = _dbSpecialtiesList.Body;

                var _LanguagesList = await _physicianService.GetLanguagesListById(_physicianId);
                ViewBag.LanguagesEditList = _LanguagesList.Body;
                var _dbLanguagesList = await _physicianService.GetAllLanguages();
                ViewBag.dbLanguagesList = _dbLanguagesList.Body;

                return View(data);

            }
            return View();
        }

        [HttpPost]
        public async Task<string> EditPhysician(Physician physician)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //using (TransactionScope scope = new TransactionScope())
                //{
                ModelState.Remove("imageUpload");
                if (ModelState.IsValid)
                {
                    var js = new JavaScriptSerializer();
                    var deserializedCredentials = (object[])js.DeserializeObject(physician.physicianCredentials);
                    var deserializedDiscipline = (object[])js.DeserializeObject(physician.physicianDiscipline);
                    var deserializedSpecialties = (object[])js.DeserializeObject(physician.physicianSpecialties);
                    var deserializedTreatmentTeam = (object[])js.DeserializeObject(physician.physicianTreatmentTeam);
                    var deserializedLanguage = (object[])js.DeserializeObject(physician.physicianLanguage);
                    string encImage = "";
                    if (physician.imageUpload != null)
                    {
                        //string fileName = Path.GetFileNameWithoutExtension(physician.imageUpload.FileName);
                        //string extension = Path.GetExtension(physician.imageUpload.FileName);
                        //fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                        //physician.ProfileImage = "~/Images/" + fileName;

                        //fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        //physician.imageUpload.SaveAs(fileName);

                        string fileName = Path.GetFileNameWithoutExtension(physician.imageUpload.FileName);
                        string extension = Path.GetExtension(physician.imageUpload.FileName);
                        //fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                        //physician.ProfileImage = "~/content/profile_pictures/" + fileName;

                        //fileName = Path.Combine(Server.MapPath("~/content/profile_pictures/"), fileName);
                        //physician.imageUpload.SaveAs(fileName);
                        //encImage = TripleDESCryptography.Encrypt(physician.ProfileImage);
                        //physician.ProfileImage = encImage;

                        fileName = "content/physician_profile/" + fileName + DateTime.Now.ToString("yymmssff") + extension;
                        ///physician.ProfileImage = "~/content/physician_profile_pictures/" + fileName;
                        ///
                        string path = ConfigurationManager.AppSettings["liveUrl"] + fileName;

                        fileName = Path.Combine(Server.MapPath("~/"), fileName);
                        physician.imageUpload.SaveAs(fileName);
                        encImage = TripleDESCryptography.Encrypt(path);
                        physician.ProfileImage = encImage;
                    }
                    else
                    {
                        encImage = "";
                    }

                    if (physician.ProfileImage == "~/Content/theme/img/profile.png")
                    {
                        physician.ProfileImage = null;
                    }

                    var Result = await _physicianService.EditPhysician(physician);


                    if (Result.Code == "200")
                    {
                        Guid _PhysicianId = physician.PhysicianId;


                        var _RemovedCredentialsList = await _physicianService.DeletePhysicianCredentials(_PhysicianId);

                        if (_RemovedCredentialsList.Code == "200")
                        {
                            foreach (var item in deserializedCredentials)
                            {

                                PhysicianCredentials NewPhysicianCredentials = new PhysicianCredentials
                                {
                                    PhysicianId = _PhysicianId,
                                    CredentialsId = Guid.Parse(item.ToString()),
                                    CreatedDate = DateTime.Now,
                                    IsActive = true
                                };

                                var result = await _physicianService.AddPhysicianCredentials(NewPhysicianCredentials);
                            }
                        }


                        var _RemovedDisciplineList = await _physicianService.DeletePhysicianDiscipline(_PhysicianId);
                        if (_RemovedDisciplineList.Code == "200")
                        {
                            foreach (var item in deserializedDiscipline)
                            {
                                PhysicianDiscipline NewPhysicianDiscipline = new PhysicianDiscipline()
                                {
                                    PhysicianId = _PhysicianId,
                                    DisciplineId = Guid.Parse(item.ToString()),
                                    CreatedDate = DateTime.Now,
                                    IsActive = true
                                };
                                var result = await _physicianService.AddPhysicianDisciplines(NewPhysicianDiscipline);
                            }
                        }


                        var _RemovedLanguageList = await _physicianService.DeletePhysicianLanguage(_PhysicianId);
                        if (_RemovedLanguageList.Code == "200")
                        {
                            foreach (var item in deserializedLanguage)
                            {
                                PhysicianLanguage NewphysicianLanguage = new PhysicianLanguage()
                                {
                                    PhysicianId = _PhysicianId,
                                    LanguageId = Guid.Parse(item.ToString()),
                                    CreatedDate = DateTime.Now,
                                    IsActive = true
                                };
                                var result = await _physicianService.AddPhysicianLanguages(NewphysicianLanguage);

                            }
                        }

                        var _RemovedSpecialtiesList = await _physicianService.DeletePhysicianSpecialties(_PhysicianId);
                        if (_RemovedSpecialtiesList.Code == "200")
                        {
                            foreach (var item in deserializedSpecialties)
                            {
                                PhysicianSpecialties NewPhysicianSpecialties = new PhysicianSpecialties()
                                {
                                    PhysicianId = _PhysicianId,
                                    SpecialtyId = Guid.Parse(item.ToString()),
                                    CreatedDate = DateTime.Now,
                                    IsActive = true
                                };
                                var result = await _physicianService.AddPhysicianSpecialties(NewPhysicianSpecialties);

                            }
                        }

                        var _RemovedTreatmentList = await _physicianService.DeletePhysicianTreatmentTeam(_PhysicianId);
                        if (_RemovedTreatmentList.Code == "200")
                        {
                            foreach (var item in deserializedTreatmentTeam)
                            {
                                PhysicianTreatmentTeam NewPhysicianSpecialties = new PhysicianTreatmentTeam()
                                {
                                    PhyicianId = _PhysicianId,
                                    TreatmentTeamId = Guid.Parse(item.ToString()),
                                    CreatedDate = DateTime.Now,
                                    IsActive = true
                                };
                                var result = await _physicianService.AddPhysicianTreatmentTeams(NewPhysicianSpecialties);

                            }
                        }


                    }
                    //error here
                 //   var data = new ResponseModel { Code = Result.Code, Status = Result.Status, Message = Result.Message, Body = Result.Body, AccessToken = Result.AccessToken };
                    return JsonConvert.SerializeObject(Result);
                  //  return JsonConvert.SerializeObject(Result); try this one
                }
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));

            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                // return Json(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, null));
                //return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Exception in code", "404", null, ""));
            }

            // return RedirectToAction("physiciansManagementList");

        }




        // POST: Physician/Delete/5
        [HttpPost]
        public async Task<string> DeletePhysician(Guid PhysicianId)
        {
            try
            {
                var data = await _physicianService.DeletePhysician(PhysicianId);
                var result = new ResponseModel { Code = data.Code, Body = data.Body, Message = data.Message, Status = data.Status, AccessToken = data.AccessToken };
                return JsonConvert.SerializeObject(result);
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, ""));
            }
        }


        public async Task<ActionResult> PushNotifcation()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PushNotifcation(string one)
        {

            return View();
        }

        //public static String SendNotificationFromFirebaseCloud(string title, string body)
        //{
        //    String sResponseFromServer = null;
        //    WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //    tRequest.Method = "post";
        //    //serverKey - Key from Firebase cloud messaging server  
        //    tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA2iBKcq0:APA91bGEqi4_KzhP_axTPgko2U-SqJ81qqSMimmkeuwG3FuZA9xD2eQMUugZLF6mjAoP1lAtTjjn5vO0eVteOEV7Oq7Qyz_O57rDUJvaxcj0de-3UM_Z-vgG5_ZUgSuugDeiHvRb-Fvk"));
        //    //Sender Id - From firebase project setting  
        //    tRequest.Headers.Add(string.Format("Sender: id={0}", "936844620461"));
        //    tRequest.ContentType = "application/json";
        //    var payload = new
        //    {
        //        //  to = "cnR1YG87yR_MhIL83VqpqY:APA91bFwHudHXYnTbWzZlEXgqLri3dPg9wM-M8v0SPkLksT-xSCpQI0Q0eYn-haw7gfaKXnix3zBj5piL1-AGbpWz3nmlHy5XU7cJ3aPTKgZn4nF1YV4CDPjrXzyQy9mSX9Ph3wzpmEK",
        //        to = "/topics/BCMNotification",
        //        priority = "high",
        //        content_available = true,
        //        notification = new
        //        {
        //            body = body,
        //            title = title,
        //            badge = 1
        //        },
        //        data = new
        //        {
        //            key1 = "value1",
        //            key2 = "value2"
        //        }

        //    };

        //    string postbody = JsonConvert.SerializeObject(payload).ToString();
        //    Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
        //    tRequest.ContentLength = byteArray.Length;
        //    using (Stream dataStream = tRequest.GetRequestStream())
        //    {
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        using (WebResponse tResponse = tRequest.GetResponse())
        //        {
        //            using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //            {
        //                if (dataStreamResponse != null)
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        sResponseFromServer = tReader.ReadToEnd();
        //                        //result.Response = sResponseFromServer;
        //                    }
        //            }
        //        }
        //    }

        //    return sResponseFromServer;
        //}

    }
}
