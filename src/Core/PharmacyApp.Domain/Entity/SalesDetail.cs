using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("SalesDetail")]
    public class SalesDetail
    {
        [Key]
        public int DetailId { get; set; }
        public int HeaderId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
