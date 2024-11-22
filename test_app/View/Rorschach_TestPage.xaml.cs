using test_app.ViewModel;

namespace test_app.View;

public partial class Rorschach_TestPage : ContentPage
{
	public Rorschach_TestPage(Rorschach_ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}