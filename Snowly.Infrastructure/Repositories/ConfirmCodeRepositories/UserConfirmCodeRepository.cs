using Microsoft.EntityFrameworkCore;
using Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCodeInterfaces;
using Snowly.Domain.Entities;
using Snowly.Infrastructure.SnowlyDatabase;

namespace Snowly.Infrastructure.Repositories.ConfirmCodeRepositories
{
    public class UserConfirmCodeRepository : IUserConfirmCodeRepository
    {
        private readonly SnowlyDbContext _snowlyDbContext;
        public UserConfirmCodeRepository(SnowlyDbContext snowlyDbContext)
        {
            _snowlyDbContext = snowlyDbContext;
        }
        public async Task<UserConfirmCode> AddUserConfirmCodeAsync(UserConfirmCode userConfirmCode, CancellationToken cancellationToken)
        {
            await _snowlyDbContext.UserConfirmCodes.AddAsync(userConfirmCode,cancellationToken).ConfigureAwait(false);
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return userConfirmCode;
        }

        public async Task<bool> ConfirmCodeAsync(string email, string code, CancellationToken cancellationToken)
        {
           UserConfirmCode? userConfirmCode = await _snowlyDbContext.UserConfirmCodes.Where(x => x.Email == email && x.Code == code).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
           if (userConfirmCode == null) return false;
           return true;
            
        }

        public async Task<UserConfirmCode?> GetUserConfirmCodeByUserIdAsync(string email, CancellationToken cancellationToken)
        {
            UserConfirmCode? userConfirmCode = await _snowlyDbContext.UserConfirmCodes.Where(x => x.Email == email).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            return userConfirmCode;
        }

        public async Task<UserConfirmCode> RefreshUserConfirmCodeAsync(string email, CancellationToken cancellationToken)
        {
            UserConfirmCode? userConfirmCode = await _snowlyDbContext.UserConfirmCodes.Where(x => x.Email == email).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if(userConfirmCode != null)
            {
                _snowlyDbContext.UserConfirmCodes.Remove(userConfirmCode);
                await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            UserConfirmCode newConfirmCode = new UserConfirmCode()
            {
                Email = email,
            };
            await _snowlyDbContext.UserConfirmCodes.AddAsync(newConfirmCode, cancellationToken).ConfigureAwait(false);
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return newConfirmCode;
            
        }
    }
}
