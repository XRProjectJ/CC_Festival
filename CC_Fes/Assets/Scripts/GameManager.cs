using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{



    public GameObject startSceneUIPanel; // 시작 화면 UI
    public GameObject spotUIPanel; // 장소선택 UI
    public GameObject textPanel; // 명상 자막 패널
    public GameObject MedPanel; //명상 패널
    public GameObject ConPanel; //상담 패널
    public GameObject talkMainPanel; //상담 패널
    public GameObject talking_User; // 상담 자막 패널
    public GameObject talking_Other; // 상담 자막 패널

    public GameObject askButton_Baby;//아기에게 질문
    public GameObject askButton_Adviser;//상담사에게 질문
    public GameObject answerButton_Baby;//아기 답변듣기
    public GameObject answerButton_Adviser;//상담사 답변듣기



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

    public Text talkingText_User; //대화 text
    public string Str_User; //대화 자막에 들어갈 내용
    public Text talkingText_Other; //대화 text
    public string Str_Other; //대화 자막에 들어갈 내용

    public AudioSource Beach_BGM; // 바다 배경 음악
    public AudioSource Mountain_BGM; // 산 배경 음악
    private bool isMusicPlaying = false; // 음악 재생 여부


    public enum Mode1 { ModeSelect, PosSelect, Meditation, Consult };
    public enum Mode2 { Mountain, Beach };
    public enum Mode3 { PosLobby, Pos1, Pos2 };
    public enum Mode4 { Baby, Consult};

    public Mode1 mode1; // 현재 행동 모드
    public Mode2 mode2; //현재 장소 모드
    public Mode3 mode3;
    public Mode4 mode4; //누구와 대화 모드

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
        ConPanel.SetActive(false);
    }


    // 명상 시작하기 / 상담 시작하기
    public void selectedSituation()
    {
        startSceneUIPanel.SetActive(false); // 시작 화면 UI 끄기
        //startSceneCam.SetActive(false); // 시작 화면 카메라 끄기
        //player.SetActive(true); // 플레이어 활성화
        //playerCam.SetActive(true); // 플레이어 시점으로 전환

        if (GameManager.Instance.GetMode1() == Mode1.Meditation)
        {
            spotUIPanel.SetActive(true); // Situation의 UI 켜기
        }
        
        else if (GameManager.Instance.GetMode1() == Mode1.Consult)
        {
            ConPanel.SetActive(true); // Situation의 UI 켜기
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
        Str_Other = "아기랑 대화해요~";
        Str_User = "말을 걸어보세요";
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
        Str_Other = "상담사랑 대화해요~";
        Str_User = "말을 걸어보세요";
        setText(talkingText_User, Str_User);
        setText(talkingText_Other, Str_Other);
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
        MedPanel.SetActive(true); //명상 패널 키기
        Debug.Log("StartMed");


        //배경음악재생
        if (GameManager.Instance.GetMode2() == Mode2.Beach)
        {
            Beach_BGM.gameObject.SetActive(true); // AudioSource를 활성화

            if (Beach_BGM != null)
            {
                Beach_BGM.Play(); // 할당된 배경 음악 재생
                isMusicPlaying = true; // 재생 상태 변경
                Debug.Log("MusicPlay");
            }
            else
            {
                Debug.LogWarning("Beach_BGM AudioSource not assigned!");
            }
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            Mountain_BGM.gameObject.SetActive(true); // AudioSource를 활성화
            if (Mountain_BGM != null)
            {
                Mountain_BGM.Play(); // 할당된 배경 음악 재생
                isMusicPlaying = true; // 재생 상태 변경
                Debug.Log("MusicPlay");
            }
            else
            {
                Debug.LogWarning("Mountain_BGM AudioSource not assigned!");
            }
        }


        //명상 진행
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
                    Beach_BGM.Pause(); // 음악 일시 정지
                    isMusicPlaying = false; // 재생 상태 변경
                    Debug.Log("MusicStop");
                }
                else
                {
                    Beach_BGM.Play(); // 음악 재생
                    isMusicPlaying = true; // 재생 상태 변경
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
                    Mountain_BGM.Pause(); // 음악 일시 정지
                    isMusicPlaying = false; // 재생 상태 변경
                    Debug.Log("MusicStop");
                }
                else
                {
                    Mountain_BGM.Play(); // 음악 재생
                    isMusicPlaying = true; // 재생 상태 변경
                    Debug.Log("MusicPlay");
                }
            }
            else
            {
                Debug.LogWarning("BGM AudioSource not assigned!");
            }
        }
    }




    //delay만큼 대기
    private IEnumerator Delayed(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }


    //text 내용 설정
    public void setText(Text text, string str)
    {
        text.text = str;
    }


    private float time;

        public IEnumerator startMed()
    {
        Debug.Log("Med_ing");
        yield return new WaitForSeconds(2f); // 2초 대기 후 시작

        uiStr = "명상 시작~!";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(2f); // 명상 자세
        uiStr = "몸에 무리가 가지 않는 자세로 비스듬히 눕거나 편하게 앉아주세요.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "양손으로 따뜻하게 배를 감싸고 명상을 시작합니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "코로 숨을 들이마쉬고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "입으로 크게 내쉽니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "큰 호흡을 수차례 한 후";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나의 원래 호흡으로 돌아옵니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "온몸 구석구석의 긴장을 풀고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "편안함을 느끼기 시작합니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "호흡을 통해 고요함과 평온함을 얻습니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나와 우리 아기는 절대적 사랑, 그 풍요로운 에너지와 연결되어 있습니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "잠시동안 우리를 둘러싼 그 사랑의 기운에 의식을 모읍니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "분홍빛과 하얀빛이 함께 어우러진 그 포근하고 예쁜 빛이";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나의 아랫배, 우리 아기가 자리한 곳에서 빛을 내는 것을 떠올려 봅니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "이 기분 좋은 치유, 평온, 사랑의 에너지가";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나와 아기를 세상 모든 것들로부터 지켜냅니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);//산일 때
        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            uiStr = "나는 지금 나무들이 곧고 아름답게 뻗어있는 푸른 숲을 바라보고 있습니다.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "예쁜 새들이 지저귀고,";
            setText(mainText, uiStr);
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Beach)//바다일 때
        {
            uiStr = "나는 지금 윤슬이 아름답게 빛나는 맑고 깨끗한 바다를 바라보고 있습니다.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "파도소리가 들려오고,";
            setText(mainText, uiStr);
        }

        yield return new WaitForSeconds(5f);
        uiStr = "바람이 살랑입니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "이 모든 것들을 만끽하며 내 마음을 채웁니다.";
        setText(mainText, uiStr);


        yield return new WaitForSeconds(5f);//산일 때
        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            uiStr = "생명 그 자체를 듬뿍 머금은";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "이 아름다운 숲을 잠시 고요한 마음으로 바라봅니다.";
            setText(mainText, uiStr);
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Beach)//바다일 때
        {
            uiStr = "생명 그 자체를 듬뿍 머금은";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "이 아름다운 물을 잠시 고요한 마음으로 바라봅니다.";
            setText(mainText, uiStr);
        }

        yield return new WaitForSeconds(5f);
        uiStr = "아무런 평가도 하지 않고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나는 그저 고요히 머무릅니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "어떤 잡념이 떠오르면";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "그 또한 평가하거나 거부하지 않고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "잠시 그 존재, 그대로를 바라보고 인정해주고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);//산일 때
        if (GameManager.Instance.GetMode2() == Mode2.Mountain)
        {
            uiStr = "이 숲의 바람을 따라 저 먼 곳으로 흘려보냅니다.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "걱정과 근심은 살랑이는 바람을 타고";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "천천히 저 멀리 수평선 너머로 사라져 갑니다.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "우리 아기는 지금 내가 마주한 이 평온한 숲과 같이";
            setText(mainText, uiStr);
        }

        else if (GameManager.Instance.GetMode2() == Mode2.Beach)//바다일 때
        {
            uiStr = "이 바다의 물을 따라 저 먼 곳으로 흘려보냅니다.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "걱정과 근심은 반짝이는 물살을 타고";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "천천히 저 멀리 수평선 너머로 사라져 갑니다.";
            setText(mainText, uiStr);

            yield return new WaitForSeconds(5f);
            uiStr = "우리 아기는 지금 내가 마주한 이 평온한 바다와 같이";
            setText(mainText, uiStr);
        }

        yield return new WaitForSeconds(5f);
        uiStr = "세상 가장 안락하고 편안한 양수 안에서";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "가장 기분 좋은 엄마의 냄새를 맡고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "가장 듣기 좋은 소리, 엄마의 심장 소리를 들으며";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "아름다운 삶을 준비하고 있습니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나와 마찬가지로 하나의 영혼인 우리 아기는";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나의 사랑을 느끼고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나와의 연결을 느끼고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "내가 보내는 사랑과 같은";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = " 무한한 사랑을 언제나 내게 보내고 있습니다.";
        setText(mainText, uiStr);



        yield return new WaitForSeconds(5f);
        uiStr = "아기가 내게 전달하는 사랑의 에너지를 느껴봅니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나와 우리 아기는 안전합니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "평온합니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나는 평온하고 안락한 출산을 할 것이며";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "아기는 건강히 무사히 나의 품에 안겨 이 세상을 맞이할 것입니다.";
        setText(mainText, uiStr);



        yield return new WaitForSeconds(5f);
        uiStr = "불안이나 걱정을 만들어내는 많은 것들이";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "내 마음에 요동을 일으키지않도록";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "아기와 나를 연결하고 있는 강인한 에너지의 고리,";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "우리 안에서 빛나는 곱고 예쁜 생명의 빛.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "이 모든 것들이 언제나 함께 합니다.";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "나와 아기를 지켜주는 이 든든한 기운에 감사한 마음을 가지고";
        setText(mainText, uiStr);

        yield return new WaitForSeconds(5f);
        uiStr = "천천히 눈을 뜹니다.";
        setText(mainText, uiStr);


        yield return null;
        yield break;
    }
}
