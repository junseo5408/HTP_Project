using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace test_app.ViewModel
{
    public partial class FiveWhysViewModel : ObservableObject
    {
        private OpenAIClient openAI = new OpenAIClient();
        private int count = 1;

        private string _baseProblem;
        public string BaseProblem
        {
            get { return _baseProblem; }
            set { _baseProblem = value; }
        }

        [ObservableProperty]
        private IList<string> qnAmessages;

        [ObservableProperty]
        private string message;


        [RelayCommand]
        async Task Send()
        {
            if (message != null)
            {
                qnAmessages.Add(message);
                await getMsgAsync(message);
            }
        }


        async Task getMsgAsync(string msg)
        {
            if (count == 5)
            {
                qnAmessages.Add("그러므로 username님의 근본원인으로는 ~가 예상됩니다.");
            }
            else
            {
                qnAmessages.Add(await openAI.Get5whysResultAsync(msg));
                count++;
            }
        }
    }
}
