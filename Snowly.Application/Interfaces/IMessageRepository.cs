using Snowly.Domain.Entities;

namespace Snowly.Application.Interfaces
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Yeni mesaj ekleme işlemi yapar
        /// </summary>
        /// <param name="newMessage">Yeni mesaj nesnesi</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Message> AddMessageAsync(Message newMessage, CancellationToken cancellationToken);


        /// <summary>
        /// Mesajı pasif olarak siler
        /// </summary>
        /// <param name="messageId">Mesajın Id'si</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteMessageAsync(Guid messageId, CancellationToken cancellationToken);

        /// <summary>
        /// Mesajları okundu olarak işaretlemeye yarar
        /// </summary>
        /// <param name="receiverId">Mesajı alıcı kullanıcının ID'si</param>
        /// <param name="senderId">Mesajı gönderici kullanıcının ID'si</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> MarkAsReadMessageAsync(Guid receiverId, Guid senderId, CancellationToken cancellationToken);


        /// <summary>
        /// İki kullanıcı arasındaki mesajları getirmek için kullanılır her istekte isteğe bağlı olarak 30 mesaj döner
        /// </summary>
        /// <param name="senderId">Gönderici kullanıcı Id</param>
        /// <param name="receiverId">Alıcı kullanıcı Id</param>
        /// <param name="messageSize">Mesajların sayısı</param>
        /// <param name="messageStack">Kaçıncı kez istek atıldı</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Message>> GetMessagesBetweenUserAsync(Guid senderId, Guid receiverId, int messageSize, int messageStack, CancellationToken cancellationToken);

        /// <summary>
        /// Message ID'ye ait olan mesajı döner
        /// </summary>
        /// <param name="messageId">Mesajın ID'si</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Message?> GetMessageById(Guid messageId, CancellationToken cancellationToken);

    }
}
