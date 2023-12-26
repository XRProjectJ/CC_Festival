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

    public GameObject playerCam; // �÷��̾� ������ ī�޶�
    public GameObject player; // �÷��̾� ��ü
    private Player playerScript; // �÷��̾� ��ũ��Ʈ

    public Text mainText; // ���� �ڸ� text
    private string uiStr; // �ڸ��� �� ����

    public enum Mode { Nothing, Beach, Mountain };
    public Mode mode; // ���� � ������� ����


    void Start()
    {
        mode = Mode.Nothing; // ������ �ƹ��� ��尡 �ƴ� ����
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
