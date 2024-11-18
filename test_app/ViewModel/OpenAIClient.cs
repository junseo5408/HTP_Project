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
        string testImg = "https://fastly.picsum.photos/id/194/600/400.jpg?hmac=t_AoqVvThXHdUypviRUOPqKcb1y0A3qTbzk1_K8KcGk";


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
                    ChatMessageContentPart.CreateTextPart("보이는 이미지에 대해 설명해줘. 한국어로"),
                    ChatMessageContentPart.CreateImagePart(new Uri(imageByte), "auto"))
            };

            ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

            return chatCompletion.Content[0].Text;
        }

    }
}
