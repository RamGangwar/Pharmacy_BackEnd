using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class AccessPermissionVM 
{
public int PermissionId {get; set;}
public int RoleId {get; set;}
public int ModuleId {get; set;}
public bool CanAdd {get; set;}
public bool CanEdit {get; set;}
public bool CanDelete {get; set;}
public bool CanView {get; set;}
public DateTime CreatedOn {get; set;}
public int CreatedBy {get; set;}
public bool IsActive {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
