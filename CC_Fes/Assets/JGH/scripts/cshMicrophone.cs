using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshMicrophone : MonoBehaviour
{
    private const string microphoneDevice = null;
    float[] voiceData;
    // Start is called before the first frame update
    void Start()
    {
        if (checkMicrophoneDevice() == true)
        {
            Debug.Log("말해");
            microphoneStart();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            microphoneStop();

        }
    }
    public bool checkMicrophoneDevice()
    {
        if(Microphone.devices.Length == 0)
        {
            Debug.LogError("마이크 연결 안됨");
            return false;
        }
        return true;
    }
    public void microphoneStart()
    {
        AudioClip audioClip = Microphone.Start(microphoneDevice, true, 10, AudioSettings.outputSampleRate);
        while(Microphone.GetPosition(microphoneDevice) <= 0) { }
        voiceData = new float[audioClip.samples];
        audioClip.GetData(voiceData, 0);
    }
    public void microphoneStop()
    {
        
        Debug.Log("Stop");
        Microphone.End(microphoneDevice);
        for (int i = 0; i < voiceData.Length; i++)
        {
            Debug.Log(voiceData[i]);
        }
    }
}
