using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CoreWebApiWithJWT.Models;

namespace CoreWebApiWithJWT.Helper
{
    public interface IJwtHelper
    {
        AuthToken GenerateAuthToken(User user);

        ClaimsPrincipal ValidateAccessToken(string accessToken, out SecurityToken securityToken);
    }
}