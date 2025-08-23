using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("Patients")]
    public class Patients 
    {
        [Key] 
 public int PatientId {get; set;}
public string FullName {get; set;}
public string Email {get; set;}
public string MobileNo {get; set;}
public string Address {get; set;}
public DateTime? Reg_Date {get; set;}
}
}
