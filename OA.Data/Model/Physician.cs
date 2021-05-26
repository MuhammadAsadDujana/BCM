using OA.Data.Common;
using OA.Data.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web;

namespace OA.Data.Model
{
    public class Physician 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhysicianId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        
        //public string Credentials { get; set; }
        public string Email { get; set; }
        public string DirectPhone { get; set; }
        public string OfficePhone { get; set; }
        
        //public string Language { get; set; }
        public DateTime JoiningDate { get; set; }
        public PhysicianStatus Status { get; set; }
        public bool IsActive { get; set; }
        
        [NotMapped]
        public string physicianCredentials { get; set; }
        [NotMapped]
        public string physicianDiscipline { get; set; }
        [NotMapped]
        public string physicianSpecialties { get; set; }
        [NotMapped]
        public string physicianTreatmentTeam { get; set; }
        [NotMapped]
        public string physicianLanguage { get; set; }

        [NotMapped]
        public string DisplaySpecialtiesTitle { get; set; }

        [NotMapped]
        public List<Guid> DisplaySpecialitiesId { get; set; }

        [NotMapped]
        public string DisplayDisciplineTitle { get; set; }

        [NotMapped]
        public List<Guid> DisplayDicipilineId { get; set; }

        [NotMapped]
        public string DisplayTreatmentTeamTitle { get; set; }

        [NotMapped]
        public List<Guid> DisplayTreatmentTeamId { get; set; }

        [NotMapped]
        public string DisplayCredentialsTitle { get; set; }

        [NotMapped]
        public List<Guid> DisplayCredentialsId { get; set; }

        [NotMapped]
        public string DisplayLanguageTitle { get; set; }

        [NotMapped]
        public List<Guid> DisplayLanguageId { get; set; }

        [NotMapped]
        public HttpPostedFileWrapper imageUpload { get; set; }

        public virtual ICollection<PhysicianSpecialties> Specialties { get; set; }
        public virtual ICollection<PhysicianDiscipline> Discipline { get; set; }
        public virtual ICollection<PhysicianTreatmentTeam> TreatmentTeam { get; set; }
        public virtual ICollection<PhysicianCredentials> Credentials { get; set; }
        public virtual ICollection<PhysicianLanguage> Language { get; set; }

        //public Guid PhysicianSpecialtyId { get; set; }
        //public PhysicianSpecialties PhysicianSpecialties { get; set; }
        //public Guid PhysicianDesciplineId { get; set; }
        //public PhysicianDiscipline PhysicianDiscipline { get; set; }
        //public Guid PhysicianTreatmentTeamId { get; set; }
        //public PhysicianTreatmentTeam PhysicianTreatmentTeam { get; set; }

    }
}
