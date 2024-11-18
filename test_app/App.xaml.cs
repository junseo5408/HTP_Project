namespace test_app
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("32372e302e30Bk4DG2ulrEuWapPxoUpDN6z5K8DLYi9u0y4Ei28wEZ0=");
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
