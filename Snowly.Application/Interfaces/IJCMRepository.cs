namespace Snowly.Application.Interfaces
{
    public interface IJCMRepository
    {
        /// <summary>
        /// JCMToken ekleme işlemi yapar
        /// </summary>
        /// <param name="userId">Kullanıcının ID'si</param>
        /// <param name="jcmToken">JCM TOKEN</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> AddJCMToken(Guid userId, string jcmToken, CancellationToken cancellationToken);

        /// <summary>
        /// JCMToken eski JCMToken ile aynı ise değiştirmez fakat JCMToken yenilendiyse yenisiyle değiştirir
        /// </summary>
        /// <param name="userId">Kullanıcının ID'si</param>
        /// <param name="jcmToken">JCM TOKEN</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> UpdateJCMToken(Guid userId, string jcmToken, CancellationToken cancellationToken);
    }
}
