using OpenAI;
using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshMicClass : MonoBehaviour
{
    private string microphoneDevice = null;
    private readonly string fileName = "output.wav";
    private int frequency = 44100;
    private OpenAIApi openAI;
    private AudioClip audioClip;
    private string message;
    float[] voiceData;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool checkMicrophoneDevice()
    {
        if (Microphone.devices.Length == 0)
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
        while (Microphone.GetPosition(microphoneDevice) <= 0) { }
        voiceData = new float[audioClip.samples];
        audioClip.GetData(voiceData, 0);
    }
    public async void microphoneStop()
    {
        Debug.Log("Stop");
        Microphone.End(microphoneDevice);
        byte[] data = SaveWav.Save(fileName, audioClip);

        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = data, Name = "audio.wav" },
            //File = Application.persistentDataPath + "/" + fileName,
            Model = "whisper-1",
            Language = "ko"
        };
        if(openAI == null)
        {
            Debug.Log("createOpenAI");
            openAI = this.GetComponent<cshChatClass>().getOpenAI();
        }
        if(req != null)
        {
            Debug.Log(req);
        }
        Debug.Log(openAI);
        // await : �񵿱� �۾����� �۾��� �Ϸ�� ������ ��� �ϵ��� �ϴ� Ű����
        var res = await openAI.CreateAudioTranscription(req);
        this.GetComponent<cshChatClass>().CallOpenAI(res.Text);
    }
}
