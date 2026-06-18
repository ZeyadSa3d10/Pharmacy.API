using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.ApplicationDtos.AuthDtos
{
    public class ApplyNewPassword
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; } = null!;
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
