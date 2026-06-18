using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Enums;


namespace Pharmacy.Domain.Models.Auth
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } =null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public Roles Role { get; set; }
        public bool Vertified { get; set; }
        public ICollection<PasswordResetCode> PasswordResetCodes { get; set; } = new List<PasswordResetCode>();
    }
}
