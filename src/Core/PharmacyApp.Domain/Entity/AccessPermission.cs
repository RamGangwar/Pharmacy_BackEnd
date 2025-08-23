using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("AccessPermission")]
    public class AccessPermission
    {
        [Key]
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
