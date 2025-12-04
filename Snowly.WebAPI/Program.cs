
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Snowly.Application;
using Snowly.Application.EmailSenderService;
using Snowly.Infrastructure.SnowlyDatabase;
using Snowly.WebAPI.Extensions;
using Snowly.WebAPI.JwtToken;
using Snowly.WebAPI.SignalRControl;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace Snowly.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            var key = Environment.GetEnvironmentVariable("JWT_KEY");
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            builder.Services.AddCors(options =>
            {
                //options.AddPolicy("AllowFrontend", policy =>
                //{
                //    policy.AllowAnyHeader()
                //          .AllowAnyMethod()
                //          .AllowCredentials();
                //    policy.AllowAnyOrigin()
                //          .AllowAnyHeader()
                //          .AllowAnyMethod();
                //});
                options.AddPolicy("snowlyPolicy", policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:5173",
                            "http://192.168.1.8:5173",
                            "https://seninfrontenddomainin.com"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly);
            });

            builder.Services.AddDbContext<SnowlyDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Insert JWT Token",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                opt.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));
            });


            builder.Services.AddServices();
            builder.Services.ValidationRegistration();

            builder.Services.AddSignalR();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SnowlyDbContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors("snowlyPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();
            app.MapHub<SnowlyChatHub>("/snowlyHub").RequireCors("snowlyPolicy");

            app.Run();
        }
    }
}
