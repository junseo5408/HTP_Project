using test_app.ViewModel;

namespace test_app.View;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = false
        });
    }

    private void EmailField_TextChanged(object sender, TextChangedEventArgs e)
    {
        LoginViewModel.testEmail = EmailField.Text;
    }
}