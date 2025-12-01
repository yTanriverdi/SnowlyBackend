using Microsoft.EntityFrameworkCore;
using Snowly.Application.Interfaces;
using Snowly.Domain.Entities;
using Snowly.Domain.Enums;
using Snowly.Infrastructure.SnowlyDatabase;

namespace Snowly.Infrastructure.Repositories.FriendShipRepositories
{
    public class FriendShipRepository : IFriendShipRepository
    {
        private readonly SnowlyDbContext _snowlyDbContext;

        public FriendShipRepository(SnowlyDbContext snowlyDbContext)
        {
            _snowlyDbContext = snowlyDbContext;
        }

        public async Task<FriendShip> AddFriendShipAsync(FriendShip friendShip, CancellationToken cancellationToken)
        {
            await _snowlyDbContext.Friendships.AddAsync(friendShip, cancellationToken).ConfigureAwait(false);
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return friendShip;
        }

        public async Task<List<FriendShip>> AllAcceptedFriendShipAsync(Guid userId, CancellationToken cancellationToken)
        {
            List<FriendShip> acceptedFriendShips = await _snowlyDbContext.Friendships.Where(x => x.Status == FriendShipStatus.Accepted && x.RequesterId == userId).Include(x => x.Addressee).Include(x => x.Requester).ToListAsync(cancellationToken).ConfigureAwait(false);
            return acceptedFriendShips;
        }

        public async Task<List<FriendShip>> AllPendingFriendShipAsync(Guid userId, CancellationToken cancellationToken)
        {
            List<FriendShip> pendingFriendShips = await _snowlyDbContext.Friendships.Where(x => x.Status == FriendShipStatus.Pending && x.RequesterId == userId).Include(x => x.Addressee).Include(x => x.Requester).ToListAsync(cancellationToken).ConfigureAwait(false);
            return pendingFriendShips;
        }
        public async Task<List<FriendShip>> AllPendingFriendShipForAddresseeAsync(Guid userId, CancellationToken cancellationToken)
        {
            List<FriendShip> pendingFriendShips = await _snowlyDbContext.Friendships.Where(x => x.Status == FriendShipStatus.Pending && x.AddresseeId == userId).Include(x => x.Addressee).Include(x => x.Requester).ToListAsync(cancellationToken).ConfigureAwait(false);
            return pendingFriendShips;
        }

        public async Task<bool> AnyFriendShipAsync(Guid requesterId, Guid addresseeId, CancellationToken cancellationToken)
        {
            FriendShip? anyFriendShip = await _snowlyDbContext.Friendships.Where(x => (x.Status == FriendShipStatus.Pending || x.Status == FriendShipStatus.Accepted) && ((x.RequesterId == requesterId && x.AddresseeId == addresseeId) || (x.RequesterId == addresseeId && x.AddresseeId == requesterId))).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (anyFriendShip == null) return false;
            return true;
        }

        public async Task<FriendShip?> GetFriendShipAsync(Guid requesterId, Guid addresseeId, CancellationToken cancellationToken)
        {
            FriendShip? anyFriendShip = await _snowlyDbContext.Friendships.Where(x => (x.Status == FriendShipStatus.Pending || x.Status == FriendShipStatus.Accepted) && ((x.RequesterId == requesterId && x.AddresseeId == addresseeId) || (x.RequesterId == addresseeId && x.AddresseeId == requesterId))).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if(anyFriendShip == null) return anyFriendShip;
            return anyFriendShip;
        }

        public async Task<bool> DeleteFriendShipAsync(Guid friendShipId, CancellationToken cancellationToken)
        {
            FriendShip? friendShip = await _snowlyDbContext.Friendships.FirstOrDefaultAsync(x => x.Id == friendShipId, cancellationToken).ConfigureAwait(false);
            if(friendShip == null) return false;
            _snowlyDbContext.Friendships.Remove(friendShip);
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> AcceptFriendShipAsync(Guid friendShipId, CancellationToken cancellationToken)
        {
            FriendShip? friendShip = await _snowlyDbContext.Friendships.FirstOrDefaultAsync(x => x.Id == friendShipId, cancellationToken).ConfigureAwait(false);
            if(friendShip == null) return false;
            if (friendShip.Status == FriendShipStatus.Accepted) return true;
            friendShip.UpdateDate = DateTime.UtcNow;
            friendShip.Status = FriendShipStatus.Accepted;
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        public async Task<FriendShip?> GetFriendShipByIdAsync(Guid friendShipId, CancellationToken cancellationToken)
        {
            return await _snowlyDbContext.Friendships.FirstOrDefaultAsync(x => x.Id == friendShipId, cancellationToken).ConfigureAwait(false);
        }
    }
}
