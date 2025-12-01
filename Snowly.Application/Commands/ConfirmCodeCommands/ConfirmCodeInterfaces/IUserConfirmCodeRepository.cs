using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCodeInterfaces
{
    public interface IUserConfirmCodeRepository
    {
        /// <summary>
        /// Kullanıcının mailine gönderilen kodun oluşturulmasını yapar
        /// </summary>
        /// <param name="userConfirmCode">Veritabanına kayıt edilecek onay kodu</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserConfirmCode> AddUserConfirmCodeAsync(UserConfirmCode userConfirmCode, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcının mailine ait olan onay kodunu getirir
        /// </summary>
        /// <param name="email">KUllanıcının maili</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserConfirmCode?> GetUserConfirmCodeByUserIdAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcının Emailini ve Onay kodunu alarak onaylama işlemine yardımcı olur
        /// </summary>
        /// <param name="email">Kullanıcının maili</param>
        /// <param name="code">Kullanıcıya gönderilen kod</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ConfirmCodeAsync(string email, string code, CancellationToken cancellationToken);

        /// <summary>
        /// Kullanıcıya gönderilen kodun yenilenmesi işlemini yapar
        /// </summary>
        /// <param name="email">Kullanıcının maili</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserConfirmCode> RefreshUserConfirmCodeAsync(string email, CancellationToken cancellationToken);
    }
}
