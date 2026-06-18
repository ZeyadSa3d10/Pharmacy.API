using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.ApplicationDtos.AuthDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "FullName Is Required")]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "PhoneNumber Is Required")]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; } = null!;
    }
}
