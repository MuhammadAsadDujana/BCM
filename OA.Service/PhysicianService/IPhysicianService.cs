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
    public interface IPhysicianService
    {
        
        Task<ResponseModel> DeletePhysicianCredentials(Guid Id);
        Task<ResponseModel> DeletePhysicianDiscipline(Guid Id);
        Task<ResponseModel> DeletePhysicianLanguage(Guid Id);
        Task<ResponseModel> DeletePhysicianSpecialties(Guid Id);
        Task<ResponseModel> DeletePhysicianTreatmentTeam(Guid Id);

        Task<IQueryable<PhysicianCredentials>> checkPhysicianCredentialsList(Guid Id);
        Task<ResponseModel> GetCredentialsListById(Guid Id);
        Task<ResponseModel> GetSpecialtiesListById(Guid Id);
        Task<ResponseModel> GetDisciplineListById(Guid Id);
        Task<ResponseModel> GetTreatmentTeamsListById(Guid Id);
        Task<ResponseModel> GetLanguagesListById(Guid Id);

        Task<ResponseModel> GetAllLanguages();
        Task<ResponseModel> GetAllCredentials();
        Task<ResponseModel> GetAllDisciplines();
        Task<ResponseModel> GetAllSpecialties();
        Task<ResponseModel> GetAllTreatmentTeams();
        Task<ResponseModel> GetPhysicianList();

        Task<ResponseModel> GetPhysicianListSimple();
        Task<ResponseModel> GetPhysicianDetails(Guid Id);

        Task<ResponseModel> GetPhysicianDetailsSimple(Guid Id);
        Task<ResponseModel> GetPhysicianByName(string name);

        Task<ResponseModel> GetSpecialties(Guid id);

        Task<ResponseModel> GetDiscipline(Guid id);
        Task<ResponseModel> GetTreatmentTeams(Guid id);
        Task<ResponseModel> GetCredentials(Guid id);

        Task<ResponseModel> GetPhysiciansBySpecialty(Guid Id);
        Task<ResponseModel> GetPhysiciansByDiscipline(Guid Id);
        Task<ResponseModel> GetPhysiciansByTreatmentTeam(Guid Id);
        Task<ResponseModel> DeletePhysician(Guid Id);
        Task<ResponseModel> AddPhysician(Physician physician);
        Task<ResponseModel> AddPhysicianCredentials(PhysicianCredentials physicianC);
        Task<ResponseModel> AddPhysicianDisciplines(PhysicianDiscipline physicianD);
        Task<ResponseModel> AddPhysicianLanguages(PhysicianLanguage physicianL);
        Task<ResponseModel> AddPhysicianSpecialties(PhysicianSpecialties physicianS);
        Task<ResponseModel> AddPhysicianTreatmentTeams(PhysicianTreatmentTeam physicianT);

        Task<ResponseModel> AddSpecialty(Specialties specialties);

        Task<ResponseModel> AddDisciplines(Discipline discipline);
        Task<ResponseModel> AddTreatmentTeam(TreatmentTeam treatment);

        Task<ResponseModel> AddCredential(Credentials credentials);

        Task<ResponseModel> EditSpecialty(Specialties specialties);
        Task<ResponseModel> EditDiscipline(Discipline discipline);
        Task<ResponseModel> EditTreatmentTeams(TreatmentTeam treatmentTeam);
        Task<ResponseModel> EditCredentials(Credentials credentials);

        Task<ResponseModel> EditPhysician(Physician physician);

        Task<ResponseModel> DeleteCredential(Guid Id);

        Task<ResponseModel> DeleteDiscipline(Guid Id);

        Task<ResponseModel> DeleteTreatment(Guid Id);
        Task<ResponseModel> DeleteSpecialty(Guid Id);

       
    }
}
