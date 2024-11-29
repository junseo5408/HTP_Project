using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class OtherViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name = Model.UserData.Name;
        [ObservableProperty]
        private string email = Model.UserData.Email;

        [RelayCommand]
        void GoInfoPage()
        {
            Shell.Current.GoToAsync(nameof(InfoPage));
        }
    }
}
