using Microsoft.AspNetCore.SignalR;
using Snowly.Application.Interfaces;
using Snowly.Domain.Entities;
using System.Runtime.CompilerServices;

namespace Snowly.WebAPI.SignalRControl
{
    public class SnowlyChatHub : Hub
    {
        private readonly IFriendShipRepository _friendShipRepository;

        public SnowlyChatHub(IFriendShipRepository friendShipRepository)
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task NotifyFriendsOnline(Guid userId, CancellationToken cancellationToken)
        {
            List<FriendShip> friendShips = await _friendShipRepository.AllAcceptedFriendShipAsync(userId, cancellationToken).ConfigureAwait(false);

            List<Guid> friendIds = friendShips
                .Select(x => x.RequesterId == userId ? x.AddresseeId : x.RequesterId)
                .ToList();

            foreach (var friendId in friendIds)
            {
                await Clients.User(friendId.ToString()).SendAsync("FriendOnline", new
                {
                    UserId = userId
                });
            }
        }


        public async Task NotifyFriendsOffline(Guid userId)
        {
            List<FriendShip> friendShips = await _friendShipRepository.AllAcceptedFriendShipAsync(userId, CancellationToken.None)
                .ConfigureAwait(false);

            List<Guid> friendIds = friendShips
                .Select(x => x.RequesterId == userId ? x.AddresseeId : x.RequesterId)
                .ToList();

            foreach (var friendId in friendIds)
            {
                await Clients.User(friendId.ToString()).SendAsync("FriendOffline", new
                {
                    UserId = userId
                });
            }
        }



    }
}
