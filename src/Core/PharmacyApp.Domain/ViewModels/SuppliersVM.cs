using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class SuppliersVM 
{
public int SupplierId {get; set;}
public string CompanyName {get; set;}
public string ContactName {get; set;}
public string Email {get; set;}
public string MobileNo {get; set;}
public string Address {get; set;}
public DateTime? CreatedOn {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
