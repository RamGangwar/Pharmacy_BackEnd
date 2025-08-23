using Dapper.Contrib.Extensions;

namespace PharmacyApp.Domain.Entity
{
    [Table("Payments")]
    public class Payments
    {
        [Key]
        public int PaymentId { get; set; }
        public int HeaderId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
    }
}
