using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startSceneUIPanel; // ���� ȭ�� UI
    public GameObject spotUIPanel; // ��Ҽ��� UI
    public GameObject textPanel; // �ڸ� �г�

    public GameObject beachSituation;
    public GameObject mountainSituation;

    public GameObject startSceneCam; // ���� ȭ�� ī�޶�
    public GameObject playerCam; // �÷��̾� ������ ī�޶�
    public GameObject player; // �÷��̾� ��ü
    private Player playerScript; // �÷��̾� ��ũ��Ʈ

    public Text mainText; // ���� �ڸ� text
    private string uiStr; // �ڸ��� �� ����

    public enum Mode1 { Nothing, Meditation, Consult };
    public enum Mode2 { Nothing, Beach, Mountain };

    public Mode1 mode1; // ���� �ൿ ��� ����
    public Mode2 mode2; //���� ��� ��� ����


    void Start()
    {
        mode1 = Mode1.Nothing; // ������ �ƹ��� ��尡 �ƴ� ����
        mode2 = Mode2.Nothing;

        playerScript = player.GetComponent<Player>();
    }


    void Update()
    {
        
    }



    public void startSituation()
    {
        startSceneUIPanel.SetActive(false); // ���� ȭ�� UI ����
        startSceneCam.SetActive(false); // ���� ȭ�� ī�޶� ����
        player.SetActive(true); // �÷��̾� Ȱ��ȭ
        playerCam.SetActive(true); // �÷��̾� �������� ��ȯ

        spotUIPanel.SetActive(true); // Situation�� UI �ѱ�
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
