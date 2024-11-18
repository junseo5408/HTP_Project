using test_app.ViewModel;

namespace test_app.View;

public partial class CameraPage : ContentPage
{
	public CameraPage(HTP_ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private void MyCamera_MediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    {
		//MyCamera.CaptureImage();
    }
}