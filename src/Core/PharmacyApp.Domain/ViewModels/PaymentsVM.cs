using System.Text.Json.Serialization; 
 namespace PharmacyApp.Domain.ViewModels 
{
 public class PaymentsVM 
{
public int PaymentId {get; set;}
public int HeaderId {get; set;}
public DateTime? PaymentDate {get; set;}
public decimal AmountPaid {get; set;}
public string PaymentMethod {get; set;}
 [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
