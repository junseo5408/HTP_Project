using Google.Cloud.Firestore;
using System.Text.Json;

namespace test_app.Model
{
    public class UserData
    {
        public static string Email { get; set; }
        public static string Name { get; set; }
        public static bool IsLoggedin { get; set; }

        public class Base
        {
            public string email { get; set; }
            public string name { get; set; }
        }

        public class ResultHTP_Data()
        {
            public string house { get; set; }
            public string tree { get; set; }
            public string person { get; set; }
            public string result { get; set; }
        }

        public class ResultRor_Data()
        {
            public string Result { get; set; }

        }

        private string FileName = "UserData.json";
        private string FilePath => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public async Task SaveUserDataAsync()
        {
            var userData = new Base
            {
                email = Email,
                name = Name
            };

            var json = JsonSerializer.Serialize(userData);
            await File.WriteAllTextAsync(FilePath, json);
            //await Application.Current.MainPage.DisplayAlert("알림", "유저정보를 저장했습니다.", "확인");
        }

        public async Task LoadUserData()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = await File.ReadAllTextAsync(FilePath);
                    var userData = JsonSerializer.Deserialize<Base>(json);

                    if (userData != null)
                    {
                        // 화면에 이름과 이메일 표시 (예: Entry 컨트롤 사용)
                        Email = userData.email;
                        Name = userData.name;
                    }
                    IsLoggedin = true;
                    //await Application.Current.MainPage.DisplayAlert("알림", "유저정보가 존재합니다.", "확인");
                }
                else
                {
                    IsLoggedin = false;
                    //await Application.Current.MainPage.DisplayAlert("알림", "유저정보가 존재하지 않습니다.", "확인");

                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("경고", "유저정보 불러오기에 실패했습니다.", "확인");
            }
        }
    }
}
