using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject startSceneUIPanel; // 시작 화면 UI
    public GameObject spotUIPanel; // 장소선택 UI
    public GameObject textPanel; // 자막 패널

    public GameObject posLobby; // 로비 포지션
    public GameObject pos1; // 1번 포지션
    public GameObject pos2; // 2번 포지션

    //public GameObject beachSituation;
    //public GameObject mountainSituation;

    //public GameObject startSceneCam; // 시작 화면 카메라
    public GameObject playerCam; // 플레이어 시점의 카메라
    public GameObject player; // 플레이어 객체
    private Player playerScript; // 플레이어 스크립트

    public Text mainText; // 메인 자막 text
    private string uiStr; // 자막에 들어갈 내용

    public enum Mode1 { Nothing, Meditation, Consult };
    public enum Mode2 { Nothing, Beach, Mountain };

    public Mode1 mode1; // 현재 행동 모드 저장
    public Mode2 mode2; //현재 장소 모드 저장

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //mode1 = Mode1.Nothing; // 시작은 아무런 모드가 아닌 상태
        //mode2 = Mode2.Nothing;

        //SceneManager.LoadScene("mountain");

        player.transform.position = posLobby.transform.position;
        player.transform.rotation = posLobby.transform.rotation;

        startSceneUIPanel.SetActive(true);
        spotUIPanel.SetActive(false);
        textPanel.SetActive(false);

        playerScript = player.GetComponent<Player>();
    }


    void Update()
    {
        

    }

    public void sceneChange()
    {
        switch (mode2)
        {
            case Mode2.Mountain:
                SceneManager.LoadScene("mountain");
                mode1 = Mode1.Meditation;
                break;
            case Mode2.Beach:
                SceneManager.LoadScene("beach");
                mode1 = Mode1.Meditation;
                break;
        }
    }


    public void selectSituation()
    {
        startSceneUIPanel.SetActive(false); // 시작 화면 UI 끄기
        //startSceneCam.SetActive(false); // 시작 화면 카메라 끄기
        //player.SetActive(true); // 플레이어 활성화
        //playerCam.SetActive(true); // 플레이어 시점으로 전환

        spotUIPanel.SetActive(true); // Situation의 UI 켜기
    }


    
    public void selectMeditation()
    {
        mode1 = Mode1.Meditation;
        textPanel.SetActive(true);
        //player.transform.position = beachSituation.transform.position;
        selectSituation();
        Debug.Log("SelectMed");
        
    }

    public void selectConsult()
    {
        mode1 = Mode1.Consult;
        textPanel.SetActive(true);
        //player.transform.position = mountainSituation.transform.position;
        selectSituation();
        Debug.Log("SelectCon");
    }

    // Moutain 씬 Pos1로 이동
    public void moveMPos1()
    {
        
        if(mode2 == Mode2.Beach)
        {
            mode2 = Mode2.Mountain;
            sceneChange();
        }
        
        startSceneUIPanel.SetActive(false); // 시작 화면 UI 끄기
        //startSceneCam.SetActive(false); // 시작 화면 카메라 끄기
        player.transform.position = pos1.transform.position;
        player.transform.rotation = pos1.transform.rotation;
        //player.SetActive(true); // 플레이어 활성화
        //playerCam.SetActive(true); // 플레이어 시점으로 전환

        spotUIPanel.SetActive(true); // Situation의 UI 켜기
    }

    // Moutain 씬 Pos2로 이동
    public void moveMPos2()
    {
        if (mode2 == Mode2.Beach)
        {
            mode2 = Mode2.Mountain;
            sceneChange();
        }

        startSceneUIPanel.SetActive(false); // 시작 화면 UI 끄기
        //startSceneCam.SetActive(false); // 시작 화면 카메라 끄기
        player.transform.position = pos2.transform.position;
        player.transform.rotation = pos2.transform.rotation;
        //player.SetActive(true); // 플레이어 활성화
        //playerCam.SetActive(true); // 플레이어 시점으로 전환

        spotUIPanel.SetActive(true); // Situation의 UI 켜기
    }

    // Beach 씬 Pos1로 이동
    public void moveMPos3()
    {
        if (mode2 == Mode2.Mountain)
        {
            mode2 = Mode2.Beach;
            sceneChange();
        }

        startSceneUIPanel.SetActive(false); // 시작 화면 UI 끄기
        //startSceneCam.SetActive(false); // 시작 화면 카메라 끄기
        player.transform.position = pos2.transform.position;
        player.transform.rotation = pos2.transform.rotation;
        //player.SetActive(true); // 플레이어 활성화
        //playerCam.SetActive(true); // 플레이어 시점으로 전환

        spotUIPanel.SetActive(true); // Situation의 UI 켜기
    }

    // Beach 씬 Pos2로 이동
    public void moveMPos4()
    {
        if (mode2 == Mode2.Mountain)
        {
            mode2 = Mode2.Beach;
            sceneChange();
        }

        startSceneUIPanel.SetActive(false); // 시작 화면 UI 끄기
        //startSceneCam.SetActive(false); // 시작 화면 카메라 끄기
        player.transform.position = pos2.transform.position;
        player.transform.rotation = pos2.transform.rotation;
        //player.SetActive(true); // 플레이어 활성화
        //playerCam.SetActive(true); // 플레이어 시점으로 전환

        spotUIPanel.SetActive(true); // Situation의 UI 켜기
    }
    public void startMeditation()
    {
        spotUIPanel.SetActive(false);
        Debug.Log("StartMed");

    }


}
