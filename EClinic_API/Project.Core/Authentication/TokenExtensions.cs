using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Project.Core.Authentication
{
    public static class TokenExtensions
    {
        public static void SetTokenOptions(this IConfiguration configuration)
        {
            TokenOptions tokenOptions = new TokenOptions
            {
                ExpireAccessToken = Convert.ToInt32(configuration["JWTToken:ExpireAccessToken"]),
                ExpireRefreshToken = Convert.ToInt32(configuration["JWTToken:ExpireRefreshToken"])
            };
            AppSettings.TokenOptions = tokenOptions;
        }
        public static TokenModel GetToken(JWTTokenInformation tokenInformation)
        {
            JWTOptions jWTOptions = AppSettings.JWTOptions;
            TokenOptions tokenOptions = AppSettings.TokenOptions;
            var accessToken = GenerateAccessToken(tokenInformation, jWTOptions, tokenOptions);
            var refreshToken = GenerateRefreshToken(tokenInformation, jWTOptions, tokenOptions);
            return new TokenModel { AccessToken = accessToken, RefreshToken = refreshToken };
        }   
        private static string GenerateAccessToken(JWTTokenInformation tokenInformation, JWTOptions jWTOptions, TokenOptions tokenOptions)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(jWTOptions.SecretKey);
            var expires = DateTime.UtcNow.AddDays(tokenOptions.ExpireAccessToken);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                     new Claim(ClaimTypes.Role,tokenInformation.Role),
                     new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                     new Claim("UserID",tokenInformation.UserID.ToString())
                }),
                Expires = expires,
                Issuer = jWTOptions.ValidIssuer,
                Audience= jWTOptions.ValidAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }
        private static string GenerateRefreshToken(JWTTokenInformation tokenInformation, JWTOptions jWTOptions, TokenOptions tokenOptions)
        {
            var claims = new[] {
                new Claim("IsRefresh", "true"),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserID",tokenInformation.UserID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!mdQ1%3Tdx8T3C61LB%ewG"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jWTOptions.ValidIssuer,
                jWTOptions.ValidAudience,
                claims,
                expires: DateTime.UtcNow.AddDays(tokenOptions.ExpireRefreshToken),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static bool CheckExpires(string Token)
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var token = new JwtSecurityToken(jwtEncodedString: Token);
            if (token.Payload.Exp < unixTimestamp)
            {
                return true;
            }
            return false;
        }
        public static Guid GetID(string Token)
        {
            var token = new JwtSecurityToken(jwtEncodedString: Token);
            var Id = token.Payload.FirstOrDefault(x => x.Key == "UserID").Value.ToString();
            return Guid.Parse(Id);
        }
        public static bool IsRefreshToken(string Token)
        {
            var token = new JwtSecurityToken(jwtEncodedString: Token);
            var isRefresh = token.Payload.FirstOrDefault(x => x.Key == "IsRefresh").Value.ToString();
            return (isRefresh != null) ? Convert.ToBoolean(isRefresh) : false;
        }
        public static bool IsValidationToken(string Token)
        {
            JWTOptions jWTOptions = AppSettings.JWTOptions;
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var parameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jWTOptions.ValidAudience,
                ValidIssuer = jWTOptions.ValidIssuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!mdQ1%3Tdx8T3C61LB%ewG")),
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                jwtTokenHandler.ValidateToken(Token, parameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
