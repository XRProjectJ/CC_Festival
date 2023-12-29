using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{



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

    public enum Mode1 { ModeSelect, PosSelect, Meditation, Consult };
    public enum Mode2 { Mountain, Beach };
    public enum Mode3 { PosLobby, Pos1, Pos2 };

    public Mode1 mode1; // 현재 행동 모드
    public Mode2 mode2; //현재 장소 모드
    public Mode3 mode3;

    public void SetMode1(Mode1 mode)
    {
        mode1 = mode;
        Debug.Log("SetMode1 To" + mode1);
    }

    public Mode1 GetMode1()
    {
        Debug.Log("GetMode1 " + mode1);
        return mode1;
    }

    public void SetMode2(Mode2 mode)
    {
        mode2 = mode;
        Debug.Log("SetMode2 To" + mode2);
    }

    public Mode2 GetMode2()
    {
        Debug.Log("GetMode2 " + mode2);
        return mode2;
    }

    public void SetMode3(Mode3 mode)
    {
        mode3 = mode;
        Debug.Log("SetMode3 To" + mode3);
    }

    public Mode3 GetMode3()
    {
        Debug.Log("GetMode3 " + mode3);
        return mode3;
    }


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }


    void Start()
    {
        //mode1 = Mode1.Nothing; // 시작은 아무런 모드가 아닌 상태
        //mode2 = Mode2.Nothing;

        Scene currentScene = SceneManager.GetActiveScene();

        switch (currentScene.name)
        {
            case "mountain":
                GameManager.Instance.SetMode2(GameManager.Mode2.Mountain);
                break;
            case "beach":
                Debug.Log("Scene Change?");
                GameManager.Instance.SetMode2(GameManager.Mode2.Beach);
                break;
        }

        switch (GameManager.Instance.GetMode1())
        {

            case Mode1.ModeSelect:
                modeSelectUI();
                break;
            case Mode1.PosSelect:
                selectMeditation();
                break;
            case Mode1.Meditation:
                selectedSituation();
                break;
            case Mode1.Consult:
                selectedSituation();
                break;
        }

        switch (GameManager.Instance.GetMode3())
        {

            case Mode3.PosLobby:
                player.transform.position = posLobby.transform.position;
                player.transform.rotation = posLobby.transform.rotation;
                break;
            case Mode3.Pos1:
                player.transform.position = pos1.transform.position;
                player.transform.rotation = pos1.transform.rotation;
                break;
            case Mode3.Pos2:
                player.transform.position = pos2.transform.position;
                player.transform.rotation = pos2.transform.rotation;
                break;
        }

        playerScript = player.GetComponent<Player>();
    }


    void Update()
    {


    }

    public void sceneChange()
    {
        switch (GameManager.Instance.GetMode2())
        {
            case Mode2.Mountain:
                SceneManager.LoadScene("mountain");
                //GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
                break;
            case Mode2.Beach:
                SceneManager.LoadScene("beach");
                //GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
                break;
        }
    }



    public void modeSelectUI()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.ModeSelect);
        player.transform.position = posLobby.transform.position;
        player.transform.rotation = posLobby.transform.rotation;

        startSceneUIPanel.SetActive(true);
        spotUIPanel.SetActive(false);
        textPanel.SetActive(false);
    }


    // 명상 시작하기 / 상담 시작하기
    public void selectedSituation()
    {
        startSceneUIPanel.SetActive(false); // 시작 화면 UI 끄기
        //startSceneCam.SetActive(false); // 시작 화면 카메라 끄기
        //player.SetActive(true); // 플레이어 활성화
        //playerCam.SetActive(true); // 플레이어 시점으로 전환

        spotUIPanel.SetActive(true); // Situation의 UI 켜기
    }



    public void selectMeditation()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.Meditation);
        textPanel.SetActive(true);
        //player.transform.position = beachSituation.transform.position;
        selectedSituation();
        Debug.Log("SelectMed");

    }

    public void selectConsult()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.Consult);
        textPanel.SetActive(true);
        //player.transform.position = mountainSituation.transform.position;
        selectedSituation();
        Debug.Log("SelectCon");
    }

    // Moutain 씬 Pos1로 이동
    public void moveMPos1()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
        GameManager.Instance.SetMode3(GameManager.Mode3.Pos1);

        if (GameManager.Instance.GetMode2() == Mode2.Beach)
        {
            GameManager.Instance.SetMode2(GameManager.Mode2.Mountain);
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
        GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
        GameManager.Instance.SetMode3(GameManager.Mode3.Pos2);

        if (GameManager.Instance.GetMode2() == Mode2.Beach)
        {
            GameManager.Instance.SetMode2(GameManager.Mode2.Mountain);
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
    public void moveBPos3()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
        GameManager.Instance.SetMode3(GameManager.Mode3.Pos1);

        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            GameManager.Instance.SetMode2(GameManager.Mode2.Beach);
            Debug.Log("Pos: SetMode2 To" + GameManager.Instance.GetMode2());
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

    // Beach 씬 Pos2로 이동
    public void moveBPos4()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
        GameManager.Instance.SetMode3(GameManager.Mode3.Pos2);

        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            GameManager.Instance.SetMode2(GameManager.Mode2.Beach);
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
        GameManager.Instance.SetMode1(GameManager.Mode1.Meditation);

        spotUIPanel.SetActive(false);
        Debug.Log("StartMed");

    }


}
