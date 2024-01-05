using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshTimer : MonoBehaviour
{
    private float time = 0.0f;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 300.0f)//5분 경과마다 경고알림
        {
            time = 0.0f;
            count++;
            warning();
        }
    }
    void warning()
    {
        int useTime = count * 5;
        string message = "사용하신지 " + useTime.ToString() + "분이 지났습니다.\n어지럽거나 속이 좋지 않으시다면 기기를 벗고 휴식을 취해주시길 바랍니다.";
        Debug.Log(message);

        // GameManager의 ShowWarningTextForDuration 함수 호출
        GameManager.Instance.ShowWarningTextForDuration(10f, message);
    }
}
