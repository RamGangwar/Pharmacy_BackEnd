using System.Text.Json.Serialization;
namespace PharmacyApp.Domain.ViewModels
{
    public class MedicinesVM
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string GenericName { get; set; }
        public string Manufacturer { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
