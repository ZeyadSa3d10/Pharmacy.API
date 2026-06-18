using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Models.Auth;

namespace Pharmacy.Domain.Interfaces.Reposatory
{
    public interface IPasswordResetCodeGenerateReposatory :IBaseRepository<PasswordResetCode>
    {
        public Task<PasswordResetCode> GetLastCodeByEmail(string email, CancellationToken cancellationToken);
    }
}
