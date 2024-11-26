using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {

        [RelayCommand]
        async Task GoHTP()
        {
            await Shell.Current.GoToAsync(nameof(HTP_StartPage));
        }

        [RelayCommand]
        async Task GoRorschach()
        {
            await Shell.Current.GoToAsync(nameof(Rorschach_StartPage));
        }

        [RelayCommand]
        async Task GoTest()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

    }
}