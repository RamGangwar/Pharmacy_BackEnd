using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("Roles")]
    public class Roles 
    {
        [Key] 
 public int RoleId {get; set;}
public string RoleName {get; set;}
public bool IsActive {get; set;}
public int CreatedBy {get; set;}
public DateTime CreatedOn {get; set;}
}
}
