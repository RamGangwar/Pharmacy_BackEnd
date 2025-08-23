using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("Suppliers")]
    public class Suppliers 
    {
        [Key] 
 public int SupplierId {get; set;}
public string CompanyName {get; set;}
public string ContactName {get; set;}
public string Email {get; set;}
public string MobileNo {get; set;}
public string Address {get; set;}
public DateTime? CreatedOn {get; set;}
}
}
