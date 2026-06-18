using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Infrastructure.Configration;
using Pharmacy.Infrastructure.Data.Context;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Application.Services;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Infrastructure.Reposatories;
using Pharmacy.Domain.Models.Auth;
using Pharmacy.Application.Services.Configration;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Application.Services.Integration_Service;
using Pharmacy.Application.Services.Integration_Service.Implmentation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Pharmacy.Api.ServiceConfigration
{
    public class ServiceConfigrations
    {
        public static void ConfigureService(IConfiguration configuration,IServiceCollection services)
        {
            AddDatabase(services,configuration);
            AddAuthentication(services, configuration);
            AddServices(services);
        }

        public static void AddDatabase(IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>()
                    ?? throw new InvalidOperationException("JWT settings are not configured properly.");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddAuthorization();
        }

        private static void AddServices(IServiceCollection services)
        {

            #region Integration Service

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFileService, FileService>();

            #endregion

            #region Versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion=new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });
            #endregion

            #region Services

            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<Icategoryservice, CategoryService>();
            services.AddScoped<IAuthService, AuthService>(); 

            #endregion



            #region Reposatories

            services.AddScoped<IProductReposatory, ProductReposatory>();
            services.AddScoped<IUserReposatory, UserReposatory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryReposatory,CategoryReposatory>();

            #endregion


        }


    }
}
