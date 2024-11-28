namespace test_app.View;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = false
        });
    }
}