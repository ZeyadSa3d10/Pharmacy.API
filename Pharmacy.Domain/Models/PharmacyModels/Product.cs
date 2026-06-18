using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Models.PharmacyModels
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string Barcode { get; set; } = null!;
        // ده علشان لو دواء محتاج روشته ولا حاجه مثلا مخدر او كدا 
        public bool PrescriptionRequired { get; set; } 

        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }

        public Supplier Supplier { get; set; } = null!;
        public int SupplierId { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
