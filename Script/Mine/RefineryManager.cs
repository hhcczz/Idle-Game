using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefineryManager : MonoBehaviour
{
    public Button RF_OpenBtn;
    public Button RF_CloseBtn;
    public Image[] RF_Img;
    public Image[] RF_Source;
    public Text[] RF_TitleText;
    public Text[] RF_InfoText;
    public Text[] RF_Price;
    public Button[] RF_BuyBtn;
    public Text[] RF_BuyText;
    public Image[] RF_NeedImg;
    public Image RF_HaveImg;
    public Text RF_HaveText;

    public GameObject[] SoldOut;

    public Button[] RF_SelectBtn;

    public Image RF_TitleImg;
    public GameObject RF_Panel;

    public Image[] RF_Frame;


    public static int RF_UpPower;
    public static float RF_Critical;
    public static int RF_PFD;
    public static int RF_AddAttack;
    public static int RF_MineAmount;

    private int RF_InPanel = 0;
    private int RF_LastPanel = -1;

    private string[] RF_Info;

    private int[] RF_PriceValue;

    private int[] RF_Power_1;
    private float[] RF_Power_2;
    private int[] RF_Power_3;
    private int[] RF_Power_4_1;
    private int[] RF_Power_4_2;

    private string[] RF_Title;

    private bool[] RF_BuyCheck = new bool[32];

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip[] RFSoundClip; // AudioClip 변수 선언

    public GameObject lackBG;
    public Button lackoutBtn;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
        RF_OpenBtn.onClick.AddListener(() => RF_Open(RF_InPanel));
        RF_CloseBtn.onClick.AddListener(RF_Close);

        lackoutBtn.onClick.AddListener(LackOut);

        for (int i = 0; i < RF_SelectBtn.Length; i++)
        {
            int index = i;

            RF_SelectBtn[index].onClick.AddListener(() => RF_Change(index));
        }

        for (int i = 0; i < RF_BuyBtn.Length; i++)
        {
            int index = i;

            RF_BuyBtn[index].onClick.AddListener(() => RF_Buy(index));
        }


        // 판매 가격
        RF_PriceValue = new int[32]
        {
            10000,
            10000,
            10000,
            25000,

            10000,
            10000,
            10000,
            25000,

            15000,
            15000,
            15000,
            30000,

            15000,
            15000,
            15000,
            30000,

            20000,
            20000,
            20000,
            40000,

            20000,
            20000,
            20000,
            40000,

            30000,
            30000,
            30000,
            30000,

            30000,
            30000,
            30000,
            50000,
        };

        // 곡괭이 공격력 
        RF_Power_1 = new int[8]
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
        // 곡괭이 크리티컬 확률 
        RF_Power_2 = new float[8]
        {
            5.0f,
            5.0f,
            6.0f,
            6.0f,
            8.0f,
            8.0f,
            16.0f,
            16.0f,

        };
        // 치명적 피해
        RF_Power_3 = new int[8]
        {
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,

        };
        // 추가 공격
        RF_Power_4_1 = new int[8]
        {
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,

        };
        // 광물 획득량
        RF_Power_4_2 = new int[8]
        {
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,

        };
        // 영약 이름
        RF_Title = new string[32]
        {
            "최하급 완드",
            "최하급 강화 완드",
            "최하급 망치 ",
            "최하급 강화 망치",

            "하급 완드",
            "하급 강화 완드",
            "하급 망치 ",
            "하급 강화 망치",

            "중급 완드",
            "중급 스피어",
            "중급 곡괭이",
            "중급 강화 곡괭이",

            "중상급 스태프",
            "중상급 강화 스태프",
            "중상급 완드",
            "중상급 강화 완드",

            "상급 완드",
            "상급 망치",
            "상급 강화 완드",
            "상급 초월 완드",

            "최상급 완드",
            "최상급 작살",
            "최상급 망치",
            "최상급 강화 망치",

            "비급 스태프",
            "비급 작살",
            "비급 스피어",
            "비급 완드",

            "신급 피쳐톤",
            "신급 곡괭이",
            "신급 스피어",
            "신급 스태프",
        };


    }

    private string ColorText(int index)
    {
        if (index == 0) return "제련소0번";
        else if (index == 1) return "제련소1번";
        else if (index == 2) return "제련소2번";
        else if (index == 3) return "제련소3번";
        else if (index == 4) return "제련소4번";
        else if (index == 5) return "제련소5번";
        else if (index == 6) return "제련소6번";
        else                 return "제련소7번";
    }

    private void LackOut()
    {
        lackBG.SetActive(false);
    }

    private void RF_Change(int index)
    {
        RF_InPanel = index;
        if (RF_LastPanel == RF_InPanel) return;
        for (int i = 0; i < 4; i++)
        {
            int _num = RF_InPanel * 4 + i;

            RF_Img[i].sprite = RF_Source[_num].sprite;
            RF_NeedImg[i].sprite = GameManager.GemSprites[RF_InPanel];

            RF_TitleText[i].text = RF_Title[_num];

            RF_Price[i].text = "x " + TextFormatter.GetThousandCommaText(RF_PriceValue[_num]);
            if (RF_BuyCheck[_num] == false)
            {
                RF_BuyText[i].text = "<color=lime>구매가능</color>";
                SoldOut[i].SetActive(false);
                RF_BuyBtn[i].interactable = true;
            }
            else
            {
                RF_BuyText[i].text = "<color=lightblue>구매완료</color>";
                SoldOut[i].SetActive(true);
                RF_BuyBtn[i].interactable = false;
            }

            Outline outline = RF_Frame[i].GetComponent<Outline>();
            RF_Frame[i].color = ColorManager.ColorChange(ColorText(index));
            outline.effectColor = ColorManager.ColorChange(ColorText(index));
        }

        

        RF_InfoText[0].text = "곡괭이 공격력 <color=cyan>+" + RF_Power_1[RF_InPanel] + "</color> 증가";
        RF_InfoText[1].text = "크리티컬 확률 <color=cyan>+" + TextFormatter.GetFloatPointCommaText_0(RF_Power_2[RF_InPanel]) + "%</color> 증가";
        RF_InfoText[2].text = "치명적 피해 <color=cyan>+" + RF_Power_3[RF_InPanel] + "%</color> 증가";
        RF_InfoText[3].text = "추가 공격 <color=cyan>+" + RF_Power_4_1[RF_InPanel] + "</color> 증가";
        RF_InfoText[4].text = "광물 획득량 <color=cyan>+" + RF_Power_4_2[RF_InPanel] + "</color> 증가";

        RF_TitleImg.sprite = GameManager.GemSprites[RF_InPanel];
        RF_HaveImg.sprite = GameManager.GemSprites[RF_InPanel];
        RF_HaveText.text = TextFormatter.GetThousandCommaText((int)ReturnStar(RF_InPanel)) + "";
        RF_SelectBtn[RF_InPanel].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);

        if (RF_LastPanel != -1) RF_SelectBtn[RF_LastPanel].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);


        RF_LastPanel = RF_InPanel;
    }

    private void RF_Open(int index)
    {
        RF_InPanel = index;

        RF_Change(index);
        RF_Panel.SetActive(true);
    }

    private void RF_Close()
    {
        RF_Panel.SetActive(false);
        RF_LastPanel = -1;
    }


    private void RF_Buy(int index)
    {
        if (RF_InPanel >= 0 && RF_InPanel < 8)
        {
            if (RF_PriceValue[RF_InPanel * 4 + index] <= ReturnStar(RF_InPanel))
            {
                if(RF_InPanel == 0) GameManager.HaveStarGrey -= RF_PriceValue[RF_InPanel * 4 + index];
                else if (RF_InPanel == 1) GameManager.HaveStarBrown -= RF_PriceValue[RF_InPanel * 4 + index];
                else if (RF_InPanel == 2) GameManager.HaveStarBlue -= RF_PriceValue[RF_InPanel * 4 + index];
                else if (RF_InPanel == 3) GameManager.HaveStarGreen -= RF_PriceValue[RF_InPanel * 4 + index];
                else if (RF_InPanel == 4) GameManager.HaveStarRed -= RF_PriceValue[RF_InPanel * 4 + index];
                else if (RF_InPanel == 5) GameManager.HaveStarYellow -= RF_PriceValue[RF_InPanel * 4 + index];
                else if (RF_InPanel == 6) GameManager.HaveStarPurple -= RF_PriceValue[RF_InPanel * 4 + index];
                else if (RF_InPanel == 7) GameManager.HaveStarOrange -= RF_PriceValue[RF_InPanel * 4 + index];

                RF_HaveText.text = TextFormatter.GetThousandCommaText((int)ReturnStar(RF_InPanel)) + "";
                RF_BuyText[index].text = "<color=lightblue>구매완료</color>";
                SoldOut[index].SetActive(true);
                RF_BuyBtn[index].interactable = false;
                RF_BuyCheck[RF_InPanel * 4 + index] = true;
                audioSource.PlayOneShot(RFSoundClip[0], 1f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
                if (index == 0) RF_UpPower += RF_Power_1[RF_InPanel];
                else if (index == 1) RF_Critical += RF_Power_2[RF_InPanel];
                else if (index == 2) RF_PFD += RF_Power_3[RF_InPanel];
                else if (index == 3)
                {
                    RF_AddAttack += RF_Power_4_1[RF_InPanel];
                    RF_MineAmount += RF_Power_4_2[RF_InPanel];
                }
            }
            else
            {
                audioSource.PlayOneShot(RFSoundClip[1], 1f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
                lackBG.SetActive(true);
            }
        }
    }

    private long ReturnStar(int index)
    {
        if (index == 0)         return GameManager.HaveStarGrey;
        else if (index == 1)    return GameManager.HaveStarBrown;
        else if (index == 2)    return GameManager.HaveStarBlue;
        else if (index == 3)    return GameManager.HaveStarGreen;
        else if (index == 4)    return GameManager.HaveStarRed;
        else if (index == 5)    return GameManager.HaveStarYellow;
        else if (index == 6)    return GameManager.HaveStarPurple;
        else if (index == 7)    return GameManager.HaveStarOrange;
        else                    return GameManager.HaveStarDark;
    }
    

}
