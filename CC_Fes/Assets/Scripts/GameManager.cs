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

    public GameObject startSceneCam; // 시작 화면 카메라
    public GameObject playerCam; // 플레이어 시점의 카메라
    public GameObject player; // 플레이어 객체
    private Player playerScript; // 플레이어 스크립트

    public Text mainText; // 메인 자막 text
    private string uiStr; // 자막에 들어갈 내용

    public enum Mode1 { Nothing, Meditation, Consult };
    public enum Mode2 { Nothing, Beach, Mountain };

    public Mode1 mode1; // 현재 행동 모드 저장
    public Mode2 mode2; //현재 장소 모드 저장


    void Start()
    {
        mode1 = Mode1.Nothing; // 시작은 아무런 모드가 아닌 상태
        mode2 = Mode2.Nothing;

        playerScript = player.GetComponent<Player>();
    }


    void Update()
    {
        
    }



    public void startSituation()
    {
        startSceneUIPanel.SetActive(false); // 시작 화면 UI 끄기
        startSceneCam.SetActive(false); // 시작 화면 카메라 끄기
        player.SetActive(true); // 플레이어 활성화
        playerCam.SetActive(true); // 플레이어 시점으로 전환

        spotUIPanel.SetActive(true); // Situation의 UI 켜기
    }



    public void startMeditation()
    {
        mode1 = Mode1.Meditation;

        textPanel.SetActive(true);
        player.transform.position = beachSituation.transform.position;
        startSituation();
        Debug.Log("StartMed");
        
    }

    public void startConsult()
    {
        mode1 = Mode1.Consult;
        textPanel.SetActive(true);
        player.transform.position = mountainSituation.transform.position;
        startSituation();
        Debug.Log("StartCon");
    }


}
