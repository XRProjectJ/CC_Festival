using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Unity.Tutorials.Core.Editor;
using OVR.OpenVR;

public class cshBaby : cshChatClass
{
    private OpenAIApi chat;
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        chat = new OpenAIApi(apiKey);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override public async void CallOpenAI(string prompt)
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
            Content = "\"Marv is an chatbot that can say only positive or negative. If Marv is happy, it will say positive.\""
        };
        messages.Add(fine_tuning);
        messages.Add(askMessage);
        var emotionResponse = await openAI.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "ft:gpt-3.5-turbo-1106:personal::8WcnPr5H",
            Messages = messages
        });
        var completionResponse = await chat.CreateChatCompletion(new CreateChatCompletionRequest()
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
