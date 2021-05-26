using OA.Data.Helper;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.DTO_Model
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string Address { get; set; }
        public string WorkAt { get; set; }
        public string Bio { get; set; }
        public string Specialty { get; set; }
        public Helper.UserType UserType { get; set; }
        public UserStatus UserStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string Zip { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenIssueDate { get; set; }
        public DateTime TokenExpiryDate { get; set; }
        public List<Guid> AreaOfInterestIds { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        //public Guid CityId { get; set; }
        public string City { get; set; }

    }
}
