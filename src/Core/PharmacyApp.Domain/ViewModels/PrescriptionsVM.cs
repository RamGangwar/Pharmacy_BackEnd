using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class PrescriptionsVM 
{
public int prescription_id {get; set;}
public int PatientId {get; set;}
public DateTime? PrescriptionDate {get; set;}
public string DoctorName {get; set;}
public string DoctorContact {get; set;}
public string Notes {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
