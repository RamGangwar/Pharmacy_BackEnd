using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class PatientsVM 
{
public int PatientId {get; set;}
public string FullName {get; set;}
public string Email {get; set;}
public string MobileNo {get; set;}
public string Address {get; set;}
public DateTime? Reg_Date {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
