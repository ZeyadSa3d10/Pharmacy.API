using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.ApplicationDtos.UserDto;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Domain.Models.Auth;
using Pharmacy.Domain.Models.PharmacyModels;
using Pharmacy.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pharmacy.Infrastructure.Reposatories
{
    public class UserReposatory : BaseRepository<User>, IUserReposatory
    {
        private readonly AppDbContext db;

        public UserReposatory(AppDbContext db) : base(db)
        {
            this.db = db;
        }
        public async Task<User> GetUserAsync(string Input, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
           return await db.Users.Where(s => s.FullName == Input || s.Email == Input || s.PhoneNumber == Input)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<User> GetUserAsync(int Id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await db.Users.Where(s => s.Id == Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

    }
}
