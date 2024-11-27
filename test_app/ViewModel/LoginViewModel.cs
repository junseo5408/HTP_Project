using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        DataBaseConnect fireBase = new DataBaseConnect();
        private bool isLoginSuccess;
        private bool isEmailCheck;

        [ObservableProperty]
        private string loginEmail;

        [ObservableProperty]
        private string loginPassword;

        [ObservableProperty]
        private string suEmail;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string suPassword;

        [RelayCommand]
        async Task Login()
        {
            isLoginSuccess = await fireBase.LoginCheck(LoginEmail, LoginPassword);
            if (!isLoginSuccess) { }
            //로그인완료
            //await Shell.Current.Navigation.PushAsync(HomePage);
        }

        [RelayCommand]
        void GoSignUpPage()
        {
            Shell.Current.GoToAsync(nameof(SignUpPage));
        }

        [RelayCommand]
        async Task SignUp_Clicked()
        {
            isEmailCheck = await fireBase.EmailCheck(SuEmail);
            if(isEmailCheck)
        }
    }
}
