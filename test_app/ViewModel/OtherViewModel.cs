using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_app.Model;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class OtherViewModel : ObservableObject
    {
        UserData user = new UserData();

        [ObservableProperty]
        private string name = Model.UserData.Name;
        [ObservableProperty]
        private string email = Model.UserData.Email;

        [RelayCommand]
        void GoInfoPage()
        {
            Shell.Current.GoToAsync(nameof(InfoPage));
        }

        [RelayCommand]
        async void TapLogOut()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("", "로그아웃을 진행할까요?", "네", "아니오");
            if (answer)
            {
                user.DeleteUserData();
                RemovePage();
                await Shell.Current.GoToAsync("///LoginPage");
            }
        }

        private void RemovePage()
        {
            var stack = Shell.Current.Navigation.NavigationStack.ToArray();
            for (int i = stack.Length - 1; i > 0; i--)
            {
                Shell.Current.Navigation.RemovePage(stack[i]);
            }
        }
    }
}
