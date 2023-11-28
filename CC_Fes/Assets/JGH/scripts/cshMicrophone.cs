using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아기 모델에 이 스크립트를 넣고 해당 모델안에 cshChatGpt 스크립트가 들어가야 함
/// </summary>
public class cshMicrophone : MonoBehaviour
{
    private string microphoneDevice = null;
    private readonly string fileName = "output.wav";
    private int frequency = 44100;
    private OpenAIApi openAi;
    private AudioClip audioClip;
    private string message;
    float[] voiceData;
    // Start is called before the first frame update
    void Start()
    {
        if (checkMicrophoneDevice() == true)
        {
            
        }
        openAi = this.GetComponent<cshChatGpt>().getOpenAiApi();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            microphoneStop();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("말하세요");
            microphoneStart();
        }
    }
    public bool checkMicrophoneDevice()
    {
        if(Microphone.devices.Length == 0)
        {
            Debug.LogError("마이크 연결 안됨");
            return false;
        }
        microphoneDevice = Microphone.devices[0];
        return true;
    }
    public void microphoneStart()
    {
        // 마이크로 들어온 데이터를 녹음하는 함수
        // 첫번째 매개변수 : 마이크 장치 이름
        // 두번째 매개변수 : 녹음된 데이터를 반복 재생할 것인지
        // 세번째 매개변수 : 몇초 동안 녹음할 것인지
        // 네번째 매개변수 : 샘플링 주파수
        // 디지털 오디오는 시간을 작은 간격으로 나누어 측정하는데,
        // 이 간격을 샘플이라고 합니다. 이때, 초당 샘플의 개수를 나타내는 것이 바로 샘플링 주파수입니다.
        // 보통 44100 을 씀
        audioClip = Microphone.Start(microphoneDevice, false, 10, frequency);

        // 마이크의 입력 위치를 반환 (커서 같은 개념인듯함)
        // 매개변수 : 마이크 장치 이름
        while(Microphone.GetPosition(microphoneDevice) <= 0) { }
        voiceData = new float[audioClip.samples];
        audioClip.GetData(voiceData, 0);
        
    }
    // async : 비동기작업을 하는 키워드
    public async void microphoneStop()
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
        // await : 비동기 작업에서 작업이 완료될 때까지 대기 하도록 하는 키워드
        var res = await openAi.CreateAudioTranscription(req);
        this.GetComponent<cshChatGpt>().CallGPT(res.Text);
    }
}
