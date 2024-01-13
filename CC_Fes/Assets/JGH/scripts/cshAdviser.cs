using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Unity.Tutorials.Core.Editor;
using OVR.OpenVR;


public class cshAdviser : cshChatClass
{
    private GameManager gameManager; // GameManager ��ũ��Ʈ�� �����ϱ� ���� ����

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
        string end = "(�� �� ����ؼ� ������)";
        end = prompt + end;
        var askMessage = new ChatMessage()
        {
            Role = "user",
            Content = end
        };

        Debug.Log(prompt);
        messages.Add(askMessage);

        // ���� ���� �̻��̸� \n�� �߰�
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
        // ��ȭ �ڸ��� GameManager�� talkingStr ������ ����.
        gameManager.Str_Other = response;
        gameManager.setText(gameManager.talkingText_Other, gameManager.Str_Other);
        
        this.GetComponent<cshTTS>().textToSpeech(response, TTSVoice.Shimmer);
    }

    string InsertNewLines(string text, int maxLineLength)
    {
        // Ư�� ���̸��� \n�� �����Ͽ� �� �ٲ�
        string result = "";
        for (int i = 0; i < text.Length; i += maxLineLength)
        {
            int length = Mathf.Min(maxLineLength, text.Length - i);
            result += text.Substring(i, length) + "\n";
        }
        return result;
    }
}
