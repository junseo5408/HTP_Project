namespace test_app.View;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();

        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = false
        });

    }
}