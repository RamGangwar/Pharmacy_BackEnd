using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseId { get; set; }
        public string PurchaseNumber { get; set; }
        public int SupplierId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
