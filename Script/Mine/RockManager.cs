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

    public GameObject Rock;

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
    public Image[] RockDifficultyFrame;

    public Text RockTitle;

    public Text[] RockInfoText;
    public Text RockInfoHP;

    public GameObject[] BrokingPanel;
    public Text[] RockClearValue;

    public Button MineNextBtn;

    public static int Rock_defeatedIndex;
    private bool ChangeButtonclick = false;

    public int LastBtnIndex;                      
    public int MoveDifficultyInStage;             
    public int FixDifficultyInStage;              
    public int MoveDifficulty;
    public int FixDifficulty;

    public bool[][] RockFixStage = new bool[GameConstants.RockNum][];
    public bool[][] RockMoveStage = new bool[GameConstants.RockNum][];

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

    private void InitializeRocks()
    {
        Rock[] FirstRocks = new Rock[]
        {
            new Rock("하급 돌덩이 <color=#63C74D>I</color>",    3,      3,   99.9f,  0.0f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>II</color>",   6,      6,   99.0f,  0.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>III</color>",  9,      9,   98.0f,  1.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>IV</color>",   12,     12,  97.0f,  2.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>V</color>",    18,     18,  96.0f,  3.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>VI</color>",   23,     23,  95.0f,  4.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>VII</color>",  28,     28,  94.0f,  5.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>VIII</color>", 33,     33,  93.0f,  6.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>IX</color>",   38,     38,  92.0f,  7.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>X</color>",    50,     50,  91.0f,  8.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>XI</color>",   58,     58,  90.0f,  9.9f,  0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>XII</color>",  64,     64,  88.0f,  11.9f, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>XIII</color>", 72,     72,  86.0f,  13.9f, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>XIV</color>",  80,     80,  84.0f,  15.9f, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>XV</color>",   95,     95,  82.0f,  17.9f, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>XVI</color>",  105,    105, 80.0f,  19.9f, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>XVII</color>", 115,    115, 75.0f,  24.9f, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0),
            new Rock("하급 돌덩이 <color=#63C74D>BOSS</color>", 130,    130, 70.0f,  29.9f, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0),

            new Rock("중급 돌덩이 <color=#63C74D>I</color>",    150,    150,  70.0f,  28.8f,  1f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>II</color>",   170,    170,  70.0f,  28.8f,  1f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>III</color>",  190,    190,  70.0f,  27.8f,  2f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>IV</color>",   210,    210,  70.0f,  27.8f,  2f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>V</color>",    250,    250,  70.0f,  26.8f,  3f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>VI</color>",   280,    280,  70.0f,  26.8f,  3f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>VII</color>",  310,    310,  70.0f,  25.8f,  4f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>VIII</color>", 340,    340,  70.0f,  25.8f,  4f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>IX</color>",   370,    370,  70.0f,  24.8f,  5f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>X</color>",    430,    430,  70.0f,  24.8f,  5f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>XI</color>",   480,    480,  70.0f,  23.8f,  6f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>XII</color>",  530,    530,  70.0f,  23.8f,  6f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>XIII</color>", 580,    580,  70.0f,  22.8f,  7f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>XIV</color>",  630,    630,  70.0f,  22.8f,  7f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>XV</color>",   730,    730,  70.0f,  21.8f,  8f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>XVI</color>",  800,    800,  70.0f,  21.8f,  8f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>XVII</color>", 870,    870,  70.0f,  20.8f,  9f, 0, 0, 0, 0, 0, 0, 0.2f, 1),
            new Rock("중급 돌덩이 <color=#63C74D>BOSS</color>", 1000,   1000, 70.0f,  20.8f,  9f, 0, 0, 0, 0, 0, 0, 0.2f, 1),

            new Rock("상급 돌덩이 <color=#63C74D>I</color>",    1100,   1100,  58.0f,  32.0f,  9.3f,  0.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>II</color>",   1200,   1200,  56.0f,  34.0f,  9.3f,  0.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>III</color>",  1300,   1300,  54.0f,  34.5f,  10.3f, 1.0f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>IV</color>",   1400,   1400,  52.0f,  36.5f,  10.3f, 1.0f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>V</color>",    1600,   1600,  50.0f,  37.0f,  11.3f, 1.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>VI</color>",   1720,   1720,  48.0f,  39.0f,  11.3f, 1.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>VII</color>",  1840,   1840,  46.0f,  39.5f,  12.3f, 2.0f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>VIII</color>", 1960,   1960,  44.0f,  41.5f,  12.3f, 2.0f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>IX</color>",   2080,   2080,  42.0f,  42.0f,  13.3f, 2.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>X</color>",    2340,   2340,  40.0f,  44.0f,  13.3f, 2.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>XI</color>",   2480,   2480,  38.0f,  44.5f,  14.3f, 3.0f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>XII</color>",  2620,   2620,  36.0f,  46.5f,  14.3f, 3.0f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>XIII</color>", 2680,   2680,  34.0f,  47.0f,  15.3f, 3.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>XIV</color>",  2820,   2820,  32.0f,  49.0f,  15.3f, 3.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>XV</color>",   3100,   3100,  30.0f,  49.5f,  16.3f, 4.0f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>XVI</color>",  3260,   3260,  28.0f,  51.5f,  16.3f, 4.0f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>XVII</color>", 3420,   3420,  26.0f,  52.0f,  17.3f, 4.5f, 0, 0, 0, 0, 0, 0.3f, 2),
            new Rock("상급 돌덩이 <color=#63C74D>BOSS</color>", 3800,   3800,  20.0f,  58.0f,  17.3f, 4.5f, 0, 0, 0, 0, 0, 0.3f, 2),

            new Rock("최상급 돌덩이 <color=#63C74D>I</color>",    4000,   4000,  18.0f,  58.375f, 18.3f, 5.0f, 0.025f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>II</color>",   4200,   4200,  16.0f,  60.375f, 18.3f, 5.0f, 0.025f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>III</color>",  4400,   4400,  14.0f,  60.85f,  19.3f, 5.5f, 0.05f,  0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>IV</color>",   4600,   4600,  12.0f,  63.325f, 19.3f, 5.5f, 0.05f,  0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>V</color>",    5000,   5000,  10.0f,  63.325f, 20.3f, 6.0f, 0.075f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>VI</color>",   5220,   5220,  8.0f,   65.325f, 20.3f, 6.0f, 0.075f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>VII</color>",  5440,   5440,  6.0f,   66.07f,  21.3f, 6.5f, 0.1f,   0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>VIII</color>", 5660,   5660,  4.0f,   68.07f,  21.3f, 6.5f, 0.1f,   0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>IX</color>",   5880,   5880,  2.0f,   68.275f, 22.3f, 7.0f, 0.125f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>X</color>",    6320,   6320,  0.0f,   70.275f, 22.3f, 7.0f, 0.125f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>XI</color>",   6560,   6560,  0.0f,   70.1f,   23.3f, 7.5f, 0.15f,  0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>XII</color>",  6800,   6800,  0.0f,   70.1f,   23.3f, 7.5f, 0.15f,  0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>XIII</color>", 7040,   7040,  0.0f,   68.35f,  24.3f, 8.0f, 0.175f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>XIV</color>",  7260,   7260,  0.0f,   68.35f,  24.3f, 8.0f, 0.175f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>XV</color>",   7760,   7760,  0.0f,   66.6f,   25.3f, 8.5f, 0.2f,   0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>XVI</color>",  8020,   8020,  0.0f,   66.6f,   25.3f, 8.5f, 0.2f,   0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>XVII</color>", 8280,   8280,  0.0f,   64.85f,  26.3f, 9.0f, 0.225f, 0, 0, 0, 0, 0.4f, 3),
            new Rock("최상급 돌덩이 <color=#63C74D>BOSS</color>", 9000,   9000,  0.0f,   64.85f,  26.3f, 9.0f, 0.225f, 0, 0, 0, 0, 0.4f, 3),

            new Rock("하급 광물 <color=#B86F50e>I</color>",    9200,   9200,   0.0f,   60.8f,    28.3f, 10.0f, 0.5f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>II</color>",   9400,   9400,   0.0f,   59.8f,    29.3f, 10.0f, 0.5f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>III</color>",  9600,   9600,   0.0f,   57.75f,   30.3f, 11.0f, 0.55f,  0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>IV</color>",   9800,   9800,   0.0f,   56.75f,   31.3f, 11.0f, 0.55f,  0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>V</color>",    10400,  10400,  0.0f,   54.7f,    32.3f, 12.0f, 0.6f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>VI</color>",   10600,  10600,  0.0f,   53.7f,    33.3f, 12.0f, 0.6f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>VII</color>",  10800,  10800,  0.0f,   51.65f,   34.3f, 13.0f, 0.65f,  0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>VIII</color>", 11000,  11000,  0.0f,   50.65f,   35.3f, 13.0f, 0.65f,  0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>IX</color>",   11200,  11200,  0.0f,   48.6f,    36.3f, 14.0f, 0.7f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>X</color>",    11600,  11600,  0.0f,   47.6f,    37.3f, 14.0f, 0.7f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>XI</color>",   11800,  11800,  0.0f,   45.55f,   38.3f, 15.0f, 0.75f,  0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>XII</color>",  12000,  12000,  0.0f,   44.55f,   39.3f, 15.0f, 0.75f,  0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>XIII</color>", 12200,  12200,  0.0f,   42.5f,    40.3f, 16.0f, 0.8f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>XIV</color>",  12400,  12400,  0.0f,   41.5f,    41.3f, 16.0f, 0.8f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>XV</color>",   12800,  12800,  0.0f,   39.45f,   42.3f, 17.0f, 0.85f,  0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>XVI</color>",  13000,  13000,  0.0f,   38.45f,   43.3f, 17.0f, 0.85f,  0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>XVII</color>", 13200,  13200,  0.0f,   36.4f,    44.3f, 18.0f, 0.9f,   0, 0, 0, 0, 0.5f, 4),
            new Rock("하급 광물 <color=#B86F50e>BOSS</color>", 13600,  13600,  0.0f,   35.4f,    45.3f, 18.0f, 0.9f,   0, 0, 0, 0, 0.5f, 4),
                                       
            new Rock("중급 광물 <color=#B86F50n>I</color>",    13820,   13820,  0.0f,  33.25f,  46.3f, 19.0f, 0.95f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>II</color>",   14040,   14040,  0.0f,  32.25f,  47.3f, 19.0f, 0.95f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>III</color>",  14260,   14260,  0.0f,  30.2f,   48.3f, 20.0f, 1.0f,  0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>IV</color>",   14480,   14480,  0.0f,  29.2f,   49.3f, 20.0f, 1.0f,  0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>V</color>",    14920,   14920,  0.0f,  27.15f,  50.3f, 21.0f, 1.05f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>VI</color>",   15140,   15140,  0.0f,  27.15f,  50.3f, 21.0f, 1.05f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>VII</color>",  15360,   15360,  0.0f,  26.1f,   50.3f, 22.0f, 1.1f,  0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>VIII</color>", 15580,   15580,  0.0f,  26.1f,   50.3f, 22.0f, 1.1f,  0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>IX</color>",   15800,   15800,  0.0f,  25.05f,  50.3f, 23.0f, 1.15f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>X</color>",    16240,   16240,  0.0f,  25.05f,  50.3f, 23.0f, 1.15f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>XI</color>",   16460,   16460,  0.0f,  24.00f,  50.3f, 24.0f, 1.2f,  0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>XII</color>",  16680,   16680,  0.0f,  24.00f,  50.3f, 24.0f, 1.2f,  0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>XIII</color>", 16900,   16900,  0.0f,  22.95f,  50.3f, 25.0f, 1.25f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>XIV</color>",  17120,   17120,  0.0f,  22.95f,  50.3f, 25.0f, 1.25f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>XV</color>",   17560,   17560,  0.0f,  21.9f,   50.3f, 26.0f, 1.3f,  0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>XVI</color>",  17780,   17780,  0.0f,  21.9f,   50.3f, 26.0f, 1.3f,  0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>XVII</color>", 18000,   18000,  0.0f,  20.85f,  50.3f, 27.0f, 1.35f, 0, 0, 0, 0, 0.6f, 5),
            new Rock("중급 광물 <color=#B86F50n>BOSS</color>", 18660,   18660,  0.0f,  20.85f,  50.3f, 27.0f, 1.35f, 0, 0, 0, 0, 0.6f, 5),
                                       
            new Rock("상급 광물 <color=#B86F50>I</color>",     19250,   19250,  0.0f,  19.6f,  50.4f,  28.0f,  1.4f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>II</color>",    19500,   19500,  0.0f,  19.6f,  50.4f,  28.0f,  1.4f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>III</color>",   19750,   19750,  0.0f,  18.55f, 50.4f,  29.0f,  1.45f,  0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>IV</color>",    20000,   20000,  0.0f,  18.55f, 50.4f,  29.0f,  1.45f,  0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>V</color>",     20500,   20500,  0.0f,  17.5f,  50.4f,  30.0f,  1.5f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>VI</color>",    20750,   20750,  0.0f,  17.5f,  50.4f,  30.0f,  1.5f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>VII</color>",   21000,   21000,  0.0f,  16.45f, 50.4f,  31.0f,  1.55f,  0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>VIII</color>",  21250,   21250,  0.0f,  16.45f, 50.4f,  31.0f,  1.55f,  0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>IX</color>",    21500,   21500,  0.0f,  15.4f,  50.4f,  32.0f,  1.6f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>X</color>",     22000,   22000,  0.0f,  15.4f,  50.4f,  32.0f,  1.6f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>XI</color>",    22250,   22250,  0.0f,  14.35f, 50.4f,  33.0f,  1.65f,  0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>XII</color>",   22500,   22500,  0.0f,  14.35f, 50.4f,  33.0f,  1.65f,  0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>XIII</color>",  22750,   22750,  0.0f,  13.3f,  50.4f,  34.0f,  1.7f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>XIV</color>",   23000,   23000,  0.0f,  13.3f,  50.4f,  34.0f,  1.7f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>XV</color>",    23500,   23500,  0.0f,  12.25f, 50.4f,  35.0f,  1.75f,  0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>XVI</color>",   23750,   23750,  0.0f,  12.25f, 50.4f,  35.0f,  1.75f,  0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>XVII</color>",  24000,   24000,  0.0f,  11.2f,  50.4f,  36.0f,  1.8f,   0, 0, 0, 0, 0.7f, 6),
            new Rock("상급 광물 <color=#B86F50>BOSS</color>",  25000,   25000,  0.0f,  11.2f,  50.4f,  36.0f,  1.8f,   0, 0, 0, 0, 0.7f, 6),

            new Rock("최상급 광물 <color=#B86F50>I</color>",    25300,   25300,  0.0f,  10.15f, 50.3f, 37.0f,  1.85f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>II</color>",   25600,   25600,  0.0f,  10.15f, 50.3f, 37.0f,  1.85f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>III</color>",  25900,   25900,  0.0f,  9.1f,   50.3f, 38.0f,  1.9f,    0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>IV</color>",   26200,   26200,  0.0f,  9.1f,   50.3f, 38.0f,  1.9f,    0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>V</color>",    26800,   26800,  0.0f,  8.05f,  50.3f, 39.0f,  1.95f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>VI</color>",   27100,   27100,  0.0f,  8.05f,  50.3f, 39.0f,  1.95f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>VII</color>",  27400,   27400,  0.0f,  7.00f,  50.3f, 40.0f,  2.00f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>VIII</color>", 27700,   27700,  0.0f,  7.00f,  50.3f, 40.0f,  2.00f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>IX</color>",   28000,   28000,  0.0f,  5.95f,  50.3f, 41.0f,  2.05f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>X</color>",    28600,   28600,  0.0f,  5.95f,  50.3f, 41.0f,  2.05f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>XI</color>",   28900,   28900,  0.0f,  4.9f,   50.3f, 42.0f,  2.1f,    0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>XII</color>",  29200,   29200,  0.0f,  4.9f,   50.3f, 42.0f,  2.1f,    0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>XIII</color>", 29500,   29500,  0.0f,  3.85f,  50.3f, 43.0f,  2.15f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>XIV</color>",  29800,   29800,  0.0f,  3.85f,  50.3f, 43.0f,  2.15f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>XV</color>",   30400,   30400,  0.0f,  2.8f,   50.3f, 44.0f,  2.2f,    0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>XVI</color>",  30700,   30700,  0.0f,  2.8f,   50.3f, 44.0f,  2.2f,    0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>XVII</color>", 31000,   31000,  0.0f,  1.75f,  50.3f, 45.0f,  2.25f,   0, 0, 0, 0, 0.8f, 7),
            new Rock("최상급 광물 <color=#B86F50>BOSS</color>", 32000,   32000,  0.0f,  1.75f,  50.3f, 45.0f,  2.25f,   0, 0, 0, 0, 0.8f, 7),

            new Rock("하급 광석 <color=#FEA222e>I</color>",    32380,   32380,  0.0f,   0.0f,   49.95f, 47.0f, 3.0f,  0.25f,     0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>II</color>",   32760,   32760,  0.0f,   0.0f,   49.95f, 47.0f, 3.0f,  0.25f,     0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>III</color>",  33140,   33140,  0.0f,   0.0f,   50.2f,  49.0f, 3.5f,   0.5f,     0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>IV</color>",   33520,   33520,  0.0f,   0.0f,   50.2f,  49.0f, 3.5f,   0.5f,     0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>V</color>",    34280,   34280,  0.0f,   0.0f,   50.2f,  51.0f, 4.0f,   0.75f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>VI</color>",   34660,   34660,  0.0f,   0.0f,   50.2f,  51.0f, 4.0f,   0.75f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>VII</color>",  35040,   35040,  0.0f,   0.0f,   50.2f,  53.0f, 4.5f,   1.00f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>VIII</color>", 35420,   35420,  0.0f,   0.0f,   50.2f,  53.0f, 4.5f,   1.00f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>IX</color>",   35800,   35800,  0.0f,   0.0f,   50.2f,  55.0f, 5.0f,   1.25f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>X</color>",    36560,   36560,  0.0f,   0.0f,   50.2f,  55.0f, 5.0f,   1.25f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>XI</color>",   36940,   36940,  0.0f,   0.0f,   50.2f,  57.0f, 5.5f,   1.5f,     0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>XII</color>",  37320,   37320,  0.0f,   0.0f,   50.2f,  57.0f, 5.5f,   1.5f,     0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>XIII</color>", 37700,   37700,  0.0f,   0.0f,   50.2f,  59.0f, 6.0f,   1.75f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>XIV</color>",  38080,   38080,  0.0f,   0.0f,   50.2f,  59.0f, 6.0f,   1.75f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>XV</color>",   38840,   38840,  0.0f,   0.0f,   50.2f,  61.0f, 6.5f,   2.00f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>XVI</color>",  39220,   39220,  0.0f,   0.0f,   50.2f,  61.0f, 6.5f,   2.00f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>XVII</color>", 39600,   39600,  0.0f,   0.0f,   50.2f,  63.0f, 7.0f,   2.25f,    0, 0, 0, 0.9f, 8),
            new Rock("하급 광석 <color=#FEA222e>BOSS</color>", 41000,   41000,  0.0f,   0.0f,   50.2f,  63.0f, 7.0f,   2.25f,    0, 0, 0, 0.9f, 8),
                                       
            new Rock("중급 광석 <color=#FEA222n>I</color>",    41500,   41500,  0.0f,  0.0f,  24.1f,  65.0f, 7.5f,    2.5f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>II</color>",   42000,   42000,  0.0f,  0.0f,  24.1f,  65.0f, 7.5f,    2.5f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>III</color>",  42500,   42500,  0.0f,  0.0f,  21.35f, 67.0f, 8.0f,    2.75f, 0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>IV</color>",   43000,   43000,  0.0f,  0.0f,  21.35f, 67.0f, 8.0f,    2.75f, 0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>V</color>",    44000,   44000,  0.0f,  0.0f,  18.6f,  69.0f, 8.5f,    3.0f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>VI</color>",   44500,   44500,  0.0f,  0.0f,  18.6f,  69.0f, 8.5f,    3.0f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>VII</color>",  45000,   45000,  0.0f,  0.0f,  15.85f, 71.0f, 9.0f,    3.25f, 0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>VIII</color>", 45500,   45500,  0.0f,  0.0f,  15.85f, 71.0f, 9.0f,    3.25f, 0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>IX</color>",   46000,   46000,  0.0f,  0.0f,  13.1f,  73.0f, 9.5f,    3.5f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>X</color>",    47000,   47000,  0.0f,  0.0f,  13.1f,  73.0f, 9.5f,    3.5f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>XI</color>",   47500,   47500,  0.0f,  0.0f,  10.35f, 75.0f, 10.0f,   3.75f, 0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>XII</color>",  48000,   48000,  0.0f,  0.0f,  10.35f, 75.0f, 10.0f,   3.75f, 0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>XIII</color>", 48500,   48500,  0.0f,  0.0f,  7.6f,   77.0f, 10.5f,   4.0f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>XIV</color>",  49000,   49000,  0.0f,  0.0f,  7.6f,   77.0f, 10.5f,   4.0f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>XV</color>",   50000,   50000,  0.0f,  0.0f,  5.45f,  78.4f, 11.0f,   4.25f, 0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>XVI</color>",  50500,   50500,  0.0f,  0.0f,  5.45f,  78.4f, 11.0f,   4.25f, 0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>XVII</color>", 51000,   51000,  0.0f,  0.0f,  4.7f,   78.4f, 11.5f,   4.5f,  0, 0, 0, 1.0f, 9),
            new Rock("중급 광석 <color=#FEA222n>BOSS</color>", 52500,   52500,  0.0f,  0.0f,  4.7f,   78.4f, 11.5f,   4.5f,  0, 0, 0, 1.0f, 9),
                                     
            new Rock("상급 광석 <color=#FEA222>I</color>",    53100,   53100,  0.0f,  0.0f,  3.95f,   78.3f,   12.0f,   4.75f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>II</color>",   53700,   53700,  0.0f,  0.0f,  3.95f,   78.3f,   12.0f,   4.75f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>III</color>",  54300,   54300,  0.0f,  0.0f,  3.2f,    78.3f,   12.5f,   5.00f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>IV</color>",   54900,   54900,  0.0f,  0.0f,  3.2f,    78.3f,   12.5f,   5.00f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>V</color>",    56100,   56100,  0.0f,  0.0f,  2.45f,   78.3f,   13.0f,   5.25f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>VI</color>",   56700,   56700,  0.0f,  0.0f,  2.45f,   78.3f,   13.0f,   5.25f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>VII</color>",  57300,   57300,  0.0f,  0.0f,  1.7f,    78.3f,   13.5f,   5.5f,  0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>VIII</color>", 57900,   57900,  0.0f,  0.0f,  1.7f,    78.3f,   13.5f,   5.5f,  0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>IX</color>",   56500,   56500,  0.0f,  0.0f,  0.95f,   78.3f,   14.0f,   5.75f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>X</color>",    57100,   57100,  0.0f,  0.0f,  0.95f,   78.3f,   14.0f,   5.75f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>XI</color>",   57800,   57800,  0.0f,  0.0f,  0.2f,    78.3f,   14.5f,   6.00f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>XII</color>",  58400,   58400,  0.0f,  0.0f,  0.2f,    78.3f,   14.5f,   6.00f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>XIII</color>", 59000,   59000,  0.0f,  0.0f,  0.0f,    77.75f,  15.0f,   6.25f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>XIV</color>",  59600,   59600,  0.0f,  0.0f,  0.0f,    77.75f,  15.0f,   6.25f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>XV</color>",   60800,   60800,  0.0f,  0.0f,  0.0f,    76.0f,   16.5f,   6.5f,  0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>XVI</color>",  61400,   61400,  0.0f,  0.0f,  0.0f,    76.0f,   16.5f,   6.5f,  0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>XVII</color>", 62000,   62000,  0.0f,  0.0f,  0.0f,    75.25f,  17.0f,   6.75f, 0, 0, 0, 1.1f, 10),
            new Rock("상급 광석 <color=#FEA222>BOSS</color>", 64000,   64000,  0.0f,  0.0f,  0.0f,    75.25f,  17.0f,   6.75f, 0, 0, 0, 1.1f, 10),

            new Rock("최상급 광석 <color=#FEA222>I</color>",    64650,   64650,  0.0f,  0.0f,  0.0f, 73.4f,   18.5f,  7.00f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>II</color>",   65300,   65300,  0.0f,  0.0f,  0.0f, 73.4f,   18.5f,  7.00f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>III</color>",  65950,   65950,  0.0f,  0.0f,  0.0f, 72.65f,  19.0f,  7.25f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>IV</color>",   66600,   66600,  0.0f,  0.0f,  0.0f, 72.65f,  19.0f,  7.25f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>V</color>",    67250,   67250,  0.0f,  0.0f,  0.0f, 71.9f,   19.5f,  7.5f,  0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>VI</color>",   67900,   67900,  0.0f,  0.0f,  0.0f, 71.9f,   19.5f,  7.5f,  0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>VII</color>",  68550,   68550,  0.0f,  0.0f,  0.0f, 71.15f,  20.0f,  7.75f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>VIII</color>", 69200,   69200,  0.0f,  0.0f,  0.0f, 71.15f,  20.0f,  7.75f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>IX</color>",   69850,   69850,  0.0f,  0.0f,  0.0f, 70.4f,   20.5f,  8.00f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>X</color>",    70500,   70500,  0.0f,  0.0f,  0.0f, 70.4f,   20.5f,  8.00f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>XI</color>",   71150,   71150,  0.0f,  0.0f,  0.0f, 69.65f,  21.0f,  8.25f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>XII</color>",  71800,   71800,  0.0f,  0.0f,  0.0f, 69.65f,  21.0f,  8.25f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>XIII</color>", 72450,   72450,  0.0f,  0.0f,  0.0f, 68.9f,   21.5f,  8.5f,  0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>XIV</color>",  73100,   73100,  0.0f,  0.0f,  0.0f, 68.9f,   21.5f,  8.5f,  0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>XV</color>",   73750,   73750,  0.0f,  0.0f,  0.0f, 68.15f,  22.0f,  8.75f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>XVI</color>",  74400,   74400,  0.0f,  0.0f,  0.0f, 68.15f,  22.0f,  8.75f, 0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>XVII</color>", 75050,   75050,  0.0f,  0.0f,  0.0f, 67.4f,   22.5f,  9.0f,  0, 0, 0, 1.2f, 11),
            new Rock("최상급 광석 <color=#FEA222>BOSS</color>", 75700,   75700,  0.0f,  0.0f,  0.0f, 67.4f,   22.5f,  9.0f,  0, 0, 0, 1.2f, 11),

            new Rock("하급 원석 <color=#8B9BB4>I</color>",    76400,   76400,  0.0f,   0.0f,   0.0f,  64.55f, 24.0f,  10.0f,    0.25f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>II</color>",   77100,   77100,  0.0f,   0.0f,   0.0f,  64.55f, 24.0f,  10.0f,    0.25f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>III</color>",  77800,   77800,  0.0f,   0.0f,   0.0f,  62.8f,  25.0f,  10.5f,    0.5f,  0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>IV</color>",   78500,   78500,  0.0f,   0.0f,   0.0f,  62.8f,  25.0f,  10.5f,    0.5f,  0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>V</color>",    79200,   79200,  0.0f,   0.0f,   0.0f,  61.05f, 26.0f,  11.0f,    0.75f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>VI</color>",   79900,   79900,  0.0f,   0.0f,   0.0f,  61.05f, 26.0f,  11.0f,    0.75f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>VII</color>",  80600,   80600,  0.0f,   0.0f,   0.0f,  59.3f,  27.0f,  11.5f,    1.0f,  0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>VIII</color>", 81300,   81300,  0.0f,   0.0f,   0.0f,  59.3f,  27.0f,  11.5f,    1.0f,  0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>IX</color>",   82000,   82000,  0.0f,   0.0f,   0.0f,  57.55f, 28.0f,  12.0f,    1.25f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>X</color>",    82700,   82700,  0.0f,   0.0f,   0.0f,  57.55f, 28.0f,  12.0f,    1.25f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>XI</color>",   83400,   83400,  0.0f,   0.0f,   0.0f,  55.8f,  29.0f,  12.5f,    1.5f,  0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>XII</color>",  84100,   84100,  0.0f,   0.0f,   0.0f,  55.8f,  29.0f,  12.5f,    1.5f,  0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>XIII</color>", 84800,   84800,  0.0f,   0.0f,   0.0f,  54.05f, 30.0f,  13.0f,    1.75f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>XIV</color>",  85500,   85500,  0.0f,   0.0f,   0.0f,  54.05f, 30.0f,  13.0f,    1.75f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>XV</color>",   86200,   86200,  0.0f,   0.0f,   0.0f,  52.3f,  31.0f,  13.5f,    2.0f,  0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>XVI</color>",  86900,   86900,  0.0f,   0.0f,   0.0f,  52.3f,  31.0f,  13.5f,    2.0f,  0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>XVII</color>", 87600,   87600,  0.0f,   0.0f,   0.0f,  50.55f, 32.0f,  14.0f,    2.25f, 0, 0, 1.3f, 12),
            new Rock("하급 원석 <color=#8B9BB4>BOSS</color>", 88300,   88300,  0.0f,   0.0f,   0.0f,  50.55f, 32.0f,  14.0f,    2.25f, 0, 0, 1.3f, 12),

            new Rock("중급 원석 <color=#8B9BB4>I</color>",    89000,   89000,  0.0f,  0.0f,  0.0f,  48.7f,  33.0f,   14.5f,  2.5f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>II</color>",   89700,   89700,  0.0f,  0.0f,  0.0f,  48.7f,  33.0f,   14.5f,  2.5f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>III</color>",  90400,   90400,  0.0f,  0.0f,  0.0f,  46.95f, 34.0f,   15.0f,  2.75f, 0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>IV</color>",   91100,   91100,  0.0f,  0.0f,  0.0f,  46.95f, 34.0f,   15.0f,  2.75f, 0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>V</color>",    91800,   91800,  0.0f,  0.0f,  0.0f,  45.2f,  35.0f,   15.5f,  3.0f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>VI</color>",   92500,   92500,  0.0f,  0.0f,  0.0f,  45.2f,  35.0f,   15.5f,  3.0f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>VII</color>",  93200,   93200,  0.0f,  0.0f,  0.0f,  43.45f, 36.0f,   16.0f,  3.25f, 0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>VIII</color>", 93900,   93900,  0.0f,  0.0f,  0.0f,  43.45f, 36.0f,   16.0f,  3.25f, 0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>IX</color>",   94600,   94600,  0.0f,  0.0f,  0.0f,  41.7f,  37.0f,   16.5f,  3.5f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>X</color>",    95300,   95300,  0.0f,  0.0f,  0.0f,  41.7f,  37.0f,   16.5f,  3.5f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>XI</color>",   96000,   96000,  0.0f,  0.0f,  0.0f,  39.95f, 38.0f,   17.0f,  3.75f, 0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>XII</color>",  96700,   96700,  0.0f,  0.0f,  0.0f,  39.95f, 38.0f,   17.0f,  3.75f, 0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>XIII</color>", 97400,   97400,  0.0f,  0.0f,  0.0f,  38.2f,  39.0f,   17.5f,  4.0f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>XIV</color>",  98100,   98100,  0.0f,  0.0f,  0.0f,  38.2f,  39.0f,   17.5f,  4.0f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>XV</color>",   98800,   98800,  0.0f,  0.0f,  0.0f,  36.45f, 40.0f,   18.0f,  4.25f, 0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>XVI</color>",  99500,   99500,  0.0f,  0.0f,  0.0f,  36.45f, 40.0f,   18.0f,  4.25f, 0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>XVII</color>", 100200,  100200, 0.0f,  0.0f,  0.0f,  34.7f,  41.0f,   18.5f,  4.5f,  0, 0, 1.4f, 13),
            new Rock("중급 원석 <color=#8B9BB4>BOSS</color>", 100900,  100900, 0.0f,  0.0f,  0.0f,  34.7f,  41.0f,   18.5f,  4.5f,  0, 0, 1.4f, 13),

            new Rock("상급 원석 <color=#8B9BB4>I</color>",    101600,   101600,  0.0f,  0.0f,  0.0f,   32.95f,  42.0f,   19.0f,  4.75f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>II</color>",   102300,   102300,  0.0f,  0.0f,  0.0f,   32.95f,  42.0f,   19.0f,  4.75f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>III</color>",  103000,   103000,  0.0f,  0.0f,  0.0f,   31.2f,   43.5f,   19.5f,  5.0f,  0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>IV</color>",   103700,   103700,  0.0f,  0.0f,  0.0f,   31.2f,   43.5f,   19.5f,  5.0f,  0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>V</color>",    104400,   104400,  0.0f,  0.0f,  0.0f,   29.45f,  44.0f,   20.0f,  5.25f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>VI</color>",   105100,   105100,  0.0f,  0.0f,  0.0f,   29.45f,  44.0f,   20.0f,  5.25f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>VII</color>",  105800,   105800,  0.0f,  0.0f,  0.0f,   27.7f,   45.5f,   20.5f,  5.5f,  0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>VIII</color>", 106500,   106500,  0.0f,  0.0f,  0.0f,   27.7f,   45.5f,   20.5f,  5.5f,  0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>IX</color>",   107200,   107200,  0.0f,  0.0f,  0.0f,   25.95f,  46.0f,   21.0f,  5.75f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>X</color>",    107900,   107900,  0.0f,  0.0f,  0.0f,   25.95f,  46.0f,   21.0f,  5.75f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>XI</color>",   108600,   108600,  0.0f,  0.0f,  0.0f,   24.2f,   47.5f,   21.5f,  6.00f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>XII</color>",  109300,   109300,  0.0f,  0.0f,  0.0f,   24.2f,   47.5f,   21.5f,  6.00f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>XIII</color>", 110000,   110000,  0.0f,  0.0f,  0.0f,   22.45f,  48.0f,   22.0f,  6.25f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>XIV</color>",  110700,   110700,  0.0f,  0.0f,  0.0f,   22.45f,  48.0f,   22.0f,  6.25f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>XV</color>",   111400,   111400,  0.0f,  0.0f,  0.0f,   20.7f,   49.5f,   22.5f,  6.5f,  0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>XVI</color>",  112100,   112100,  0.0f,  0.0f,  0.0f,   20.7f,   49.5f,   22.5f,  6.5f,  0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>XVII</color>", 112800,   112800,  0.0f,  0.0f,  0.0f,   18.95f,  50.0f,   23.0f,  6.75f, 0, 0, 1.5f, 14),
            new Rock("상급 원석 <color=#8B9BB4>BOSS</color>", 113500,   113500,  0.0f,  0.0f,  0.0f,   18.95f,  50.0f,   23.0f,  6.75f, 0, 0, 1.5f, 14),

            new Rock("최상급 원석 <color=#8B9BB4>I</color>",    114200,   114200,  0.0f,  0.0f,  0.0f, 16.9f,    51.0f,  23.5f,  7.00f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>II</color>",   114900,   114900,  0.0f,  0.0f,  0.0f, 15.9f,    52.0f,  23.5f,  7.00f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>III</color>",  115600,   115600,  0.0f,  0.0f,  0.0f, 14.15f,   53.0f,  24.0f,  7.25f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>IV</color>",   116300,   116300,  0.0f,  0.0f,  0.0f, 13.15f,   54.0f,  24.0f,  7.25f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>V</color>",    117000,   117000,  0.0f,  0.0f,  0.0f, 11.4f,    55.0f,  24.5f,  7.5f,  0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>VI</color>",   117700,   117700,  0.0f,  0.0f,  0.0f, 10.4f,    56.0f,  24.5f,  7.5f,  0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>VII</color>",  118400,   118400,  0.0f,  0.0f,  0.0f, 8.65f,    57.0f,  25.0f,  7.75f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>VIII</color>", 119100,   119100,  0.0f,  0.0f,  0.0f, 7.65f,    58.0f,  25.0f,  7.75f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>IX</color>",   119800,   119800,  0.0f,  0.0f,  0.0f, 5.9f,     59.0f,  25.5f,  8.00f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>X</color>",    120500,   120500,  0.0f,  0.0f,  0.0f, 4.9f,     60.0f,  25.5f,  8.00f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>XI</color>",   121200,   121200,  0.0f,  0.0f,  0.0f, 3.15f,    61.0f,  26.0f,  8.25f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>XII</color>",  121900,   121900,  0.0f,  0.0f,  0.0f, 2.15f,    62.0f,  26.0f,  8.25f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>XIII</color>", 122600,   122600,  0.0f,  0.0f,  0.0f, 0.4f,     63.0f,  26.5f,  8.5f,  0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>XIV</color>",  123300,   123300,  0.0f,  0.0f,  0.0f, 0.0f,     63.4f,  26.5f,  8.5f,  0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>XV</color>",   124000,   124000,  0.0f,  0.0f,  0.0f, 0.0f,     62.65f, 27.0f,  8.75f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>XVI</color>",  124700,   124700,  0.0f,  0.0f,  0.0f, 0.0f,     62.65f, 27.0f,  8.75f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>XVII</color>", 125400,   125400,  0.0f,  0.0f,  0.0f, 0.0f,     61.9f,  27.5f,  9.00f, 0, 0, 1.6f, 15),
            new Rock("최상급 원석 <color=#8B9BB4>BOSS</color>", 126100,   126100,  0.0f,  0.0f,  0.0f, 0.0f,     61.9f,  27.5f,  9.00f, 0, 0, 1.6f, 15),

            new Rock("하급 영원석 <color=#878E99>I</color>",    126900,   126900,  0.0f,   0.0f,   0.0f,  0.0f,  60.3f,  28.0f,    10.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>II</color>",   127700,   127700,  0.0f,   0.0f,   0.0f,  0.0f,  60.3f,  28.0f,    10.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>III</color>",  128500,   128500,  0.0f,   0.0f,   0.0f,  0.0f,  58.8f,  29.0f,    10.5f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>IV</color>",   129300,   129300,  0.0f,   0.0f,   0.0f,  0.0f,  58.8f,  29.0f,    10.5f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>V</color>",    130100,   130100,  0.0f,   0.0f,   0.0f,  0.0f,  57.3f,  30.0f,    11.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>VI</color>",   130900,   130900,  0.0f,   0.0f,   0.0f,  0.0f,  57.3f,  30.0f,    11.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>VII</color>",  131700,   131700,  0.0f,   0.0f,   0.0f,  0.0f,  55.8f,  31.0f,    11.5f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>VIII</color>", 132500,   132500,  0.0f,   0.0f,   0.0f,  0.0f,  55.8f,  31.0f,    11.5f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>IX</color>",   133300,   133300,  0.0f,   0.0f,   0.0f,  0.0f,  54.3f,  32.0f,    12.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>X</color>",    134100,   134100,  0.0f,   0.0f,   0.0f,  0.0f,  54.3f,  32.0f,    12.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>XI</color>",   134900,   134900,  0.0f,   0.0f,   0.0f,  0.0f,  52.8f,  33.0f,    12.5f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>XII</color>",  135700,   135700,  0.0f,   0.0f,   0.0f,  0.0f,  52.8f,  33.0f,    12.5f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>XIII</color>", 135500,   135500,  0.0f,   0.0f,   0.0f,  0.0f,  51.3f,  34.0f,    13.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>XIV</color>",  135300,   135300,  0.0f,   0.0f,   0.0f,  0.0f,  51.3f,  34.0f,    13.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>XV</color>",   135100,   135100,  0.0f,   0.0f,   0.0f,  0.0f,  49.8f,  35.0f,    13.5f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>XVI</color>",  135900,   135900,  0.0f,   0.0f,   0.0f,  0.0f,  49.8f,  35.0f,    13.5f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>XVII</color>", 136700,   136700,  0.0f,   0.0f,   0.0f,  0.0f,  48.3f,  36.0f,    14.0f, 0, 0, 1.7f, 16),
            new Rock("하급 영원석 <color=#878E99>BOSS</color>", 137500,   137500,  0.0f,   0.0f,   0.0f,  0.0f,  48.3f,  36.0f,    14.0f, 0, 0, 1.7f, 16),

            new Rock("중급 영원석 <color=#878E99>I</color>",    138300,   138300,  0.0f,  0.0f,  0.0f,  0.0f,  46.7f,   37.0f,  14.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>II</color>",   139100,   139100,  0.0f,  0.0f,  0.0f,  0.0f,  46.7f,   37.0f,  14.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>III</color>",  139900,   139900,  0.0f,  0.0f,  0.0f,  0.0f,  45.2f,   38.0f,  15.0f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>IV</color>",   140700,   140700,  0.0f,  0.0f,  0.0f,  0.0f,  45.2f,   38.0f,  15.0f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>V</color>",    141500,   141500,  0.0f,  0.0f,  0.0f,  0.0f,  43.7f,   39.0f,  15.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>VI</color>",   142300,   142300,  0.0f,  0.0f,  0.0f,  0.0f,  43.7f,   39.0f,  15.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>VII</color>",  143100,   143100,  0.0f,  0.0f,  0.0f,  0.0f,  42.2f,   40.0f,  16.0f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>VIII</color>", 143900,   143900,  0.0f,  0.0f,  0.0f,  0.0f,  42.2f,   40.0f,  16.0f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>IX</color>",   144700,   144700,  0.0f,  0.0f,  0.0f,  0.0f,  40.7f,   41.0f,  16.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>X</color>",    145500,   145500,  0.0f,  0.0f,  0.0f,  0.0f,  40.7f,   41.0f,  16.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>XI</color>",   146300,   146300,  0.0f,  0.0f,  0.0f,  0.0f,  39.2f,   42.0f,  17.0f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>XII</color>",  147100,   147100,  0.0f,  0.0f,  0.0f,  0.0f,  39.2f,   42.0f,  17.0f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>XIII</color>", 147900,   147900,  0.0f,  0.0f,  0.0f,  0.0f,  37.7f,   43.0f,  17.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>XIV</color>",  148700,   148700,  0.0f,  0.0f,  0.0f,  0.0f,  37.7f,   43.0f,  17.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>XV</color>",   149500,   149500,  0.0f,  0.0f,  0.0f,  0.0f,  36.2f,   44.0f,  18.0f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>XVI</color>",  150300,   150300,  0.0f,  0.0f,  0.0f,  0.0f,  36.2f,   44.0f,  18.0f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>XVII</color>", 151100,   151100,  0.0f,  0.0f,  0.0f,  0.0f,  34.7f,   45.0f,  18.5f, 0, 0, 1.8f, 17),
            new Rock("중급 영원석 <color=#878E99>BOSS</color>", 151900,   151900,  0.0f,  0.0f,  0.0f,  0.0f,  34.7f,   45.0f,  18.5f, 0, 0, 1.8f, 17),

            new Rock("상급 영원석 <color=#878E99>I</color>",    152700,   152700,  0.0f,  0.0f,  0.0f,   0.0f,  33.1f,   46.0f,  19.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>II</color>",   153500,   153500,  0.0f,  0.0f,  0.0f,   0.0f,  33.1f,   46.0f,  19.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>III</color>",  154300,   154300,  0.0f,  0.0f,  0.0f,   0.0f,  31.6f,   47.0f,  19.5f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>IV</color>",   155100,   155100,  0.0f,  0.0f,  0.0f,   0.0f,  31.6f,   47.0f,  19.5f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>V</color>",    155900,   155900,  0.0f,  0.0f,  0.0f,   0.0f,  30.1f,   48.0f,  20.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>VI</color>",   156700,   156700,  0.0f,  0.0f,  0.0f,   0.0f,  30.1f,   48.0f,  20.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>VII</color>",  157500,   157500,  0.0f,  0.0f,  0.0f,   0.0f,  28.6f,   49.0f,  20.5f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>VIII</color>", 158300,   158300,  0.0f,  0.0f,  0.0f,   0.0f,  28.6f,   49.0f,  20.5f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>IX</color>",   159100,   159100,  0.0f,  0.0f,  0.0f,   0.0f,  27.1f,   50.0f,  21.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>X</color>",    159900,   159900,  0.0f,  0.0f,  0.0f,   0.0f,  27.1f,   50.0f,  21.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>XI</color>",   160700,   160700,  0.0f,  0.0f,  0.0f,   0.0f,  25.6f,   51.0f,  21.5f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>XII</color>",  161500,   161500,  0.0f,  0.0f,  0.0f,   0.0f,  25.6f,   51.0f,  21.5f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>XIII</color>", 162300,   162300,  0.0f,  0.0f,  0.0f,   0.0f,  24.1f,   52.0f,  22.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>XIV</color>",  163100,   163100,  0.0f,  0.0f,  0.0f,   0.0f,  24.1f,   52.0f,  22.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>XV</color>",   163900,   163900,  0.0f,  0.0f,  0.0f,   0.0f,  22.6f,   53.0f,  22.5f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>XVI</color>",  164700,   164700,  0.0f,  0.0f,  0.0f,   0.0f,  22.6f,   53.0f,  22.5f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>XVII</color>", 165500,   165500,  0.0f,  0.0f,  0.0f,   0.0f,  21.1f,   54.0f,  23.0f, 0, 0, 1.9f, 18),
            new Rock("상급 영원석 <color=#878E99>BOSS</color>", 166300,   166300,  0.0f,  0.0f,  0.0f,   0.0f,  21.1f,   54.0f,  23.0f, 0, 0, 1.9f, 18),

            new Rock("최상급 영원석 <color=#878E99>I</color>",    167100,   167100,  0.0f,  0.0f,  0.0f, 0.0f,    19.5f,  55.0f,  23.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>II</color>",   167900,   167900,  0.0f,  0.0f,  0.0f, 0.0f,    19.5f,  55.0f,  23.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>III</color>",  168700,   168700,  0.0f,  0.0f,  0.0f, 0.0f,    18.0f,  56.0f,  24.0f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>IV</color>",   169500,   169500,  0.0f,  0.0f,  0.0f, 0.0f,    18.0f,  56.0f,  24.0f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>V</color>",    170300,   170300,  0.0f,  0.0f,  0.0f, 0.0f,    16.5f,  57.0f,  24.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>VI</color>",   171100,   171100,  0.0f,  0.0f,  0.0f, 0.0f,    16.5f,  57.0f,  24.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>VII</color>",  171900,   171900,  0.0f,  0.0f,  0.0f, 0.0f,    15.0f,  58.0f,  25.0f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>VIII</color>", 172700,   172700,  0.0f,  0.0f,  0.0f, 0.0f,    15.0f,  58.0f,  25.0f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>IX</color>",   173500,   173500,  0.0f,  0.0f,  0.0f, 0.0f,    13.5f,  59.0f,  25.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>X</color>",    174300,   174300,  0.0f,  0.0f,  0.0f, 0.0f,    13.5f,  59.0f,  25.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>XI</color>",   175100,   175100,  0.0f,  0.0f,  0.0f, 0.0f,    12.0f,  60.0f,  26.0f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>XII</color>",  175900,   175900,  0.0f,  0.0f,  0.0f, 0.0f,    12.0f,  60.0f,  26.0f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>XIII</color>", 176700,   176700,  0.0f,  0.0f,  0.0f, 0.0f,    10.5f,  61.0f,  26.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>XIV</color>",  177500,   177500,  0.0f,  0.0f,  0.0f, 0.0f,    10.5f,  61.0f,  26.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>XV</color>",   178300,   178300,  0.0f,  0.0f,  0.0f, 0.0f,    9.0f,   62.0f,  27.0f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>XVI</color>",  179100,   179100,  0.0f,  0.0f,  0.0f, 0.0f,    9.0f,   62.0f,  27.0f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>XVII</color>", 179900,   179900,  0.0f,  0.0f,  0.0f, 0.0f,    7.5f,   63.0f,  27.5f, 0, 0, 2.0f, 19),
            new Rock("최상급 영원석 <color=#878E99>BOSS</color>", 180700,   180700,  0.0f,  0.0f,  0.0f, 0.0f,    7.5f,   63.0f,  27.5f, 0, 0, 2.0f, 19),

            new Rock("하급 영혼석 <color=#2CE8F5>I</color>",    181600,   181600,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  67.65f,   30.0f, 0.25f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>II</color>",   182500,   182500,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  67.65f,   30.0f, 0.25f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>III</color>",  183400,   183400,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  66.4f,    31.0f, 0.5f,  0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>IV</color>",   184300,   184300,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  66.4f,    31.0f, 0.5f,  0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>V</color>",    185200,   185200,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  65.15f,   32.0f, 0.75f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>VI</color>",   186100,   186100,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  65.15f,   32.0f, 0.75f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>VII</color>",  187000,   187000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  63.9f,    33.0f, 1.00f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>VIII</color>", 187900,   187900,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  63.9f,    33.0f, 1.00f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>IX</color>",   188800,   188800,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  62.65f,   34.0f, 1.25f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>X</color>",    189700,   189700,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  62.65f,   34.0f, 1.25f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>XI</color>",   190600,   190600,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  61.4f,    35.0f, 1.5f,  0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>XII</color>",  191500,   191500,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  61.4f,    35.0f, 1.5f,  0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>XIII</color>", 192400,   192400,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  60.15f,   36.0f, 1.75f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>XIV</color>",  193300,   193300,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  60.15f,   36.0f, 1.75f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>XV</color>",   194200,   194200,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  58.9f,    37.0f, 2.00f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>XVI</color>",  195100,   195100,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  58.9f,    37.0f, 2.00f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>XVII</color>", 196000,   196000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  57.65f,   38.0f, 2.25f, 0, 2.1f, 20),
            new Rock("하급 영혼석 <color=#2CE8F5>BOSS</color>", 196900,   196900,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  57.65f,   38.0f, 2.25f, 0, 2.1f, 20),

            new Rock("중급 영혼석 <color=#2CE8F5>I</color>",    197800,   197800,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   56.3f,   39.0f,  2.5f,  0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>II</color>",   198700,   198700,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   56.3f,   39.0f,  2.5f,  0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>III</color>",  199600,   199600,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   55.05f,  40.0f,  2.75f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>IV</color>",   200500,   200500,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   55.05f,  40.0f,  2.75f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>V</color>",    201400,   201400,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   53.8f,   41.0f,  3.00f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>VI</color>",   202300,   202300,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   53.8f,   41.0f,  3.00f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>VII</color>",  203200,   203200,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   52.55f,  42.0f,  3.25f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>VIII</color>", 204100,   204100,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   52.55f,  42.0f,  3.25f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>IX</color>",   205000,   205000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   51.3f,   43.0f,  3.5f,  0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>X</color>",    205900,   205900,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   51.3f,   43.0f,  3.5f,  0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>XI</color>",   206800,   206800,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   50.05f,  44.0f,  3.75f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>XII</color>",  207700,   207700,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   50.05f,  44.0f,  3.75f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>XIII</color>", 208600,   208600,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   48.8f,   45.0f,  4.00f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>XIV</color>",  209500,   209500,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   48.8f,   45.0f,  4.00f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>XV</color>",   210400,   210400,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   47.55f,  46.0f,  4.25f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>XVI</color>",  211300,   211300,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   47.55f,  46.0f,  4.25f, 0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>XVII</color>", 212200,   212200,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   46.3f,   47.0f,  4.5f,  0, 2.2f, 21),
            new Rock("중급 영혼석 <color=#2CE8F5>BOSS</color>", 213100,   213100,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   46.3f,   47.0f,  4.5f,  0, 2.2f, 21),

            new Rock("상급 영혼석 <color=#2CE8F5>I</color>",    214000,   214000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   45.05f,  48.0f, 4.75f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>II</color>",   214900,   214900,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   45.05f,  48.0f, 4.75f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>III</color>",  215800,   215800,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   43.8f,   49.0f, 5.00f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>IV</color>",   216700,   216700,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   43.8f,   49.0f, 5.00f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>V</color>",    217600,   217600,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   42.55f,  50.0f, 5.25f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>VI</color>",   218500,   218500,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   42.55f,  50.0f, 5.25f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>VII</color>",  219400,   219400,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   41.3f,   51.0f, 5.5f,  0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>VIII</color>", 220300,   220300,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   41.3f,   51.0f, 5.5f,  0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>IX</color>",   221200,   221200,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   40.05f,  52.0f, 5.75f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>X</color>",    222100,   222100,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   40.05f,  52.0f, 5.75f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>XI</color>",   223000,   223000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   38.8f,   53.0f, 6.00f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>XII</color>",  223900,   223900,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   38.8f,   53.0f, 6.00f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>XIII</color>", 224800,   224800,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   37.55f,  54.0f, 6.25f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>XIV</color>",  225700,   225700,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   37.55f,  54.0f, 6.25f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>XV</color>",   226600,   226600,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   36.3f,   55.0f, 6.5f,  0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>XVI</color>",  227500,   227500,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   36.3f,   55.0f, 6.5f,  0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>XVII</color>", 228400,   228400,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   35.05f,  56.0f, 6.75f, 0, 2.3f, 22),
            new Rock("상급 영혼석 <color=#2CE8F5>BOSS</color>", 229300,   229300,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   35.05f,  56.0f, 6.75f, 0, 2.3f, 22),

            new Rock("최상급 영혼석 <color=#2CE8F5>I</color>",    230200,   230200,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  33.6f,   57.0f, 7.00f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>II</color>",   231100,   231100,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  33.6f,   57.0f, 7.00f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>III</color>",  232000,   232000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  32.35f,  58.0f, 7.25f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>IV</color>",   232900,   232900,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  32.35f,  58.0f, 7.25f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>V</color>",    233800,   233800,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  31.1f,   59.0f, 7.5f,  0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>VI</color>",   234700,   234700,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  31.1f,   59.0f, 7.5f,  0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>VII</color>",  235600,   235600,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  29.85f,  60.0f, 7.75f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>VIII</color>", 236500,   236500,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  29.85f,  60.0f, 7.75f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>IX</color>",   237400,   237400,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  28.6f,   61.0f, 8.00f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>X</color>",    238300,   238300,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  28.6f,   61.0f, 8.00f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>XI</color>",   239200,   239200,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  27.35f,  62.0f, 8.25f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>XII</color>",  240100,   240100,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  27.35f,  62.0f, 8.25f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>XIII</color>", 241000,   241000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  26.1f,   63.0f, 8.5f,  0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>XIV</color>",  241900,   241900,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  26.1f,   63.0f, 8.5f,  0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>XV</color>",   242800,   242800,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  24.85f,  64.0f, 8.75f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>XVI</color>",  243700,   243700,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  24.85f,  64.0f, 8.75f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>XVII</color>", 244600,   244600,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  23.6f,   65.0f, 9.00f, 0, 2.4f, 23),
            new Rock("최상급 영혼석 <color=#2CE8F5>BOSS</color>", 245500,   245500,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  23.6f,   65.0f, 9.00f, 0, 2.4f, 23),

            new Rock("하급 용암석 <color=#A00044>I</color>",    247000,   247000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  26.55f,   60.7f, 10.0f, 0.25f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>II</color>",   248000,   248000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  26.55f,   60.7f, 10.0f, 0.25f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>III</color>",  249000,   249000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  25.8f,    60.7f, 10.5f, 0.5f,  2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>IV</color>",   250000,   250000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  25.8f,    60.7f, 10.5f, 0.5f,  2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>V</color>",    251000,   251000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  25.05f,   60.7f, 11.0f, 0.75f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>VI</color>",   252000,   252000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  25.05f,   60.7f, 11.0f, 0.75f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>VII</color>",  253000,   253000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  24.3f,    60.7f, 11.5f, 1.00f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>VIII</color>", 254000,   254000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  24.3f,    60.7f, 11.5f, 1.00f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>IX</color>",   255000,   255000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  23.55f,   60.7f, 12.0f, 1.25f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>X</color>",    256000,   256000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  23.55f,   60.7f, 12.0f, 1.25f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>XI</color>",   257000,   257000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  22.8f,    60.7f, 12.5f, 1.5f,  2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>XII</color>",  258000,   258000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  22.8f,    60.7f, 12.5f, 1.5f,  2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>XIII</color>", 259000,   259000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  22.05f,   60.7f, 13.0f, 1.75f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>XIV</color>",  260000,   260000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  22.05f,   60.7f, 13.0f, 1.75f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>XV</color>",   261000,   261000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  21.3f,    60.7f, 13.5f, 2.00f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>XVI</color>",  262000,   262000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  21.3f,    60.7f, 13.5f, 2.00f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>XVII</color>", 263000,   263000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  20.55f,   60.7f, 14.0f, 2.25f, 2.5f, 24),
            new Rock("하급 용암석 <color=#A00044>BOSS</color>", 264000,   264000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  20.55f,   60.7f, 14.0f, 2.25f, 2.5f, 24),

            new Rock("중급 용암석 <color=#A00044>I</color>",    265000,   265000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   19.7f,  60.7f,  14.5f, 2.5f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>II</color>",   266000,   266000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   19.7f,  60.7f,  14.5f, 2.5f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>III</color>",  267000,   267000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   18.95f, 60.7f,  15.0f, 2.75f, 2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>IV</color>",   268000,   268000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   18.95f, 60.7f,  15.0f, 2.75f, 2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>V</color>",    269000,   269000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   18.2f,  60.7f,  15.5f, 3.0f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>VI</color>",   270000,   270000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   18.2f,  60.7f,  15.5f, 3.0f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>VII</color>",  271000,   271000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   17.45f, 60.7f,  16.0f, 3.25f, 2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>VIII</color>", 272000,   272000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   17.45f, 60.7f,  16.0f, 3.25f, 2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>IX</color>",   273000,   273000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   16.7f,  60.7f,  16.5f, 3.5f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>X</color>",    274000,   274000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   16.7f,  60.7f,  16.5f, 3.5f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>XI</color>",   275000,   275000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   15.95f, 60.7f,  17.0f, 3.75f, 2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>XII</color>",  276000,   276000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   15.95f, 60.7f,  17.0f, 3.75f, 2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>XIII</color>", 277000,   277000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   15.2f,  60.7f,  17.5f, 4.0f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>XIV</color>",  278000,   278000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   15.2f,  60.7f,  17.5f, 4.0f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>XV</color>",   279000,   279000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   14.45f, 60.7f,  18.0f, 4.25f, 2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>XVI</color>",  280000,   280000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   14.45f, 60.7f,  18.0f, 4.25f, 2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>XVII</color>", 281000,   281000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   13.7f,  60.7f,  18.5f, 4.5f,  2.6f, 25),
            new Rock("중급 용암석 <color=#A00044>BOSS</color>", 282000,   282000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   13.7f,  60.7f,  18.5f, 4.5f,  2.6f, 25),

            new Rock("상급 용암석 <color=#A00044>I</color>",    283000,   283000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   12.85f,  60.7f, 19.0f, 4.75f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>II</color>",   284000,   284000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   12.85f,  60.7f, 19.0f, 4.75f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>III</color>",  285000,   285000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   12.1f,   60.7f, 19.5f, 5.0f,  2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>IV</color>",   286000,   286000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   12.1f,   60.7f, 19.5f, 5.0f,  2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>V</color>",    287000,   287000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   11.35f,  60.7f, 20.0f, 5.25f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>VI</color>",   288000,   288000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   11.35f,  60.7f, 20.0f, 5.25f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>VII</color>",  289000,   289000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   10.6f,   60.7f, 20.5f, 5.5f,  2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>VIII</color>", 290000,   290000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   10.6f,   60.7f, 20.5f, 5.5f,  2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>IX</color>",   291000,   291000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   9.85f,   60.7f, 21.0f, 5.75f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>X</color>",    292000,   292000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   9.85f,   60.7f, 21.0f, 5.75f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>XI</color>",   293000,   293000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   9.1f,    60.7f, 21.5f, 6.0f,  2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>XII</color>",  294000,   294000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   9.1f,    60.7f, 21.5f, 6.0f,  2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>XIII</color>", 295000,   295000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   8.35f,   60.7f, 22.0f, 6.25f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>XIV</color>",  296000,   296000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   8.35f,   60.7f, 22.0f, 6.25f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>XV</color>",   297000,   297000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   7.6f,    60.7f, 22.5f, 6.5f,  2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>XVI</color>",  298000,   298000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   7.6f,    60.7f, 22.5f, 6.5f,  2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>XVII</color>", 299000,   299000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   6.85f,   60.7f, 23.0f, 6.75f, 2.7f, 26),
            new Rock("상급 용암석 <color=#A00044>BOSS</color>", 300000,   300000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   6.85f,   60.7f, 23.0f, 6.75f, 2.7f, 26),

            new Rock("최상급 용암석 <color=#A00044>I</color>",    301000,   301000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.6f,   60.7f, 23.5f, 7.0f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>II</color>",   302000,   302000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.6f,   60.7f, 23.5f, 7.0f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>III</color>",  303000,   303000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  5.25f,  60.7f, 24.0f, 7.25f, 2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>IV</color>",   304000,   304000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  5.25f,  60.7f, 24.0f, 7.25f, 2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>V</color>",    305000,   305000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  4.5f,   60.7f, 24.5f, 7.5f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>VI</color>",   306000,   306000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  4.5f,   60.7f, 24.5f, 7.5f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>VII</color>",  307000,   307000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  3.0f,   60.7f, 25.0f, 7.75f, 2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>VIII</color>", 308000,   308000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  3.0f,   60.7f, 25.0f, 7.75f, 2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>IX</color>",   309000,   309000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  2.25f,  60.7f, 25.5f, 8.0f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>X</color>",    310000,   310000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  2.25f,  60.7f, 25.5f, 8.0f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>XI</color>",   311000,   311000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  1.5f,   60.7f, 26.0f, 8.25f, 2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>XII</color>",  312000,   312000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  1.5f,   60.7f, 26.0f, 8.25f, 2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>XIII</color>", 313000,   313000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.75f,  60.7f, 26.5f, 8.5f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>XIV</color>",  314000,   314000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.75f,  60.7f, 26.5f, 8.5f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>XV</color>",   315000,   315000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,   60.7f, 27.0f, 8.75f, 2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>XVI</color>",  316000,   316000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,   60.7f, 27.0f, 8.75f, 2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>XVII</color>", 317000,   317000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,   60.7f, 27.5f, 9.0f,  2.8f, 27),
            new Rock("최상급 용암석 <color=#A00044>BOSS</color>", 318000,   318000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,   60.7f, 27.5f, 9.0f,  2.8f, 27),

            new Rock("하급 지옥석 <color=#FF0044>I</color>",    320000,   320000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   47.1f, 40.0f, 10.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>II</color>",   322000,   322000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   47.1f, 40.0f, 10.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>III</color>",  324000,   324000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   45.6f, 41.0f, 10.5f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>IV</color>",   326000,   326000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   45.6f, 41.0f, 10.5f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>V</color>",    328000,   328000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   44.1f, 42.0f, 11.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>VI</color>",   330000,   330000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   44.1f, 42.0f, 11.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>VII</color>",  332000,   332000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   42.6f, 43.0f, 11.5f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>VIII</color>", 334000,   334000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   42.6f, 43.0f, 11.5f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>IX</color>",   336000,   336000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   41.1f, 44.0f, 12.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>X</color>",    338000,   338000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   41.1f, 44.0f, 12.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>XI</color>",   340000,   340000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   39.6f, 45.0f, 12.5f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>XII</color>",  342000,   342000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   39.6f, 45.0f, 12.5f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>XIII</color>", 344000,   344000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   38.1f, 46.0f, 13.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>XIV</color>",  346000,   346000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   38.1f, 46.0f, 13.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>XV</color>",   348000,   348000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   36.6f, 47.0f, 13.5f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>XVI</color>",  350000,   350000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   36.6f, 47.0f, 13.5f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>XVII</color>", 352000,   352000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   35.1f, 48.0f, 14.0f, 2.9f, 28),
            new Rock("하급 지옥석 <color=#FF0044>BOSS</color>", 354000,   354000,  0.0f,   0.0f,   0.0f,  0.0f,  0.0f,  0.0f,   35.1f, 48.0f, 14.0f, 2.9f, 28),

            new Rock("중급 지옥석 <color=#FF0044>I</color>",    356000,   356000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 33.5f,  49.0f, 14.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>II</color>",   358000,   358000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 33.5f,  49.0f, 14.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>III</color>",  360000,   360000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 32.0f,  50.0f, 15.0f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>IV</color>",   362000,   362000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 32.0f,  50.0f, 15.0f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>V</color>",    364000,   364000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 30.5f,  51.0f, 15.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>VI</color>",   366000,   366000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 30.5f,  51.0f, 15.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>VII</color>",  368000,   368000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 29.0f,  52.0f, 16.0f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>VIII</color>", 370000,   370000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 29.0f,  52.0f, 16.0f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>IX</color>",   372000,   372000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 27.5f,  53.0f, 16.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>X</color>",    374000,   374000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 27.5f,  53.0f, 16.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>XI</color>",   376000,   376000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 26.0f,  54.0f, 17.0f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>XII</color>",  378000,   378000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 26.0f,  54.0f, 17.0f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>XIII</color>", 380000,   380000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 24.5f,  55.0f, 17.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>XIV</color>",  382000,   382000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 24.5f,  55.0f, 17.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>XV</color>",   384000,   384000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 23.0f,  56.0f, 18.0f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>XVI</color>",  386000,   386000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 23.0f,  56.0f, 18.0f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>XVII</color>", 388000,   388000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 21.5f,  57.0f, 18.5f, 3.0f, 29),
            new Rock("중급 지옥석 <color=#FF0044>BOSS</color>", 390000,   390000,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,   0.0f, 21.5f,  57.0f, 18.5f, 3.0f, 29),

            new Rock("상급 지옥석 <color=#FF0044>I</color>",    392000,   392000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  19.9f, 58.0f, 19.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>II</color>",   394000,   394000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  19.9f, 58.0f, 19.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>III</color>",  396000,   396000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  18.4f, 59.0f, 19.5f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>IV</color>",   398000,   398000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  18.4f, 59.0f, 19.5f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>V</color>",    400000,   400000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  16.9f, 60.0f, 20.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>VI</color>",   402000,   402000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  16.9f, 60.0f, 20.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>VII</color>",  404000,   404000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  15.4f, 61.0f, 20.5f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>VIII</color>", 406000,   406000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  15.4f, 61.0f, 20.5f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>IX</color>",   408000,   408000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  13.9f, 62.0f, 21.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>X</color>",    410000,   410000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  13.9f, 62.0f, 21.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>XI</color>",   412000,   412000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  12.4f, 63.0f, 21.5f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>XII</color>",  414000,   414000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  12.4f, 63.0f, 21.5f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>XIII</color>", 416000,   416000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  10.9f, 64.0f, 22.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>XIV</color>",  418000,   418000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  10.9f, 64.0f, 22.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>XV</color>",   420000,   420000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  9.4f,  65.0f, 22.5f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>XVI</color>",  422000,   422000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  9.4f,  65.0f, 22.5f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>XVII</color>", 424000,   424000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  7.9f,  66.0f, 23.0f, 3.1f, 30),
            new Rock("상급 지옥석 <color=#FF0044>BOSS</color>", 426000,   426000,  0.0f,  0.0f,  0.0f,   0.0f,  0.0f,   0.0f,  7.9f,  66.0f, 23.0f, 3.1f, 30),

            new Rock("최상급 지옥석 <color=#FF0044>I</color>",    428000,   428000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  6.3f, 67.0f, 23.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>II</color>",   430000,   430000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  6.3f, 67.0f, 23.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>III</color>",  433000,   433000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  4.8f, 68.0f, 24.0f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>IV</color>",   436000,   436000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  4.8f, 68.0f, 24.0f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>V</color>",    439000,   439000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  3.3f, 69.0f, 24.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>VI</color>",   442000,   442000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  3.3f, 69.0f, 24.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>VII</color>",  445000,   445000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  2.5f, 69.3f, 25.0f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>VIII</color>", 448000,   448000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  2.5f, 69.3f, 25.0f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>IX</color>",   451000,   451000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  2.0f, 69.3f, 25.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>X</color>",    455000,   455000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  2.0f, 69.3f, 25.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>XI</color>",   459000,   459000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  1.5f, 69.3f, 26.0f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>XII</color>",  463000,   463000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  1.5f, 69.3f, 26.0f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>XIII</color>", 467000,   467000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  1.0f, 69.3f, 26.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>XIV</color>",  471000,   471000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  1.0f, 69.3f, 26.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>XV</color>",   480000,   480000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  0.5f, 69.3f, 27.0f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>XVI</color>",  485000,   485000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  0.5f, 69.3f, 27.0f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>XVII</color>", 490000,   490000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  0.0f, 69.3f, 27.5f, 3.2f, 31),
            new Rock("최상급 지옥석 <color=#FF0044>BOSS</color>", 500000,   500000,  0.0f,  0.0f,  0.0f, 0.0f,    0.0f,  0.0f,  0.0f, 69.3f, 27.5f, 3.2f, 31),
        };

        // 다른 몬스터들을 몬스터 리스트에 추가
        rocks.AddRange(FirstRocks);
    }

    private void Awake()
    {
        InitializeRocks();
    }

    // Start is called before the first frame update
    void Start()
    {

        FixDifficultyInStage = 0;
        MoveDifficultyInStage = 0;
        LastBtnIndex = 0;
        MoveDifficulty = 0;
        FixDifficulty = 0;

        Rock_defeatedIndex = 0;

        GameManager.RockstageClearDict["하급 돌덩이"] = new bool[18];
        GameManager.RockstageClearDict["중급 돌덩이"] = new bool[18];
        GameManager.RockstageClearDict["상급 돌덩이"] = new bool[18];
        GameManager.RockstageClearDict["최상급 돌덩이"] = new bool[18];

        GameManager.RockstageClearDict["하급 광물"] = new bool[18];
        GameManager.RockstageClearDict["중급 광물"] = new bool[18];
        GameManager.RockstageClearDict["상급 광물"] = new bool[18];
        GameManager.RockstageClearDict["최상급 광물"] = new bool[18];

        GameManager.RockstageClearDict["하급 광석"] = new bool[18];
        GameManager.RockstageClearDict["중급 광석"] = new bool[18];
        GameManager.RockstageClearDict["상급 광석"] = new bool[18];
        GameManager.RockstageClearDict["최상급 광석"] = new bool[18];

        GameManager.RockstageClearDict["하급 원석"] = new bool[18];
        GameManager.RockstageClearDict["중급 원석"] = new bool[18];
        GameManager.RockstageClearDict["상급 원석"] = new bool[18];
        GameManager.RockstageClearDict["최상급 원석"] = new bool[18];

        GameManager.RockstageClearDict["하급 영원석"] = new bool[18];
        GameManager.RockstageClearDict["중급 영원석"] = new bool[18];
        GameManager.RockstageClearDict["상급 영원석"] = new bool[18];
        GameManager.RockstageClearDict["최상급 영원석"] = new bool[18];

        GameManager.RockstageClearDict["하급 영혼석"] = new bool[18];
        GameManager.RockstageClearDict["중급 영혼석"] = new bool[18];
        GameManager.RockstageClearDict["상급 영혼석"] = new bool[18];
        GameManager.RockstageClearDict["최상급 영혼석"] = new bool[18];

        GameManager.RockstageClearDict["하급 용암석"] = new bool[18];
        GameManager.RockstageClearDict["중급 용암석"] = new bool[18];
        GameManager.RockstageClearDict["상급 용암석"] = new bool[18];
        GameManager.RockstageClearDict["최상급 용암석"] = new bool[18];

        GameManager.RockstageClearDict["하급 지옥석"] = new bool[18];
        GameManager.RockstageClearDict["중급 지옥석"] = new bool[18];
        GameManager.RockstageClearDict["상급 지옥석"] = new bool[18];
        GameManager.RockstageClearDict["최상급 지옥석"] = new bool[18];

        Debug.Log("광산 초기화 했음");
        GameManager.RockstageClearDict["하급 돌덩이"][0] = true;

        Debug.Log("확인 : " + GameManager.RockstageClearDict["하급 돌덩이"][1]);

        RockLevelBoxOpenBtn.onClick.AddListener(() => RockLevelBoxOpen(MoveDifficultyInStage));
        RockDifficulty_NextBtn.onClick.AddListener(RockNext);
        RockDifficulty_ReturnBtn.onClick.AddListener(RockReturn);
        RockLevelOutBtn.onClick.AddListener(RockLevelOut);
        RockSelOutBtn.onClick.AddListener(RockLevelOut);
        RockSummonsBtn.onClick.AddListener(RockSummons);

        MineNextBtn.onClick.AddListener(MineNext);

        for (int i = 0; i < GameConstants.RockNum; i++)
        {
            RockMoveStage[i] = new bool[18];
            RockFixStage[i] = new bool[18];
        }

        for (int i = 0; i < 4; i++)
        {
            int index = i;

            RockLevelSelOpenBtn[index].onClick.AddListener(() => RockDifficultyOpen(index));
        }

        for(int i = 0; i < 18; i++)
        {
            int index = i;

            RockSelBtn[index].onClick.AddListener(() => ChangeRockInfo(index));
        }
        RockSummons();
    }

    private string[] RockDifficultyName = new string[32]
    {
        "Cave - <color=#63C74D>하급</color> 돌덩이",
        "Cave - <color=#63C74D>중급</color> 돌덩이",
        "Cave - <color=#63C74D>상급</color> 돌덩이",
        "Cave - <color=#63C74D>최상급</color> 돌덩이",

        "Cave - <color=#B86F50>하급</color> 광물",
        "Cave - <color=#B86F50>중급</color> 광물",
        "Cave - <color=#B86F50>상급</color> 광물",
        "Cave - <color=#B86F50>최상급</color> 광물",

        "Cave - <color=#FEA222>하급</color> 광석",
        "Cave - <color=#FEA222>중급</color> 광석",
        "Cave - <color=#FEA222>상급</color> 광석",
        "Cave - <color=#FEA222>최상급</color> 광석",

        "Cave - <color=#8B9BB4>하급</color> 원석",
        "Cave - <color=#8B9BB4>중급</color> 원석",
        "Cave - <color=#8B9BB4>상급</color> 원석",
        "Cave - <color=#8B9BB4>최상급</color> 원석",

        "Cave - <color=#878E99>하급</color> 영원석",
        "Cave - <color=#878E99>중급</color> 영원석",
        "Cave - <color=#878E99>상급</color> 영원석",
        "Cave - <color=#878E99>최상급</color> 영원석",

        "Cave - <color=#2CE8F5>하급</color> 영혼석",
        "Cave - <color=#2CE8F5>중급</color> 영혼석",
        "Cave - <color=#2CE8F5>상급</color> 영혼석",
        "Cave - <color=#2CE8F5>최상급</color> 영혼석",

        "Cave - <color=#A00044>하급</color> 용암석",
        "Cave - <color=#A00044>중급</color> 용암석",
        "Cave - <color=#A00044>상급</color> 용암석",
        "Cave - <color=#A00044>최상급</color> 용암석",

        "Cave - <color=#FF0044>하급</color> 지옥석",
        "Cave - <color=#FF0044>중급</color> 지옥석",
        "Cave - <color=#FF0044>상급</color> 지옥석",
        "Cave - <color=#FF0044>최상급</color> 지옥석",
    };

    private string[] RockTitleString = new string[32]
        {
        "돌덩이 - <color=#63C74D>Easy</color>",
        "돌덩이 - <color=#63C74D>Normal</color>",
        "돌덩이 - <color=#63C74D>Hard</color>",
        "돌덩이 - <color=#63C74D>Extreme</color>",

        "광물 - <color=#B86F50>Easy</color>",
        "광물 - <color=#B86F50>Normal</color>",
        "광물 - <color=#B86F50>Hard</color>",
        "광물 - <color=#B86F50>Extreme</color>",

        "광석 - <color=#FEA222>Easy</color>",
        "광석 - <color=#FEA222>Normal</color>",
        "광석 - <color=#FEA222>Hard</color>",
        "광석 - <color=#FEA222>Extreme</color>",

        "원석 - <color=#8B9BB4>Easy</color>",
        "원석 - <color=#8B9BB4>Normal</color>",
        "원석 - <color=#8B9BB4>Hard</color>",
        "원석 - <color=#8B9BB4>Extreme</color>",

        "영원석 - <color=#878E99>Easy</color>",
        "영원석 - <color=#878E99>Normal</color>",
        "영원석 - <color=#878E99>Hard</color>",
        "영원석 - <color=#878E99>Extreme</color>",

        "영혼석 - <color=#2CE8F5>Easy</color>",
        "영혼석 - <color=#2CE8F5>Normal</color>",
        "영혼석 - <color=#2CE8F5>Hard</color>",
        "영혼석 - <color=#2CE8F5>Extreme</color",

        "용암석 - <color=#A00044>Easy</color>",
        "용암석 - <color=#A00044>Normal</color>",
        "용암석 - <color=#A00044>Hard</color>",
        "용암석 - <color=#A00044>Extreme</color>",

        "지옥석 - <color=#FF0044>Easy</color>",
        "지옥석 - <color=#FF0044>Normal</color> ",
        "지옥석 - <color=#FF0044>Hard</color>",
        "지옥석 - <color=#FF0044>Extreme</color>",
        };

    private void MineNext()
    {
        for (int i = 0; i < 18; i++)
        {
            GameManager.RockstageClearDict["하급 돌덩이"][i] = true;
            GameManager.RockstageClearDict["중급 돌덩이"][i] = true;
            GameManager.RockstageClearDict["상급 돌덩이"][i] = true;
            GameManager.RockstageClearDict["최상급 돌덩이"][i] = true;

            GameManager.RockstageClearDict["하급 광물"][i] = true;
            GameManager.RockstageClearDict["중급 광물"][i] = true;
            GameManager.RockstageClearDict["상급 광물"][i] = true;
            GameManager.RockstageClearDict["최상급 광물"][i] = true;

            GameManager.RockstageClearDict["하급 광석"][i] = true;
            GameManager.RockstageClearDict["중급 광석"][i] = true;
            GameManager.RockstageClearDict["상급 광석"][i] = true;
            GameManager.RockstageClearDict["최상급 광석"][i] = true;

            GameManager.stageClearDict["EasySlime"][i] = true;
            GameManager.stageClearDict["NormalSlime"][i] = true;
            GameManager.stageClearDict["HardSlime"][i] = true;
            GameManager.stageClearDict["ExtremeSlime"][i] = true;

            GameManager.stageClearDict["EasyWolf"][i] = true;
            GameManager.stageClearDict["NormalWolf"][i] = true;
            GameManager.stageClearDict["HardWolf"][i] = true;
            GameManager.stageClearDict["ExtremeWolf"][i] = true;

            GameManager.stageClearDict["EasyGolem"][i] = true;
            GameManager.stageClearDict["NormalGolem"][i] = true;
            GameManager.stageClearDict["HardGolem"][i] = true;
            GameManager.stageClearDict["ExtremeGolem"][i] = true;

            GameManager.stageClearDict["EasyMushRoom"][i] = true;
            GameManager.stageClearDict["NormalMushRoom"][i] = true;
            GameManager.stageClearDict["HardMushRoom"][i] = true;
            GameManager.stageClearDict["ExtremeMushRoom"][i] = true;

            GameManager.stageClearDict["EasySkull"][i] = true;
            GameManager.stageClearDict["NormalSkull"][i] = true;
            GameManager.stageClearDict["HardSkull"][i] = true;
            GameManager.stageClearDict["ExtremeSkull"][i] = true;

            GameManager.stageClearDict["EasyGoblin"][i] = true;
            GameManager.stageClearDict["NormalGoblin"][i] = true;
            GameManager.stageClearDict["HardGoblin"][i] = true;
            GameManager.stageClearDict["ExtremeGoblin"][i] = true;

            GameManager.stageClearDict["EasyFlyingEye"][i] = true;
            GameManager.stageClearDict["NormalFlyingEye"][i] = true;
            GameManager.stageClearDict["HardFlyingEye"][i] = true;
            GameManager.stageClearDict["ExtremeFlyingEye"][i] = true;

        }

        GameManager.Pickaxe_Damage += 10000;

        EnemyManager.Instance.FixDifficulty = 3;
        EnemyManager.Instance.FixDifficultyInStage = 6;
        EnemyManager.Instance.LastBtnIndex = 17;
        EnemyManager.Instance.MoveDifficultyInStage = 6;
        EnemyManager.Instance.MoveDifficulty = 3;
    }

    private void RockLevelBoxOpen(int index)
    {
        MoveDifficultyInStage = index;

        for (int i = 0; i < 4; i++)
        {
            RockImage[i].sprite = RockSprite[index * 4 + i];
            RockDifficultyNameText[i].text = RockDifficultyName[index * 4 + i];
        }

        // 클리어 확인
        int[] ClearCheck;

        ClearCheck = new int[4];
        for (int i = 0; i < 18; i++)
        {
            if (GameManager.RockstageClearDict[RockName(index, 0)][i] == true) ClearCheck[0]++;
            if (GameManager.RockstageClearDict[RockName(index, 1)][i] == true) ClearCheck[1]++;
            if (GameManager.RockstageClearDict[RockName(index, 2)][i] == true) ClearCheck[2]++;
            if (GameManager.RockstageClearDict[RockName(index, 3)][i] == true) ClearCheck[3]++;
        }

        for (int i = 0; i < 4; i++)
        {
            RockClearValue[i].text = ClearCheck[i] + " / 18";
            if (ClearCheck[i] > 0)
            {
                BrokingPanel[i].SetActive(false);
                RockLevelSelOpenBtn[i].interactable = true;
            }
            else
            {
                BrokingPanel[i].SetActive(true);
                RockLevelSelOpenBtn[i].interactable = false;
            }
            RockDifficultyFrame[i].color = ColorManager.ColorChange("광물" + MoveDifficultyInStage + "번");
        }

        if (MoveDifficultyInStage == 0) RockDifficulty_ReturnBtn.gameObject.SetActive(false);
        else RockDifficulty_ReturnBtn.gameObject.SetActive(true);

        if(MoveDifficultyInStage == 7) RockDifficulty_NextBtn.gameObject.SetActive(false);
        else RockDifficulty_NextBtn.gameObject.SetActive(true);

        

        RockLevelBox.SetActive(true);
        RockDifficultyPanel.SetActive(true);

        RockLevelOutBtn.gameObject.SetActive(true);
        RockSelOutBtn.gameObject.SetActive(false);
    }
    private void RockDifficultyCheck(int index, int movedifficultyinstage)
    {
        UpdateRockButtons(RockName(movedifficultyinstage, index), "민트색", "기본색", "투명기본색", index + movedifficultyinstage * 4);
    }

    private void RockDifficultyOpen(int index)
    {
        MoveDifficulty = index;
        ChangeButtonclick = false;

        RockDifficultyCheck(index, MoveDifficultyInStage);

        
        RockInfoImage.sprite = RockSprite[index + MoveDifficultyInStage * 4];
        
        RockInfoFrame.color = ColorManager.ColorChange("광물" + MoveDifficultyInStage + "번");

        RockSelOutBtn.gameObject.SetActive(true);
        RockLevelOutBtn.gameObject.SetActive(false);

        RockDifficultyPanel.SetActive(false);
        RockSelPanel.SetActive(true);

        UpdateInfoValue();
    }

    private void UpdateRockButtons(string rockType, string defeatedColor, string interactableColor, string nonInteractableColor, int difficulty)
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
        if (RockFixStage[FixDifficulty + FixDifficultyInStage * 4][Rock_defeatedIndex] == true && difficulty == FixDifficulty + FixDifficultyInStage * 4)
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
        if (index == LastBtnIndex) return;

        // 이전 버튼의 색상을 변경합니다.
        if (GameManager.RockstageClearDict[RockName(MoveDifficultyInStage, MoveDifficulty)][LastBtnIndex] == true) ChangeButtonColor(RockSelBtn[LastBtnIndex], "기본색");

        // 현재 선택한 버튼의 색상 변경
        ChangeButtonColor(RockSelBtn[index], "민트색");

        // 전 넘버는 false로
        RockMoveStage[MoveDifficulty + MoveDifficultyInStage * 4][LastBtnIndex] = false;

        // MoveInStage를 true 변경
        RockMoveStage[MoveDifficulty + MoveDifficultyInStage * 4][index] = true;

        // 현재 선택한 버튼을 이전에 선택한 버튼으로 설정
        LastBtnIndex = index;

        // 슬라임 정보를 사용하여 UI 업데이트
        UpdateInfoValue();
    }

    private void RockSummons()
    {
        if (GameManager.RockstageClearDict[RockName(MoveDifficultyInStage, MoveDifficulty)][LastBtnIndex] == false) return;
        if (ChangeButtonclick == false && MoveDifficulty != FixDifficulty) return; // 선택 안했을때 리턴시키기

        // 보스 몬스터 정보 가져오기
        Rock_defeatedIndex = LastBtnIndex;

        for (int i = 0; i < 18; i++)
        {
            RockFixStage[MoveDifficulty + MoveDifficultyInStage * 4][i] = false;
            RockMoveStage[MoveDifficulty + MoveDifficultyInStage * 4][i] = false;
        }

        FixDifficulty = MoveDifficulty;
        FixDifficultyInStage = MoveDifficultyInStage;

        RockTitle.text = RockTitleString[FixDifficulty + FixDifficultyInStage * 4];

        int _Defeat = GetRockIndex(FixDifficulty + FixDifficultyInStage * 4, Rock_defeatedIndex);

        Rock selectedRock = GetRockbyIndex(_Defeat);

        Image image = Rock.GetComponent<Image>();
        image.sprite = RockSprite[FixDifficulty + FixDifficultyInStage * 4];

        // 보스의 인덱스만 true로 설정합니다.
        RockMoveStage[FixDifficulty + 4 * FixDifficultyInStage][Rock_defeatedIndex] = true;
        RockFixStage[FixDifficulty + 4 * FixDifficultyInStage][Rock_defeatedIndex] = true;

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

    public void PowerSummons()
    {
        MoveDifficultyInStage = FixDifficultyInStage;

        Rock_defeatedIndex = LastBtnIndex;

        int _Defeat = GetRockIndex(FixDifficulty + FixDifficultyInStage * 4, Rock_defeatedIndex);


        Rock selectedRock = GetRockbyIndex(_Defeat);

        RockTitle.text = RockTitleString[FixDifficulty + FixDifficultyInStage * 4];


        Image image = Rock.GetComponent<Image>();
        image.sprite = RockSprite[FixDifficulty + FixDifficultyInStage * 4];

        // 보스의 인덱스만 true로 설정합니다.
        RockMoveStage[FixDifficulty + 4 * FixDifficultyInStage][Rock_defeatedIndex] = true;
        RockFixStage[FixDifficulty + 4 * FixDifficultyInStage][Rock_defeatedIndex] = true;

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

    private void RockNext()
    {
        MoveDifficultyInStage++;

        if (MoveDifficultyInStage == 7) RockDifficulty_NextBtn.gameObject.SetActive(false);
        else RockDifficulty_NextBtn.gameObject.SetActive(true);

        RockLevelBoxOpen(MoveDifficultyInStage);
    }

    private void RockReturn()
    {
        MoveDifficultyInStage--;

        if (MoveDifficultyInStage == 0) RockDifficulty_ReturnBtn.gameObject.SetActive(false);
        else RockDifficulty_ReturnBtn.gameObject.SetActive(true);

        RockLevelBoxOpen(MoveDifficultyInStage);
    }

    private void RockLevelOut()
    {
        // 만약 마지막 버튼이 결정한 버튼 Index숫자와 다를때 마지막 버튼을 결정한 버튼으로 변경
        if (LastBtnIndex != Rock_defeatedIndex)
        {
            // 나머지 요소는 모두 false로 설정합니다.
            for (int i = 0; i < 18; i++) RockMoveStage[MoveDifficulty + MoveDifficultyInStage * 4][i] = false;

            RockMoveStage[MoveDifficulty + MoveDifficultyInStage * 4][LastBtnIndex] = RockFixStage[FixDifficulty + FixDifficultyInStage * 4][Rock_defeatedIndex];
            LastBtnIndex = Rock_defeatedIndex;

            UpdateInfoValue();
        }

        // 만약 마지막 선택 난이도가 결정 난이도와 다를때 선택 난이도를 결정 난이도로 설정

        if (FixDifficulty != MoveDifficulty) MoveDifficulty = FixDifficulty;
        if (FixDifficultyInStage != MoveDifficultyInStage) MoveDifficultyInStage = FixDifficultyInStage;

        RockLevelBox.SetActive(false);
        RockSelPanel.SetActive(false);
        RockDifficultyPanel.SetActive(false);
    }

    public string RockName(int movedifficultyinstage, int index)
    {
        if (movedifficultyinstage == 0)
        {
            if (index == 0) return "하급 돌덩이";
            else if (index == 1) return "중급 돌덩이";
            else if (index == 2) return "상급 돌덩이";
            else return "최상급 돌덩이";
        }
        else if (movedifficultyinstage == 1)
        {
            if (index == 0) return "하급 광물";
            else if (index == 1) return "중급 광물";
            else if (index == 2) return "상급 광물";
            else return "최상급 광물";
        }
        else if (movedifficultyinstage == 2)
        {
            if (index == 0) return "하급 광석";
            else if (index == 1) return "중급 광석";
            else if (index == 2) return "상급 광석";
            else return "최상급 광석";
        }
        else if (movedifficultyinstage == 3)
        {
            if (index == 0) return "하급 원석";
            else if (index == 1) return "중급 원석";
            else if (index == 2) return "상급 원석";
            else return "최상급 원석";
        }
        else if (movedifficultyinstage == 4)
        {
            if (index == 0) return "하급 영원석";
            else if (index == 1) return "중급 영원석";
            else if (index == 2) return "상급 영원석";
            else return "최상급 영원석";
        }
        else if (movedifficultyinstage == 5)
        {
            if (index == 0) return "하급 영혼석";
            else if (index == 1) return "중급 영혼석";
            else if (index == 2) return "상급 영혼석";
            else return "최상급 영혼석";
        }
        else if (movedifficultyinstage == 6)
        {
            if (index == 0) return "하급 용암석";
            else if (index == 1) return "중급 용암석";
            else if (index == 2) return "상급 용암석";
            else return "최상급 용암석";
        }
        else
        {
            if (index == 0) return "하급 지옥석";
            else if (index == 1) return "중급 지옥석";
            else if (index == 2) return "상급 지옥석";
            else return "최상급 지옥석";
        }
    }

    // 돌 선택 색깔 변경 관리
    public void ChangeButtonColor(Button button, string color)
    {
        // 버튼의 이미지 컴포넌트 가져오기
        Image buttonImage = button.GetComponent<Image>();
        // 버튼의 이미지 색상 변경
        buttonImage.color = ColorManager.ColorChange(color);
    }

    private int GetRockIndex(int difficulty, int level)
    {
        return level + difficulty * 18;
    }

    private void UpdateInfoValue()
    {
        Rock rock;

        int _Defeat = GetRockIndex(MoveDifficulty + MoveDifficultyInStage * 4, LastBtnIndex);

        rock = GetRockbyIndex(_Defeat);

        double maxHealth = rock.MaxHealth;
        RockTitle.text = RockTitleString[MoveDifficulty + MoveDifficultyInStage * 4];
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
        if (index < 0 || index >= rocks.Count)
        {
            return rocks[0];
        }

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

        int _Defeat = GetRockIndex(FixDifficulty + FixDifficultyInStage * 4, Rock_defeatedIndex);

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
        Bonus += RefineryManager.RF_MineAmount;

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
