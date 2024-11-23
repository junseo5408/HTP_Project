
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using test_app.Model;
using test_app.View;

namespace test_app.ViewModel
{
    public partial class Rorschach_ViewModel : ObservableObject
    {
        private int choiseIndex;
        OpenAIClient openAI;
        private List<string> answers = new List<string>();

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
        private int progressValue;

        [ObservableProperty]
        private string result;


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

        public void SetValue(int index)
        {
            Step = RorschachTest.TestItems[index].Step;
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
                    //gpt 분석이동
                    await ConvertToBase64();
                    await getMsgAsync();
                }
                else
                {

                }
            }
        }

        async Task getMsgAsync()
        {
            openAI = new OpenAIClient();
            Result = await openAI.GetRorschachResultAsync(answers);

            //await GoResultPage();
        }

        async Task ConvertToBase64()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using (var stream = await FileSystem.OpenAppPackageFileAsync("r{" + (i + 1) + "}.png"))
                    using (MemoryStream memory = new MemoryStream())
                    {
                        // 파일 내용을 메모리 스트림으로 복사
                        await stream.CopyToAsync(memory);

                        // Base64로 변환
                        byte[] bytes = memory.ToArray();
                        string mimeType = "image/png";
                        string dataUrl = $"data:{mimeType};base64,{Convert.ToBase64String(bytes)}";
                        RorschachTest.SampleImagesUrl[i] = dataUrl;
                        // 결과 페이지로 이동 (필요 시 추가 작업)
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("오류", $"로르샤흐 Base64 변환 중 문제가 발생했습니다: {ex.Message}", "확인");
                }
            }
        }
    }
}
