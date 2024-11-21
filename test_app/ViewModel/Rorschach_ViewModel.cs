
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class Rorschach_ViewModel : ObservableObject
    {
        [RelayCommand]
        async Task GoHome()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task GoResult()
        {
            await Shell.Current.GoToAsync(nameof(Rorschach_ResultPage));
        }
    }
}
