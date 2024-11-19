using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using OpenAI.Chat;

namespace test_app.ViewModel
{
    public class OpenAIClient
    {
        private readonly AzureOpenAIClient _openAIClient;
        private readonly string _deploymentName;
        string testImg = "https://mindtoflow.me/wp-content/uploads/2023/07/KakaoTalk_20230712_132813024.jpg";
        private string prompt = "답변은 한국어로 이루어 져야되.\r\n" +
            "사용자가 보낸 사진에 있는 그림을 바탕으로 H(house)T(tree)P(person) 검사 결과를 알려줘.\r\n" +
            "집,나무,사람 순서대로 결과를 알려주는데에 집에 대한 결과를 1a 와 1b 사이에 적어줘 (1a본문내용1b) \r\n" +
            "그리고 나무는 2a, 2b 사이에, 사람은 3a, 3b 사이에 적어줘 " +
            "각 카테고리마다 최소 80자 이상 최대 100자 이내로 간추뤄서 답변이 이루어져야되고 마지막으로 종합결과를 출력해줘 " +
            "종합결과에는 위 내용들과 그림의 상태 및 내용을 인식하여 그린이의 심리분석을 자세히 해주어야해 " +
            "4a, 4b 사이에 내용이 출력되어야되 그리고 250자 이 300자 이내로 출력되어야되\r\n\r\n" +
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

        public async Task<string> GetImageDescriptionAsync(string imageByte)
        {
            var chatClient = _openAIClient.GetChatClient(_deploymentName);

            List<ChatMessage> messages = new List<ChatMessage>
            {
                new UserChatMessage(
                    ChatMessageContentPart.CreateTextPart(prompt),
                    //ChatMessageContentPart.CreateImagePart(new Uri(imageByte), "auto"))
                    ChatMessageContentPart.CreateImagePart(new Uri(testImg), "auto"))

            };

            ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

            return chatCompletion.Content[0].Text;
        }
    }
}
