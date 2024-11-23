using test_app.ViewModel;

namespace test_app.View;

public partial class Rorschach_TestPage : ContentPage
{
	public Rorschach_TestPage()
	{
		InitializeComponent();
        BindingContext = new Rorschach_ViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // 초기 데이터 설정
        (BindingContext as Rorschach_ViewModel)?.SetValue(0);
    }

}