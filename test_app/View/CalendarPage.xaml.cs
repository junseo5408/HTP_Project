using Syncfusion.Maui.Calendar;
using test_app.ViewModel;

namespace test_app.View;

public partial class CalendarPage : ContentPage
{
    SfCalendar calendar = new SfCalendar();
    public CalendarPage(ArchiveViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}