using Microsoft.EntityFrameworkCore;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Domain.Entities;
using Snowly.Domain.Enums;
using Snowly.Infrastructure.SnowlyDatabase;

namespace Snowly.Infrastructure.Repositories.UserRepositories.UserConcretes
{
    public class UserRepository : IUserRepository
    {
        private readonly SnowlyDbContext _snowlyDbContext;

        public UserRepository(SnowlyDbContext snowlyDbContext)
        {
            _snowlyDbContext = snowlyDbContext;
        }
        public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await _snowlyDbContext.Users.AddAsync(user);
                await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı kaydedilirken hata meydana geldi", ex);
            }
        }

        public async Task<bool> AnyUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
               return await _snowlyDbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<bool> ConfirmEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                User? user = await _snowlyDbContext.Users.Where(x => x.Email == email && x.Status == UserStatus.Active).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                if (user is null)
                    return false;
                if (user.EmailConfirmed)
                    return true;
                user.EmailConfirmed = true;
                await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı onaylama esnasında hata meydana geldi", ex);
            }
        }

        public async Task<bool> DeleteUserAsync(string email, CancellationToken cancellationToken)
        {
            User? user = await _snowlyDbContext.Users.Where(x => x.Email == email && x.Status == UserStatus.Active).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if(user == null) return false;
            user.Status = UserStatus.Passive;
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _snowlyDbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Status == UserStatus.Active, cancellationToken).ConfigureAwait(false);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _snowlyDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> OnlineChangeAsync(Guid userId, bool isOnline, CancellationToken cancellationToken)
        {
            User? user = await _snowlyDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken).ConfigureAwait(false);
            if(user == null) return false;
            user.IsOnline = isOnline;
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> PasswordChangeAsync(Guid userId, string newHashedPassword, CancellationToken cancellationToken)
        {
            User? user = await _snowlyDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId).ConfigureAwait(false);
            if(user == null) return false;
            user.Password = newHashedPassword;
            user.UpdateDate = DateTime.UtcNow;
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        public async Task<User?> UpdateUserAsync(string email, string firstName, string lastName, CancellationToken cancellationToken)
        {
            User? user = await _snowlyDbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken).ConfigureAwait(false);
            if(user == null) return null;
            user.FirstName = firstName;
            user.LastName = lastName;
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return user;
        }
    }
}
