# ASP.NET Core 2.2 / JWT Example
ASP.NET Core 2.2 WebAPI / JWT を使ったWebAPIのサンプル。  

* AccessTokenの有効期限は60秒（テスト用に短く src/CoreWebApiWithJWT/appsettings.json）  
* RefreshTokenでAccessTokenRefreshToken更新に対応  
* JWT(https://github.com/jwt-dotnet/jwt)  

## 環境  

* Visual Studio 2019 Preview  
* ASP.NET Core 2.2  
* JWT 5.0.0-beta4  

## 動作  
1. 以下で認証を行いAccessToken / RefreshTokenを取得します。  
```
POST https://localhost:44341/api/token
{ "Email": "daigo@testtest.jp",  "Password": "P@ssword" }
```
![image](https://user-images.githubusercontent.com/1695858/51103094-d8ff2580-1824-11e9-8827-d94521d11107.png)

2. 以下でトークンによる認証を行いAccessToken / RefreshTokenを取得します。  
```
GET https://localhost:44341/api/vaGET 
Bearer Token : 1で取得したアクセストークン
```
![image](https://user-images.githubusercontent.com/1695858/51103099-ddc3d980-1824-11e9-8253-4b1421a6a480.png)

3. 以下でアクセストークン/リフレッシュトークンを更新します。  
```
POST https://localhost:44341/api/token/rePOST 
{
 "accessToken": 1で取得したアクセストークン
 "refreshToken": 1で取得したリフレッシュトークン
    
}
```
![image](https://user-images.githubusercontent.com/1695858/51103111-e87e6e80-1824-11e9-8aa3-9691e4747e80.png)
