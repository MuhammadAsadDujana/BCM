using OA.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA.Data.Model
{
    public class State 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StateId { get; set; }
        public string StateTitle { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Country")]
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
