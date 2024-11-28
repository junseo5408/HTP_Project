using test_app.ViewModel;

namespace test_app.View;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
        vm.LoginCheck();

        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = false
        });
    }
}