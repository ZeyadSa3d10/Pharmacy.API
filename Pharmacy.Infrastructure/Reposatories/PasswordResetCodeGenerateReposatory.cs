using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Domain.Models.Auth;
using Pharmacy.Infrastructure.Data.Context;

namespace Pharmacy.Infrastructure.Reposatories
{
    public class PasswordResetCodeGenerateReposatory :BaseRepository<PasswordResetCode>, IPasswordResetCodeGenerateReposatory
    {
        private readonly AppDbContext db;

        public PasswordResetCodeGenerateReposatory(Data.Context.AppDbContext db) : base(db)
        {
            this.db = db;
        }
        public async Task<PasswordResetCode> GetLastCodeByEmail(string email,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await db.passwordResetCodes
                .Where(x => x.Email == email)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
        }

    }
}
