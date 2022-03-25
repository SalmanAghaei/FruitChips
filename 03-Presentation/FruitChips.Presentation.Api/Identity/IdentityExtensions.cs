using Utilities;
using System.Text;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FruitChips.Presentation.Api.Identity
{
    public static class IdentityExtensions
    {

        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return identity?.FindFirst(claimType)?.Value;
        }
        public static string FindFirstValue(this IIdentity identity, string claimType)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity?.FindFirstValue(claimType);
        }

        public static string GetUserId(this IIdentity identity)
        {
            return identity?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            var userId = identity?.GetUserId();
            return userId.HasValue()
                ? (T)Convert.ChangeType(userId, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public static string GetUserName(this IIdentity identity)
        {
            return identity?.FindFirstValue(ClaimTypes.Name);
        }
        public static AuthenticationBuilder AddCustomeJwtBearer(this AuthenticationBuilder builder)
            => builder.AddCustomeJwtBearer(JwtBearerDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddCustomeJwtBearer(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
            => builder.AddCustomeJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddCustomeJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, Action<JwtBearerOptions> configureOptions)
            => builder.AddCustomeJwtBearer(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddCustomeJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<JwtBearerOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerPostConfigureOptions>());
            return builder.AddScheme<JwtBearerOptions, CustomeJwtBearerHandler>(authenticationScheme, displayName, configureOptions);
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtInfo settings)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCustomeJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(settings.Key);
                var encryptionkey = Encoding.UTF8.GetBytes(settings.EncryptKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = settings.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = settings.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;


            });
            return services;
        }
    }
}
