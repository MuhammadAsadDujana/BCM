using OA.Data.Common;
using OA.Data.DTO_Model;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repository
{
    public interface IPhysicianRepository
    {
        
        Task<ResponseModel> DeletePhysicianCredentialsById(Guid id);
        Task<ResponseModel> DeletePhysicianDisciplineById(Guid id);
        Task<ResponseModel> DeletePhysicianLanguageById(Guid id);
        Task<ResponseModel> DeletePhysicianSpecialtiesById(Guid id);
        Task<ResponseModel> DeletePhysicianTreatmentTeamById(Guid id);
        
        Task<IQueryable<PhysicianCredentials>> checkPhysicianCredentials(Guid Id);
        Task<IQueryable<Credentials>> GetPhysicianCredentialsList(Guid Id);

        Task<IQueryable<Discipline>> GetPhysicianDisciplinesList(Guid Id);

        Task<IQueryable<Specialties>> GetPhysicianSpecialtiesList(Guid Id);

        Task<IQueryable<TreatmentTeam>> GetPhysicianTreatmentTeamsList(Guid Id);

        Task<IQueryable<Language>> GetPhysicianLanguagesList(Guid Id); 

        Task<IQueryable<Language>> GetAllLanguages();
        Task<IQueryable<Credentials>> GetAllCredentials();
        Task<IQueryable<Discipline>> GetAllDisciplines();
        Task<IQueryable<Specialties>> GetAllSpecialties();
        Task<IQueryable<TreatmentTeam>> GetAllTreatmentTeams();
        Task<IQueryable<Physician>> GetPhysicianList();

        Task<IQueryable<Physician>> GetPhysicianListWeb();
        
        Task<Physician> GetPhysicianDetails(Guid Id);

        Task<Physician> GetPhysicianDetailsWeb(Guid Id);

        Task<Physician> DeletePhysicianById(Guid Id);

        Task<Credentials> DeleteCredentialById(Guid Id);
        Task<Discipline> DeleteDisciplineById(Guid Id);
        Task<TreatmentTeam> DeleteTreatmentById(Guid Id);
        Task<Specialties> DeleteSpecialtyById(Guid Id);
        

        Task<ResponseModel> AddNewPhysician(Physician physician);


        Task<ResponseModel> AddNewPhysicianCredentials(PhysicianCredentials physicianC);

        Task<ResponseModel> AddNewPhysicianDiscipline(PhysicianDiscipline physicianD);
        Task<ResponseModel> AddNewPhysicianLanguages(PhysicianLanguage physicianL);
        Task<ResponseModel> AddNewPhysicianSpecialties(PhysicianSpecialties physicianS);
        Task<ResponseModel> AddNewPhysicianTreatmentTeam(PhysicianTreatmentTeam physicianT);
        Task<IQueryable<Physician>> GetPhysicianByName(string name);
        Task<IQueryable<Physician>> GetPhysiciansBySpecialty(Guid Id);

        Task<Specialties> GetSpecialtiesById(Guid Id);

        Task<Discipline> GetDisciplineById(Guid Id);

        Task<TreatmentTeam> GetTreatmentTeamById(Guid Id);

        Task<Credentials> GetCredentialsById(Guid Id);

        Task<IQueryable<Physician>> GetPhysiciansByDiscipline(Guid Id);
        Task<IQueryable<Physician>> GetPhysiciansByTreatmentTeam(Guid Id);
        Task<ResponseModel> UpdatePhysicianAsync(Physician entity);


        Task<ResponseModel> AddNewSpecialty(Specialties specialties);

        Task<ResponseModel> AddNewDiscipline(Discipline discipline);
        Task<ResponseModel> AddNewTreatmentTeam(TreatmentTeam treatment);

        Task<ResponseModel> AddNewCredentials(Credentials credentials);


        Task<ResponseModel> EditNewSpecialty(Specialties specialties);
        Task<ResponseModel> EditNewDiscipline(Discipline discipline);
        Task<ResponseModel> EditNewTreatmentTeam(TreatmentTeam treatment);
        Task<ResponseModel> EditNewCredentials(Credentials credentials);

        Task<ResponseModel> EditPhysicianById(Physician physician);

        Task<IQueryable<Specialties>> GetSpecialtiesList();
        Task<IQueryable<Discipline>> GetDisciplineList();
        Task<IQueryable<TreatmentTeam>> GetTreatmentTeamList();
        Task<IQueryable<Credentials>> GetCredentialsList();



    }
}
