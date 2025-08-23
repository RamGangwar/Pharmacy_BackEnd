using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("Discounts")]
    public class Discounts 
    {
        [Key] 
 public int DiscountId {get; set;}
public string DiscountType {get; set;}
public decimal DiscountValue {get; set;}
public DateTime? StartDate {get; set;}
public DateTime? EndDate {get; set;}
public int MedicineId {get; set;}
}
}
