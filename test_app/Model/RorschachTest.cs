namespace test_app.Model
{
    public class RorschachTest
    {
        public ImageSource SampleImage;
        public int Step;
        public string FirstChoice;
        public string SecondChoice;
        public string ThirdChoice;

        public static IList<RorschachTest> TestItems = new List<RorschachTest>
        {
            new RorschachTest{ Step = 1, SampleImage = "r1.png", FirstChoice="나비", SecondChoice="박쥐",ThirdChoice ="괴물"},
            new RorschachTest{ Step = 2, SampleImage = "r2.png", FirstChoice="곰", SecondChoice="핏자국",ThirdChoice ="불꽃"},
            new RorschachTest{ Step = 3, SampleImage = "r3.png", FirstChoice="춤추는 사람", SecondChoice="심장",ThirdChoice =""},
            new RorschachTest{ Step = 4, SampleImage = "r4.png", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = 5, SampleImage = "r5.png", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = 6, SampleImage = "r6.png", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = 7, SampleImage = "r7.png", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = 8, SampleImage = "r8.png", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = 9, SampleImage = "r9.png", FirstChoice="", SecondChoice="",ThirdChoice =""},
            new RorschachTest{ Step = 10, SampleImage = "r10.png", FirstChoice="", SecondChoice="",ThirdChoice =""}
        };

        public static string[] SampleImagesUrl;
    }
}
