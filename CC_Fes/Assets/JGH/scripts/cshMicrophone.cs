using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ʊ� �𵨿� �� ��ũ��Ʈ�� �ְ� �ش� �𵨾ȿ� cshChatGpt ��ũ��Ʈ�� ���� ��
/// </summary>
public class cshMicrophone : MonoBehaviour
{
    private string microphoneDevice = null;
    private readonly string fileName = "output.wav";
    private int frequency = 44100;
    private OpenAIApi openAi;
    private AudioClip audioClip;
    private string message;
    private bool isMicOn = false;
    float[] voiceData;
    // Start is called before the first frame update
    void Start()
    {    

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(isMicOn == true)
            {
                microphoneStop("baby");
                isMicOn = false;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isMicOn == true)
            {
                microphoneStop("adviser");
                isMicOn = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(isMicOn == false)
            {
                Debug.Log("���ϼ���");
                microphoneStart();
                isMicOn = true;
            }
            
        }
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (isMicOn == false)
            {
                Debug.Log("���ϼ���");
                microphoneStart();
                isMicOn = true;
            }
            
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (isMicOn == true)
            {
                microphoneStop("baby");
                isMicOn = false;
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            if (isMicOn == true)
            {
                microphoneStop("adviser");
                isMicOn = false;
            }
        }
    }
    public bool checkMicrophoneDevice()
    {
        if(Microphone.devices.Length == 0)
        {
            Debug.LogError("����ũ ���� �ȵ�");
            return false;
        }
        microphoneDevice = Microphone.devices[0];
        return true;
    }
    public void microphoneStart()
    {
        // ����ũ�� ���� �����͸� �����ϴ� �Լ�
        // ù��° �Ű����� : ����ũ ��ġ �̸�
        // �ι�° �Ű����� : ������ �����͸� �ݺ� ����� ������
        // ����° �Ű����� : ���� ���� ������ ������
        // �׹�° �Ű����� : ���ø� ���ļ�
        // ������ ������� �ð��� ���� �������� ������ �����ϴµ�,
        // �� ������ �����̶�� �մϴ�. �̶�, �ʴ� ������ ������ ��Ÿ���� ���� �ٷ� ���ø� ���ļ��Դϴ�.
        // ���� 44100 �� ��
        audioClip = Microphone.Start(microphoneDevice, false, 10, frequency);

        // ����ũ�� �Է� ��ġ�� ��ȯ (Ŀ�� ���� �����ε���)
        // �Ű����� : ����ũ ��ġ �̸�
        while(Microphone.GetPosition(microphoneDevice) <= 0) { }
        voiceData = new float[audioClip.samples];
        audioClip.GetData(voiceData, 0);
        
    }
    // async : �񵿱��۾��� �ϴ� Ű����
    public async void microphoneStop(string who)
    {
        
        Debug.Log("Stop");
        Microphone.End(microphoneDevice);
        byte[] data = SaveWav.Save(fileName, audioClip);

        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = data, Name = "audio.wav" },
            // File = Application.persistentDataPath + "/" + fileName,
            Model = "whisper-1",
            Language = "ko"
        };
        if(openAi == null)
        {
            openAi = this.GetComponent<cshChatGpt>().getOpenAiApi();
        }
        // await : �񵿱� �۾����� �۾��� �Ϸ�� ������ ��� �ϵ��� �ϴ� Ű����
        var res = await openAi.CreateAudioTranscription(req);
        this.GetComponent<cshChatGpt>().CallGPT(res.Text, who);
    }
}
