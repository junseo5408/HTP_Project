using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace test_app.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        OauthService oauthHelper = new OauthService();

        [RelayCommand]
        async Task LoginGoogle()
        {
            await getUserEmail();
        }
        //login
        private async Task getUserEmail()
        {
            string userEmail = await oauthHelper.GetUserEmailAsync();
            Debug.WriteLine(userEmail);
        }
    }
}
