using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using test_app.Model;
using test_app.View;
using static test_app.Model.FirebaseProperty;
using static test_app.Model.UserData;


namespace test_app.ViewModel
{
    public partial class ArchiveViewModel : ObservableObject
    {
        private DataBaseConnect _connect = new DataBaseConnect();

        public static ICollection<ResultHTP_Data> HTP_Collection;
        public static ICollection<ResultRor_Data> Ror_Collection;

        [RelayCommand]
        async Task GoResultPage()
        {
            await LoadAndSet();
            await Shell.Current.GoToAsync(nameof(ResultListPage));
        }

        async Task LoadAndSet()
        {
            HTP_Collection = await _connect.LoadHTP_Data();
            Ror_Collection = await _connect.LoadRorData();
        }
    }
}
