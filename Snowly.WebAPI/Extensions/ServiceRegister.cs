using Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCodeInterfaces;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.EmailSenderService;
using Snowly.Application.Interfaces;
using Snowly.Application.MessageSecure;
using Snowly.Infrastructure.Repositories.ConfirmCodeRepositories;
using Snowly.Infrastructure.Repositories.FriendShipRepositories;
using Snowly.Infrastructure.Repositories.JCMRepositories;
using Snowly.Infrastructure.Repositories.MessageRepositories;
using Snowly.Infrastructure.Repositories.RefreshTokenRepositories;
using Snowly.Infrastructure.Repositories.UserRepositories.UserConcretes;

namespace Snowly.WebAPI.Extensions
{
    public static class ServiceRegister
    {
        /// <summary>
        /// Tüm Servislerin kaydını yapar Program.cs içine ekle
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFriendShipRepository, FriendShipRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserConfirmCodeRepository, UserConfirmCodeRepository>();
            services.AddScoped<IJCMRepository, JCMRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<EmailSender>();
            services.AddScoped<MessageCrypto>();
            return services;
        }

    }
}
