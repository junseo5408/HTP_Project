using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading.Tasks;

namespace test_app
{
    public class OauthService
    {
        private static readonly string[] Scopes = { "https://www.googleapis.com/auth/userinfo.email" };
    private const string CredentialPath = "client_secret_1025506103898-f55m1j5pfj439f4lqjc62083t0okchhe.apps.googleusercontent.com.json";
string tokenDirectory = Path.Combine(FileSystem.Current.AppDataDirectory, "GoogleOAuthTokens");

    /// <summary>
    /// 사용자의 이메일을 반환하는 메서드
    /// </summary>
    public async Task<string> GetUserEmailAsync()
    {
        // 사용자 정보를 가져옴
        Userinfo userInfo = await GetUserInfoAsync();

        // 이메일 반환
        return userInfo.Email;
    }

    /// <summary>
    /// 사용자의 전체 정보를 반환하는 메서드
    /// </summary>
    public async Task<Userinfo> GetUserInfoAsync()
    {
        // 인증된 사용자 자격증명 가져오기
        UserCredential credential = await GetUserCredentialAsync();

        // OAuth2 API 클라이언트 생성
        var oauth2Service = new Oauth2Service(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential
        });

        // 사용자 정보 가져오기
        return await oauth2Service.Userinfo.Get().ExecuteAsync();
    }

    /// <summary>
    /// 사용자 인증 정보를 가져오는 메서드
    /// </summary>
    private async Task<UserCredential> GetUserCredentialAsync()
    {
            using (var stream = await FileSystem.OpenAppPackageFileAsync("client_secret_1025506103898-f55m1j5pfj439f4lqjc62083t0okchhe.apps.googleusercontent.com.json"))
            {
                return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(tokenDirectory, true)
                );
            }
    }
    }
}
