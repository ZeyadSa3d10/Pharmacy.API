using Pharmacy.Domain.Models.Auth;
using Pharmacy.Domain.Models.PharmacyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces.Reposatory
{
    public interface IUserReposatory :IBaseRepository<User>
    {
        public Task<User> GetUserAsync(string Input, CancellationToken cancellationToken);
        public Task<User> GetUserAsync(int ID, CancellationToken cancellationToken);

    }
}
