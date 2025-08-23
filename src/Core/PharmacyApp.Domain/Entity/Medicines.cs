using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("Medicines")]
    public class Medicines 
    {
        [Key] 
 public int MedicineId {get; set;}
public string MedicineName {get; set;}
public string GenericName {get; set;}
public string Manufacturer {get; set;}
public int SupplierId {get; set;}
public string Category {get; set;}
public decimal Price {get; set;}
public int StockQuantity {get; set;}
public DateTime? ExpiryDate {get; set;}
public DateTime? CreatedOn {get; set;}
}
}
