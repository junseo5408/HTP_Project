using Android.Graphics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using test_app.Model;
using test_app.View;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using Image = SixLabors.ImageSharp.Image;



namespace test_app.ViewModel
{
    public partial class HTP_ViewModel : ObservableObject
    {
        OpenAIClient openAI;
        private string _outputMsg = HTP_Data.ResultMsg;
        private string resizedImagePath;
        //private bool _resultOut = HTP_Data.isResultOut;
        //private bool _isLoading = HTP_Data.isLoading;

        public string OutputMsg
        {
            get { return _outputMsg; }
            set { _outputMsg = value; }
        }

        //public bool ResultOut
        //{
        //    get { return _resultOut; }
        //    set { _resultOut = value; }
        //}

        //public bool isLoading
        //{
        //    get { return _isLoading; }
        //    set { _isLoading = value; }
        //}

        [RelayCommand]
        async Task GoHome()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task GoCameraPage()
        {
            await Shell.Current.GoToAsync(nameof(CameraPage));
        }

        [RelayCommand]
        async Task OpenMediaPicker()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult myPhoto = await MediaPicker.Default.PickPhotoAsync();

                if (myPhoto != null)
                {
                    // 로컬 파일 경로 설정
                    string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);

                    try
                    {
                        // 원본 이미지를 로컬 저장소에 저장
                        using (Stream sourceStream = await myPhoto.OpenReadAsync())
                        using (FileStream localFileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await sourceStream.CopyToAsync(localFileStream);
                        }

                        // Base64 변환 및 추가 작업
                        await OpenLoadingPage();
                        await ConvertToBase64(ResizeImage(localFilePath, localFilePath, 800, 600));
                    }
                    catch (Exception ex)
                    {
                        // 오류 메시지 출력
                        await Shell.Current.DisplayAlert("오류", $"이미지 처리 중 문제가 발생했습니다: {ex.Message}", "확인");
                    }
                }
            }
            else
            {
                // MediaPicker 지원되지 않는 디바이스 처리
                await Shell.Current.DisplayAlert("경고", "해당 디바이스는 사진 선택을 지원하지 않습니다.", "확인");
            }
        }

        [RelayCommand]
        async Task TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // 로컬 파일 경로 설정
                    string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    try
                    {
                        // 파일을 로컬로 저장
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        using (FileStream localFileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await sourceStream.CopyToAsync(localFileStream);
                        }

                        // 저장된 파일을 Base64로 변환
                        await OpenLoadingPage();
                        await ConvertToBase64(ResizeImage(localFilePath, localFilePath, 800, 600));
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("오류", $"사진 처리 중 문제가 발생했습니다: {ex.Message}", "확인");
                    }
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("경고", "해당 디바이스는 사진 캡처를 지원하지 않습니다.", "확인");
            }
        }

        async Task ConvertToBase64(string filePath)
        {
            try
            {
                // 파일을 읽기 전용 모드로 열기
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (MemoryStream memory = new MemoryStream())
                {
                    // 파일 내용을 메모리 스트림으로 복사
                    await stream.CopyToAsync(memory);

                    // Base64로 변환
                    byte[] bytes = memory.ToArray();
                    HTP_Data.base64String = Convert.ToBase64String(bytes);

                    // 결과 페이지로 이동 (필요 시 추가 작업)
                    await getMsgAsync();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("오류", $"Base64 변환 중 문제가 발생했습니다: {ex.Message}", "확인");
            }
        }

        async Task getMsgAsync()
        {
            string mimeType = "image/Jpeg"; // 이미지 형식에 따라 적절히 설정
            string dataUrl = $"data:{mimeType};base64,{HTP_Data.base64String}";

            openAI = new OpenAIClient();
            HTP_Data.ResultMsg = await openAI.GetImageDescriptionAsync(dataUrl);
            HTP_Data.isLoading = false;
            HTP_Data.isResultOut = true;
            await GoResultPage();
        }


        async Task GoResultPage()
        {
            await Shell.Current.GoToAsync(nameof(HTP_ResultPage));
        }

        async Task OpenLoadingPage()
        {
            await Shell.Current.GoToAsync(nameof(LoadingPage));
        }

        string ResizeImage(string inputPath, string outputPath, int width, int height)
        {
            using (var image = Image.Load(inputPath))
            {
                // 이미지 리사이즈
                image.Mutate(x => x.Resize(width, height));

                // JPEG 포맷으로 저장 (품질 설정 가능)
                image.Save(outputPath, new JpegEncoder { Quality = 75 });
            }

            return outputPath;
        }

    }
}