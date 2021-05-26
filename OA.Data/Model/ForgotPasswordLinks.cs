using OA.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA.Data.Model
{
    public class ForgotPasswordLinks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ForgotPasswordLinkID { get; set; }

        [Required]
        public string Email { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; }

        public virtual User User { get; set; }
    }
}
