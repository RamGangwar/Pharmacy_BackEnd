using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("SalesHeader")]
    public class SalesHeader
    {
        [Key]
        public int HeaderId { get; set; }
        public string HeaderNumber { get; set; }
        public int PatientId { get; set; }
        public DateTime? CeatedOn { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }

    }
}
