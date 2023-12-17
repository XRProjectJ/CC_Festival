using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Unity.Tutorials.Core.Editor;
using OVR.OpenVR;

public class cshChatGpt : MonoBehaviour
{
    private string engine = "text-davinci-003";
    private OpenAIApi baby;
    private OpenAIApi adviser;
    private List<ChatMessage> messages = new List<ChatMessage>();
    // Start is called before the first frame update
    void Start()
    {
        string apiKey = System.Environment.GetEnvironmentVariable("YOUR_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("API KEY 설정이 안되어 있음, 설정해도 오류가 난다면 재부팅 해보길 바람");
            return;
        }
        
        adviser = new OpenAIApi(apiKey);
        baby = new OpenAIApi(apiKey);
        //CallGPT(prompt);
    }
    public OpenAIApi getAdviser()
    {
        return adviser;
    }
    public OpenAIApi getBaby()
    {
        return baby;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public async void CallAdviser(string prompt)
    {
        var askMessage = new ChatMessage()
        {
            Role = "user",
            Content = prompt
        };
       
        Debug.Log(prompt);
        messages.Add(askMessage);
        var completionResponse = await adviser.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });
        string response = completionResponse.Choices[0].Message.Content;
        Debug.Log(response);
        this.GetComponent<cshTTS>().textToSpeech(response, TTSVoice.Shimmer);
    }
    public async void CallBaby(string prompt)
    {
        var askMessage = new ChatMessage()
        {
            Role = "user",
            Content = prompt
        };

        Debug.Log(prompt);
        var fine_tuning = new ChatMessage()
        {
            Role = "system",
            Content = "\"Marv is an  chatbot that can say only positive or negative. If Marv is happy, it will say positive.\""
        };
        messages.Add(fine_tuning);
        messages.Add(askMessage);
        var emotionResponse = await baby.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "ft:gpt-3.5-turbo-1106:personal::8WcnPr5H",
            Messages = messages
        });
        var completionResponse = await adviser.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });
        string emotion = emotionResponse.Choices[0].Message.Content;
        Debug.Log(emotion);
        string response = completionResponse.Choices[0].Message.Content;
        Debug.Log(response);
        this.GetComponent<cshTTS>().textToSpeech(response, TTSVoice.Nova);
    }
}
