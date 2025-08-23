using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class ModulesVM 
{
public int ModuleId {get; set;}
public string ModuleName {get; set;}
public string PageURL {get; set;}
public string IconURL {get; set;}
public int ParentId {get; set;}
public int DisplayOrder {get; set;}
public bool IsActive {get; set;}
public DateTime CreatedOn {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
