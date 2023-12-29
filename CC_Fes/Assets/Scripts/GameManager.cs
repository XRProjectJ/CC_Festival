using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{



    public GameObject startSceneUIPanel; // ���� ȭ�� UI
    public GameObject spotUIPanel; // ��Ҽ��� UI
    public GameObject textPanel; // �ڸ� �г�

    public GameObject posLobby; // �κ� ������
    public GameObject pos1; // 1�� ������
    public GameObject pos2; // 2�� ������

    //public GameObject beachSituation;
    //public GameObject mountainSituation;

    //public GameObject startSceneCam; // ���� ȭ�� ī�޶�
    public GameObject playerCam; // �÷��̾� ������ ī�޶�
    public GameObject player; // �÷��̾� ��ü
    private Player playerScript; // �÷��̾� ��ũ��Ʈ

    public Text mainText; // ���� �ڸ� text
    private string uiStr; // �ڸ��� �� ����

    public enum Mode1 { ModeSelect, PosSelect, Meditation, Consult };
    public enum Mode2 { Mountain, Beach };
    public enum Mode3 { PosLobby, Pos1, Pos2 };

    public Mode1 mode1; // ���� �ൿ ���
    public Mode2 mode2; //���� ��� ���
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
        //mode1 = Mode1.Nothing; // ������ �ƹ��� ��尡 �ƴ� ����
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


    // ��� �����ϱ� / ��� �����ϱ�
    public void selectedSituation()
    {
        startSceneUIPanel.SetActive(false); // ���� ȭ�� UI ����
        //startSceneCam.SetActive(false); // ���� ȭ�� ī�޶� ����
        //player.SetActive(true); // �÷��̾� Ȱ��ȭ
        //playerCam.SetActive(true); // �÷��̾� �������� ��ȯ

        spotUIPanel.SetActive(true); // Situation�� UI �ѱ�
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

    // Moutain �� Pos1�� �̵�
    public void moveMPos1()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
        GameManager.Instance.SetMode3(GameManager.Mode3.Pos1);

        if (GameManager.Instance.GetMode2() == Mode2.Beach)
        {
            GameManager.Instance.SetMode2(GameManager.Mode2.Mountain);
            sceneChange();
        }

        startSceneUIPanel.SetActive(false); // ���� ȭ�� UI ����
        //startSceneCam.SetActive(false); // ���� ȭ�� ī�޶� ����
        player.transform.position = pos1.transform.position;
        player.transform.rotation = pos1.transform.rotation;
        //player.SetActive(true); // �÷��̾� Ȱ��ȭ
        //playerCam.SetActive(true); // �÷��̾� �������� ��ȯ

        spotUIPanel.SetActive(true); // Situation�� UI �ѱ�
    }

    // Moutain �� Pos2�� �̵�
    public void moveMPos2()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
        GameManager.Instance.SetMode3(GameManager.Mode3.Pos2);

        if (GameManager.Instance.GetMode2() == Mode2.Beach)
        {
            GameManager.Instance.SetMode2(GameManager.Mode2.Mountain);
            sceneChange();
        }

        startSceneUIPanel.SetActive(false); // ���� ȭ�� UI ����
        //startSceneCam.SetActive(false); // ���� ȭ�� ī�޶� ����
        player.transform.position = pos2.transform.position;
        player.transform.rotation = pos2.transform.rotation;
        //player.SetActive(true); // �÷��̾� Ȱ��ȭ
        //playerCam.SetActive(true); // �÷��̾� �������� ��ȯ

        spotUIPanel.SetActive(true); // Situation�� UI �ѱ�
    }

    // Beach �� Pos1�� �̵�
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

        startSceneUIPanel.SetActive(false); // ���� ȭ�� UI ����
        //startSceneCam.SetActive(false); // ���� ȭ�� ī�޶� ����
        player.transform.position = pos1.transform.position;
        player.transform.rotation = pos1.transform.rotation;
        //player.SetActive(true); // �÷��̾� Ȱ��ȭ
        //playerCam.SetActive(true); // �÷��̾� �������� ��ȯ

        spotUIPanel.SetActive(true); // Situation�� UI �ѱ�
    }

    // Beach �� Pos2�� �̵�
    public void moveBPos4()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.PosSelect);
        GameManager.Instance.SetMode3(GameManager.Mode3.Pos2);

        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            GameManager.Instance.SetMode2(GameManager.Mode2.Beach);
            sceneChange();
        }

        startSceneUIPanel.SetActive(false); // ���� ȭ�� UI ����
        //startSceneCam.SetActive(false); // ���� ȭ�� ī�޶� ����
        player.transform.position = pos2.transform.position;
        player.transform.rotation = pos2.transform.rotation;
        //player.SetActive(true); // �÷��̾� Ȱ��ȭ
        //playerCam.SetActive(true); // �÷��̾� �������� ��ȯ

        spotUIPanel.SetActive(true); // Situation�� UI �ѱ�
    }
    public void startMeditation()
    {
        GameManager.Instance.SetMode1(GameManager.Mode1.Meditation);

        spotUIPanel.SetActive(false);
        Debug.Log("StartMed");

    }


}
