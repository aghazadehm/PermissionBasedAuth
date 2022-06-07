using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InsuranceApi.Identity
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBuiltInAuthorization(this IServiceCollection services, TokenValidationOptions tokenValidationOptions)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = tokenValidationOptions.ValidateIssuer,
                    ValidateAudience = tokenValidationOptions.ValidateAudience,
                    ValidateLifetime = tokenValidationOptions.ValidateLifetime,
                    ValidateIssuerSigningKey = tokenValidationOptions.ValidateIssuerSigningKey,
                    ValidIssuer = tokenValidationOptions.ValidIssuer, //Configuration["Jwt:Issuer"],
                    ValidAudience = tokenValidationOptions.ValidAudience, //Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValidationOptions.IssuerSigningKey)) // Configuration["Jwt:Key"]))
                };
            });
            return services;
        }
    }

}
