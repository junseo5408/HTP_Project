
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace test_app.ViewModel
{
    public partial class Rorschach_ViewModel : ObservableObject
    {
        [RelayCommand]
        async Task GoHome()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
