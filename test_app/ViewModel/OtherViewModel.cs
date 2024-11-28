using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.ViewModel
{
    public partial class OtherViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name = Model.UserData.Name;
        [ObservableProperty]
        private string email = Model.UserData.Email;
    }
}
