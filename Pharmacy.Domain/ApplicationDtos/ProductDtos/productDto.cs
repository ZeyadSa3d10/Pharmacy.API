using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Pharmacy.Domain.ApplicationDtos.ProductDtos
{

    public class productDto
    {   
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string Barcode { get; set; } = null!;
         
        public bool PrescriptionRequired { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }
        public IFormFile formFile { get; set; } = null!;
    }
}