using System.Text.Json.Serialization;
namespace PharmacyApp.Domain.ViewModels
{
    public class PurchaseOrderVM
    {
        public int PurchaseId { get; set; }
        public string PurchaseNumber { get; set; }
        public int SupplierId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<PurchaseDetailVM> Detail { get; set; }= new List<PurchaseDetailVM>();
        [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
