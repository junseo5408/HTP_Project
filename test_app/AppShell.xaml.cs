using test_app.View;

namespace test_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HTP_StartPage), typeof(HTP_StartPage));
            Routing.RegisterRoute(nameof(Rorschach_StartPage), typeof(Rorschach_StartPage));
            Routing.RegisterRoute(nameof(HTP_ResultPage), typeof(HTP_ResultPage));
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(Rorschach_TestPage), typeof(Rorschach_TestPage));
            Routing.RegisterRoute(nameof(Rorschach_ResultPage), typeof(Rorschach_ResultPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(FiveWhysPage), typeof(FiveWhysPage));
            Routing.RegisterRoute(nameof(InfoPage), typeof(InfoPage));
            Routing.RegisterRoute(nameof(ResultListPage), typeof(ResultListPage));
            Routing.RegisterRoute(nameof(CalendarPage), typeof(CalendarPage));


            //Routing.RegisterRoute(nameof(CameraPage), typeof(CameraPage));
        }
    }
}
