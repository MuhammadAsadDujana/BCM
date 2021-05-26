using OA.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA.Data.Model
{
    public class PhysicianLanguage
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhysicianLanguageId { get; set; }

        [ForeignKey("Physician")]
        public Guid PhysicianId { get; set; }

        [ForeignKey("Language")]
        public Guid LanguageId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Physician Physician { get; set; }
        public virtual Language Language { get; set; }
    }
}
