using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class PurchaseDetailVM 
{
public int DetailId {get; set;}
public int PurchaseId {get; set;}
public int MedicineId {get; set;}
public int Quantity {get; set;}
public decimal Price {get; set;}
public decimal TotalPrice {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
