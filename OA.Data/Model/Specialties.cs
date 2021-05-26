﻿using OA.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA.Data.Model
{
    public class Specialties 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SpecialtyId { get; set; }
        public string SpecialtyTitle { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        //public Guid PhysicianSpecialtyId { get; set; }
    }
}
