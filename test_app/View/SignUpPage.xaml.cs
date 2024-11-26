using test_app.ViewModel;

namespace test_app.View;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(LoginViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = false
        });
    }
}