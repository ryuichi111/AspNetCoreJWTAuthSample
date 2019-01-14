using CoreWebApiWithJWT.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CoreWebApiWithJWT.Helper
{
    public class JwtHelper : IJwtHelper
    {
        private readonly JwtSettings _jwtSettings = null;

        public JwtHelper(IOptions<JwtSettings> jwtSettings)
        {
            this._jwtSettings = jwtSettings.Value;
        }

        public AuthToken GenerateAuthToken(User user)
        {
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.ID),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Iat, (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).ToString(), ClaimValueTypes.Integer64),
                 new Claim("email", user.Email),
                 new Claim("nickname", user.NickName),
             };

            var jwt = new JwtSecurityToken(
                this._jwtSettings.Issuer,
                this._jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddSeconds(this._jwtSettings.TokenExpireSeconds),
                signingCredentials: _jwtSettings.SigningCredentials);

            return new AuthToken(new JwtSecurityTokenHandler().WriteToken(jwt), this.GenerateRefreshToken(), this._jwtSettings.TokenExpireSeconds);
        }

        public ClaimsPrincipal ValidateAccessToken(string accessToken, out SecurityToken securityToken)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal cp = jwtSecurityTokenHandler.ValidateToken(
                accessToken,
                new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = this._jwtSettings.Issuer,
                    ValidAudience = this._jwtSettings.Audience,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = this._jwtSettings.SigningCredentials.Key,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                },
                out securityToken);
            return cp;
        }

        private string GenerateRefreshToken()
        {
            string token = "";
            var randomNumber = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }
            return token;
        }

    }
}
