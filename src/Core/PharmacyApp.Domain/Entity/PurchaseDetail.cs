using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("PurchaseDetail")]
    public class PurchaseDetail
    {
        [Key]
        public int DetailId { get; set; }
        public int PurchaseId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
