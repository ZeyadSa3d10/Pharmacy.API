using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Models.PharmacyModels
{
    public class Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
