using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class InventoryVM 
{
public int InventoryId {get; set;}
public int MedicineId {get; set;}
public string Type {get; set;}
public int Quantity {get; set;}
public string SourceNumber {get; set;}
public DateTime? TransactionDate {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
