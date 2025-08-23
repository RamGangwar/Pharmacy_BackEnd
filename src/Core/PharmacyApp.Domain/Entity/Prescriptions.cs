using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("Prescriptions")]
    public class Prescriptions 
    {
        [Key] 
 public int prescription_id {get; set;}
public int PatientId {get; set;}
public DateTime? PrescriptionDate {get; set;}
public string DoctorName {get; set;}
public string DoctorContact {get; set;}
public string Notes {get; set;}
}
}
