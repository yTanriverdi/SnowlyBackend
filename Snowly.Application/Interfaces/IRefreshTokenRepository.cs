using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// Kullanıcı için RefreshToken kaydı yapar
        /// </summary>
        /// <param name="refreshToken">Veritabanına kaydedilecek RefreshToken</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken, CancellationToken cancellationToken);


        /// <summary>
        /// Refresh token aktif mi değil mi kontrol eder
        /// Token DB’de yoksa, süresi dolmuşsa veya revoke edilmişse false döner
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si</param>
        /// <param name="token">RefreshToken string</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RefreshToken?> GetActiveTokenAsync(Guid userId, string token, CancellationToken cancellationToken);

        /// <summary>
        /// Token’ı süresi geçmiş olarak deiştirip yeni bir RefreshToken ile değiştirir
        /// </summary>
        /// <param name="oldToken">Eski RefreshToken</param>
        /// <param name="newToken">Yeni RefreshToken</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RefreshToken> RotateRefreshTokenAsync(RefreshToken oldToken, RefreshToken newToken, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcı ID'sine ait olan RefreshToken'i getirir
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RefreshToken?> GetRefreshTokenAsync(Guid userId, CancellationToken cancellationToken);

    }
}
