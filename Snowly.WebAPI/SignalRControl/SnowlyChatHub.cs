using MediatR;
using Microsoft.AspNetCore.SignalR;
using Snowly.Application.Commands.UserCommands.OnlineUser;
using Snowly.Application.Interfaces;
using Snowly.Domain.Entities;
using System.Runtime.CompilerServices;

namespace Snowly.WebAPI.SignalRControl
{
    public class SnowlyChatHub : Hub
    {
        private readonly IFriendShipRepository _friendShipRepository;
        private readonly IMediator _mediator;

        public SnowlyChatHub(IFriendShipRepository friendShipRepository, IMediator mediator)
        {
            _friendShipRepository = friendShipRepository;
            _mediator = mediator;
        }

        public async Task NotifyFriendsOnline(Guid userId)
        {
            var cancellationToken = Context.ConnectionAborted;

            List<FriendShip> friendShips =
                await _friendShipRepository
                    .AllAcceptedFriendShipAsync(userId, cancellationToken)
                    .ConfigureAwait(false);

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
            List<FriendShip> friendShips =
                await _friendShipRepository
                    .AllAcceptedFriendShipAsync(userId, CancellationToken.None)
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



        public override async Task OnConnectedAsync()
        {
            var userIdString = Context.UserIdentifier;

            if (Guid.TryParse(userIdString, out var userId))
            {
                await _mediator.Send(new OnlineUserCommand(userId, true));
                await NotifyFriendsOnline(userId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userIdString = Context.UserIdentifier;

            if (Guid.TryParse(userIdString, out var userId))
            {
                await _mediator.Send(new OnlineUserCommand(userId, false));
                await NotifyFriendsOffline(userId);
            }

            await base.OnDisconnectedAsync(exception);
        }


    }
}
