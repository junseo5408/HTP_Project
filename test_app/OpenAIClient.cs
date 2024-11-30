using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace test_app
{
    public class OpenAIClient
    {
        private readonly AzureOpenAIClient _openAIClient;
        private readonly string _deploymentName;
        //string testImg = "https://mindtoflow.me/wp-content/uploads/2023/07/KakaoTalk_20230712_132813024.jpg";
        private string HTPprompt = "답변은 한국어로 이루어 주어야합니다.\r\n" +
            "사용자가 보낸 사진에 있는 그림을 바탕으로 H(house)T(tree)P(person) 검사 결과를 알려줘.\r\n" +
            "집,나무,사람 순서대로 결과를 알려주는데에 집에 대한 결과를 1a 와 1b 사이에 적어줘 (1a본문내용1b) \r\n" +
            "그리고 나무는 2a, 2b 사이에, 사람은 3a, 3b 사이에 적어줘 " +
            "각 카테고리마다 최소 80자 이상 최대 100자 이내로 간추뤄서 답변이 이루어져야되고 마지막으로 종합결과를 출력해줘 " +
            "종합결과에는 위 내용들과 그림의 상태 및 내용을 인식하여 그린이의 심리분석을 자세히 해주어야해 " +
            "4a, 4b 사이에 내용이 출력되어야되 그리고 200자 이 250자 이내로 출력되어야되\r\n\r\n" +
            "마지막으로 모든 결과는 친절한 말투로 이루어져야되\r\n\r\n" +
            "출력예시:" +
            "\r\n1a출력내용1b\r\n" +
            "2a출력내용2b\r\n" +
            "3a출력내용3b\r\n" +
            "4a출력내용4b";

        public OpenAIClient()
        {
            string endpoint = "https://gpt4oazure.openai.azure.com/";
            string apiKey = "34cTmT3i4rlCKDHbcoOSt0pVmLZrHDDkQMQgrjjZAbwTpcoz2oMHJQQJ99AKACNns7RXJ3w3AAABACOGFum7";
            string modelName = "gpt-4o";
            var uriEndpoint = new Uri(endpoint);
            var credentials = new AzureKeyCredential(apiKey);
            _openAIClient = new AzureOpenAIClient(uriEndpoint, credentials);
            _deploymentName = modelName;
        }

        public async Task<string> GetPictureDescriptionAsync(string imageByte)
        {
            var chatClient = _openAIClient.GetChatClient(_deploymentName);

            List<ChatMessage> messages = new List<ChatMessage>
            {
                new UserChatMessage(
                    ChatMessageContentPart.CreateTextPart(HTPprompt),
                    ChatMessageContentPart.CreateImagePart(new Uri(imageByte), "auto"))
                    //ChatMessageContentPart.CreateImagePart(new Uri(testImg), "auto"))

            };

            ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

            return chatCompletion.Content[0].Text;
        }


        public async Task<string> GetRorschachResultAsync(List<string> values)
        {
            string prompt = "답변은 한국어로 이루어져야합니다.\r\n" +
                "공개된 로르샤흐 테스트의 잉크얼룩 순서대로 얼룩이 무엇과 닮았는지 답변을 하면 로르샤흐 검사에 따라 답변을 분석하여 검사결과를 알려주세요.\r\n" +
                "답변은 검사결과에 대해서만 알려줘야하고 친절한 말투로 이루어져야하며 검사결과 외 의 답변은 나와서는 안됩니다.";

            string answers = "1번: " + values[0] + "\r\n" + "2번: " + values[1] + "\r\n" + "3번: " + values[2] + "\r\n" + "4번: " + values[3] + "\r\n" + "5번: " + values[4] + "\r\n" + "6번: " + values[5] + "\r\n" +
                "7번: " + values[6] + "\r\n" + "8번: " + values[7] + "\r\n" + "9번: " + values[8] + "\r\n" + "10번: " + values[9] + "\r\n";

            var chatClient = _openAIClient.GetChatClient(_deploymentName);

            List<ChatMessage> messages = new List<ChatMessage>
            {
                new UserChatMessage(
                    ChatMessageContentPart.CreateTextPart(prompt),
                    ChatMessageContentPart.CreateTextPart(answers))
            };

            ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

            return chatCompletion.Content[0].Text;
        }

        public async Task<string> Get5whysResultAsync(string msg)
        {
            string prompt = "";


            var chatClient = _openAIClient.GetChatClient(_deploymentName);

            List<ChatMessage> messages = new List<ChatMessage>
            {
                new UserChatMessage(
                    ChatMessageContentPart.CreateTextPart(prompt),
                    ChatMessageContentPart.CreateTextPart(msg))
            };

            ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

            return chatCompletion.Content[0].Text;
        }
    }
}
