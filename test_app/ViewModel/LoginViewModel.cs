using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        DataBaseConnect fireBase = new DataBaseConnect();
        private bool isUser;
        private bool isEmailCheck;

        [ObservableProperty]
        private string loginEmail;

        [ObservableProperty]
        private string loginPwd;

        [ObservableProperty]
        private string regEmail;

        [ObservableProperty]
        private string regPwd;

        [ObservableProperty]
        private string regCheckPwd;

        [ObservableProperty]
        private string regName;

        Model.UserData userData = new Model.UserData();
        [RelayCommand]
        async Task Login_Clicked()
        {
            isUser = await fireBase.LoginCheck(LoginEmail, LoginPwd);
            if (isUser)
            {
                await userData.SaveUserDataAsync();
                await GoHome();
            }
            else await Application.Current.MainPage.DisplayAlert("로그인 실패", "이메일 또는 비밀번호를 확인해주세요", "확인");
        }

        async Task GoHome()
        {
            RemovePage();
            //await Shell.Current.GoToAsync(nameof(HomePage));
            await Shell.Current.GoToAsync("///HomePage");
        }

        [RelayCommand]
        void GoSignUpPage()
        {
            Shell.Current.GoToAsync(nameof(SignUpPage));
        }

        [RelayCommand]
        public async Task SignUp_Clicked()
        {
            isEmailCheck = await fireBase.EmailCheck(RegEmail);
            if (isEmailCheck)
            {
                await fireBase.Add_User(RegEmail, RegName, RegPwd);
                await Application.Current.MainPage.DisplayAlert("환영합니다", "회원가입에 성공하셨습니다.", "확인");
                await Shell.Current.GoToAsync("..");
            }
        }

        private void RemovePage()
        {
            var stack = Shell.Current.Navigation.NavigationStack.ToArray();
            for (int i = stack.Length - 1; i > 0; i--)
            {
                Shell.Current.Navigation.RemovePage(stack[i]);
            }
        }

        public async Task LoginCheck()
        {
            await userData.LoadUserData();
            if (Model.UserData.IsLoggedin == true)
            {
                //await Application.Current.MainPage.DisplayAlert("로그인정보", "로그인이 되어있습니다.", "확인");

                await Shell.Current.GoToAsync("///HomePage");
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("로그인정보", "로그인이 안되어있습니다.", "확인");
            }

        }
    }
}
