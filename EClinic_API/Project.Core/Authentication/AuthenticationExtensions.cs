using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Model;
using System.Text;

namespace Project.Core.Authentication
{
    public static class AuthenticationExtensions
    {
        public static JWTOptions GetJWTOptions(this IConfiguration configuration)
        {
            JWTOptions jWTOptions = new JWTOptions
            {
                ValidIssuer = configuration["JWT:ValidIssuer"],
                ValidAudience = configuration["JWT:ValidAudience"],
                SecretKey = configuration["JWT:SecretKey"]
            };
            AppSettings.JWTOptions = jWTOptions;
            return AppSettings.JWTOptions;
        }
        public static IServiceCollection AddMyAuthentication(this IServiceCollection services, JWTOptions jWTOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jWTOptions.ValidAudience,
                    ValidIssuer = jWTOptions.ValidIssuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTOptions.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddleware>();
            return services;
        }
    }
}

