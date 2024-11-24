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
            new RorschachTest{ Step = 1, SampleImage = "r1.png", FirstChoice="나비", SecondChoice="박쥐",ThirdChoice ="잭 오 랜턴"},
            new RorschachTest{ Step = 2, SampleImage = "r2.png", FirstChoice="곰", SecondChoice="핏자국",ThirdChoice ="불꽃"},
            new RorschachTest{ Step = 3, SampleImage = "r3.png", FirstChoice="두 사람", SecondChoice="심장",ThirdChoice ="가슴"},
            new RorschachTest{ Step = 4, SampleImage = "r4.png", FirstChoice="부츠", SecondChoice="고릴라",ThirdChoice ="곰"},
            new RorschachTest{ Step = 5, SampleImage = "r5.png", FirstChoice="나비", SecondChoice="박쥐",ThirdChoice ="움직이는 사람"},
            new RorschachTest{ Step = 6, SampleImage = "r6.png", FirstChoice="코", SecondChoice="잠수함",ThirdChoice ="동물 가죽"},
            new RorschachTest{ Step = 7, SampleImage = "r7.png", FirstChoice="두 여자", SecondChoice="뇌운",ThirdChoice ="램프"},
            new RorschachTest{ Step = 8, SampleImage = "r8.png", FirstChoice="갈비뼈", SecondChoice="동물",ThirdChoice ="나무"},
            new RorschachTest{ Step = 9, SampleImage = "r9.png", FirstChoice="담배 연기", SecondChoice="꽃",ThirdChoice ="폭발"},
            new RorschachTest{ Step = 10, SampleImage = "r10.png", FirstChoice="헤엄치는 해저동물들", SecondChoice="불가사리",ThirdChoice ="별"}
        };

        public static string[] SampleImagesUrl;
    }
}
