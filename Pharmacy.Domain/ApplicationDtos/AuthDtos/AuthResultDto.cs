using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.ApplicationDtos.AuthDtos
{
    public class AuthResultDto
    {
        public string Token { get; set; } = null!;
        //public string  Refr{ get; set; }
    }
}
