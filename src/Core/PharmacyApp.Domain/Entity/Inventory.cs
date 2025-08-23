using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int MedicineId { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string SourceNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
