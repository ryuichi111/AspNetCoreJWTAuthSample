# ASP.NET Core 2.2 / JWT Example
ASP.NET Core 2.2 WebAPI / JWT を使ったWebAPIのサンプル。  

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
![image](https://user-images.githubusercontent.com/1695858/51103017-8e7da900-1824-11e9-929a-1e672226d89e.png)

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
