using Snowly.Domain.Entities;

namespace Snowly.Application.Interfaces
{
    public interface IFriendShipRepository
    {
        /// <summary>
        /// Yeni arkadaşlık isteği oluşturur
        /// </summary>
        /// <param name="friendShip">FriendShip nesnesi</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<FriendShip> AddFriendShipAsync(FriendShip friendShip, CancellationToken cancellationToken);

        /// <summary>
        /// 2 kullanıcı arasında arkadaşlık isteği var mı kontrolü
        /// </summary>
        /// <param name="requesterId">Arkadaşlık isteği gönderen kullanıcı Id</param>
        /// <param name="addresseeId">Arkadaşlık isteği alan kullanıcı Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyFriendShipAsync(Guid requesterId, Guid addresseeId, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcı ID'ye ait olan kabul edilmemiş arkadaşlık isteklerini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<FriendShip>> AllPendingFriendShipAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcı ID'ye ait olan kendisine gönderilen arkadaşlık isteklerini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<FriendShip>> AllPendingFriendShipForAddresseeAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcı ID'ye ait olan kabul edilmiş arkadaşlıkları getirir
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<FriendShip>> AllAcceptedFriendShipAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcılardan gelen alıcı ve gönderici ID'lerine ait olan Arkadaşlığı getirir
        /// </summary>
        /// <param name="requesterId">İsteği gönderici fark etmiyor</param>
        /// <param name="addresseeId">Alıcı fark etmiyor</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<FriendShip?> GetFriendShipAsync(Guid requesterId, Guid addresseeId, CancellationToken cancellationToken);

        /// <summary>
        /// FriendShipId'ye ait olan FriendShip (Arkadaşlığı) siler
        /// </summary>
        /// <param name="friendShipId">FriendShip nesnesi ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteFriendShipAsync(Guid friendShipId, CancellationToken cancellationToken);

        /// <summary>
        /// FriendShipId'ye ait olan FriendShip (Arkadaşlığı) onaylar
        /// </summary>
        /// <param name="friendShipId">FriendShip nesnesi ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AcceptFriendShipAsync(Guid friendShipId, CancellationToken cancellationToken);


        /// <summary>
        /// FriendShipId'ye ait olan FriendShip'i döner
        /// </summary>
        /// <param name="friendShipId">FriendShip nesnesi ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<FriendShip?> GetFriendShipByIdAsync(Guid friendShipId, CancellationToken cancellationToken);
    }
}
