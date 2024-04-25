using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rock
{
    public string Name { get; set; }
    public double CurHealth { get; set; }
    public double MaxHealth { get; set; }
    public float StarGrey { get; set; }
    public float StarBrown { get; set; }
    public float StarBlue { get; set; }
    public float StarGreen { get; set; }
    public float StarRed { get; set; }
    public float StarYellow { get; set; }
    public float StarPurple { get; set; }
    public float StarOrange { get; set; }
    public float StarDark { get; set; }
    public float ReinforceScroll { get; set; }
    public int Difficulty { get; set; }
    // 기타 필요한 속성들

    public Rock(string name, double curhealth, double maxhealth, float grey, float brown, float blue, float green, float red, float yellow, float purple, float orange, float dark, float reinforcescroll, int difficulty)
    {
        Name = name;
        CurHealth = curhealth;
        MaxHealth = maxhealth;
        StarGrey = grey;
        StarBrown = brown;
        StarBlue = blue;
        StarGreen = green;
        StarRed = red;
        StarYellow = yellow;
        StarPurple = purple;
        StarOrange = orange;
        StarDark = dark;
        ReinforceScroll = reinforcescroll;
        Difficulty = difficulty;
    }
}

public class RockManager : MonoBehaviour
{


    private static RockManager instance;

    public static RockManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RockManager>();
                if (instance == null)
                {
                    Debug.LogError("RockManager 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }

    private List<Rock> rocks = new();

    public static Rock rock;

    public GameObject RockLevelBox;
    public GameObject RockDifficultyPanel;
    public GameObject RockSelPanel;

    public Image[] RockImage;
    public Sprite[] RockSprite;
    public Text[] RockDifficultyNameText;

    public Button RockLevelBoxOpenBtn;
    public Button[] RockLevelSelOpenBtn;
    public Button[] RockSelBtn;

    public Button RockLevelOutBtn;
    public Button RockSelOutBtn;

    public Button RockDifficulty_ReturnBtn;
    public Button RockDifficulty_NextBtn;

    public Button RockSummonsBtn;

    public Image RockInfoImage;
    public Image RockInfoFrame;

    public Text RockTitle;

    public Text[] RockInfoText;
    public Text RockInfoHP;

    public static int Rock_defeatedIndex = 0;
    public string RockType;
    private int LastSelBtnIndex = 0;
    private bool ChangeButtonclick = false;

    public static int DifficultyMainNum;
    public static int RockFixDifficulty;
    public static int RockMoveDifficulty;

    private bool[][] RockFixStage = new bool[4][];
    private bool[][] RockMoveStage = new bool[4][];

    public Text RealRockName;
    public static double currentHP;
    public static double maxHP;
    public static float Chance_StarGrey;
    public static float Chance_StarBrown;
    public static float Chance_StarBlue;
    public static float Chance_StarGreen;
    public static float Chance_StarRed;
    public static float Chance_StarYellow;
    public static float Chance_StarPurple;
    public static float Chance_StarOrange;
    public static float Chance_StarDark;

    private void InitializeMonsters()
    {
        Rock[] FirstRocks = new Rock[]
        {
            new Rock("하급 돌덩이 <color=lime>I</color>",    3,      3,   99.9f, 0,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>II</color>",   6,      6,   99.0f,  0.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>III</color>",  9,      9,   98.0f,  1.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>IV</color>",   12,     12,  97.0f,  2.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>V</color>",    18,     18,  96.0f,  3.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>VI</color>",   23,     23,  95.0f,  4.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>VII</color>",  28,     28,  94.0f,  5.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>VIII</color>", 33,     33,  93.0f,  6.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>IX</color>",   38,     38,  92.0f,  7.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>X</color>",    50,     50,  91.0f,  8.9f,  0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>XI</color>",   58,     58,  90.0f,  9.9f, 0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>XII</color>",  64,     64,  88.0f,  11.9f, 0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>XIII</color>", 72,     72,  86.0f,  13.9f, 0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>XIV</color>",  80,     80,  84.0f,  15.9f, 0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>XV</color>",   95,     95,  82.0f,  17.9f, 0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>XVI</color>",  105,    105, 80.0f,  19.9f, 0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>XVII</color>", 115,    115, 75.0f,  24.9f, 0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=lime>BOSS</color>", 130,    130, 70.0f,  29.9f, 0f, 0f, 0, 0, 0, 0, 0, 0.1f, 0),

            new Rock("중급 돌덩이 <color=lime>I</color>",    150,    150,  70.0f,  28.8f,  1f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>II</color>",   170,    170,  70.0f,  28.8f,  1f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>III</color>",  190,    190,  70.0f,  27.8f,  2f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>IV</color>",   210,    210,  70.0f,  27.8f,  2f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>V</color>",    250,    250,  70.0f,  26.8f,  3f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>VI</color>",   280,    280,  70.0f,  26.8f,  3f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>VII</color>",  310,    310,  70.0f,  25.8f,  4f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>VIII</color>", 340,    340,  70.0f,  25.8f,  4f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>IX</color>",   370,    370,  70.0f,  24.8f,  5f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>X</color>",    430,    430,  70.0f,  24.8f,  5f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>XI</color>",   480,    480,  70.0f,  23.8f,  6f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>XII</color>",  530,    530,  70.0f,  23.8f,  6f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>XIII</color>", 580,    580,  70.0f,  22.8f,  7f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>XIV</color>",  630,    630,  70.0f,  22.8f,  7f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>XV</color>",   730,    730,  70.0f,  21.8f,  8f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>XVI</color>",  800,    800,  70.0f,  21.8f,  8f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>XVII</color>", 870,    870,  70.0f,  20.8f,  9f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=lime>BOSS</color>", 1000,   1000, 70.0f,  20.8f,  9f, 0f, 0, 0, 0, 0, 0, 0.2f, 1),

            new Rock("고급 돌덩이 <color=lime>I</color>",    1100,   1100,  58.0f,  32.0f,  9.3f,  0.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>II</color>",   1200,   1200,  56.0f,  34.0f,  9.3f,  0.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>III</color>",  1300,   1300,  54.0f,  34.5f,  10.3f, 1.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>IV</color>",   1400,   1400,  52.0f,  36.5f,  10.3f, 1.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>V</color>",    1600,   1600,  50.0f,  37.0f,  11.3f, 1.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>VI</color>",   1720,   1720,  48.0f,  39.0f,  11.3f, 1.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>VII</color>",  1840,   1840,  46.0f,  39.5f,  12.3f, 2.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>VIII</color>", 1960,   1960,  44.0f,  41.5f,  12.3f, 2.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>IX</color>",   2080,   2080,  42.0f,  42.0f,  13.3f, 2.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>X</color>",    2340,   2340,  40.0f,  44.0f,  13.3f, 2.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>XI</color>",   2480,   2480,  38.0f,  44.5f,  14.3f, 3.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>XII</color>",  2620,   2620,  36.0f,  46.5f,  14.3f, 3.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>XIII</color>", 2680,   2680,  34.0f,  47.0f,  15.3f, 3.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>XIV</color>",  2820,   2820,  32.0f,  49.0f,  15.3f, 3.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>XV</color>",   3100,   3100,  30.0f,  49.5f,  16.3f, 4.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>XVI</color>",  3260,   3260,  28.0f,  51.5f,  16.3f, 4.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>XVII</color>", 3420,   3420,  26.0f,  52.0f,  17.3f, 4.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("고급 돌덩이 <color=lime>BOSS</color>", 3800,   3800,  20.0f,  58.0f,  17.3f, 4.5f, 0, 0, 0, 0, 0, 0.2f, 2),

            new Rock("최고급 돌덩이 <color=lime>I</color>",    1100,   1100,  58.0f,  32.0f,  9.3f,  0.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>II</color>",   1200,   1200,  56.0f,  34.0f,  9.3f,  0.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>III</color>",  1300,   1300,  54.0f,  34.5f,  10.3f, 1.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>IV</color>",   1400,   1400,  52.0f,  36.5f,  10.3f, 1.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>V</color>",    1600,   1600,  50.0f,  37.0f,  11.3f, 1.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>VI</color>",   1720,   1720,  48.0f,  39.0f,  11.3f, 1.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>VII</color>",  1840,   1840,  46.0f,  39.5f,  12.3f, 2.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>VIII</color>", 1960,   1960,  44.0f,  41.5f,  12.3f, 2.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>IX</color>",   2080,   2080,  42.0f,  42.0f,  13.3f, 2.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>X</color>",    2340,   2340,  40.0f,  44.0f,  13.3f, 2.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>XI</color>",   2480,   2480,  38.0f,  44.5f,  14.3f, 3.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>XII</color>",  2620,   2620,  36.0f,  46.5f,  14.3f, 3.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>XIII</color>", 2680,   2680,  34.0f,  47.0f,  15.3f, 3.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>XIV</color>",  2820,   2820,  32.0f,  49.0f,  15.3f, 3.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>XV</color>",   3100,   3100,  30.0f,  49.5f,  16.3f, 4.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>XVI</color>",  3260,   3260,  28.0f,  51.5f,  16.3f, 4.0f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>XVII</color>", 3420,   3420,  26.0f,  52.0f,  17.3f, 4.5f, 0, 0, 0, 0, 0, 0.2f, 2),
            new Rock("최고급 돌덩이 <color=lime>BOSS</color>", 3800,   3800,  20.0f,  58.0f,  17.3f, 4.5f, 0, 0, 0, 0, 0, 0.2f, 2),
        };

        // 다른 몬스터들을 몬스터 리스트에 추가
        rocks.AddRange(FirstRocks);
    }

    private void Awake()
    {
        InitializeMonsters();
    }

    // Start is called before the first frame update
    void Start()
    {

        DifficultyMainNum = 0;
        Rock_defeatedIndex = 0;

        GameManager.RockstageClearDict["하급 돌덩이"] = new bool[18];
        GameManager.RockstageClearDict["중급 돌덩이"] = new bool[18];
        GameManager.RockstageClearDict["상급 돌덩이"] = new bool[18];
        GameManager.RockstageClearDict["최상급 돌덩이"] = new bool[18];

        GameManager.RockstageClearDict["하급 돌덩이"][0] = true;

        RockLevelBoxOpenBtn.onClick.AddListener(() => RockLevelBoxOpen(DifficultyMainNum));
        RockDifficulty_NextBtn.onClick.AddListener(RockNext);
        RockDifficulty_ReturnBtn.onClick.AddListener(RockReturn);
        RockLevelOutBtn.onClick.AddListener(RockLevelOut);
        RockSelOutBtn.onClick.AddListener(RockLevelOut);
        RockSummonsBtn.onClick.AddListener(RockSummons);


        for (int i = 0; i < 4; i++)
        {
            RockMoveStage[i] = new bool[18];
            RockFixStage[i] = new bool[18];
        }

        for (int i = 0; i < 4; i++)
        {
            int index = i;

            RockLevelSelOpenBtn[index].onClick.AddListener(() => RockLevelSelOpen(index));
        }

        for(int i = 0; i < 18; i++)
        {
            int index = i;

            RockSelBtn[index].onClick.AddListener(() => ChangeRockInfo(index));
        }
        RockSummons();
    }

    private string[] RockDifficultyName = new string[8]
    {
        "Cave - <color=lime>하급</color> 돌덩이",
        "Cave - <color=fuchsia>중급</color> 돌덩이",
        "Cave - <color=red>고급</color> 돌덩이",
        "Cave - <color=maroon>최고급</color> 돌덩이",

        "Cave - <color=lime>하급</color> 광물",
        "Cave - <color=fuchsia>중급</color> 광물",
        "Cave - <color=red>고급</color> 광물",
        "Cave - <color=maroon>최고급</color> 광물",
    };

    private string[] RockTitleString = new string[8]
        {
            "Cave - <color=lime><size=38>Easy</size></color>",
            "Cave - <color=cyan><size=38>Normal</size></color>",
            "Cave - <color=red><size=38>Hard</size></color>",
            "Cave - <color=maroon><size=38>Extreme</size></color>",
             
            "Cave - <color=lime><size=38>Easy</size></color>",
            "Cave - <color=cyan><size=38>Normal</size></color>",
            "Cave - <color=red><size=38>Hard</size></color>",
            "Cave - <color=maroon><size=38>Extreme</size></color>",
        };

    private void RockLevelBoxOpen(int maindifficulty)
    {
        DifficultyMainNum = maindifficulty;
        RockLevelUpdate();

        if (maindifficulty == 0) RockDifficulty_ReturnBtn.gameObject.SetActive(false);
        else RockDifficulty_ReturnBtn.gameObject.SetActive(true);

        if(maindifficulty == 1) RockDifficulty_NextBtn.gameObject.SetActive(false);
        else RockDifficulty_NextBtn.gameObject.SetActive(true);

        RockLevelBox.SetActive(true);
        RockDifficultyPanel.SetActive(true);

        RockLevelOutBtn.gameObject.SetActive(true);
        RockSelOutBtn.gameObject.SetActive(false);
    }

    private void RockLevelSelOpen(int index)
    {
        ChangeButtonclick = false;
        RockMoveDifficulty = index;

        if (DifficultyMainNum == 0)
        {

            if (index == 0)  // 난이도만 선택
            {
                UpdateEnemyButtons("하급 돌덩이", "민트색", "기본색", "투명기본색", 0);
                RockInfoFrame.color = ColorManager.ColorChange("라임색");
            }
            else if (index == 1)  // 난이도만 선택
            {
                UpdateEnemyButtons("중급 돌덩이", "민트색", "기본색", "투명기본색", 1);
                RockInfoFrame.color = ColorManager.ColorChange("핑크색");
            }
            else if (index == 2)  // 난이도만 선택
            {
                UpdateEnemyButtons("고급 돌덩이", "민트색", "기본색", "투명기본색", 2);
                RockInfoFrame.color = ColorManager.ColorChange("빨간색");
            }
            else if (index == 3)  // 난이도만 선택
            {
                UpdateEnemyButtons("최고급 돌덩이", "민트색", "기본색", "투명기본색", 3);
                RockInfoFrame.color = ColorManager.ColorChange("maroon");
            }

            RockInfoImage.sprite = RockSprite[index + DifficultyMainNum * 4];
        }

        UpdateInfoValue();
        RockSelOutBtn.gameObject.SetActive(true);
        RockLevelOutBtn.gameObject.SetActive(false);

        RockDifficultyPanel.SetActive(false);
        RockSelPanel.SetActive(true);
    }

    private void UpdateEnemyButtons(string rockType, string defeatedColor, string interactableColor, string nonInteractableColor, int difficulty)
    {
        for (int i = 0; i < RockSelBtn.Length; i++)
        {
            int index_ = i; // 클로저로 인덱스를 보존

            if (GameManager.RockstageClearDict[rockType][index_] == true)
            {
                // 클리어된 스테이지일 때 버튼 색상 변경
                ChangeButtonColor(RockSelBtn[index_], interactableColor);
                RockSelBtn[index_].interactable = true;
            }
            else
            {
                // 클리어되지 않은 스테이지일 때 버튼 색상 변경
                ChangeButtonColor(RockSelBtn[index_], nonInteractableColor);
                RockSelBtn[index_].interactable = false;
            }
        }

        // 만약 PinInStage[][] == true면 색상 변경 이것만! : 현재 선택된 스테이지라는 뜻
        if (RockFixStage[RockFixDifficulty][Rock_defeatedIndex] == true && difficulty == RockFixDifficulty)
        {
            // 현재 선택된 스테이지일 때 버튼 색상 변경
            ChangeButtonColor(RockSelBtn[Rock_defeatedIndex], defeatedColor);
        }
    }
    private void ChangeRockInfo(int index)
    {

        if (ChangeButtonclick == false) ChangeButtonclick = true;
        // index : 현재   LastSelBtnIndex : 마지막

        // 현재와 마지막이 같으면 리턴;
        if (index == LastSelBtnIndex) return;

        // 이전 버튼의 색상을 변경합니다.
        if (GameManager.RockstageClearDict[RockName()][LastSelBtnIndex] == true) ChangeButtonColor(RockSelBtn[LastSelBtnIndex], "기본색");

        // 현재 선택한 버튼의 색상 변경
        ChangeButtonColor(RockSelBtn[index], "민트색");

        // 전 넘버는 false로
        RockMoveStage[RockMoveDifficulty][LastSelBtnIndex] = false;

        // MoveInStage를 true 변경
        RockMoveStage[RockMoveDifficulty][index] = true;

        // 현재 선택한 버튼을 이전에 선택한 버튼으로 설정
        LastSelBtnIndex = index;

        // 슬라임 정보를 사용하여 UI 업데이트
        UpdateInfoValue();
    }

    private void RockSummons()
    {

        if (GameManager.RockstageClearDict[RockName()][LastSelBtnIndex] == false) return;
        if (ChangeButtonclick == false && RockMoveDifficulty != RockFixDifficulty) return; // 선택 안했을때 리턴시키기

        // 보스 몬스터 정보 가져오기
        Rock_defeatedIndex = LastSelBtnIndex;

        for (int i = 0; i < 18; i++)
        {
            RockFixStage[RockMoveDifficulty][i] = false;
            RockMoveStage[RockMoveDifficulty][i] = false;
        }

        RockFixDifficulty = RockMoveDifficulty;

        RockTitle.text = RockTitleString[RockFixDifficulty + RockFixDifficulty * 4];

        int _Defeat = GetRockIndex(Rock_defeatedIndex);

        Rock selectedRock = GetRockbyIndex(_Defeat);

        RockMoveDifficulty = RockFixDifficulty;

        // 보스의 인덱스만 true로 설정합니다.
        RockMoveStage[RockFixDifficulty][Rock_defeatedIndex] = true;
        RockFixStage[RockFixDifficulty][Rock_defeatedIndex] = true;

        // 보스 몬스터 속성 설정
        RealRockName.text = selectedRock.Name;
        maxHP = selectedRock.MaxHealth;
        currentHP = selectedRock.MaxHealth;
        Chance_StarGrey = selectedRock.StarGrey;
        Chance_StarBrown = selectedRock.StarBrown;
        Chance_StarBlue = selectedRock.StarBlue;
        Chance_StarGreen = selectedRock.StarGreen;
        Chance_StarRed = selectedRock.StarRed;
        Chance_StarYellow = selectedRock.StarYellow;
        Chance_StarPurple = selectedRock.StarPurple;
        Chance_StarOrange = selectedRock.StarOrange;
        Chance_StarDark = selectedRock.StarDark;


        // 보스 몬스터 속성에 따른 보정 (예: 보스 레벨에 따른 HP 보정)
        //if (GameManager.WarrantLevel[15] >= 1) maxHP *= (1 - GameManager.Warrant_Power[15] / 100f);

        RockLevelBox.SetActive(false);
        RockSelPanel.SetActive(false);
    }

    private void RockLevelUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            RockImage[i].sprite = RockSprite[i + DifficultyMainNum * 4];
            RockDifficultyNameText[i].text = RockDifficultyName[i + DifficultyMainNum * 4];
        }
    }

    private void RockNext()
    {
        DifficultyMainNum++;

        if (DifficultyMainNum == 1) RockDifficulty_NextBtn.gameObject.SetActive(false);
        else RockDifficulty_NextBtn.gameObject.SetActive(true);

        RockDifficulty_ReturnBtn.gameObject.SetActive(true);

        RockLevelUpdate();
    }

    private void RockReturn()
    {
        DifficultyMainNum--;

        if (DifficultyMainNum == 0) RockDifficulty_ReturnBtn.gameObject.SetActive(false);
        else RockDifficulty_ReturnBtn.gameObject.SetActive(true);

        RockDifficulty_NextBtn.gameObject.SetActive(true);

        RockLevelUpdate();
    }

    private void RockLevelOut()
    {
        // 만약 마지막 버튼이 결정한 버튼 Index숫자와 다를때 마지막 버튼을 결정한 버튼으로 변경
        if (LastSelBtnIndex != Rock_defeatedIndex)
        {
            // 나머지 요소는 모두 false로 설정합니다.
            for (int i = 0; i < 18; i++) RockMoveStage[RockMoveDifficulty][i] = false;

            RockMoveStage[RockMoveDifficulty][LastSelBtnIndex] = RockFixStage[RockFixDifficulty][Rock_defeatedIndex];
            LastSelBtnIndex = Rock_defeatedIndex;
        }

        // 만약 마지막 선택 난이도가 결정 난이도와 다를때 선택 난이도를 결정 난이도로 설정
        if (RockFixDifficulty != RockMoveDifficulty) RockMoveDifficulty = RockFixDifficulty;

        RockLevelBox.SetActive(false);
        RockSelPanel.SetActive(false);
        RockDifficultyPanel.SetActive(false);
    }

    private string RockName()
    {
        if (DifficultyMainNum == 0)
        {
            RockType = RockMoveDifficulty switch
            {
                0 => "하급 돌덩이",
                1 => "중급 돌덩이",
                2 => "상급 돌덩이",
                3 => "최상급 돌덩이",
                // 필요에 따라 다른 InStage에 대한 처리 추가
                _ => "Unknown", // 기본값 설정 혹은 오류 처리
            };
        }

        return RockType;
    }

    // 돌 선택 색깔 변경 관리
    public void ChangeButtonColor(Button button, string color)
    {
        // 버튼의 이미지 컴포넌트 가져오기
        Image buttonImage = button.GetComponent<Image>();
        // 버튼의 이미지 색상 변경
        buttonImage.color = ColorManager.ColorChange(color);
    }

    private int GetRockIndex(int index)
    {

        if (RockFixDifficulty== 0) index = Rock_defeatedIndex;
        else if (RockFixDifficulty == 1) index = Rock_defeatedIndex + 18;
        else if (RockFixDifficulty == 2) index = Rock_defeatedIndex + 36;
        else if (RockFixDifficulty == 3) index = Rock_defeatedIndex + 54;

        return index;
    }
    private void UpdateInfoValue()
    {
        Rock rock;

        int _Defeat = LastSelBtnIndex;

        if (RockMoveDifficulty == 0) _Defeat = LastSelBtnIndex;
        else if (RockMoveDifficulty == 1) _Defeat = LastSelBtnIndex + 18;
        else if (RockMoveDifficulty == 2) _Defeat = LastSelBtnIndex + 36;
        else if (RockMoveDifficulty == 3) _Defeat = LastSelBtnIndex + 54;

        rock = GetRockbyIndex(_Defeat);

        double maxHealth = rock.MaxHealth;

        RockInfoHP.text = "<color=lightblue>" + TextFormatter.GetThousandCommaText((long)maxHealth) + "</color>";

        RockInfoText[0].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarGrey) + "%</color>";
        RockInfoText[1].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarBrown) + "%</color>";
        RockInfoText[2].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarBlue) + "%</color>";
        RockInfoText[3].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarGreen) + "%</color>";
        RockInfoText[4].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarRed) + "%</color>";
        RockInfoText[5].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarYellow) + "%</color>";
        RockInfoText[6].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarPurple) + "%</color>";
        RockInfoText[7].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarOrange) + "%</color>";
        RockInfoText[8].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.StarDark) + "%</color>";
        RockInfoText[9].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_0(rock.ReinforceScroll) + "%</color>";
    }
    // 돌 넘버 받기
    public Rock GetRockbyIndex(int index)
    {
        // index에 해당하는 광물 정보 반환
        return rocks[index];
    }

    public static IEnumerator RegenHP() // IEnumerator 반환 타입으로 변경
    {
        yield return new WaitForSeconds(0.1f);
        if (currentHP < maxHP) currentHP += maxHP / 100f;
        else currentHP = maxHP;
    }


    [System.Obsolete]
    public void GrantRewards(int Rock_defeatedIndex, int Bonus)
    {
        // 광물 정보 가져오기

        int _Defeat = GetRockIndex(Rock_defeatedIndex);

        Rock currentRock = GetRockbyIndex(_Defeat);


        float Reward_RocksRandom = Random.value;

        int Reward_HaveStarGrey;
        int Reward_HaveStarBrown;
        int Reward_HaveStarBlue;
        int Reward_HaveStarGreen;
        int Reward_HaveStarRed;
        int Reward_HaveStarYellow;
        int Reward_HaveStarPurple;
        int Reward_HaveStarOrange;
        int Reward_HaveStarDark;
        int Reward_ReinforceScroll;


        // 광물의 정보를 가져와서 해당 보상을 계산

        float[] dustProbabilities = {
             currentRock.StarGrey,
             currentRock.StarBrown,
             currentRock.StarBlue,
             currentRock.StarGreen,
             currentRock.StarRed,
             currentRock.StarYellow,
             currentRock.StarPurple,
             currentRock.StarOrange,
             currentRock.StarDark,
             currentRock.ReinforceScroll
        };

        int[] dustRewards = new int[dustProbabilities.Length];
        float cumulativeProbability = 0;
        
        for (int i = 0; i < dustProbabilities.Length; i++)
        {
            cumulativeProbability += dustProbabilities[i] / 100f;
            if (Reward_RocksRandom < cumulativeProbability)
            {
                dustRewards[i] = Rock_defeatedIndex + 1;
                break; // 보상이 결정되면 루프 종료
            }
        }

        //각 보상 변수에 값 할당
        Reward_HaveStarGrey = dustRewards[0];
        Reward_HaveStarBrown = dustRewards[1];
        Reward_HaveStarBlue = dustRewards[2];
        Reward_HaveStarGreen = dustRewards[3];
        Reward_HaveStarRed = dustRewards[4];
        Reward_HaveStarYellow = dustRewards[5];
        Reward_HaveStarPurple = dustRewards[6];
        Reward_HaveStarOrange = dustRewards[7];
        Reward_HaveStarDark = dustRewards[8];
        Reward_ReinforceScroll = dustRewards[9];

        //

        // 권능

        if (GameManager.WarrantLevel[9] >= 1) Bonus += GameManager.Warrant_Power[9];
        if (MineAdManager.AdPlaying[2] == true) Bonus += MineAdManager.AdPowerValue[2];

        // 플레이어에게 보상 지급
        GameManager.HaveStarGrey += Reward_HaveStarGrey * Bonus;
        GameManager.HaveStarBrown += Reward_HaveStarBrown * Bonus;
        GameManager.HaveStarBlue += Reward_HaveStarBlue * Bonus;
        GameManager.HaveStarGreen += Reward_HaveStarGreen * Bonus;
        GameManager.HaveStarRed += Reward_HaveStarRed * Bonus;
        GameManager.HaveStarYellow += Reward_HaveStarYellow * Bonus;
        GameManager.HaveStarPurple += Reward_HaveStarPurple * Bonus;
        GameManager.HaveStarOrange += Reward_HaveStarOrange * Bonus;
        GameManager.HaveStarDark += Reward_HaveStarDark * Bonus;
        GameManager.RelicsReinforceScroll += Reward_ReinforceScroll;

        currentHP = maxHP;

    }

}
