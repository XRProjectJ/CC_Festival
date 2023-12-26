using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Unity.Tutorials.Core.Editor;
using OVR.OpenVR;


public class cshAdviser : cshChatClass
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
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
        messages.Add(askMessage);
        var completionResponse = await openAI.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });
        string response = completionResponse.Choices[0].Message.Content;
        Debug.Log(response);
        this.GetComponent<cshTTS>().textToSpeech(response, TTSVoice.Shimmer);
    }
}
