using System.Linq;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using CoreWebApiWithJWT.Helper;
using CoreWebApiWithJWT.Models;

namespace CoreWebApiWithJWT.Controllers
{
    /// <summary>
    /// 認証トークンを払い出すコントローラ
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtHelper _jwtHelper;


        public TokenController(IOptions<JwtSettings> jwtSettings, IJwtHelper jwtHelper)
        {
            this._jwtSettings = jwtSettings.Value;
            this._jwtHelper = jwtHelper;
        }

        /// <summary>
        /// POST api/token
        /// ログイン処理を行いAccessToken / RefreshToken / ExpireInを返す。
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("")]
        public ActionResult Login([FromBody] LoginRequest login)
        {
            var user = Models.User.Authenticate(login.Email, login.Password);
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var authToken = this._jwtHelper.GenerateAuthToken(user);

            user.RefreshToken = authToken.RefreshToken;

            return this.CreateJsonContentResult(authToken); ;
        }

        /// <summary>
        /// POST api/token/refresh
        /// AccessToken / RefreshTokenを受け取り、更新したAccessToken / RefreshTokenを返す。
        /// </summary>
        /// <param name="updateTokenRequest"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public ActionResult RefreshToken([FromBody] UpdateTokenRequest updateTokenRequest)
        {
            SecurityToken securityToken = null;
            // アクセストークンからClaimsPrincipalを取得
            ClaimsPrincipal cp = this._jwtHelper.ValidateAccessToken(updateTokenRequest.AccessToken, out securityToken);
            if (cp == null)
            {
                return BadRequest(ModelState);
            }
            // ClaimsPrincipalからID（ユーザID）を取得
            var id = cp.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            // ユーザIDからUserオブジェクトを取得
            var user = Models.User.GetByID(id);
            // リフレッシュトークンの妥当性をチェック
            if (user == null || user.RefreshToken != updateTokenRequest.RefreshToken)
            {
                return BadRequest(ModelState);
            }

            // トークンを更新
            var authToken = this._jwtHelper.GenerateAuthToken(user);
            user.RefreshToken = authToken.RefreshToken;

            return this.CreateJsonContentResult(authToken); ;
        }

        /// <summary>
        /// オブジェクトをJsonにしてContentResultを作成
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private ContentResult CreateJsonContentResult(object obj)
        {
            JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            ContentResult contentResult = new ContentResult();
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)HttpStatusCode.OK;
            contentResult.Content = JsonConvert.SerializeObject(obj, Formatting.Indented, Settings);
            return contentResult;
        }
    }
}

