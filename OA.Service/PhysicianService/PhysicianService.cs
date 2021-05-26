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
    [Export(typeof(IPhysicianService))]
    public class PhysicianService : IPhysicianService
    {
        private readonly IPhysicianRepository _physicianRepository;
        private readonly ILogRepository _logRepository;


        public PhysicianService() 
        {
            _physicianRepository = new PhysicianRepository();
            _logRepository = new LogRepository();
        }
        public PhysicianService(IPhysicianRepository physicianRepository, ILogRepository logRepository)
        {
            this._physicianRepository = physicianRepository;
            this._logRepository = logRepository;
        }

        public async Task<ResponseModel> GetAllLanguages()
        {
            try
            {
                var data = await _physicianRepository.GetAllLanguages();
                if (data == null)
                {
              //      await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllLanguages", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllLanguages", Description = "Successfully get language list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
           //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllLanguages", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetAllCredentials()
        {
            try
            {
                var data = await _physicianRepository.GetAllCredentials();
                if (data == null)
                {
              //      await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllCredentials", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllCredentials", Description = "Successfully get credencial list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllCredentials", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetAllDisciplines()
        {
            try
            {
                var data = await _physicianRepository.GetAllDisciplines();
                if (data == null)
                {
            //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllDisciplines", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllDisciplines", Description = "Successfully get decipline list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllDisciplines", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetAllSpecialties()
        {
            try
            {
                var data = await _physicianRepository.GetAllSpecialties();
                if (data == null)
                {
              //      await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllSpecialties", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllSpecialties", Description = "Successfully get speciality list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllSpecialties", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetAllTreatmentTeams()
        {
            try
            {
                var data = await _physicianRepository.GetAllTreatmentTeams();
                if (data == null)
                {
            //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllTreatmentTeams", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

            //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllTreatmentTeams", Description = "Successfully get treatment team list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetAllTreatmentTeams", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        

       public async Task<IQueryable<PhysicianCredentials>> checkPhysicianCredentialsList(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.checkPhysicianCredentials(Id);
                //if (data == null)
                //{
                //    //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                //    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                //}

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Successfully get treatment team list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return data;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return null;
            }
        }


        public async Task<ResponseModel> GetCredentialsListById(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysicianCredentialsList(Id);
                if (data == null)
                {
            //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Successfully get treatment team list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
           //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetSpecialtiesListById(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysicianSpecialtiesList(Id);
                if (data == null)
                {
                    //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Successfully get treatment team list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }


        public async Task<ResponseModel> GetDisciplineListById(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysicianDisciplinesList(Id);
                if (data == null)
                {
                    //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Successfully get treatment team list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetTreatmentTeamsListById(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysicianTreatmentTeamsList(Id);
                if (data == null)
                {
                    //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Successfully get treatment team list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        
        public async Task<ResponseModel> GetLanguagesListById(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysicianLanguagesList(Id);
                if (data == null)
                {
                    //        await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", Description = "Successfully get treatment team list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Success", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianCredentialsById", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        

       public async Task<ResponseModel> GetPhysicianListSimple()
        {
            try
            {
                var physicians = await _physicianRepository.GetPhysicianListWeb();
                if (physicians == null)
                {
                    //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianList", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                foreach (var item in physicians)
                {
                 //   item.DisplayTreatmentTeamId = new List<Guid>(item.TreatmentTeam.Select(z => z.TreatmentTeam.TreatmentTeamId).ToList());
                    item.DisplayTreatmentTeamTitle = string.Join(",", item.TreatmentTeam.Select(z => z.TreatmentTeam.DiseaseTitle).ToList());

                //    item.DisplayDicipilineId = new List<Guid>(item.Discipline.Select(z => z.Discipline.DisciplineId).ToList());
                    item.DisplayDisciplineTitle = string.Join(",", item.Discipline.Select(z => z.Discipline.DisciplineTitle).ToList());

                    //item.DisplayCredentialsId = new List<Guid>(item.Credentials.Select(z => z.Credentials.CredentialsId).ToList());
                    //item.DisplayCredentialsTitle = string.Join(",", item.Credentials.Select(z => z.Credentials.CredentialsTitle).ToList());

                 //   item.DisplaySpecialitiesId = new List<Guid>(item.Specialties.Select(z => z.Specialties.SpecialtyId).ToList());
                    item.DisplaySpecialtiesTitle = string.Join(",", item.Specialties.Select(z => z.Specialties.SpecialtyTitle).ToList());

                    //item.DisplayLanguageId = new List<Guid>(item.Language.Select(z => z.Language.LanguageId).ToList());
                    //item.DisplayLanguageTitle = string.Join(",", item.Language.Select(z => z.Language.LanguageTitle).ToList());

                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianList", Description = "Successfully get physician list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician List", "200", physicians, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianList", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetPhysicianList()
        {
            try
            {
                var physicians = await _physicianRepository.GetPhysicianList();
                if (physicians == null)
                {
               //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianList", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //foreach (var item in physicians)
                //{
                //    if (item.ProfileImage != null)
                //    {
                //        item.ProfileImage = TripleDESCryptography.Encrypt(item.ProfileImage);
                //        await _physicianRepository.UpdatePhysicianAsync(item);
                //    }
                //}

                foreach (var item in physicians)
                {
                    item.DisplayTreatmentTeamId = new List<Guid>(item.TreatmentTeam.Select(z => z.TreatmentTeam.TreatmentTeamId).ToList());
                    item.DisplayTreatmentTeamTitle = string.Join(",", item.TreatmentTeam.Select(z => z.TreatmentTeam.DiseaseTitle).ToList());

                    item.DisplayDicipilineId = new List<Guid>(item.Discipline.Select(z => z.Discipline.DisciplineId).ToList());
                    item.DisplayDisciplineTitle = string.Join(",", item.Discipline.Select(z => z.Discipline.DisciplineTitle).ToList());

                    item.DisplayCredentialsId = new List<Guid>(item.Credentials.Select(z => z.Credentials.CredentialsId).ToList());
                    item.DisplayCredentialsTitle = string.Join(",", item.Credentials.Select(z => z.Credentials.CredentialsTitle).ToList());

                    item.DisplaySpecialitiesId = new List<Guid>(item.Specialties.Select(z => z.Specialties.SpecialtyId).ToList());
                    item.DisplaySpecialtiesTitle = string.Join(",", item.Specialties.Select(z => z.Specialties.SpecialtyTitle).ToList());

                    item.DisplayLanguageId = new List<Guid>(item.Language.Select(z => z.Language.LanguageId).ToList());
                    item.DisplayLanguageTitle = string.Join(",", item.Language.Select(z => z.Language.LanguageTitle).ToList());

                    //item.DisplayTreatmentTeamId = new List<Guid>();
                    //foreach (var d in item.TreatmentTeam)
                    //{
                    //    item.DisplayTreatmentTeamId.Add(d.TreatmentTeamId);

                    //    if (item.DisplayTreatmentTeamTitle == null)
                    //    {
                    //        item.DisplayTreatmentTeamTitle = d.TreatmentTeam.DiseaseTitle;
                    //    }
                    //    else
                    //    {
                    //        item.DisplayTreatmentTeamTitle += ", " + d.TreatmentTeam.DiseaseTitle;
                    //    }
                    //}

                    //item.DisplayDicipilineId = new List<Guid>();
                    //foreach (var d in item.Discipline)
                    //{
                    //    item.DisplayDicipilineId.Add(d.DisciplineId);

                    //    if (item.DisplayDisciplineTitle == null)
                    //    {
                    //        item.DisplayDisciplineTitle = d.Discipline.DisciplineTitle;
                    //    }
                    //    else
                    //    {
                    //        item.DisplayDisciplineTitle += ", " + d.Discipline.DisciplineTitle;
                    //    }
                    //}

                    //item.DisplayCredentialsId = new List<Guid>();
                    //foreach (var d in item.Credentials)
                    //{
                    //    item.DisplayCredentialsId.Add(d.CredentialsId);

                    //    if (item.DisplayCredentialsTitle == null)
                    //    {
                    //        item.DisplayCredentialsTitle = d.Credentials.CredentialsTitle;
                    //    }
                    //    else
                    //    {
                    //        item.DisplayCredentialsTitle += ", " + d.Credentials.CredentialsTitle;
                    //    }
                    //}

                    //item.DisplaySpecialitiesId = new List<Guid>();
                    //foreach (var d in item.Specialties)
                    //{
                    //    item.DisplaySpecialitiesId.Add(d.SpecialtyId);

                    //    if (item.DisplaySpecialtiesTitle == null)
                    //    {
                    //        item.DisplaySpecialtiesTitle = d.Specialties.SpecialtyTitle;
                    //    }
                    //    else
                    //    {
                    //        item.DisplaySpecialtiesTitle += ", " + d.Specialties.SpecialtyTitle;
                    //    }
                    //}

                    //item.DisplayLanguageId = new List<Guid>();
                    //foreach (var d in item.Language)
                    //{
                    //    item.DisplayLanguageId.Add(d.LanguageId);

                    //    if (item.DisplayLanguageTitle == null)
                    //    {
                    //        item.DisplayLanguageTitle = d.Language.LanguageTitle;
                    //    }
                    //    else
                    //    {
                    //        item.DisplayLanguageTitle += ", " + d.Language.LanguageTitle;
                    //    }
                    //}
                }
       
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianList", Description = "Successfully get physician list.", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician List", "200", physicians, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianList", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> GetPhysicianDetails(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysicianDetails(Id);
                if (data == null)
                {
              //      await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianDetails", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

            //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianDetails", Description = "Successfully get physician details", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianDetails", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetPhysicianDetailsSimple(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysicianDetailsWeb(Id);
                if (data == null)
                {
                    //      await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianDetails", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianDetails", Description = "Successfully get physician details", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianDetails", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetPhysicianByName(string name)
        {
            try
            {
                var data = await _physicianRepository.GetPhysicianByName(name);
                if (data == null)
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianByName", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianByName", Description = "Successfully get physicians by name", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Search By Name", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysicianByName", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetPhysiciansBySpecialty(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysiciansBySpecialty(Id);
                if (data == null)
                {
             //       await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansBySpecialty", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansBySpecialty", Description = "Successfully get physicians by specialty", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Search By Speciality", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansBySpecialty", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetSpecialties(Guid id)
        {

            try
            {
                var data = await _physicianRepository.GetSpecialtiesById(id);
                if (data == null)
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", Description = "Successfully get physicians by specialty", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Search By Speciality", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetDiscipline(Guid id)
        {

            try
            {
                var data = await _physicianRepository.GetDisciplineById(id);
                if (data == null)
                {
                    //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", Description = "Successfully get physicians by specialty", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Search By Speciality", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetTreatmentTeams(Guid id)
        {

            try
            {
                var data = await _physicianRepository.GetTreatmentTeamById(id);
                if (data == null)
                {
                    //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", Description = "Successfully get physicians by specialty", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Search By Speciality", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetCredentials(Guid id)
        {

            try
            {
                var data = await _physicianRepository.GetCredentialsById(id);
                if (data == null)
                {
                    //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

                //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", Description = "Successfully get physicians by specialty", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Search By Speciality", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetSpecialties", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetPhysiciansByDiscipline(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysiciansByDiscipline(Id);
                if (data == null)
                {
                 //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansByDiscipline", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansByDiscipline", Description = "Successfully get physicians by discipline", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Search By Discipline", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansByDiscipline", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> GetPhysiciansByTreatmentTeam(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.GetPhysiciansByTreatmentTeam(Id);
                if (data == null)
                {
                  //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansByTreatmentTeam", Description = "Failed: Record not found", EventType = EventType.Get, CreatedDate = DateTime.Now, CreatedBy = null, IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Record not found", "404", null, null);
                }

             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansByTreatmentTeam", Description = "Successfully get physicians by treatment team", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Search By Treatment Team", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "GetPhysiciansByTreatmentTeam", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> DeletePhysician(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeletePhysicianById(Id);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysician", Description = "Successfully removed physician", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Physician Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysician", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> DeleteCredential(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeleteCredentialById(Id);
            //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeleteCredential", Description = "Successfully removed Credential", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Credential Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeleteCredential", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }


        public async Task<ResponseModel> DeletePhysicianCredentials(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeletePhysicianCredentialsById(Id);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianCredentials", Description = "Successfully removed PhysicianCredentials", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Credential Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianCredentials", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }
        public async Task<ResponseModel> DeletePhysicianDiscipline(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeletePhysicianDisciplineById(Id);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianDiscipline", Description = "Successfully removed PhysicianDiscipline", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Credential Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianDiscipline", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> DeletePhysicianLanguage(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeletePhysicianLanguageById(Id);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianLanguage", Description = "Successfully removed PhysicianLanguage", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Credential Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianLanguage", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> DeletePhysicianSpecialties(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeletePhysicianSpecialtiesById(Id);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianSpecialties", Description = "Successfully removed PhysicianSpecialties", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Credential Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianSpecialties", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> DeletePhysicianTreatmentTeam(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeletePhysicianTreatmentTeamById(Id);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianTreatmentTeam", Description = "Successfully removed PhysicianTreatmentTeam", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Credential Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeletePhysicianTreatmentTeam", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }




        public async Task<ResponseModel> DeleteDiscipline(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeleteDisciplineById(Id);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeleteDiscipline", Description = "Successfully removed Credential", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get discipline Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeleteDiscipline", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> DeleteTreatment(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeleteTreatmentById(Id);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeleteTreatment", Description = "Successfully removed Credential", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get treatment Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeleteTreatment", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }

        public async Task<ResponseModel> DeleteSpecialty(Guid Id)
        {
            try
            {
                var data = await _physicianRepository.DeleteSpecialtyById(Id);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeleteSpecialty", Description = "Successfully removed Credential", EventType = EventType.Get, CreatedDate = DateTime.Now, IsActive = true });
                return ResponseHandler.GetResponse("Success", "Get Specialty Details", "200", data, null);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "DeleteSpecialty", EventType = EventType.Get, Description = "Exception: " + ex.Message, CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }
        }



        public async Task<ResponseModel> AddPhysician(Physician physician)
        {


            try
            {
                var response = await _physicianRepository.AddNewPhysician(physician);
                if (response.Code == "200")
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysician", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", physician.FirstName, physician.LastName), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", physician, null);
                }
                else
                {
                 //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysician", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", physician.FirstName, physician.LastName), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", physician, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysician", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", physician.FirstName, physician.LastName), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }

           
        }
   
        

        public async Task<ResponseModel> AddPhysicianCredentials(PhysicianCredentials physicianC)
        {


            try
            {
                var response = await _physicianRepository.AddNewPhysicianCredentials(physicianC);
                if (response.Code == "200")
                {
              //      await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysician", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianC, null);
                }
                else
                {
               //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysician", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", physicianC, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysician", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }

        public async Task<ResponseModel> AddPhysicianDisciplines(PhysicianDiscipline physicianD)
        {


            try
            {
                var response = await _physicianRepository.AddNewPhysicianDiscipline(physicianD);
                if (response.Code == "200")
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianDisciplines", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianD, null);
                }
                else
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianDisciplines", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", physicianD, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianDisciplines", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }

        public async Task<ResponseModel> AddPhysicianLanguages(PhysicianLanguage physicianL)
        {


            try
            {
                var response = await _physicianRepository.AddNewPhysicianLanguages(physicianL);
                if (response.Code == "200")
                {
                 //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianLanguages", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianL, null);
                }
                else
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianLanguages", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", physicianL, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
               // await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianLanguages", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }

        public async Task<ResponseModel> AddPhysicianSpecialties(PhysicianSpecialties physicianS)
        {


            try
            {
                var response = await _physicianRepository.AddNewPhysicianSpecialties(physicianS);
                if (response.Code == "200")
                {
               //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianSpecialties", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianS, null);
                }
                else
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianSpecialties", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", physicianS, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianSpecialties", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }

        public async Task<ResponseModel> AddPhysicianTreatmentTeams(PhysicianTreatmentTeam physicianL)
        {


            try
            {
                var response = await _physicianRepository.AddNewPhysicianTreatmentTeam(physicianL);
                if (response.Code == "200")
                {
             //       await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianTreatmentTeams", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianL, null);
                }
                else
                {
               //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianTreatmentTeams", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", physicianL, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddPhysicianTreatmentTeams", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0},{1}", null, null), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }

        

     public async Task<ResponseModel> EditSpecialty(Specialties specialties)
        {
            try
            {
                var _specialties = await _physicianRepository.GetSpecialtiesList();
                if (_specialties.Count() > 0)
                {
                    var IsAlreadyExists = _specialties.Where(x => x.SpecialtyTitle == specialties.SpecialtyTitle).FirstOrDefault();
                    if (IsAlreadyExists != null)
                    {
                        //await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Title already exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "This record already exists. Use a different title instead.", "404", specialties, null);
                    }
                }

                var response = await _physicianRepository.EditNewSpecialty(specialties);
                if (response.Code == "200")
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditSpecialty", Description = "Specialties record updated in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", specialties.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", response, null);
                }
                else
                {
                 //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditSpecialty", Description = "Specialties update in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", specialties.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", response, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditSpecialty", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", specialties.ModifiedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }


        public async Task<ResponseModel> EditDiscipline(Discipline discipline)
        {


            try
            {
                var _discipline = await _physicianRepository.GetDisciplineList();
                if (_discipline.Count() > 0)
                {
                    var IsAlreadyExists = _discipline.Where(x => x.DisciplineTitle == discipline.DisciplineTitle).FirstOrDefault();
                    if (IsAlreadyExists != null)
                    {
                        //await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Title already exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "This record already exists. Use a different title instead.", "404", discipline, null);
                    }
                }

                var response = await _physicianRepository.EditNewDiscipline(discipline);
                if (response.Code == "200")
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditDiscipline", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", discipline.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", discipline, null);
                }
                else
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditDiscipline", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", discipline.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", discipline, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditDiscipline", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", discipline.ModifiedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }


        public async Task<ResponseModel> EditTreatmentTeams(TreatmentTeam treatmentTeam)
        {


            try
            {
                var _treatment = await _physicianRepository.GetTreatmentTeamList();
                if (_treatment.Count() > 0)
                {
                    var IsAlreadyExists = _treatment.Where(x => x.DiseaseTitle == treatmentTeam.DiseaseTitle).FirstOrDefault();
                    if (IsAlreadyExists != null)
                    {
                        //await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Title already exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "This record already exists. Use a different title instead.", "404", treatmentTeam, null);
                    }
                }

                var response = await _physicianRepository.EditNewTreatmentTeam(treatmentTeam);
                if (response.Code == "200")
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditTreatmentTeams", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", treatmentTeam.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", treatmentTeam, null);
                }
                else
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditTreatmentTeams", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", treatmentTeam.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", treatmentTeam, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditTreatmentTeams", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", treatmentTeam.ModifiedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }


        public async Task<ResponseModel> EditCredentials(Credentials credentials)
        {


            try
            {
                var _credentials = await _physicianRepository.GetCredentialsList();
                if (_credentials.Count() > 0)
                {
                    var IsAlreadyExists = _credentials.Where(x => x.CredentialsTitle == credentials.CredentialsTitle).FirstOrDefault();
                    if (IsAlreadyExists != null)
                    {
                        //await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Title already exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "This record already exists. Use a different title instead.", "404", credentials, null);
                    }
                }

                var response = await _physicianRepository.EditNewCredentials(credentials);
                if (response.Code == "200")
                {
                 //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditTreatmentTeams", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", credentials.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", credentials, null);
                }
                else
                {
               //     await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditTreatmentTeams", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", credentials.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", credentials, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditTreatmentTeams", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", credentials.ModifiedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }


        public async Task<ResponseModel> EditPhysician(Physician physician)
        {


            try
            {
                var response = await _physicianRepository.EditPhysicianById(physician);
                if (response.Code == "200")
                {
                 //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditPhysician", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", physician.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", null, null);
                }
                else
                {
                 //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditPhysician", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", physician.ModifiedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "EditPhysician", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", physician.ModifiedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }

        

        public async Task<ResponseModel> AddSpecialty(Specialties specialties)
        {


            try
            {
                var _specialties = await _physicianRepository.GetSpecialtiesList();
                if(_specialties.Count() > 0)
                {
                    var IsAlreadyExists = _specialties.Where(x => x.SpecialtyTitle.ToLower() == specialties.SpecialtyTitle.ToLower()).FirstOrDefault();
                    if (IsAlreadyExists != null)
                    {
                        //await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Title already exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "This record already exists. Use a different title instead.", "404", specialties, null);
                    }
                }

                var response = await _physicianRepository.AddNewSpecialty(specialties);
                if (response.Code == "200")
                {
                 //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddSpecialty", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", specialties.CreatedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", specialties, null);
                }
                else
                {
                  //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddSpecialty", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", specialties.CreatedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", specialties, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddSpecialty", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", specialties.CreatedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }


        public async Task<ResponseModel> AddDisciplines(Discipline discipline)
        {


            try
            {
                var _discipline = await _physicianRepository.GetDisciplineList();
                if (_discipline.Count() > 0)
                {
                    var IsAlreadyExists = _discipline.Where(x => x.DisciplineTitle.ToLower() == discipline.DisciplineTitle.ToLower()).FirstOrDefault();
                    if (IsAlreadyExists != null)
                    {
                        //await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Title already exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "This record already exists. Use a different title instead.", "404", discipline, null);
                    }
                }

                var response = await _physicianRepository.AddNewDiscipline(discipline);
                if (response.Code == "200")
                {
                  //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddDisciplines", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", discipline.CreatedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", discipline, null);
                }
                else
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddDisciplines", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", discipline.CreatedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", discipline, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddDisciplines", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", discipline.CreatedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }

        public async Task<ResponseModel> AddTreatmentTeam(TreatmentTeam treatment)
        {


            try
            {
                var _treatment = await _physicianRepository.GetTreatmentTeamList();
                if (_treatment.Count() > 0)
                {
                    var IsAlreadyExists = _treatment.Where(x => x.DiseaseTitle.ToLower() == treatment.DiseaseTitle.ToLower()).FirstOrDefault();
                    if (IsAlreadyExists != null)
                    {
                        //await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Title already exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "This record already exists. Use a different title instead.", "404", treatment, null);
                    }
                }

                var response = await _physicianRepository.AddNewTreatmentTeam(treatment);
                if (response.Code == "200")
                {
                //    await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddTreatmentTeam", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", treatment.CreatedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", treatment, null);
                }
                else
                {
                 //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddTreatmentTeam", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", treatment.CreatedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", treatment, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
              //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddTreatmentTeam", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", treatment.CreatedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }


        public async Task<ResponseModel> AddCredential(Credentials credentials)
        {


            try
            {

                var _credentials = await _physicianRepository.GetCredentialsList();
                if (_credentials.Count() > 0)
                {
                    var IsAlreadyExists = _credentials.Where(x => x.CredentialsTitle.ToLower() == credentials.CredentialsTitle.ToLower()).FirstOrDefault();
                    if (IsAlreadyExists != null)
                    {
                        //await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "Login", EventType = EventType.Login, Description = "Title already exists", CreatedDate = DateTime.Now, CreatedBy = "", IsActive = true });
                        return ResponseHandler.GetResponse("Failed", "This record already exists. Use a different title instead.", "404", credentials, null);
                    }
                }

                var response = await _physicianRepository.AddNewCredentials(credentials);
                if (response.Code == "200")
                {
              //      await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddCredential", Description = "Physician record inserted in database", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", credentials.CreatedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Success", "Inserted", "200", credentials, null);
                }
                else
                {
                  //  await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddCredential", Description = "Physician insertion in database Failed", EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", credentials.CreatedBy), IsActive = true });
                    return ResponseHandler.GetResponse("Failed", "Entity is null", "404", credentials, null);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
             //   await _logRepository.InsertAsync(new Logs() { UserId = null, Operation = "AddCredential", Description = "Exception: " + ex.Message, EventType = EventType.Add, CreatedDate = DateTime.Now, CreatedBy = string.Format("{0}", credentials.CreatedBy), IsActive = true });
                return ResponseHandler.GetResponse("Exception", ConstantMessages.SomethingWentWrong, "404", null, "");
            }


        }










    }


}
