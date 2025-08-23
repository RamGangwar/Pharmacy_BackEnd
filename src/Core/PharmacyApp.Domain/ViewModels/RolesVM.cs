using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class RolesVM 
{
public int RoleId {get; set;}
public string RoleName {get; set;}
public bool IsActive {get; set;}
public int CreatedBy {get; set;}
public DateTime CreatedOn {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
