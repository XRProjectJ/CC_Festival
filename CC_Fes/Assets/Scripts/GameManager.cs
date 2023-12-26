using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startSceneUIPanel; // 시작 화면 UI
    public GameObject spotUIPanel; // 장소선택 UI
    public GameObject textPanel; // 자막 패널

    public GameObject beachSituation;
    public GameObject mountainSituation;

    public GameObject playerCam; // 플레이어 시점의 카메라
    public GameObject player; // 플레이어 객체
    private Player playerScript; // 플레이어 스크립트

    public Text mainText; // 메인 자막 text
    private string uiStr; // 자막에 들어갈 내용

    public enum Mode { Nothing, Beach, Mountain };
    public Mode mode; // 현재 어떤 모드인지 저장


    void Start()
    {
        mode = Mode.Nothing; // 시작은 아무런 모드가 아닌 상태
    }


    void Update()
    {
        
    }

    public void startSea()
    {
        mode = Mode.Beach;

        player.transform.position = beachSituation.transform.position;
        
    }

    public void startMountaion()
    {
        mode = Mode.Mountain;

        player.transform.position = mountainSituation.transform.position;

    }


}
