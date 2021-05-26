using OA.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA.Data.Model
{
    public class TreatmentTeam 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TreatmentTeamId { get; set; }
        public string DiseaseTitle { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        //public Guid PhysicianTreatmentId { get; set; }

    }
}
