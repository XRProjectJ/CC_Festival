using OpenAI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class cshTTS : MonoBehaviour
{
    private OpenAIWrapper openAIWrapper;

    // private 여도 Inspector 창에서 접근할 수 있도록 함
    [SerializeField] private TTSModel model = TTSModel.TTS_1;
    [SerializeField] private TTSVoice voice = TTSVoice.Onyx;
    [SerializeField, Range(0.25f, 4.0f)] private float speed = 1f;
    [SerializeField]private AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
        if (!audioPlayer)
        {
            audioPlayer = GetComponent<AudioPlayer>();
        }
        openAIWrapper = FindObjectOfType<OpenAIWrapper>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void textToSpeech(string text)
    {
        byte[] audioData = await openAIWrapper.RequestTextToSpeech(text, model, voice, speed);
        if (audioData != null)
        {
            audioPlayer.ProcessAudioBytes(audioData);
        }
        else
        {
            Debug.LogError("오디오 데이터 생성 실패");
        }
    }
    public async void textToSpeech(string text, TTSModel model, TTSVoice voice, float speed)
    {
        this.model = model;
        this.voice = voice;
        this.speed = speed;
        textToSpeech(text);
    }
}
