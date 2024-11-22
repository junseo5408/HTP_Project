
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
        private ImageSource _sampleImg;
        private int _progressValue;
        private string _first;
        private string _second;
        private string _third;


        public int Step
        {
            get { return _step; }
        }

        public ImageSource SampleImage
        {
            get { return _sampleImg; }
        }

        public string First
        {
            get { return _first; }
        }

        public string Second
        {
            get { return _second; }
        }

        public string Third
        {
            get { return _third; }
        }

        public int ProgressValue
        {
            get { return _progressValue; }
        }

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

        void SetValue(int step)
        {
            _step = RorschachTest.TestItems[step-1].Step;
            _sampleImg = RorschachTest.TestItems[step - 1].SampleImage;
            _first = RorschachTest.TestItems[step - 1].FirstChoice;
            _second = RorschachTest.TestItems[step - 1].SecondChoice;
            _third = RorschachTest.TestItems[step - 1].ThirdChoice;
            _progressValue = _step * 10;
        }
        async Task recodeSelection(int index)
        {
            // if(step ==4)

        }
    }
}
