using Microsoft.EntityFrameworkCore;
using Snowly.Application.Interfaces;
using Snowly.Domain.Entities;
using Snowly.Infrastructure.SnowlyDatabase;

namespace Snowly.Infrastructure.Repositories.JCMRepositories
{
    public class JCMRepository : IJCMRepository
    {
        private readonly SnowlyDbContext _snowlyDbContext;

        public JCMRepository(SnowlyDbContext snowlyDbContext)
        {
            _snowlyDbContext = snowlyDbContext;
        }

        public async Task<string> AddJCMToken(Guid userId, string jcmToken, CancellationToken cancellationToken)
        {
            User? user = await _snowlyDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId).ConfigureAwait(false);
            user!.JCMToken = jcmToken;
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return jcmToken;
        }

        public async Task<string> UpdateJCMToken(Guid userId, string jcmToken, CancellationToken cancellationToken)
        {
            User? user = await _snowlyDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId).ConfigureAwait(false);
            if (user!.JCMToken == jcmToken) return jcmToken;
            user.JCMToken = jcmToken;
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return jcmToken;
        }
    }
}
