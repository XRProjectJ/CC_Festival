using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Unity.Tutorials.Core.Editor;
using OVR.OpenVR;

public class cshBaby : cshChatClass
{
    private GameManager gameManager; // GameManager 스크립트에 접근하기 위한 변수
    private OpenAIApi chat;
    public ParticleSystem heartMaker;
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        chat = new OpenAIApi(apiKey);
        gameManager = GameManager.Instance;
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


        // 일정 길이 이상이면 \n을 추가
        int maxLineLength_User = 70;
        if (prompt.Length > maxLineLength_User)
        {
            prompt = InsertNewLines(prompt, maxLineLength_User);
        }
        gameManager.Str_User = prompt;
        gameManager.setText(gameManager.talkingText_User, gameManager.Str_User);



        var emotionResponse = await openAI.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "ft:gpt-3.5-turbo-1106:personal::8WcnPr5H",
            Messages = messages
        });
        /* var completionResponse = await chat.CreateChatCompletion(new CreateChatCompletionRequest()
         {
             Model = "gpt-3.5-turbo-0613",
             Messages = messages
         });*/

        /*string response = completionResponse.Choices[0].Message.Content;
        Debug.Log(response);

        int maxLineLength_Other = 40;
        if (response.Length > maxLineLength_Other)
        {
            response = InsertNewLines(response, maxLineLength_Other);
        }
        gameManager.Str_Other = response;
        gameManager.setText(gameManager.talkingText_Other, gameManager.Str_Other);

        this.GetComponent<cshTTS>().textToSpeech(response, TTSVoice.Nova);*/
        string emotion = emotionResponse.Choices[0].Message.Content;
        Debug.Log(emotion);
        if(emotion == "positive")
        {
            heartMaker.Play();
        }
        
    }
    string InsertNewLines(string text, int maxLineLength)
    {
        // 특정 길이마다 \n을 삽입하여 줄 바꿈
        string result = "";
        for (int i = 0; i < text.Length; i += maxLineLength)
        {
            int length = Mathf.Min(maxLineLength, text.Length - i);
            result += text.Substring(i, length) + "\n";
        }
        return result;
    }
}
