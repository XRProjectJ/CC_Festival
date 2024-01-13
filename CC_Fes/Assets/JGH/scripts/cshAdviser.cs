using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Unity.Tutorials.Core.Editor;
using OVR.OpenVR;


public class cshAdviser : cshChatClass
{
    private GameManager gameManager; // GameManager 스크립트에 접근하기 위한 변수

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public async void CallOpenAI(string prompt)
    {
        string end = "(세 줄 요약해서 말해줘)";
        end = prompt + end;
        var askMessage = new ChatMessage()
        {
            Role = "user",
            Content = end
        };

        Debug.Log(prompt);
        messages.Add(askMessage);

        // 일정 길이 이상이면 \n을 추가
        int maxLineLength_User = 70;
        if (prompt.Length > maxLineLength_User)
        {
            prompt = InsertNewLines(prompt, maxLineLength_User);
        }
        gameManager.Str_User = prompt;
        gameManager.setText(gameManager.talkingText_User, gameManager.Str_User);
        var completionResponse = await openAI.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });
        string response = completionResponse.Choices[0].Message.Content;
        Debug.Log(response);

        int maxLineLength_Other = 40;
        if (response.Length > maxLineLength_Other)
        {
            response = InsertNewLines(response, maxLineLength_Other);
        }
        // 대화 자막을 GameManager의 talkingStr 변수에 저장.
        gameManager.Str_Other = response;
        gameManager.setText(gameManager.talkingText_Other, gameManager.Str_Other);
        
        this.GetComponent<cshTTS>().textToSpeech(response, TTSVoice.Shimmer);
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
