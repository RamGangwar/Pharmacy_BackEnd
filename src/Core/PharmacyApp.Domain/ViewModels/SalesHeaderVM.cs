using System.Text.Json.Serialization;
namespace PharmacyApp.Domain.ViewModels
{
    public class SalesHeaderVM
    {
        public int HeaderId { get; set; }
        public string HeaderNumber { get; set; }
        public int PatientId { get; set; }
        public DateTime? CeatedOn { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public List<SalesDetailVM> Detail { get; set; } = new List<SalesDetailVM>();

        [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
