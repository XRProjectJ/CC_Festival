using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Unity.Tutorials.Core.Editor;

public class cshChatGpt : MonoBehaviour
{
    private string engine = "text-davinci-003";
    private OpenAIApi openAi;
    private List<ChatMessage> messages = new List<ChatMessage>();
    // Start is called before the first frame update
    void Start()
    {
        string apiKey = System.Environment.GetEnvironmentVariable("YOUR_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("API KEY ������ �ȵǾ� ����, �����ص� ������ ���ٸ� ����� �غ��� �ٶ�");
            return;
        }
        
        openAi = new OpenAIApi(apiKey);

        //CallGPT(prompt);
    }
    public OpenAIApi getOpenAiApi()
    {
        return openAi;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void CallGPT(string prompt, string who)
    {
        var askMessage = new ChatMessage()
        {
            Role = "user",
            Content = prompt
        };
        Debug.Log(prompt);
        messages.Add(askMessage);
        var completionResponse = await openAi.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });
        string response = completionResponse.Choices[0].Message.Content;
        Debug.Log(response);
        if (who.Equals("baby"))
        {
            this.GetComponent<cshTTS>().textToSpeech(response, TTSVoice.Nova);
        }
        else
        {
            this.GetComponent<cshTTS>().textToSpeech(response, TTSVoice.Shimmer);
        }
    }

}
