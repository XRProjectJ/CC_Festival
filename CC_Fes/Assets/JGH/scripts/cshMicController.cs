using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshMicController : MonoBehaviour
{
    private bool isMicOn = false;
    public GameObject baby;
    public GameObject adviser;
    private cshMicClass babyMic;
    private cshMicClass adviserMic;
    private string microphoneDevice = null;
    // Start is called before the first frame update
    void Start()
    {
        babyMic = baby.GetComponent<cshMicClass>();
        adviserMic = adviser.GetComponent<cshMicClass>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isMicOn)
            {
               babyMic.microphoneStop();
                isMicOn = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isMicOn == true)
            {
                adviserMic.microphoneStop();
                isMicOn = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isMicOn == false && babyMic.checkMicrophoneDevice())
            {
                Debug.Log("富窍技夸(baby)");
                babyMic.microphoneStart();
                isMicOn = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isMicOn == false && adviserMic.checkMicrophoneDevice())
            {
                Debug.Log("富窍技夸(adviser)");
                adviserMic.microphoneStart();
                isMicOn = true;
            }

        }
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (isMicOn == false && adviserMic.checkMicrophoneDevice())
            {
                Debug.Log("富窍技夸(adviser)");
                adviserMic.microphoneStart();
                isMicOn = true;
            }

        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (isMicOn == true)
            {
                adviser.GetComponent<cshMicClass>().microphoneStop();
                isMicOn = false;
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            if (isMicOn == false && babyMic.checkMicrophoneDevice())
            {
                Debug.Log("富窍技夸(baby)");
                babyMic.microphoneStart();
                isMicOn = true;
            }

        }
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            if (isMicOn == true)
            {
                baby.GetComponent<cshMicClass>().microphoneStop();
                isMicOn = false;
            }
        }
    }




    public void askBaby()
    {
        if (isMicOn == false && babyMic.checkMicrophoneDevice())
        {
            Debug.Log("富窍技夸(baby)");
            babyMic.microphoneStart();
            isMicOn = true;
        }
    }

    public void askAdviser()
    {
        if (isMicOn == false && adviserMic.checkMicrophoneDevice())
        {
            Debug.Log("富窍技夸(adviser)");
            adviserMic.microphoneStart();
            isMicOn = true;
        }
    }
    

    public void answerBaby()
    {
        if (isMicOn)
        {
            babyMic.microphoneStop();
            isMicOn = false;
        }
    }

    public void answerAdviser()
    {

            adviserMic.microphoneStop();
            isMicOn = false;

    }
}
