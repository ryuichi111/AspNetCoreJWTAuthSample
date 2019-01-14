namespace CoreWebApiWithJWT.Models
{
    public sealed class AuthToken
    {
        public string AccessToken { get; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; }

        public AuthToken(string accessToken, string refreshToken, int expiresIn)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
            this.ExpiresIn = expiresIn;
        }
    }
}
