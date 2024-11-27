using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private int loginEmail;

        [ObservableProperty]
        private string loginPassword;

        [ObservableProperty]
        private int suEmail;

        [ObservableProperty]
        private string suPassword;

        [RelayCommand]
        async Task Login()
        {
            //DB 연결후 이메일 비밀번호 비교
            //만약없으면 알림 


        }

        [RelayCommand]
        void GoSignUpPage()
        {
            Shell.Current.GoToAsync(nameof(SignUpPage));
        }

        [RelayCommand]
        void SignUp_Clicked()
        {
            //수정
        }
    }
}
