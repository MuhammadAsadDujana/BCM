using OA.Data.Common;
using OA.Data.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA.Data.Model
{
    public class User 
    {
        public User()
        {
            this.UserAreaOfInterest = new HashSet<UserAreaOfInterest>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(250)]
        public string ProfileImage { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(250)]
        public string WorkAt { get; set; }

        [StringLength(250)]
        public string Bio { get; set; }

        [StringLength(100)]
        public string Specialty { get; set; }

        [Required]
        public Helper.UserType UserType { get; set; }
        public Helper.UserStatus UserStatus { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [ForeignKey("Country")]
        public Guid CountryId { get; set; }

        [ForeignKey("State")]
        public Guid StateId { get; set; }

        [StringLength(100)]
        public string City { get; set; }
        //[ForeignKey("City")]
        //public Guid CityId { get; set; }

        [StringLength(100)]
        public string Zip { get; set; }

        //[ForeignKey("AreaOfInterest")]
        //public Guid AreadOfInterestId { get; set; }

        [NotMapped]
        public string DisplayAreaOfInterestTitle { get; set; }

        [NotMapped]
        public string DisplayAreaOfInterestIds { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenIssueDate { get; set; }
        public DateTime TokenExpiryDate { get; set; }

        [StringLength(100)]
        public string DeviceType { get; set; }

        [StringLength(200)]
        public string DeviceToken { get; set; }

        //public virtual AreaOfInterest AreaOfInterest { get; set; }
        public virtual ICollection<UserAreaOfInterest> UserAreaOfInterest { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        //public virtual City City { get; set; }

    }
}
