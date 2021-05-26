using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Helper;
using OA.Data.Model;
using OA.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OA.Repository
{
    public class PhysicianRepository : IPhysicianRepository
    {
        private readonly ApplicationContext _dContext;
        public PhysicianRepository()
        {
            _dContext = new ApplicationContext();
        }

        public async Task<IQueryable<Language>> GetAllLanguages()
        {
            var data = await _dContext.Language.Where(x => x.IsActive == true).ToListAsync();
                        if (data.Count != 0)
                return data.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Credentials>> GetAllCredentials()
        {
            var data = await _dContext.Credentials.Where(x => x.IsActive == true).ToListAsync();
            if (data.Count != 0)
                return data.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Discipline>> GetAllDisciplines()
        {
            var data = await _dContext.Discipline.Where(x => x.IsActive == true).ToListAsync();
            if (data.Count != 0)
                return data.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Specialties>> GetAllSpecialties()
        {
            var data = await _dContext.Specialties.Where(x => x.IsActive == true).ToListAsync();
            if (data.Count != 0)
                return data.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<TreatmentTeam>> GetAllTreatmentTeams()
        {
            var data = await _dContext.TreatmentTeam.Where(x => x.IsActive == true).ToListAsync();
            if (data.Count != 0)
                return data.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Physician>> GetPhysicianList()
        {
            var physician = await _dContext.Physician.Where(x => x.IsActive == true && x.Status == PhysicianStatus.Published).ToListAsync();

            if (physician.Count != 0)
                return physician.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Physician>> GetPhysicianListWeb()
        {
            var physician = await _dContext.Physician.Where(x => x.IsActive == true).OrderByDescending(x => x.CreatedDate).ToListAsync();
            if (physician.Count != 0)
                return physician.AsQueryable();
            else
                return null;
        }

        public async Task<Physician> GetPhysicianDetails(Guid Id)
        {
            var physician = await _dContext.Physician.Where(x => x.PhysicianId == Id && x.IsActive == true && x.Status == PhysicianStatus.Published).FirstOrDefaultAsync();
            return physician;
        }

        public async Task<Physician> GetPhysicianDetailsWeb(Guid Id)
        {
            var physician = await _dContext.Physician.Where(x => x.PhysicianId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            //var DirectPhoneFormat = String.Format("{0:(###) ###-####}", physician.DirectPhone);
            //physician.DirectPhone = DirectPhoneFormat;
            return physician;
        }


        public async Task<IQueryable<PhysicianCredentials>> checkPhysicianCredentials(Guid Id)
        {
            var physician = await _dContext.PhysicianCredentials.Where(x => x.PhysicianId.Equals(Id) && x.IsActive == true).ToListAsync();
            if (physician.Count != 0)
                return physician.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Credentials>> GetPhysicianCredentialsList(Guid Id)
        {
            var PhysicianCredentials = await (from c in _dContext.Credentials
                      join pc in _dContext.PhysicianCredentials on c.CredentialsId equals pc.CredentialsId
                      where pc.PhysicianId.Equals(Id) && pc.IsActive == true
                      select new  
                      {
                         c.CredentialsId,
                         c.CredentialsTitle
                      }).ToListAsync();

            List<Credentials> CredentialsList = new List<Credentials>();

            foreach (var item in PhysicianCredentials)
            {
                CredentialsList.Add(new Credentials { CredentialsId = item.CredentialsId, CredentialsTitle = item.CredentialsTitle });
            }

            if (PhysicianCredentials.Count != 0)
                return CredentialsList.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Discipline>> GetPhysicianDisciplinesList(Guid Id)
        {
            var PhysicianDiscipline = await (from c in _dContext.Discipline
                                         join pc in _dContext.PhysicianDiscipline on c.DisciplineId equals pc.DisciplineId
                                         where pc.PhysicianId.Equals(Id) && pc.IsActive == true
                                             select new
                                         {
                                            c.DisciplineId,
                                            c.DisciplineTitle
                                         }).ToListAsync();

            List<Discipline> DisciplineList = new List<Discipline>();

            foreach (var item in PhysicianDiscipline)
            {
                DisciplineList.Add(new Discipline { DisciplineId = item.DisciplineId, DisciplineTitle = item.DisciplineTitle });
            }

            if (PhysicianDiscipline.Count != 0)
                return DisciplineList.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Specialties>> GetPhysicianSpecialtiesList(Guid Id)
        {
            var PhysicianSpecialties = await (from c in _dContext.Specialties
                                             join pc in _dContext.PhysicianSpecialties on c.SpecialtyId equals pc.SpecialtyId
                                             where pc.PhysicianId.Equals(Id) && pc.IsActive == true
                                              select new
                                             {
                                                 c.SpecialtyId,
                                                 c.SpecialtyTitle
                                             }).ToListAsync();

            List<Specialties> SpecialtiesList = new List<Specialties>();

            foreach (var item in PhysicianSpecialties)
            {
                SpecialtiesList.Add(new Specialties { SpecialtyId = item.SpecialtyId, SpecialtyTitle = item.SpecialtyTitle });
            }

            if (PhysicianSpecialties.Count != 0)
                return SpecialtiesList.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<TreatmentTeam>> GetPhysicianTreatmentTeamsList(Guid Id)
        {
            var PhysicianTreatmentTeam = await (from c in _dContext.TreatmentTeam
                                              join pc in _dContext.PhysicianTreatmentTeam on c.TreatmentTeamId equals pc.TreatmentTeamId
                                              where pc.PhyicianId.Equals(Id) && pc.IsActive == true
                                                select new
                                              {
                                                  c.TreatmentTeamId,
                                                  c.DiseaseTitle
                                              }).ToListAsync();

            List<TreatmentTeam> TreatmentTeamList = new List<TreatmentTeam>();

            foreach (var item in PhysicianTreatmentTeam)
            {
                TreatmentTeamList.Add(new TreatmentTeam { TreatmentTeamId = item.TreatmentTeamId, DiseaseTitle = item.DiseaseTitle });
            }

            if (PhysicianTreatmentTeam.Count != 0)
                return TreatmentTeamList.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Language>> GetPhysicianLanguagesList(Guid Id)
        {
            var PhysicianLanguage = await (from c in _dContext.Language
                                               join pc in _dContext.PhysicianLanguage on c.LanguageId equals pc.LanguageId
                                               where pc.PhysicianId.Equals(Id) && pc.IsActive == true
                                           select new
                                               {
                                                   c.LanguageId,
                                                   c.LanguageTitle
                                               }).ToListAsync();

            List<Language> LanguageList = new List<Language>();

            foreach (var item in PhysicianLanguage)
            {
                LanguageList.Add(new Language { LanguageId = item.LanguageId, LanguageTitle = item.LanguageTitle });
            }

            if (PhysicianLanguage.Count != 0)
                return LanguageList.AsQueryable();
            else
                return null;
        }

        public async Task<Physician> DeletePhysicianById(Guid Id)
        {
            var physician = await _dContext.Physician.Where(x => x.PhysicianId ==Id && x.IsActive == true).FirstOrDefaultAsync();
            if(physician != null)
            {
                physician.IsActive = false;
                 _dContext.Entry(physician).State = EntityState.Deleted;
                await _dContext.SaveChangesAsync();
            }

            return physician;
        }


        public async Task<Credentials> DeleteCredentialById(Guid Id)
        {
            var Credentials = await _dContext.Credentials.Where(x => x.CredentialsId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            if (Credentials != null)
            {
                Credentials.IsActive = false;
                _dContext.Entry(Credentials).State = EntityState.Deleted;
                await _dContext.SaveChangesAsync();
            }

            return Credentials;
        }

        public async Task<Discipline> DeleteDisciplineById(Guid Id)
        {
            var Discipline = await _dContext.Discipline.Where(x => x.DisciplineId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            if (Discipline != null)
            {
                 Discipline.IsActive = false;
                _dContext.Entry(Discipline).State = EntityState.Deleted;
                await _dContext.SaveChangesAsync();
            }

            return Discipline;
        }

        public async Task<TreatmentTeam> DeleteTreatmentById(Guid Id)
        {
            var TreatmentTeam = await _dContext.TreatmentTeam.Where(x => x.TreatmentTeamId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            if (TreatmentTeam != null)
            {
                  TreatmentTeam.IsActive = false;
                _dContext.Entry(TreatmentTeam).State = EntityState.Deleted;
                await _dContext.SaveChangesAsync();
            }

            return TreatmentTeam;
        }
        public async Task<Specialties> DeleteSpecialtyById(Guid Id)
        {
            var Specialties = await _dContext.Specialties.Where(x => x.SpecialtyId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            if (Specialties != null)
            {
                Specialties.IsActive = false;
                _dContext.Entry(Specialties).State = EntityState.Deleted;
                await _dContext.SaveChangesAsync();
            }

            return Specialties;
        }
      

        public async Task<ResponseModel> AddNewPhysician(Physician physician)
        {
            if (physician == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(physician).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", physician, null);
        }

        public async Task<ResponseModel> EditNewSpecialty(Specialties specialties)
        {
            if (specialties == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
      
                var data = _dContext.Specialties.Find(specialties.SpecialtyId);

            if(data != null)
            {
                data.SpecialtyTitle = specialties.SpecialtyTitle;
                data.ModifiedDate = DateTime.Now;
                //data.ModifiedBy = Session["UserId"].ToString()

                _dContext.Entry(data).State = EntityState.Modified;
                await _dContext.SaveChangesAsync();
                return ResponseHandler.GetResponse("Success", "Updated", "200", data, null);
            }


            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
        }

        public async Task<ResponseModel> EditNewDiscipline(Discipline discipline)
        {
            if (discipline == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            var data = _dContext.Discipline.Find(discipline.DisciplineId);

            if(data != null)
            {
                data.DisciplineTitle = discipline.DisciplineTitle;
                data.ModifiedDate = DateTime.Now;
                //data.ModifiedBy = Session["UserId"].ToString()

                _dContext.Entry(data).State = EntityState.Modified;
                await _dContext.SaveChangesAsync();
                return ResponseHandler.GetResponse("Success", "Updated", "200", data, null);
            }

            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
        }


        public async Task<ResponseModel> EditNewTreatmentTeam(TreatmentTeam treatment)
        {
            if (treatment == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            var data = _dContext.TreatmentTeam.Find(treatment.TreatmentTeamId);

            if(data != null)
            {
                data.DiseaseTitle = treatment.DiseaseTitle;
                data.ModifiedDate = DateTime.Now;
                //data.ModifiedBy = Session["UserId"].ToString()

                _dContext.Entry(data).State = EntityState.Modified;
                await _dContext.SaveChangesAsync();
                return ResponseHandler.GetResponse("Success", "Updated", "200", data, null);
            }

            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
        }


        public async Task<ResponseModel> EditNewCredentials(Credentials credentials)
        {
            if (credentials == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            var data = _dContext.Credentials.Find(credentials.CredentialsId);

            if(data != null)
            {
                data.CredentialsTitle = credentials.CredentialsTitle;
                data.ModifiedDate = DateTime.Now;
                //data.ModifiedBy = Session["UserId"].ToString()

                _dContext.Entry(data).State = EntityState.Modified;
                await _dContext.SaveChangesAsync();
                return ResponseHandler.GetResponse("Success", "Updated", "200", data, null);
            }
            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

        }

        public async Task<ResponseModel> EditPhysicianById(Physician physician)
        {
            if (physician == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            var data = _dContext.Physician.Find(physician.PhysicianId);

            if (data != null)
            {
                data.Status = physician.Status;
                data.FirstName = physician.FirstName;
                data.LastName = physician.LastName;
                data.DirectPhone = physician.DirectPhone;
                data.OfficePhone = physician.OfficePhone;
                data.Email = physician.Email;
                //  data.JoiningDate = physician.JoiningDate;
                data.JoiningDate = physician.JoiningDate == null || String.IsNullOrEmpty(physician.JoiningDate.ToString()) ? DateTime.Now : physician.JoiningDate;
                data.ModifiedDate = DateTime.Now;
                //data.ModifiedBy = Session["UserId"].ToString();
                data.ProfileImage = physician.ProfileImage;
                //data.ProfileImage = physician.ProfileImage == "" ? null : Images.Base64ToImage(physician.ProfileImage, Server);
                _dContext.Entry(data).State = EntityState.Modified;
                await _dContext.SaveChangesAsync();


                return ResponseHandler.GetResponse("Success", "Updated", "200", data, null);
            }
            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

        }

        

        public async Task<ResponseModel> AddNewSpecialty(Specialties specialties)
        {
            if (specialties == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(specialties).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", specialties, null);
        }


        
        public async Task<ResponseModel> AddNewDiscipline(Discipline discipline)
        {
            if (discipline == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(discipline).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", discipline, null);
        }

        public async Task<ResponseModel> AddNewTreatmentTeam(TreatmentTeam treatment)
        {
            if (treatment == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(treatment).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", treatment, null);
        }

        public async Task<ResponseModel> AddNewCredentials(Credentials credentials)
        {
            if (credentials == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(credentials).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", credentials, null);
        }


        

        public async Task<ResponseModel> AddNewPhysicianCredentials(PhysicianCredentials physicianC)
        {
            if (physicianC == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(physicianC).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianC, null);
        }


        public async Task<ResponseModel> AddNewPhysicianDiscipline(PhysicianDiscipline physicianD)
        {
            if (physicianD == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(physicianD).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianD, null);
        }

        public async Task<ResponseModel> AddNewPhysicianLanguages(PhysicianLanguage physicianL)
        {
            if (physicianL == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(physicianL).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianL, null);
        }

        public async Task<ResponseModel> AddNewPhysicianSpecialties(PhysicianSpecialties physicianS)
        {
            if (physicianS == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(physicianS).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianS, null);
        }

        public async Task<ResponseModel> AddNewPhysicianTreatmentTeam(PhysicianTreatmentTeam physicianT)
        {
            if (physicianT == null)
                return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);

            _dContext.Entry(physicianT).State = EntityState.Added;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Inserted", "200", physicianT, null);
        }
        




        public async Task<IQueryable<Physician>> GetPhysicianByName(string name)
        {
            var physician = await _dContext.Physician.Where(x => (x.FirstName.Equals(name) || x.LastName.Equals(name) || (x.FirstName + " " + x.LastName).Contains(name)) && x.IsActive == true).ToListAsync();
            if (physician.Count != 0)
                return physician.AsQueryable();
            else
                return null;
        }

        public async Task<IQueryable<Physician>> GetPhysiciansBySpecialty(Guid Id)
        {
            var Physicians = await _dContext.PhysicianSpecialties.Where(x => x.SpecialtyId == Id && x.IsActive == true).Select(z => z.PhysicianId).ToListAsync();
            if (Physicians.Count != 0)
            {
                var data = await _dContext.Physician.Where(x => Physicians.Contains(x.PhysicianId) && x.IsActive == true && x.Status == PhysicianStatus.Published).ToListAsync();
                if (data.Count != 0)
                    return data.AsQueryable();
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<Specialties> GetSpecialtiesById(Guid Id)
        {
            var data = await _dContext.Specialties.Where(x => x.SpecialtyId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            if (data != null)
            {
               return data;   
            }
            else
                return null;
        }

        public async Task<Discipline> GetDisciplineById(Guid Id)
        {
            var data = await _dContext.Discipline.Where(x => x.DisciplineId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            if (data != null)
            {
                return data;
            }
            else
                return null;
        }


        public async Task<TreatmentTeam> GetTreatmentTeamById(Guid Id)
        {
            var data = await _dContext.TreatmentTeam.Where(x => x.TreatmentTeamId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            if (data != null)
            {
                return data;
            }
            else
                return null;
        }


        public async Task<Credentials> GetCredentialsById(Guid Id)
        {
            var data = await _dContext.Credentials.Where(x => x.CredentialsId.Equals(Id) && x.IsActive == true).FirstOrDefaultAsync();
            if (data != null)
            {
                return data;
            }
            else
                return null;
        }

        public async Task<IQueryable<Physician>> GetPhysiciansByDiscipline(Guid Id)
        {
            var Physicians = await _dContext.PhysicianDiscipline.Where(x => x.DisciplineId == Id && x.IsActive == true).Select(z => z.PhysicianId).ToListAsync();
            if(Physicians.Count != 0)
            {
                var data = await _dContext.Physician.Where(x => Physicians.Contains(x.PhysicianId) && x.IsActive == true && x.Status == PhysicianStatus.Published).ToListAsync();
                if (data.Count != 0)
                    return data.AsQueryable();
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<IQueryable<Physician>> GetPhysiciansByTreatmentTeam(Guid Id)
        {
            var Physicians = await _dContext.PhysicianTreatmentTeam.Where(x => x.TreatmentTeamId ==Id && x.IsActive == true).Select(z => z.PhyicianId).ToListAsync();
            if (Physicians.Count != 0)
            {
                var data = await _dContext.Physician.Where(x => Physicians.Contains(x.PhysicianId) && x.IsActive == true && x.Status == PhysicianStatus.Published).ToListAsync();
                if (data.Count != 0)
                    return data.AsQueryable();
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<ResponseModel> UpdatePhysicianAsync(Physician entity)
        {
            //_dContext.Physician.AddRange(entity);
            _dContext.Entry(entity).State = EntityState.Modified;
            await _dContext.SaveChangesAsync();
            return ResponseHandler.GetResponse("Success", "Updated", "200", entity, null);
        }


        public async Task<ResponseModel> DeletePhysicianCredentialsById(Guid Id)
        {
            var physicianCredentials = await _dContext.PhysicianCredentials.Where(x => x.PhysicianId.Equals(Id)).ToListAsync();
            if (physicianCredentials != null)
            {
                physicianCredentials.ForEach(x => _dContext.Entry(x).State = EntityState.Deleted);

                //     _dContext.Entry(PhysicianCredentials).State = EntityState.Deleted;
                _dContext.SaveChanges();
                return ResponseHandler.GetResponse("Success", "Removed", "200", physicianCredentials, null);
            }

            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
        }

        public async Task<ResponseModel> DeletePhysicianDisciplineById(Guid Id)
        {
            var physicianDiscipline = await _dContext.PhysicianDiscipline.Where(x => x.PhysicianId.Equals(Id)).ToListAsync();
            if (physicianDiscipline != null)
            {
                physicianDiscipline.ForEach(x => _dContext.Entry(x).State = EntityState.Deleted);

                //     _dContext.Entry(PhysicianCredentials).State = EntityState.Deleted;
                _dContext.SaveChanges();
                return ResponseHandler.GetResponse("Success", "Removed", "200", physicianDiscipline, null);
            }

            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
        }

        public async Task<ResponseModel> DeletePhysicianLanguageById(Guid id)
        {
            var physicianLanguage = await _dContext.PhysicianLanguage.Where(x => x.PhysicianId.Equals(id)).ToListAsync();
            if (physicianLanguage != null)
            {
                physicianLanguage.ForEach(x => _dContext.Entry(x).State = EntityState.Deleted);

                //     _dContext.Entry(PhysicianCredentials).State = EntityState.Deleted;
                _dContext.SaveChanges();
                return ResponseHandler.GetResponse("Success", "Removed", "200", physicianLanguage, null);
            }

            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
        }

        public async Task<ResponseModel> DeletePhysicianSpecialtiesById(Guid id)
        {
            var physicianSpecialties = await _dContext.PhysicianSpecialties.Where(x => x.PhysicianId.Equals(id)).ToListAsync();
            if (physicianSpecialties != null)
            {
                physicianSpecialties.ForEach(x => _dContext.Entry(x).State = EntityState.Deleted);

                //     _dContext.Entry(PhysicianCredentials).State = EntityState.Deleted;
                _dContext.SaveChanges();
                return ResponseHandler.GetResponse("Success", "Removed", "200", physicianSpecialties, null);
            }

            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
        }

        public async Task<ResponseModel> DeletePhysicianTreatmentTeamById(Guid id)
        {
            var physicianTreatmentTeam = await _dContext.PhysicianTreatmentTeam.Where(x => x.PhyicianId.Equals(id)).ToListAsync();
            if (physicianTreatmentTeam != null)
            {
                physicianTreatmentTeam.ForEach(x => _dContext.Entry(x).State = EntityState.Deleted);

                //     _dContext.Entry(PhysicianCredentials).State = EntityState.Deleted;
                _dContext.SaveChanges();
                return ResponseHandler.GetResponse("Success", "Removed", "200", physicianTreatmentTeam, null);
            }

            return ResponseHandler.GetResponse("Failed", "Entity is null", "404", null, null);
        }


        public async Task<IQueryable<Specialties>> GetSpecialtiesList()
        {
            try
            {
                var data = await _dContext.Specialties.Where(x => x.IsActive == true).ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<IQueryable<Discipline>> GetDisciplineList()
        {
            try
            {
                var data = await _dContext.Discipline.Where(x => x.IsActive == true).ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IQueryable<TreatmentTeam>> GetTreatmentTeamList()
        {
            try
            {
                var data = await _dContext.TreatmentTeam.Where(x => x.IsActive == true).ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IQueryable<Credentials>> GetCredentialsList()
        {
            try
            {
                var data = await _dContext.Credentials.Where(x => x.IsActive == true).ToListAsync();
                return data.AsQueryable();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

  

    }
}
