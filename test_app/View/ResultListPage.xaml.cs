using test_app.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace test_app.View;

public partial class ResultListPage : ContentPage
{
	public ResultListPage(ArchiveViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

        HTP_List.ItemsSource = ArchiveViewModel.HTP_Collection;
        RorList.ItemsSource = ArchiveViewModel.Ror_Collection;

        htp_count.Text = ArchiveViewModel.HTP_Collection.Count().ToString();
        ror_count.Text = ArchiveViewModel.Ror_Collection.Count().ToString();
    }
}