using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using test_app.View;
using test_app.ViewModel;
using Syncfusion.Maui.Core.Hosting;
//using test_app.ViewModel;

namespace test_app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkitCamera()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomeViewModel >();

            builder.Services.AddTransient<HTP_StartPage>();
            builder.Services.AddTransient<HTP_ResultPage>();
            builder.Services.AddTransient<HTP_ViewModel>();

            builder.Services.AddTransient<Rorschach_StartPage>();
            builder.Services.AddTransient<Rorschach_ViewModel>();
            builder.Services.AddTransient<Rorschach_TestPage>();
            builder.Services.AddTransient<Rorschach_ResultPage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
