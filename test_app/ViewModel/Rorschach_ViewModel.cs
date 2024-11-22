
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using test_app.Model;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class Rorschach_ViewModel : ObservableObject
    {
        private int choiseIndex;
        private int _step;
        private int progressValue;

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public int ProgressValue
        {
            get { return progressValue; }
            set { progressValue = value; }
        }

        private IList<RorschachTest> _rorschach = new List<RorschachTest>
        {
            new RorschachTest{ Step = "1", TestImage = "R1", FirstChoice="나비", SecondChoice="박쥐",ThirdChoice ="괴물"},
            new RorschachTest{ Step = "2", TestImage = "R2", FirstChoice="곰", SecondChoice="핏자국",ThirdChoice ="불꽃"},
            new RorschachTest{ Step = "3", TestImage = "R3", FirstChoice="춤추는 사람", SecondChoice="심장",ThirdChoice ="나비"},
            new RorschachTest{ Step = "4", TestImage = "R4", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = "5", TestImage = "R5", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = "6", TestImage = "R6", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = "7", TestImage = "R7", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = "8", TestImage = "R8", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = "9", TestImage = "R9", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = "10", TestImage = "R0", FirstChoice="", SecondChoice="",ThirdChoice =""}
        };

        [RelayCommand]
        async Task GoHome()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task GoStartPage()
        {
            await Shell.Current.GoToAsync(nameof(Rorschach_StartPage));
        }

        [RelayCommand]
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
            choiseIndex = 0;

        }

        [RelayCommand]
        async Task SecondChoise()
        {
            choiseIndex = 1;

        }

        [RelayCommand]
        async Task ThirdChoise()
        {
            choiseIndex = 2;

        }

        [RelayCommand]
        async Task FourthChoise()
        {
            choiseIndex = 3;

        }

        async Task recodeSelection(int index)
        {
            // if(step ==4)

        }
    }
}
