using test_app.ViewModel;

namespace test_app.View;

public partial class HTP_ResultPage : ContentPage
{
	public HTP_ResultPage(HTP_ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
}