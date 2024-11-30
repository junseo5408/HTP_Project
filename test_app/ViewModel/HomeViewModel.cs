using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        [RelayCommand]
        void GoHTP()
        {
            Shell.Current.GoToAsync(nameof(HTP_StartPage));
        }

        [RelayCommand]
        void GoRorschach()
        {
            Shell.Current.GoToAsync(nameof(Rorschach_StartPage));
        }
    }
}