namespace CoreWebApiWithJWT.Models
{
    /// <summary>
    /// トークン更新リクエストモデル
    /// </summary>
    public class UpdateTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
