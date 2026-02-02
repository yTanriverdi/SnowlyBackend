using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.UserCommands.UserInterfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Kullanıcı ekleme işlemi yapar
        /// </summary>
        /// <param name="user">Kullanıcı entity</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
        /// <summary>
        /// Email'e ait kullanıcı var mı kontrol eder
        /// </summary>
        /// <param name="email">Kullanıcı maili</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyUserByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Parametredeki Email adresine ait kullanıcının hesabını onaylar
        /// </summary>
        /// <param name="email">Kullanıcı maili</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ConfirmEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Parametredeki Email adresine ait kullanıcının şifresi ile birlikte hesabını pasif olarak siler
        /// </summary>
        /// <param name="email">Kullanıcının maili</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(string email, CancellationToken cancellationToken);


        /// <summary>
        /// Parametredeki Email adresine ait kullanıcıyı getirir
        /// </summary>
        /// <param name="email">Kullanıcı maili</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Parametredeki UserId'ye ait kullanıcıyı getirir
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcının bilgilerini güncellemek için kullanılır
        /// </summary>
        /// <param name="email">Kullanıcı maili</param>
        /// <param name="firstName">Kullanıcının ismi</param>
        /// <param name="lastName">Kullanıcının soyadı</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> UpdateUserAsync(string email, string firstName, string lastName, CancellationToken cancellationToken);


        /// <summary>
        /// Kullanıcının şifre değiştirme işlemini yapar
        /// </summary>
        /// <param name="userId">Kullanıcının ID'si</param>
        /// <param name="newHashedPassword">Kullanıcının yeni HASHlenmiş şifresi</param>
        /// <returns></returns>
        Task<bool> PasswordChangeAsync(Guid userId, string newHashedPassword, CancellationToken cancellationToken);


        /// <summary>
        /// Kullanıcı ID'ye ait olan kullanıcının Çevrimiçi olup olmadığını ayarlar
        /// </summary>
        /// <param name="userId">Kullanıcının ID'si</param>
        /// <param name="isOnline">Kullanıcı çıkış mı yapıyor giriş mi ?</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> OnlineChangeAsync(Guid userId, bool isOnline, CancellationToken cancellationToken);
    }
}
