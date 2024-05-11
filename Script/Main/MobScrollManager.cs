using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobScrollManager : MonoBehaviour
{
    public Button MS_OpenBtn;
    public Button MS_CloseBtn;
    public Image[] MS_Img;
    public Image[] MS_ImgSource;
    public Sprite[] MSScroll_Sprite;
    public Text[] MS_TitleText;
    public Text[] MS_InfoText;
    public Text[] MS_Price;
    public Button[] MS_BuyBtn;
    public Text[] MS_BuyText;
    public Image[] MS_NeedImg;
    public Image MS_HaveImg;
    public Text MS_HaveText;

    public GameObject[] SoldOut;

    public Button[] MS_SelectBtn;

    public Image MS_TitleImg;
    public GameObject MS_Panel;


    public static int MS_UpPower;
    public static float MS_UpAttackSpeed;
    public static int MS_UpExp;
    public static int MS_UpArmorPenetration; 
    public static int MS_UpEarnGold;

    private int MS_InPanel = 0;
    private int MS_LastPanel = -1;

    private string[] MS_Info;

    private int[] MS_PriceValue;

    private int[] MS_Power_1;
    private float[] MS_Power_2;
    private int[] MS_Power_3;
    private int[] MS_Power_4_1;
    private int[] MS_Power_4_2;

    private string[] MS_Title;

    private bool[] MS_BuyCheck = new bool[32];

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip[] MSSoundClip; // AudioClip 변수 선언

    public GameObject lackBG;
    public Button lackoutBtn;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
        MS_OpenBtn.onClick.AddListener(() => MS_Open(MS_InPanel));
        MS_CloseBtn.onClick.AddListener(MS_Close);

        lackoutBtn.onClick.AddListener(LackOut);

        for (int i = 0; i < MS_SelectBtn.Length; i++)
        {
            int index = i;

            MS_SelectBtn[index].onClick.AddListener(() => MS_Change(index));
        }

        for(int i = 0; i < MS_BuyBtn.Length; i++)
        {
            int index = i;

            MS_BuyBtn[index].onClick.AddListener(() => MS_Buy(index));
        }


        // 판매 가격
        MS_PriceValue = new int[32]
        {
            1000,
            1000,
            1000,
            2500,

            1000,
            1000,
            1000,
            2500,

            2000,
            2000,
            2000,
            4000,

            2000,
            2000,
            2000,
            4000,

            3000,
            3000,
            3000,
            6000,

            4000,
            4000,
            4000,
            8000,

            5000,
            5000,
            5000,
            10000,

            7000,
            7000,
            7000,
            15000,
        };

        // 공격력 
        MS_Power_1 = new int[8]
        {
            100,
            150,
            200,
            300,
            400,
            600,
            800,
            1200,

        };
        // 공격속도 
        MS_Power_2 = new float[8]
        {
            0.2f,
            0.2f,
            0.25f,
            0.25f,
            0.3f,
            0.3f,
            0.4f,
            0.5f,

        };
        // 경험치
        MS_Power_3 = new int[8]
        {
            100,
            250,
            200,
            300,
            400,
            600,
            800,
            1200,

        };
        // 방어력 관통력
        MS_Power_4_1 = new int[8]
        {
            100,
            150,
            200,
            300,
            400,
            600,
            800,
            1200,

        };
        // 골드 획득량 
        MS_Power_4_2 = new int[8]
        {
            100,
            150,
            200,
            300,
            400,
            600,
            800,
            1200,

        };
        // 영약 이름
        MS_Title = new string[32]
        {
            "초급 힘의 영약",
            "초급 속도의 영약",
            "초급 경험의 영약",
            "초급 강화의 영약",

            "하급 힘의 영약",
            "하급 속도의 영약",
            "하급 경험의 영약",
            "하급 강화의 영약",

            "중급 힘의 영약",
            "중급 속도의 영약",
            "중급 경험의 영약",
            "중급 강화의 영약",

            "중상급 힘의 영약",
            "중상급 속도의 영약",
            "중상급 경험의 영약",
            "중상급 강화의 영약",

            "상급 힘의 영약",
            "상급 속도의 영약",
            "상급 경험의 영약",
            "상급 강화의 영약",

            "최상급 힘의 영약",
            "최상급 속도의 영약",
            "최상급 경험의 영약",
            "최상급 강화의 영약",

            "비급 힘의 영약",
            "비급 속도의 영약",
            "비급 경험의 영약",
            "비급 강화의 영약",

            "신급 힘의 영약",
            "신급 속도의 영약",
            "신급 경험의 영약",
            "신급 강화의 영약",
        };
        

    }

    private void LackOut()
    {
        lackBG.SetActive(false);
    }

    private void MS_Change(int index)
    {
        MS_InPanel = index;
        if (MS_LastPanel == MS_InPanel) return;
        for (int i = 0; i < 4; i++)
        {
            int _num = MS_InPanel * 4 + i;

            MS_Img[i].sprite = MS_ImgSource[_num].sprite;
            MS_NeedImg[i].sprite = MSScroll_Sprite[MS_InPanel];

            MS_TitleText[i].text = MS_Title[_num];

            MS_Price[i].text = "x " + TextFormatter.GetThousandCommaText(MS_PriceValue[_num]);
            if(MS_BuyCheck[_num] == false)
            {
                MS_BuyText[i].text = "<color=lime>구매가능</color>";
                SoldOut[i].SetActive(false);
                MS_BuyBtn[i].interactable = true;
            }
            else
            {
                MS_BuyText[i].text = "<color=lightblue>구매완료</color>";
                SoldOut[i].SetActive(true);
                MS_BuyBtn[i].interactable = false;
            }
        }

        MS_InfoText[0].text = "공격력 + " + MS_Power_1[MS_InPanel] + "증가";
        MS_InfoText[1].text = "공격속도 + " + TextFormatter.GetFloatPointCommaText_0(MS_Power_2[MS_InPanel]) + "% 증가";
        MS_InfoText[2].text = "경험치 + " + MS_Power_3[MS_InPanel] + "% 증가";
        MS_InfoText[3].text = "방어구 관통력 + " + MS_Power_4_1[MS_InPanel] + "증가";
        MS_InfoText[4].text = "골드 획득량 + " + MS_Power_4_2[MS_InPanel] + "% 증가";

        MS_TitleImg.sprite = MSScroll_Sprite[MS_InPanel];
        MS_HaveImg.sprite = MSScroll_Sprite[MS_InPanel];
        MS_HaveText.text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[MS_InPanel]) + "";
        MS_SelectBtn[MS_InPanel].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);

        if (MS_LastPanel != -1) MS_SelectBtn[MS_LastPanel].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);


        MS_LastPanel = MS_InPanel;
    }

    private void MS_Open(int index)
    {
        MS_InPanel = index;

        MS_Change(index);
        MS_Panel.SetActive(true);
    }

    private void MS_Close()
    {
        MS_Panel.SetActive(false);
        MS_LastPanel = -1;
    }


    private void MS_Buy(int index)
    {
        if (MS_InPanel >= 0 && MS_InPanel < 8)
        {
            if (MS_PriceValue[MS_InPanel * 4 + index] <= GameManager.PlayerHaveMobScroll[MS_InPanel])
            {
                GameManager.PlayerHaveMobScroll[MS_InPanel] -= MS_PriceValue[MS_InPanel * 4 + index];
                MS_HaveText.text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[MS_InPanel]) + "";
                MS_BuyText[index].text = "<color=lightblue>구매완료</color>";
                SoldOut[index].SetActive(true);
                MS_BuyBtn[index].interactable = false;
                MS_BuyCheck[MS_InPanel * 4 + index] = true;
                audioSource.PlayOneShot(MSSoundClip[0], 1f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
                if (index == 0) MS_UpPower += MS_Power_1[MS_InPanel];
                else if (index == 1) MS_UpAttackSpeed += MS_Power_2[MS_InPanel];
                else if (index == 2) MS_UpExp += MS_Power_3[MS_InPanel];
                else if (index == 3)
                {
                    MS_UpArmorPenetration += MS_Power_4_1[MS_InPanel];
                    MS_UpEarnGold += MS_Power_4_2[MS_InPanel];
                }
            }
            else
            {
                audioSource.PlayOneShot(MSSoundClip[1], 1f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
                lackBG.SetActive(true);
            }
        }
    }


}
