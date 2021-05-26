using OA.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA.Data.Model
{
    public class PhysicianCredentials
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhysicianCredentialsId { get; set; }

        [ForeignKey("Physician")]
        public Guid PhysicianId { get; set; }

        [ForeignKey("Credentials")]
        public Guid CredentialsId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Physician Physician { get; set; }
        public virtual Credentials Credentials { get; set; }
    }
}
