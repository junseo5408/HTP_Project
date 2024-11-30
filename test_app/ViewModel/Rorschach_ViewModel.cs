using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using test_app.Model;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class Rorschach_ViewModel : ObservableObject
    {
        OpenAIClient openAI;
        DataBaseConnect db = new DataBaseConnect();
        private List<string> answers = new List<string>();

        [ObservableProperty]
        private string name = Model.UserData.Name;

        [ObservableProperty]
        private float persent;

        [ObservableProperty]
        private int step;

        [ObservableProperty]
        private ImageSource? sampleImage;

        [ObservableProperty]
        private string first;

        [ObservableProperty]
        private string second;

        [ObservableProperty]
        private string third;

        [ObservableProperty]
        private string fourth;

        [ObservableProperty]
        private int progressValue;

        private string _outputMsg = RorschachTest.ResultMsg;
        public string OutputMsg
        {
            get { return _outputMsg; }
            set { _outputMsg = value; }
        }


        [RelayCommand]
        async Task GoHome()
        {
            RemovePage();
            await Shell.Current.GoToAsync("//HomePage");
        }

        [RelayCommand]
        async Task GoStartPage()
        {
            await Shell.Current.GoToAsync(nameof(Rorschach_StartPage));
        }

        async Task GoResultPage()
        {
            await Shell.Current.GoToAsync(nameof(Rorschach_ResultPage));
        }

        [RelayCommand]
        async Task StartTest()
        {
            await Shell.Current.GoToAsync(nameof(Rorschach_TestPage));
        }

        //테스트 답안버튼선택
        [RelayCommand]
        async Task FirstChoise()
        {
            await recodeSelection(First);

        }

        [RelayCommand]
        async Task SecondChoise()
        {
            await recodeSelection(Second);

        }

        [RelayCommand]
        async Task ThirdChoise()
        {
            await recodeSelection(Third);

        }

        [RelayCommand]
        async Task FourthChoise()
        {
            await recodeSelection("떠오르지 않음");

        }

        async Task OpenLoadingPage()
        {
            await Shell.Current.GoToAsync(nameof(LoadingPage));
        }

        private void RemovePage()
        {
            var stack = Shell.Current.Navigation.NavigationStack.ToArray();
            for (int i = stack.Length - 1; i > 0; i--)
            {
                Shell.Current.Navigation.RemovePage(stack[i]);
            }
        }

        public void SetValue(int index)
        {
            Step = RorschachTest.TestItems[index].Step;
            Persent = Step * 10;
            SampleImage = RorschachTest.TestItems[index].SampleImage;
            First = RorschachTest.TestItems[index].FirstChoice;
            Second = RorschachTest.TestItems[index].SecondChoice;
            Third = RorschachTest.TestItems[index].ThirdChoice;
            //ProgressValue = Step+1 * 10;
            Debug.WriteLine($"Step: {Step}, First: {First}, Second: {Second}, Third: {Third}");
        }
        async Task recodeSelection(string choise)
        {
            if (Step != 10)
            {
                answers.Add(choise);
                SetValue(Step);
            }
            else if (Step == 10)
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("진단을 시작해볼까요?", "수정이 필요하시면 아니요를 선택해주세요", "네", "아니오");
                if (answer == true)
                {
                    answers.Add(choise);
                    //gpt 분석이동
                    await OpenLoadingPage();

                    await getMsgAsync();

                    await GoResultPage();
                }
                else
                {

                }
            }
        }

        async Task getMsgAsync()
        {
            openAI = new OpenAIClient();
            RorschachTest.ResultMsg = await openAI.GetRorschachResultAsync(answers);
            await db.Add_RorResult(RorschachTest.ResultMsg);
            RemovePage();
        }
    }
}
