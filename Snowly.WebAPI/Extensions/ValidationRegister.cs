
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection.Metadata;

namespace Snowly.WebAPI.Extensions
{
    public static class ValidationRegister
    {
        public static IServiceCollection ValidationRegistration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(typeof(APIAssemblyReference).Assembly);
            return services;

        }
    }
}
