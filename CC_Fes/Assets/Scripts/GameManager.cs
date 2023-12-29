using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

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

    public enum Mode1 { Nothing, Meditation, Consult };
    public enum Mode2 { Nothing, Beach, Mountain };

    public Mode1 mode1; // ���� �ൿ ��� ����
    public Mode2 mode2; //���� ��� ��� ����

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
        //mode1 = Mode1.Nothing; // ������ �ƹ��� ��尡 �ƴ� ����
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
        startSceneUIPanel.SetActive(false); // ���� ȭ�� UI ����
        //startSceneCam.SetActive(false); // ���� ȭ�� ī�޶� ����
        //player.SetActive(true); // �÷��̾� Ȱ��ȭ
        //playerCam.SetActive(true); // �÷��̾� �������� ��ȯ

        spotUIPanel.SetActive(true); // Situation�� UI �ѱ�
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

    // Moutain �� Pos1�� �̵�
    public void moveMPos1()
    {
        
        if(mode2 == Mode2.Beach)
        {
            mode2 = Mode2.Mountain;
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
        if (mode2 == Mode2.Beach)
        {
            mode2 = Mode2.Mountain;
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
    public void moveMPos3()
    {
        if (mode2 == Mode2.Mountain)
        {
            mode2 = Mode2.Beach;
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

    // Beach �� Pos2�� �̵�
    public void moveMPos4()
    {
        if (mode2 == Mode2.Mountain)
        {
            mode2 = Mode2.Beach;
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
        spotUIPanel.SetActive(false);
        Debug.Log("StartMed");

    }


}
