using Microsoft.IdentityModel.Tokens;

namespace CoreWebApiWithJWT
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Subject { get; set; }

        public string Audience { get; set; }

        public int TokenExpireSeconds { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}
