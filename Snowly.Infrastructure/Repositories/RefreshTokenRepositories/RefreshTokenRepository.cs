using Snowly.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowly.Domain.Entities;
using Snowly.Infrastructure.SnowlyDatabase;
using Microsoft.EntityFrameworkCore;

namespace Snowly.Infrastructure.Repositories.RefreshTokenRepositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SnowlyDbContext _snowlyDbContext;

        public RefreshTokenRepository(SnowlyDbContext snowlyDbContext)
        {
            _snowlyDbContext = snowlyDbContext;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _snowlyDbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken).ConfigureAwait(false);
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return refreshToken;
        }

        public async Task<RefreshToken?> GetActiveTokenAsync(Guid userId, string token, CancellationToken cancellationToken)
        {
            return await _snowlyDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token && !x.IsRevoked && x.Expires > DateTime.UtcNow, cancellationToken).ConfigureAwait(false);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _snowlyDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken).ConfigureAwait(false);
        }

        public async Task<RefreshToken> RotateRefreshTokenAsync(RefreshToken oldToken, RefreshToken newToken, CancellationToken cancellationToken)
        {
            oldToken.IsRevoked = true;
            await _snowlyDbContext.RefreshTokens.AddAsync(newToken, cancellationToken).ConfigureAwait(false);
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return newToken;
        }
    }
}
