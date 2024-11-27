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
        private string _houseMsg = HTP_Data.HouseMsg;
        private string _treeMsg = HTP_Data.TreeMsg;
        private string _personMsg = HTP_Data.PersonMsg;
        //private bool _resultOut = HTP_Data.isResultOut;
        //private bool _isLoading = HTP_Data.isLoading;

        public string OutputMsg
        {
            get { return _outputMsg; }
            set { _outputMsg = value; }
        }

        public string HouseMsg
        {
            get { return _houseMsg; }
            set { _houseMsg = value; }
        }

        public string TreeMsg
        {
            get { return _treeMsg; }
            set { _treeMsg = value; }
        }

        public string PersonMsg
        {
            get { return _personMsg; }
            set { _personMsg = value; }
        }

        [RelayCommand]
        async Task GoHome()
        {
            RemovePage();
            await Shell.Current.GoToAsync("//HomePage");
        }

        [RelayCommand]
        async Task OpenMediaPicker()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult myPhoto = await MediaPicker.Default.PickPhotoAsync();

                if (myPhoto != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                    try
                    {
                        using (Stream sourceStream = await myPhoto.OpenReadAsync())
                        using (FileStream localFileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await sourceStream.CopyToAsync(localFileStream);
                        }

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
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    try
                    {
                        using (Stream sourceStream = await photo.OpenReadAsync())
                        using (FileStream localFileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await sourceStream.CopyToAsync(localFileStream);
                        }
                        await OpenLoadingPage();
                        await ConvertToBase64(ResizeImage(localFilePath, localFilePath, 700, 500));
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

        [RelayCommand]
        async Task Testing()
        {
            await getMsgAsync();
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

        //Get Message H.T.P
        async Task getMsgAsync()
        {
            string mimeType = "image/Jpeg";
            string dataUrl = $"data:{mimeType};base64,{HTP_Data.base64String}";

            openAI = new OpenAIClient();
            string all_msg = await openAI.GetPictureDescriptionAsync(dataUrl);
            SetMiddleString(all_msg);
            //HTP_Data.isLoading = false;
            //HTP_Data.isResultOut = true;
            await GoResultPage();
        }

        async Task GoResultPage()
        {
            await Shell.Current.GoToAsync(nameof(HTP_ResultPage));
            RemovePage();
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
                image.Save(outputPath, new JpegEncoder { Quality = 60 });
            }

            return outputPath;
        }

        private void RemovePage()
        {
            var stack = Shell.Current.Navigation.NavigationStack.ToArray();
            for (int i = stack.Length - 1; i > 0; i--)
            {
                Shell.Current.Navigation.RemovePage(stack[i]);
            }
        }

        //텍스트 분리
        private void SetMiddleString(string str)
        {
            string a1 = "1a";
            string b1 = "1b";
            string a2 = "2a";
            string b2 = "2b";
            string a3 = "3a";
            string b3 = "3b";
            string a4 = "4a";
            string b4 = "4b";


            // 유효성 검증
            if (string.IsNullOrEmpty(str))
            {
                //Console.WriteLine("입력 문자열이 비어있습니다.");
                return;
            }

            try
            {
                // 1a와 1b 사이 내용 추출
                int startIndex1 = str.IndexOf(a1) + a1.Length;
                int endIndex1 = str.IndexOf(b1);
                if (startIndex1 > 0 && endIndex1 > startIndex1)
                {
                    HTP_Data.HouseMsg = str.Substring(startIndex1, endIndex1 - startIndex1).Replace("\n","").Replace("\r","");
                }

                // 2a와 2b 사이 내용 추출
                int startIndex2 = str.IndexOf(a2) + a2.Length;
                int endIndex2 = str.IndexOf(b2);
                if (startIndex2 > 0 && endIndex2 > startIndex2)
                {
                    HTP_Data.TreeMsg = str.Substring(startIndex2, endIndex2 - startIndex2).Replace("\n", "").Replace("\r", "");
                }

                // 3a와 3b 사이 내용 추출
                int startIndex3 = str.IndexOf(a3) + a3.Length;
                int endIndex3 = str.IndexOf(b3);
                if (startIndex3 > 0 && endIndex3 > startIndex3)
                {
                    HTP_Data.PersonMsg = str.Substring(startIndex3, endIndex3 - startIndex3).Replace("\n", "").Replace("\r", "");
                }

                // 4a와 4b 사이 내용 추출
                int startIndex4 = str.IndexOf(a4) + a4.Length;
                int endIndex4 = str.IndexOf(b4);
                if (startIndex4 > 0 && endIndex4 > startIndex4)
                {
                    HTP_Data.ResultMsg = str.Substring(startIndex4, endIndex4 - startIndex4).Replace("\n", "").Replace("\r", "");
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine($"오류 발생: {ex.Message}");
            }
        }
    }
}