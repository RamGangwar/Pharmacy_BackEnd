using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Model
{
    public class UsersModel : UserBaseModel
    {
        public string SecondaryEmail { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public int CategoryId { get; set; }
        public DateTime? DateOfEstablishment { get; set; }
        public int? IndustryId { get; set; }
        public int? LocationId { get; set; }
        public string CoverPic { get; set; }
        public string CoverPic1 { get; set; }
    }
}
