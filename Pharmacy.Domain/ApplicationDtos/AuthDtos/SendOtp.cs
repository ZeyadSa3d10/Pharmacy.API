using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.ApplicationDtos.AuthDtos
{
    public class SendOtp
    {
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Otp Is Required")]
        [MinLength(6, ErrorMessage = "Otp Must Be 6 Digits")]
        [MaxLength(6, ErrorMessage = "Otp Must Be 6 Digits")]
        public string Otp { get; set; } = null!;
    }
}
