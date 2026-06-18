using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Models.Auth;

namespace Pharmacy.Domain.Interfaces.Service
{
    public interface ITokenService
    {
        public string GenerateToken(User applicationUser, CancellationToken ct);
    }
}
