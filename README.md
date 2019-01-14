# ASP.NET Core 2.2 / JWT Example
ASP.NET Core 2.2 WebAPI / JWT を使ったWebAPIのサンプル。  

  
1. 以下で認証を行いAccessToken / RefreshTokenを取得します。  
```
POST https://localhost:44341/api/token
{ "Email": "daigo@testtest.jp",  "Password": "P@ssword" }
```

2. 以下でトークンによる認証を行いAccessToken / RefreshTokenを取得します。  
```
GET https://localhost:44341/api/vaGET 
Bearer Token : 1で取得したアクセストークン
```

3. 以下でアクセストークン/リフレッシュトークンを更新します。  
```
POST https://localhost:44341/api/token/rePOST 
{
 "accessToken": 1で取得したアクセストークン
 "refreshToken": 1で取得したリフレッシュトークン
    
}
```
