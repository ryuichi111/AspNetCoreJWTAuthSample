namespace CoreWebApiWithJWT.Models
{
    /// <summary>
    /// ログインリクエストモデル
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
