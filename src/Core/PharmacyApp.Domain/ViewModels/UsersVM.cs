using System.Text.Json.Serialization;
namespace PharmacyApp.Domain.ViewModels
{
    public class UsersVM
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
