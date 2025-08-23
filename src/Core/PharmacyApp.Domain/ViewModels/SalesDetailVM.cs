using System.Text.Json.Serialization;
namespace PharmacyApp.Domain.ViewModels
{
    public class SalesDetailVM
    {
        public int DetailId { get; set; }
        public int HeaderId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public MedicinesVM Medicine { get; set; }
        [JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
