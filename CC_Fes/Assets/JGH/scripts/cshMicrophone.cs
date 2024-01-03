using OpenAI;
using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ʊ� �𵨿� �� ��ũ��Ʈ�� �ְ� �ش� �𵨾ȿ� cshChatGpt ��ũ��Ʈ�� ���� ��
/// ����� �Ⱦ��̴� ��ũ��Ʈ �Դϴ�.
/// </summary>
public class cshMicrophone : MonoBehaviour
{
    private string microphoneDevice = null;
    private readonly string fileName = "output.wav";
    private int frequency = 44100;
    private OpenAIApi baby;
    private OpenAIApi adviser;
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
        if(baby == null)
        {
            baby = this.GetComponent<cshChatGpt>().getBaby();
        }
        if (adviser == null)
        {
            adviser = this.GetComponent<cshChatGpt>().getAdviser();
        }
        // await : �񵿱� �۾����� �۾��� �Ϸ�� ������ ��� �ϵ��� �ϴ� Ű����
        if (who.Equals("baby"))
        {
            var res = await baby.CreateAudioTranscription(req);
            this.GetComponent<cshChatGpt>().CallBaby(res.Text);
        }
        else
        {
            var res = await adviser.CreateAudioTranscription(req);
            this.GetComponent<cshChatGpt>().CallAdviser(res.Text);
        }
       
        
    }


    public void askBaby()
    {
        if (isMicOn == false)
            {
                Debug.Log("���ϼ���");
                microphoneStart();
                isMicOn = true;
        }
    }

    public void askAdviser()
    {
        if (isMicOn == false)
        {
            Debug.Log("���ϼ���");
            microphoneStart();
            isMicOn = true;
        }
    }
}
