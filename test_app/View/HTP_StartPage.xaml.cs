using test_app.ViewModel;

namespace test_app.View;

public partial class HTP_StartPage : ContentPage
{
	public HTP_StartPage(HTP_ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}