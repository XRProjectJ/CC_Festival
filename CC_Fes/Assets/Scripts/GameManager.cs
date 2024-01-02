using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{



    public GameObject startSceneUIPanel; // ���� ȭ�� UI
    public GameObject spotUIPanel; // ��Ҽ��� UI
    public GameObject textPanel; // ��� �ڸ� �г�
    public GameObject MedPanel; //��� �г�
    public GameObject ConPanel; //��� �г�
    public GameObject talkMainPanel; //��� �г�
    public GameObject talking_User; // ��� �ڸ� �г�
    public GameObject talking_Other; // ��� �ڸ� �г�

    public GameObject askButton_Baby;//�Ʊ⿡�� ����
    public GameObject askButton_Adviser;//���翡�� ����
    public GameObject answerButton_Baby;//�Ʊ� �亯���
    public GameObject answerButton_Adviser;//���� �亯���



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

    public Text talkingText_User; //��ȭ text
    public string Str_User; //��ȭ �ڸ��� �� ����
    public Text talkingText_Other; //��ȭ text
    public string Str_Other; //��ȭ �ڸ��� �� ����

    public AudioSource Beach_BGM; // �ٴ� ��� ����
    public AudioSource Mountain_BGM; // �� ��� ����
    private bool isMusicPlaying = false; // ���� ��� ����


    public enum Mode1 { ModeSelect, PosSelect, Meditation, Consult };
    public enum Mode2 { Mountain, Beach };
    public enum Mode3 { PosLobby, Pos1, Pos2 };
    public enum Mode4 { Baby, Consult};

    public Mode1 mode1; // ���� �ൿ ���
    public Mode2 mode2; //���� ��� ���
    public Mode3 mode3;
    public Mode4 mode4; //������ ��ȭ ���

    public static GameManager Instance;
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

    public void SetMode4_Baby()
    {
        mode4 = GameManager.Mode4.Baby;
        Debug.Log("SetMod4 To" + mode4);
    }

    public void SetMode4_Con()
    {
        mode4 = GameManager.Mode4.Consult;
        Debug.Log("SetMod4 To" + mode4);
    }

    public Mode4 GetMode4()
    {
        Debug.Log("GetMode4 " + mode4);
        return mode4;
    }

    

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
        ConPanel.SetActive(false);
    }


    // ��� �����ϱ� / ��� �����ϱ�
    public void selectedSituation()
    {
        startSceneUIPanel.SetActive(false); // ���� ȭ�� UI ����
        //startSceneCam.SetActive(false); // ���� ȭ�� ī�޶� ����
        //player.SetActive(true); // �÷��̾� Ȱ��ȭ
        //playerCam.SetActive(true); // �÷��̾� �������� ��ȯ

        if (GameManager.Instance.GetMode1() == Mode1.Meditation)
        {
            spotUIPanel.SetActive(true); // Situation�� UI �ѱ�
        }
        
        else if (GameManager.Instance.GetMode1() == Mode1.Consult)
        {
            ConPanel.SetActive(true); // Situation�� UI �ѱ�
        }
        
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
        //player.transform.position = mountainSituation.transform.position;
        selectedSituation();
        Debug.Log("SelectCon");
    }


    public void talkingWithBaby()
    {
        ConPanel.SetActive(false);
        talkMainPanel.SetActive(true);

        askButton_Baby.SetActive(true);
        askButton_Adviser.SetActive(false);
        answerButton_Baby.SetActive(true);
        answerButton_Adviser.SetActive(false);

        Debug.Log("TalkingWithBaby");
        Str_Other = "�Ʊ�� ��ȭ�ؿ�~";
        Str_User = "���� �ɾ����";
        setText(talkingText_User, Str_User);
        setText(talkingText_Other, Str_Other);
    }

    public void talkingWithPro()
    {
        ConPanel.SetActive(false);
        talkMainPanel.SetActive(true);

        askButton_Baby.SetActive(false);
        askButton_Adviser.SetActive(true);
        answerButton_Baby.SetActive(false);
        answerButton_Adviser.SetActive(true);

        Debug.Log("TalkingWithPro");
        Str_Other = "����� ��ȭ�ؿ�~";
        Str_User = "���� �ɾ����";
        setText(talkingText_User, Str_User);
        setText(talkingText_Other, Str_Other);
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
        MedPanel.SetActive(true); //��� �г� Ű��
        Debug.Log("StartMed");


        //����������
        if (GameManager.Instance.GetMode2() == Mode2.Beach)
        {
            Beach_BGM.gameObject.SetActive(true); // AudioSource�� Ȱ��ȭ

            if (Beach_BGM != null)
            {
                Beach_BGM.Play(); // �Ҵ�� ��� ���� ���
                isMusicPlaying = true; // ��� ���� ����
                Debug.Log("MusicPlay");
            }
            else
            {
                Debug.LogWarning("Beach_BGM AudioSource not assigned!");
            }
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            Mountain_BGM.gameObject.SetActive(true); // AudioSource�� Ȱ��ȭ
            if (Mountain_BGM != null)
            {
                Mountain_BGM.Play(); // �Ҵ�� ��� ���� ���
                isMusicPlaying = true; // ��� ���� ����
                Debug.Log("MusicPlay");
            }
            else
            {
                Debug.LogWarning("Mountain_BGM AudioSource not assigned!");
            }
        }


        //��� ����
        StartCoroutine("startMed");

    }



    public void ToggleBackgroundMusic()
    {
        if (GameManager.Instance.GetMode2() == Mode2.Beach)
        {
            if (Beach_BGM != null)
            {
                if (isMusicPlaying)
                {
                    Beach_BGM.Pause(); // ���� �Ͻ� ����
                    isMusicPlaying = false; // ��� ���� ����
                    Debug.Log("MusicStop");
                }
                else
                {
                    Beach_BGM.Play(); // ���� ���
                    isMusicPlaying = true; // ��� ���� ����
                    Debug.Log("MusicPlay");
                }
            }
            else
            {
                Debug.LogWarning("BGM AudioSource not assigned!");
            }
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            if (Mountain_BGM != null)
            {
                if (isMusicPlaying)
                {
                    Mountain_BGM.Pause(); // ���� �Ͻ� ����
                    isMusicPlaying = false; // ��� ���� ����
                    Debug.Log("MusicStop");
                }
                else
                {
                    Mountain_BGM.Play(); // ���� ���
                    isMusicPlaying = true; // ��� ���� ����
                    Debug.Log("MusicPlay");
                }
            }
            else
            {
                Debug.LogWarning("BGM AudioSource not assigned!");
            }
        }
    }




    //delay��ŭ ���
    private IEnumerator Delayed(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }


    //text ���� ����
    public void setText(Text text, string str)
    {
        text.text = str;
    }


    private float time;

        public IEnumerator startMed()
    {
        Debug.Log("Med_ing");
        yield return new WaitForSeconds(2f); // 2�� ��� �� ����

        uiStr = "��� ����~!";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(2f); // ��� �ڼ�
        uiStr = "���� ������ ���� �ʴ� �ڼ��� �񽺵��� ���ų� ���ϰ� �ɾ��ּ���.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "������� �����ϰ� �踦 ���ΰ� ����� �����մϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�ڷ� ���� ���̸�����";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "������ ũ�� �����ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "ū ȣ���� ������ �� ��";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� ���� ȣ������ ���ƿɴϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�¸� ���������� ������ Ǯ��";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "������� ������ �����մϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "ȣ���� ���� ����԰� ������� ����ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� �츮 �Ʊ�� ������ ���, �� ǳ��ο� �������� ����Ǿ� �ֽ��ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "��õ��� �츮�� �ѷ��� �� ����� �� �ǽ��� �����ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "��ȫ���� �Ͼ���� �Բ� ��췯�� �� �����ϰ� ���� ����";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� �Ʒ���, �츮 �ƱⰡ �ڸ��� ������ ���� ���� ���� ���÷� ���ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�� ��� ���� ġ��, ���, ����� ��������";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� �Ʊ⸦ ���� ��� �͵�κ��� ���ѳ��ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);//���� ��
        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            uiStr = "���� ���� �������� ��� �Ƹ���� �����ִ� Ǫ�� ���� �ٶ󺸰� �ֽ��ϴ�.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "���� ������ �����Ͱ�,";
            setText(mainText, uiStr);
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Beach)//�ٴ��� ��
        {
            uiStr = "���� ���� ������ �Ƹ���� ������ ���� ������ �ٴٸ� �ٶ󺸰� �ֽ��ϴ�.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "�ĵ��Ҹ��� �������,";
            setText(mainText, uiStr);
        }

        yield return new WaitForSeconds(5f);
        uiStr = "�ٶ��� ����Դϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�� ��� �͵��� �����ϸ� �� ������ ä��ϴ�.";
        setText(mainText, uiStr);


        yield return new WaitForSeconds(5f);//���� ��
        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            uiStr = "���� �� ��ü�� ��� �ӱ���";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "�� �Ƹ��ٿ� ���� ��� ����� �������� �ٶ󺾴ϴ�.";
            setText(mainText, uiStr);
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Beach)//�ٴ��� ��
        {
            uiStr = "���� �� ��ü�� ��� �ӱ���";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "�� �Ƹ��ٿ� ���� ��� ����� �������� �ٶ󺾴ϴ�.";
            setText(mainText, uiStr);
        }

        yield return new WaitForSeconds(5f);
        uiStr = "�ƹ��� �򰡵� ���� �ʰ�";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� ���� ����� �ӹ����ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "� ����� ��������";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�� ���� ���ϰų� �ź����� �ʰ�";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "��� �� ����, �״�θ� �ٶ󺸰� �������ְ�";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);//���� ��
        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            uiStr = "�� ���� �ٶ��� ���� �� �� ������ ��������ϴ�.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "������ �ٽ��� ����̴� �ٶ��� Ÿ��";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "õõ�� �� �ָ� ���� �ʸӷ� ����� ���ϴ�.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "�츮 �Ʊ�� ���� ���� ������ �� ����� ���� ����";
            setText(mainText, uiStr);
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Beach)//�ٴ��� ��
        {
            uiStr = "�� �ٴ��� ���� ���� �� �� ������ ��������ϴ�.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "������ �ٽ��� ��¦�̴� ������ Ÿ��";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "õõ�� �� �ָ� ���� �ʸӷ� ����� ���ϴ�.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "�츮 �Ʊ�� ���� ���� ������ �� ����� �ٴٿ� ����";
            setText(mainText, uiStr);
        }

        yield return new WaitForSeconds(5f);
        uiStr = "���� ���� �ȶ��ϰ� ����� ��� �ȿ���";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� ��� ���� ������ ������ �ð�";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� ��� ���� �Ҹ�, ������ ���� �Ҹ��� ������";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�Ƹ��ٿ� ���� �غ��ϰ� �ֽ��ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� ���������� �ϳ��� ��ȥ�� �츮 �Ʊ��";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� ����� ������";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "������ ������ ������";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� ������ ����� ����";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = " ������ ����� ������ ���� ������ �ֽ��ϴ�.";
        setText(mainText, uiStr);



        yield return new WaitForSeconds(5f);
        uiStr = "�ƱⰡ ���� �����ϴ� ����� �������� �������ϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� �츮 �Ʊ�� �����մϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "����մϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� ����ϰ� �ȶ��� ����� �� ���̸�";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�Ʊ�� �ǰ��� ������ ���� ǰ�� �Ȱ� �� ������ ������ ���Դϴ�.";
        setText(mainText, uiStr);



        yield return new WaitForSeconds(5f);
        uiStr = "�Ҿ��̳� ������ ������ ���� �͵���";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�� ������ �䵿�� ����Ű���ʵ���";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�Ʊ�� ���� �����ϰ� �ִ� ������ �������� ��,";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�츮 �ȿ��� ������ ���� ���� ������ ��.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "�� ��� �͵��� ������ �Բ� �մϴ�.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "���� �Ʊ⸦ �����ִ� �� ����� �� ������ ������ ������";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "õõ�� ���� ��ϴ�.";
        setText(mainText, uiStr);


        yield return null;
        yield break;
    }
}
