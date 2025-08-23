using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class DiscountsVM 
{
public int DiscountId {get; set;}
public string DiscountType {get; set;}
public decimal DiscountValue {get; set;}
public DateTime? StartDate {get; set;}
public DateTime? EndDate {get; set;}
public int MedicineId {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
