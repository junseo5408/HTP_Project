using test_app.ViewModel;

namespace test_app.View;

public partial class OtherPage : ContentPage
{
	public OtherPage(OtherViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}