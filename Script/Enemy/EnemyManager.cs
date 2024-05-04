using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Monster
{
    public string Name { get; set; }
    public double CurHealth { get; set; }
    public double MaxHealth { get; set; }
    public float ExperienceValue { get; set; }
    public int MoneyReward { get; set; }
    public int RedStoneReward { get; set; }
    public float ScrollDrop { get; set; }
    public int Difficulty { get; set; }
    // 기타 필요한 속성들

    public Monster(string name, double curhealth, double maxhealth, float experience, int money, int redstone, float scrolldrop, int difficulty)
    {
        Name = name;
        CurHealth = curhealth;
        MaxHealth = maxhealth;
        ExperienceValue = experience;
        MoneyReward = money;
        RedStoneReward = redstone;
        ScrollDrop = scrolldrop;
        Difficulty = difficulty;
    }
}

public class EnemyManager : MonoBehaviour
{

    private static EnemyManager instance;

    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyManager>();
                if (instance == null)
                {
                    Debug.LogError("EnemyManager 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        InitializeMonsters();
    }


    private List<Monster> monsters = new();

    private void InitializeMonsters()
    {
        Monster[] slimes = new Monster[]
        {
            new Monster("초급 슬라임 <color=lime>I</color>",     4,     4,     1,    3,      5,  5f, 0),
            new Monster("초급 슬라임 <color=lime>II</color>",    10,    10,    2,    6,      5,  5f, 0),
            new Monster("초급 슬라임 <color=lime>III</color>",   20,    20,    4,    12,     10, 5f, 0),
            new Monster("초급 슬라임 <color=lime>IV</color>",    30,    30,    6,    18,     10, 5f, 0),
            new Monster("초급 슬라임 <color=lime>V</color>",     50,    50,    10,   30,     15, 5f, 0),
            new Monster("초급 슬라임 <color=lime>VI</color>",    65,    65,    14,   42,     15, 5f, 0),
            new Monster("초급 슬라임 <color=lime>VII</color>",   80,    80,    18,   54,     20, 5f, 0),
            new Monster("초급 슬라임 <color=lime>VIII</color>",  95,    95,    22,   66,     20, 5f, 0),
            new Monster("초급 슬라임 <color=lime>IX</color>",    110,   110,   26,   78,     25, 5f, 0),
            new Monster("초급 슬라임 <color=lime>X</color>",     150,   150,   40,   120,    25, 10f, 0),
            new Monster("초급 슬라임 <color=lime>XI</color>",    170,   170,   48,   144,    30, 10f, 0),
            new Monster("초급 슬라임 <color=lime>XII</color>",   190,   190,   56,   168,    30, 10f, 0),
            new Monster("초급 슬라임 <color=lime>XIII</color>",  210,   210,   64,   192,    35, 10f, 0),
            new Monster("초급 슬라임 <color=lime>XIV</color>",   230,   230,   72,   216,    35, 10f, 0),
            new Monster("초급 슬라임 <color=lime>XV</color>",    290,   290,   90,   270,    40, 10f, 0),
            new Monster("초급 슬라임 <color=lime>XVI</color>",   320,   320,   100,  300,    40, 10f, 0),
            new Monster("초급 슬라임 <color=lime>XVII</color>",  350,   350,   110,  330,    45, 10f, 0),
            new Monster("초급 슬라임 <color=lime>BOSS</color>",  500,   500,   150,  450,    50, 10f, 0),

            new Monster("중급 슬라임 <color=cyan>I</color>",     560,   560,   165,  495,    55,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>II</color>",    620,   620,   180,  540,    55,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>III</color>",   680,   680,   195,  575,    60,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>IV</color>",    740,   740,   210,  630,    60,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>V</color>",     890,   890,   230,  690,    65,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>VI</color>",    970,   970,   250,  750,    65,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>VII</color>",   1050,  1050,  270,  810,    70,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>VIII</color>",  1130,  1130,  290,  870,    70,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>IX</color>",    1210,  1210,  310,  930,    75,  15f, 1),
            new Monster("중급 슬라임 <color=cyan>X</color>",     1500,  1500,  350,  1050,   75,  20f, 1),
            new Monster("중급 슬라임 <color=cyan>XI</color>",    1600,  1600,  375,  1125,   80,  20f, 1),
            new Monster("중급 슬라임 <color=cyan>XII</color>",   1700,  1700,  400,  1200,   80,  20f, 1),
            new Monster("중급 슬라임 <color=cyan>XIII</color>",  1800,  1800,  425,  1275,   85,  20f, 1),
            new Monster("중급 슬라임 <color=cyan>XIV</color>",   1900,  1900,  450,  1350,   85,  20f, 1),
            new Monster("중급 슬라임 <color=cyan>XV</color>",    2200,  2200,  515,  1545,   90,  20f, 1),
            new Monster("중급 슬라임 <color=cyan>XVI</color>",   2350,  2350,  545,  1635,   90,  20f, 1),
            new Monster("중급 슬라임 <color=cyan>XVII</color>",  2500,  2500,  575,  1725,   95,  20f, 1),
            new Monster("중급 슬라임 <color=cyan>BOSS</color>",  3000,  3000,  620,  1860,   95,  20f, 1),

            new Monster("상급 슬라임 <color=red>I</color>",     3300,  3300,     660,   1980,   100,  25f, 2),
            new Monster("상급 슬라임 <color=red>II</color>",    3600,  3600,     720,   2160,   100,  25f, 2),
            new Monster("상급 슬라임 <color=red>III</color>",   3900,  3900,     780,   2340,   105,  25f, 2),
            new Monster("상급 슬라임 <color=red>IV</color>",    4200,  4200,     820,   2460,   105,  25f, 2),
            new Monster("상급 슬라임 <color=red>V</color>",     5000,  5000,     920,   2760,   110,  25f, 2),
            new Monster("상급 슬라임 <color=red>VI</color>",    5350,  5350,     970,   2910,   110,  25f, 2),
            new Monster("상급 슬라임 <color=red>VII</color>",   5700,  5700,     1020,  3060,   115,  25f, 2),
            new Monster("상급 슬라임 <color=red>VIII</color>",  6050,  6050,     1070,  3210,   115,  25f, 2),
            new Monster("상급 슬라임 <color=red>IX</color>",    6400,  6400,     1120,  3360,   120,  25f, 2),
            new Monster("상급 슬라임 <color=red>X</color>",     6750,  6750,     1370,  4110,   120,  30f, 2),
            new Monster("상급 슬라임 <color=red>XI</color>",    7100,  7100,     1440,  4320,   125,  30f, 2),
            new Monster("상급 슬라임 <color=red>XII</color>",   7450,  7450,     1510,  4530,   125,  30f, 2),
            new Monster("상급 슬라임 <color=red>XIII</color>",  7800,  7800,     1580,  4740,   130,  30f, 2),
            new Monster("상급 슬라임 <color=red>XIV</color>",   8150,  8150,     1650,  4950,   130,  30f, 2),
            new Monster("상급 슬라임 <color=red>XV</color>",    9000,  9000,     2000,  6000,   135,  30f, 2),
            new Monster("상급 슬라임 <color=red>XVI</color>",   9450,  9450,     2100,  6300,   135,  30f, 2),
            new Monster("상급 슬라임 <color=red>XVII</color>",  9900,  9900,     2200,  6600,   140,  30f, 2),
            new Monster("상급 슬라임 <color=red>BOSS</color>",  11000, 11000,    2500,  7500,   140,  30f, 2),

            new Monster("최상급 슬라임 <color=maroon>I</color>",     11600, 11600,  2650,  7950,    145,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>II</color>",    12200, 12200,  2800,  8400,    145,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>III</color>",   12800, 12800,  2950,  8850,    150,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>IV</color>",    13400, 13400,  3100,  9300,    150,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>V</color>",     15000, 15000,  3300,  9900,    155,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>VI</color>",    15700, 15700,  3450,  10350,   155,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>VII</color>",   16400, 16400,  3600,  10800,   160,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>VIII</color>",  17100, 17100,  3750,  11250,   160,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>IX</color>",    17800, 17800,  3900,  11700,   165,  40f,  3),
            new Monster("최상급 슬라임 <color=maroon>X</color>",     19500, 19500,  4200,  12600,   165,  50f, 3),
            new Monster("최상급 슬라임 <color=maroon>XI</color>",    20300, 20300,  4360,  13080,   170,  50f, 3),
            new Monster("최상급 슬라임 <color=maroon>XII</color>",   21100, 21100,  4520,  13560,   170,  50f, 3),
            new Monster("최상급 슬라임 <color=maroon>XIII</color>",  21900, 21900,  4680,  14040,   175,  50f, 3),
            new Monster("최상급 슬라임 <color=maroon>XIV</color>",   22700, 22700,  4840,  14520,   175,  50f, 3),
            new Monster("최상급 슬라임 <color=maroon>XV</color>",    25000, 25000,  5020,  15060,   180,  50f, 3),
            new Monster("최상급 슬라임 <color=maroon>XVI</color>",   25900, 25900,  5200,  15600,   180,  50f, 3),
            new Monster("최상급 슬라임 <color=maroon>XVII</color>",  26800, 26800,  5380,  16140,   185,  50f, 3),
            new Monster("최상급 슬라임 <color=maroon>BOSS</color>",  30000, 30000,  5600,  16800,   185,  50f, 3),

            new Monster("초급 늑대 <color=lime>I</color>",    31000, 31000,  5800,  17400,   190,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>II</color>",   32000, 32000,  6000,  18000,   190,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>III</color>",  33000, 33000,  6200,  18600,   195,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>IV</color>",   34000, 34000,  6400,  19200,   195,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>V</color>",    37000, 37000,  6600,  19800,   200,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>VI</color>",   38200, 38200,  6800,  20400,   200,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>VII</color>",  39400, 39400,  7000,  21000,   205,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>VIII</color>", 40600, 40600,  7200,  21600,   205,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>IX</color>",   42200, 42200,  7400,  22200,   210,  4.5f,  4),
            new Monster("초급 늑대 <color=lime>X</color>",    45800, 45800,  7600,  22800,   210,  9f,  4),
            new Monster("초급 늑대 <color=lime>XI</color>",   47200, 47200,  7800,  23400,   215,  9f,  4),
            new Monster("초급 늑대 <color=lime>XII</color>",  48600, 48600,  8000,  24000,   215,  9f,  4),
            new Monster("초급 늑대 <color=lime>XIII</color>", 50000, 50000,  8200,  24600,   220,  9f,  4),
            new Monster("초급 늑대 <color=lime>XIV</color>",  51400, 51400,  8400,  25200,   225,  9f,  4),
            new Monster("초급 늑대 <color=lime>XV</color>",   55000, 55000,  8600,  25800,   230,  9f,  4),
            new Monster("초급 늑대 <color=lime>XVI</color>",  56600, 56600,  8800,  26400,   230,  9f,  4),
            new Monster("초급 늑대 <color=lime>XVII</color>", 58200, 58200,  9000,  27000,   235,  9f,  4),
            new Monster("초급 늑대 <color=lime>BOSS</color>", 63000, 63000,  9200,  27600,   235,  9f,  4),

            new Monster("중급 늑대 <color=cyan>I</color>",    64700, 64700,  9500,   28500,   240,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>II</color>",   66400, 66400,  9800,   29400,   240,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>III</color>",  68100, 68100,  10100,  30300,   245,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>IV</color>",   69800, 69800,  10400,  31200,   245,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>V</color>",    73000, 73000,  10700,  32100,   250,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>VI</color>",   74800, 74800,  11000,  33000,   250,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>VII</color>",  76600, 76600,  11300,  33900,   255,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>VIII</color>", 78400, 78400,  11600,  34800,   255,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>IX</color>",   80200, 80200,  11900,  35700,   260,  13.5f,  5),
            new Monster("중급 늑대 <color=cyan>X</color>",    84000, 84000,  12200,  36600,   260,  18f,  5),
            new Monster("중급 늑대 <color=cyan>XI</color>",   85900, 85900,  12500,  37500,   265,  18f,  5),
            new Monster("중급 늑대 <color=cyan>XII</color>",  67800, 67800,  12800,  38400,   265,  18f,  5),
            new Monster("중급 늑대 <color=cyan>XIII</color>", 69700, 69700,  13100,  39300,   270,  18f,  5),
            new Monster("중급 늑대 <color=cyan>XIV</color>",  74000, 74000,  13400,  40200,   270,  18f,  5),
            new Monster("중급 늑대 <color=cyan>XV</color>",   76000, 76000,  13700,  41100,   275,  18f,  5),
            new Monster("중급 늑대 <color=cyan>XVI</color>",  78000, 78000,  14000,  42000,   275,  18f,  5),
            new Monster("중급 늑대 <color=cyan>XVII</color>", 80000, 80000,  14300,  42900,   280,  18f,  5),
            new Monster("중급 늑대 <color=cyan>BOSS</color>", 90000, 90000,  14600,  43800,   280,  18f,  5),

            new Monster("상급 늑대 <color=red>I</color>",    93000,  93000,   15000,  45000,   285,  22.5f,  6),
            new Monster("상급 늑대 <color=red>II</color>",   96000,  96000,   15400,  46200,   285,  22.5f,  6),
            new Monster("상급 늑대 <color=red>III</color>",  99000,  99000,   15800,  47400,   290,  22.5f,  6),
            new Monster("상급 늑대 <color=red>IV</color>",   102000, 102000,  16200,  48600,   290,  22.5f,  6),
            new Monster("상급 늑대 <color=red>V</color>",    110000, 110000,  16600,  49800,   295,  22.5f,  6),
            new Monster("상급 늑대 <color=red>VI</color>",   113500, 113500,  17000,  51000,   295,  22.5f,  6),
            new Monster("상급 늑대 <color=red>VII</color>",  117000, 117000,  17400,  52200,   300,  22.5f,  6),
            new Monster("상급 늑대 <color=red>VIII</color>", 120500, 120500,  17800,  53400,   300,  22.5f,  6),
            new Monster("상급 늑대 <color=red>IX</color>",   124000, 124000,  18200,  54600,   305,  22.5f,  6),
            new Monster("상급 늑대 <color=red>X</color>",    130000, 130000,  18600,  55800,   305,  27f,  6),
            new Monster("상급 늑대 <color=red>XI</color>",   134000, 134000,  19000,  57000,   310,  27f,  6),
            new Monster("상급 늑대 <color=red>XII</color>",  138000, 138000,  19400,  58200,   310,  27f,  6),
            new Monster("상급 늑대 <color=red>XIII</color>", 142000, 142000,  19800,  59400,   315,  27f,  6),
            new Monster("상급 늑대 <color=red>XIV</color>",  146000, 146000,  20200,  60600,   315,  27f,  6),
            new Monster("상급 늑대 <color=red>XV</color>",   154000, 154000,  20600,  61800,   320,  27f,  6),
            new Monster("상급 늑대 <color=red>XVI</color>",  158500, 158500,  21000,  63000,   320,  27f,  6),
            new Monster("상급 늑대 <color=red>XVII</color>", 163000, 163000,  21400,  64200,   325,  27f,  6),
            new Monster("상급 늑대 <color=red>BOSS</color>", 170000, 170000,  21800,  65400,   325,  27f,  6),

            new Monster("최상급 늑대 <color=maroon>I</color>",    175000, 175000,  23000,  69000,   330,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>II</color>",   180000, 180000,  23500,  70500,   330,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>III</color>",  185000, 185000,  24000,  72000,   335,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>IV</color>",   190000, 190000,  24500,  73500,   335,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>V</color>",    200000, 200000,  25000,  75000,   340,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>VI</color>",   205500, 205500,  25500,  76500,   340,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>VII</color>",  211000, 211000,  26000,  78000,   345,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>VIII</color>", 216500, 216500,  26500,  79500,   345,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>IX</color>",   222000, 222000,  27000,  81000,   350,  36f,  7),
            new Monster("최상급 늑대 <color=maroon>X</color>",    235000, 235000,  27500,  82500,   350,  40.5f,  7),
            new Monster("최상급 늑대 <color=maroon>XI</color>",   241000, 241000,  28000,  84000,   355,  40.5f,  7),
            new Monster("최상급 늑대 <color=maroon>XII</color>",  247000, 247000,  28500,  85500,   355,  40.5f,  7),
            new Monster("최상급 늑대 <color=maroon>XIII</color>", 253000, 253000,  29000,  87000,   360,  40.5f,  7),
            new Monster("최상급 늑대 <color=maroon>XIV</color>",  259000, 259000,  29500,  88500,   360,  40.5f,  7),
            new Monster("최상급 늑대 <color=maroon>XV</color>",   270000, 270000,  30000,  90000,   365,  40.5f,  7),
            new Monster("최상급 늑대 <color=maroon>XVI</color>",  276500, 276500,  30500,  91500,   365,  40.5f,  7),
            new Monster("최상급 늑대 <color=maroon>XVII</color>", 283000, 283000,  31000,  93000,   370,  40.5f,  7),
            new Monster("최상급 늑대 <color=maroon>BOSS</color>", 300000, 300000,  40000,  120000,  370,  40.5f,  7),

            new Monster("초급 골렘 <color=lime>I</color>",    320000, 320000,  41500,  124500,   375,  4f,  8),
            new Monster("초급 골렘 <color=lime>II</color>",   340000, 340000,  43000,  128000,   375,  4f,  8),
            new Monster("초급 골렘 <color=lime>III</color>",  360000, 360000,  44500,  131500,   380,  4f,  8),
            new Monster("초급 골렘 <color=lime>IV</color>",   380000, 380000,  46000,  135000,   380,  4f,  8),
            new Monster("초급 골렘 <color=lime>V</color>",    400000, 400000,  47500,  138500,   385,  4f,  8),
            new Monster("초급 골렘 <color=lime>VI</color>",   420000, 420000,  49000,  142000,   385,  4f,  8),
            new Monster("초급 골렘 <color=lime>VII</color>",  440000, 440000,  50500,  145500,   390,  4f,  8),
            new Monster("초급 골렘 <color=lime>VIII</color>", 460000, 460000,  52000,  149000,   390,  4f,  8),
            new Monster("초급 골렘 <color=lime>IX</color>",   480000, 480000,  53500,  152500,   395,  4f,  8),
            new Monster("초급 골렘 <color=lime>X</color>",    500000, 500000,  55000,  156000,   395,  8f,  8),
            new Monster("초급 골렘 <color=lime>XI</color>",   520000, 520000,  56500,  159500,   400,  8f,  8),
            new Monster("초급 골렘 <color=lime>XII</color>",  540000, 540000,  58000,  163000,   400,  8f,  8),
            new Monster("초급 골렘 <color=lime>XIII</color>", 560000, 560000,  59500,  166500,   405,  8f,  8),
            new Monster("초급 골렘 <color=lime>XIV</color>",  580000, 580000,  61000,  170000,   405,  8f,  8),
            new Monster("초급 골렘 <color=lime>XV</color>",   600000, 600000,  62500,  173500,   410,  8f,  8),
            new Monster("초급 골렘 <color=lime>XVI</color>",  620000, 620000,  64000,  177000,   410,  8f,  8),
            new Monster("초급 골렘 <color=lime>XVII</color>", 640000, 640000,  65500,  180500,   415,  8f,  8),
            new Monster("초급 골렘 <color=lime>BOSS</color>", 680000, 680000,  70000,  210000,   415,  8f,  8),

            new Monster("중급 골렘 <color=cyan>I</color>",    710000,  710000,   72000,   216000,   420,  12f,  9),
            new Monster("중급 골렘 <color=cyan>II</color>",   740000,  740000,   74000,   222000,   420,  12f,  9),
            new Monster("중급 골렘 <color=cyan>III</color>",  770000,  770000,   76000,   228000,   425,  12f,  9),
            new Monster("중급 골렘 <color=cyan>IV</color>",   800000,  800000,   78000,   234000,   425,  12f,  9),
            new Monster("중급 골렘 <color=cyan>V</color>",    830000,  830000,   80000,   240000,   430,  12f,  9),
            new Monster("중급 골렘 <color=cyan>VI</color>",   860000,  860000,   82000,   246000,   430,  12f,  9),
            new Monster("중급 골렘 <color=cyan>VII</color>",  890000,  890000,   84000,   252000,   435,  12f,  9),
            new Monster("중급 골렘 <color=cyan>VIII</color>", 920000,  920000,   86000,   258000,   435,  12f,  9),
            new Monster("중급 골렘 <color=cyan>IX</color>",   950000,  950000,   88000,   264000,   440,  12f,  9),
            new Monster("중급 골렘 <color=cyan>X</color>",    980000,  980000,   90000,   270000,   440,  16f,  9),
            new Monster("중급 골렘 <color=cyan>XI</color>",   1010000, 1010000,  92000,   276000,   445,  16f,  9),
            new Monster("중급 골렘 <color=cyan>XII</color>",  1040000, 1040000,  94000,   282000,   445,  16f,  9),
            new Monster("중급 골렘 <color=cyan>XIII</color>", 1070000, 1070000,  96000,   288000,   450,  16f,  9),
            new Monster("중급 골렘 <color=cyan>XIV</color>",  1100000, 1100000,  98000,   294000,   450,  16f,  9),
            new Monster("중급 골렘 <color=cyan>XV</color>",   1130000, 1130000,  100000,  300000,   455,  16f,  9),
            new Monster("중급 골렘 <color=cyan>XVI</color>",  1160000, 1160000,  102000,  306000,   455,  16f,  9),
            new Monster("중급 골렘 <color=cyan>XVII</color>", 1190000, 1190000,  104000,  312000,   460,  16f,  9),
            new Monster("중급 골렘 <color=cyan>BOSS</color>", 1220000, 1220000,  110000,  330000,   460,  16f,  9),

            new Monster("상급 골렘 <color=yellow>I</color>",    1260000, 1260000,  120000,  360000,   465,  20f,  10),
            new Monster("상급 골렘 <color=yellow>II</color>",   1300000, 1300000,  125000,  375000,   465,  20f,  10),
            new Monster("상급 골렘 <color=yellow>III</color>",  1340000, 1340000,  130000,  390000,   470,  20f,  10),
            new Monster("상급 골렘 <color=yellow>IV</color>",   1380000, 1380000,  135000,  405000,   470,  20f,  10),
            new Monster("상급 골렘 <color=yellow>V</color>",    1420000, 1420000,  140000,  420000,   475,  20f,  10),
            new Monster("상급 골렘 <color=yellow>VI</color>",   1460000, 1460000,  145000,  435000,   475,  20f,  10),
            new Monster("상급 골렘 <color=yellow>VII</color>",  1500000, 1500000,  150000,  450000,   480,  20f,  10),
            new Monster("상급 골렘 <color=yellow>VIII</color>", 1540000, 1540000,  155000,  465000,   480,  20f,  10),
            new Monster("상급 골렘 <color=yellow>IX</color>",   1580000, 1580000,  160000,  480000,   485,  20f,  10),
            new Monster("상급 골렘 <color=yellow>X</color>",    1620000, 1620000,  165000,  495000,   485,  24f,  10),
            new Monster("상급 골렘 <color=yellow>XI</color>",   1660000, 1660000,  170000,  510000,   490,  24f,  10),
            new Monster("상급 골렘 <color=yellow>XII</color>",  1700000, 1700000,  175000,  525000,   490,  24f,  10),
            new Monster("상급 골렘 <color=yellow>XIII</color>", 1740000, 1740000,  180000,  540000,   495,  24f,  10),
            new Monster("상급 골렘 <color=yellow>XIV</color>",  1780000, 1780000,  185000,  555000,   495,  24f,  10),
            new Monster("상급 골렘 <color=yellow>XV</color>",   1820000, 1820000,  190000,  570000,   500,  24f,  10),
            new Monster("상급 골렘 <color=yellow>XVI</color>",  1860000, 1860000,  195000,  585000,   500,  24f,  10),
            new Monster("상급 골렘 <color=yellow>XVII</color>", 1900000, 1900000,  200000,  600000,   505,  24f,  10),
            new Monster("상급 골렘 <color=yellow>BOSS</color>", 2000000, 2000000,  220000,  660000,   505,  24f,  10),

            new Monster("최상급 골렘 <color=#FF7474>I</color>",    2050000, 2050000,  223000,  669000,  510,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>II</color>",   2100000, 2100000,  226000,  678000,  510,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>III</color>",  2150000, 2150000,  229000,  687000,  515,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>IV</color>",   2200000, 2200000,  232000,  696000,  515,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>V</color>",    2250000, 2250000,  235000,  705000,  520,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>VI</color>",   2300000, 2300000,  238000,  714000,  520,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>VII</color>",  2350000, 2350000,  241000,  723000,  525,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>VIII</color>", 2400000, 2400000,  244000,  732000,  525,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>IX</color>",   2450000, 2450000,  247000,  741000,  530,  32f,  11),
            new Monster("최상급 골렘 <color=#FF7474>X</color>",    2500000, 2500000,  250000,  750000,  530,  36f,  11),
            new Monster("최상급 골렘 <color=#FF7474>XI</color>",   2550000, 2550000,  253000,  759000,  535,  36f,  11),
            new Monster("최상급 골렘 <color=#FF7474>XII</color>",  2600000, 2600000,  256000,  768000,  535,  36f,  11),
            new Monster("최상급 골렘 <color=#FF7474>XIII</color>", 2650000, 2650000,  259000,  777000,  540,  36f,  11),
            new Monster("최상급 골렘 <color=#FF7474>XIV</color>",  2700000, 2700000,  262000,  786000,  540,  36f,  11),
            new Monster("최상급 골렘 <color=#FF7474>XV</color>",   2750000, 2750000,  265000,  795000,  545,  36f,  11),
            new Monster("최상급 골렘 <color=#FF7474>XVI</color>",  2800000, 2800000,  268000,  804000,  545,  36f,  11),
            new Monster("최상급 골렘 <color=#FF7474>XVII</color>", 2850000, 2850000,  271000,  813000,  550,  36f,  11),
            new Monster("최상급 골렘 <color=#FF7474>BOSS</color>", 2900000, 2900000,  280000,  840000,  550,  36f,  11),
        };

        // 다른 몬스터들을 몬스터 리스트에 추가
        monsters.AddRange(slimes);
    }

    public GameObject[] BrokingPanel;

    public Slider Enemy_HPBar;
    public Text Enemy_HPText;

    public Text EnemyTitle;
    private string[] EnemyTitleString;

    [SerializeField]
    public GameObject EnemyLevelPanel;                  //  적 레벨 패널
    public GameObject EnemyLevelSelPanel;               //  적 레벨 선택 패널
    public GameObject EnemyDifficultyPanel;             //  적 난이도 패널

    public Button EnemyLevelPanel_OpenBtn;              //  적 레벨 패널 여는 버튼
    public Button[] EnemyDifficulty_SelBtn;                //  적 난이도 버튼
    public Button[] EnemyLevel_SelBtn;                  //  적 레벨 버튼

    public Button EnemyDifficulty_ReturnBtn;
    public Button EnemyDifficulty_NextBtn;

    public Button Enemy_SummonsBtn;                     //  적 소환 버튼

    public Text[] Enemy_DifficultyText;                 //  적 난이도 텍스트
    public Text[] Enemy_InfoText;                       //  적 정보 텍스트

    public GameObject[] Enemy;                            //  적 오브젝트

    public Image[] EnemyDifficultyImg = new Image[5];

    public Image[] EnemyImg;
    public Sprite[] MS_Sprite;

    public Animator[] enemyAnimators;


    private float experienceValue = 0f;                 // 적 처치 보상 : 경험치
    private int moneyReward = 0;                        // 적 처치 보상 : 돈
    private int RedStone = 0;                           // 적 처치 보상 : 붉은 보석
    public static double maxHP;                         // 적 최대 체력
    public static double currentHP;                     // 적 현재 체력

    public Text EnemyName;                              // 몹 이름
    public Button ExitBtn;                              // 몹 선택 나가기 버튼
    public Button ExitSelBtn;                           // 몹 선택 나가기 버튼

    public int LastBtnIndex = -1;                      // 이전에 선택한 버튼의 인덱스
    public int MoveDifficultyInStage = -1;             // 0 슬라임 
    public int FixDifficultyInStage = -1;              // 0 슬라임 
    public int MoveDifficulty = 0;
    public int FixDifficulty = 0;

    public Image EnemyInfoImg;
    public Image EnemyInfoFrame;
    public Text EnemyInfoText;

    private string[] ScrollMobText;

    private string[] DifficultyValue;

    public Image MobScroll;
    public Text MobText;
    public Text[] MobClearValue;

    // 획득 정보

    public Text Acquisition_ExpValue;
    public Text Acquisition_MoneyValue;
    public Text Acquisition_RedStoneValue;
    public Text Acquisition_MobScrollValue;

    bool Stop = false;
    private bool ChangeButtonclick = false;

    private int RegenValue;


    public GameObject[] BG;

    public GameObject Redstone;
    public GameObject[] Scroll;
    public RectTransform EnemyBox;

    // 소리
    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip DeathSoundClip; // AudioClip 변수 선언

    private void Start()
    {
        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        GameManager.stageClearDict["EasySlime"] = new bool[18];
        GameManager.stageClearDict["NormalSlime"] = new bool[18];
        GameManager.stageClearDict["HardSlime"] = new bool[18];
        GameManager.stageClearDict["ExtremeSlime"] = new bool[18];

        GameManager.stageClearDict["EasyWolf"] = new bool[18];
        GameManager.stageClearDict["NormalWolf"] = new bool[18];
        GameManager.stageClearDict["HardWolf"] = new bool[18];
        GameManager.stageClearDict["ExtremeWolf"] = new bool[18];

        GameManager.stageClearDict["EasyGolem"] = new bool[18];
        GameManager.stageClearDict["NormalGolem"] = new bool[18];
        GameManager.stageClearDict["HardGolem"] = new bool[18];
        GameManager.stageClearDict["ExtremeGolem"] = new bool[18];

        GameManager.stageClearDict["EasySlime"][0] = true;

        GameManager.Enemy_defeatedIndex = 0;

        FixDifficultyInStage = 0;
        MoveDifficultyInStage = 0;
        LastBtnIndex = 0;
        MoveDifficulty = 0;
        FixDifficulty = 0;

        for(int i = 0; i < BG.Length; i++)
        {
            if (FixDifficultyInStage == i) BG[i].SetActive(true);
            else BG[i].SetActive(false);
        }
        

        EnemyTitleString = new string[12]
        {
            "넓은 들판 - <color=lime><size=38>Easy</size></color>",
            "넓은 들판 - <color=cyan><size=38>Normal</size></color>",
            "넓은 들판 - <color=red><size=38>Hard</size></color>",
            "넓은 들판 - <color=maroon><size=38>Extreme</size></color>",

            "깊은 초원 - <color=lime><size=38>Easy</size></color>",
            "깊은 초원 - <color=cyan><size=38>Normal</size></color>",
            "깊은 초원 - <color=red><size=38>Hard</size></color>",
            "깊은 초원 - <color=maroon><size=38>Extreme</size></color>",

            "정글 사원 - <color=lime><size=38>Easy</size></color>",
            "정글 사원 - <color=cyan><size=38>Normal</size></color>",
            "정글 사원 - <color=yellow><size=38>Hard</size></color>",
            "정글 사원 - <color=#FF7474><size=38>Extreme</size></color>",
        };

        EnemyTitle.text = EnemyTitleString[FixDifficulty + FixDifficultyInStage * 4];

        DifficultyValue = new string[12]
        {
            "넓은 들판 - <color=lime>Easy</color>",
            "넓은 들판 - <color=cyan>Normal</color>",
            "넓은 들판 - <color=red>Hard</color>",
            "넓은 들판 - <color=maroon>Extreme</color>",
        
            "깊은 초원 - <color=lime>Easy</color>",
            "깊은 초원 - <color=cyan>Normal</color>",
            "깊은 초원 - <color=red>Hard</color>",
            "깊은 초원 - <color=maroon>Extreme</color>",

            "정글 사원 - <color=lime>Easy</color>",
            "정글 사원 - <color=cyan>Normal</color>",
            "정글 사원 - <color=red>Hard</color>",
            "정글 사원 - <color=#FF7474>Extreme</color>",

        };

        ScrollMobText = new string[3]
        {
            "슬라임 비급서 획득 가능",
            "늑대 비급서 획득 가능",
            "골렘 비급서 획득 가능",
        };


        Enemy_HPBar = GetComponentInChildren<Slider>();
        Enemy_HPBar.value = 1f;

        ChangeEnemyInfo(0);
        EnemySummons(); // 첫 번째 적 생성

        currentHP = maxHP; 
        EnemyLevelPanel_OpenBtn.onClick.AddListener(() => EnemyListPanel(MoveDifficultyInStage));
        ExitBtn.onClick.AddListener(EnemyListExitPanel);
        ExitSelBtn.onClick.AddListener(EnemyListExitPanel);
        Enemy_SummonsBtn.onClick.AddListener(() => EnemySummons());

        EnemyDifficulty_NextBtn.onClick.AddListener(EnemyListPanelNext);
        EnemyDifficulty_ReturnBtn.onClick.AddListener(EnemyListPanelReturn);

        for (int i = 0; i < EnemyLevel_SelBtn.Length; i++)
        {
            int index = i; // 클로저로 인덱스를 보존

            EnemyLevel_SelBtn[index].onClick.AddListener(() => ChangeEnemyInfo(index));
        }
        
        for(int i = 0; i < EnemyDifficulty_SelBtn.Length; i++)
        {
            int index = i;

            EnemyDifficulty_SelBtn[index].onClick.AddListener(() => EnemyDifficultyOpen(index));
        }

        Acquisition_ExpValue.text = "0";
        Acquisition_MoneyValue.text = "0";
        Acquisition_RedStoneValue.text = "0";
        Acquisition_MobScrollValue.text = "0";

    }

    [System.Obsolete]
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Enemy_Hit == true)
        {
            enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].SetTrigger("Hit");
            GameManager.Enemy_Hit = false;
        }
        HP_Text();
        Handle();

        if (currentHP <= 0)
        {
            enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].SetTrigger("Death");

            if ((ChangeManager.InPanel == 2 || ChangeManager.InPanel == 3) && BossManager.fighting == false) audioSource.PlayOneShot(DeathSoundClip, 0.7f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
            GrantRewards(GameManager.Enemy_defeatedIndex);
            UpdateEnemyStageClear(FixDifficultyInStage, FixDifficulty); // InStage 값으로 적의 스테이지 클리어 여부 업데이트 메서드 호출

        }

        if (Stop == false) StartCoroutine(RegenHP()); // RegenHP 코루틴 호출

    }

    /*
     * 몬스터 추가할때 해야할것
     * Start() - 몬스터 이름 추가할것
     * 
     * 확인할것
     * FixDifficultyInStage, FixDifficulty, 
     * 
     */




    

    // 다음으로 넘어가기 - 체크했음
    public void UpdateEnemyStageClear(int difficultyInStage, int fixDifficulty)
    {
        // BOSS 처치
        // 마지막 Extreme단계 클리어시 다음 Easy로 넘어가기
        if (fixDifficulty == 3 && GameManager.Enemy_defeatedIndex == 17 && GameManager.stageClearDict[EnemyReturnName(difficultyInStage + 1, 0)][0] == false)
        {
            FixDifficultyInStage++;
            GameManager.stageClearDict[EnemyReturnName(difficultyInStage + 1, 0)][0] = true;
            for (int i = 0; i < 17; i++) GameManager.PinInStage[fixDifficulty + difficultyInStage * 4][i] = false;
            GameManager.Enemy_defeatedIndex = 0;
            FixDifficulty = 0;
            LastBtnIndex = GameManager.Enemy_defeatedIndex;

            PowerBossSummons();
        } 

        else if(GameManager.Enemy_defeatedIndex == 17 && GameManager.stageClearDict[EnemyReturnName(difficultyInStage, fixDifficulty + 1)][0] == false)
        {
            GameManager.stageClearDict[EnemyReturnName(difficultyInStage, fixDifficulty + 1)][0] = true;
            for (int i = 0; i < 17; i++) GameManager.PinInStage[fixDifficulty + difficultyInStage * 4][i] = false;
            GameManager.Enemy_defeatedIndex = 0;
            FixDifficulty++;
            LastBtnIndex = GameManager.Enemy_defeatedIndex;

            PowerBossSummons();
        }

        else if (GameManager.Enemy_defeatedIndex == 17) return;

        // 다음 스테이지가 아직 열리지 않은 경우 열어줌
        else if (GameManager.stageClearDict[EnemyReturnName(difficultyInStage, fixDifficulty)][GameManager.Enemy_defeatedIndex + 1] == false && GameManager.stageClearDict[EnemyReturnName(difficultyInStage, fixDifficulty)][GameManager.Enemy_defeatedIndex] == true)
        {
            GameManager.stageClearDict[EnemyReturnName(difficultyInStage, fixDifficulty)][GameManager.Enemy_defeatedIndex + 1] = true; // 다음 스테이지 열기
            GameManager.Enemy_defeatedIndex++;
            LastBtnIndex = GameManager.Enemy_defeatedIndex;

            PowerBossSummons();
        }
    }



    // 적 리스트 종료 관리 - 체크했음
    void EnemyListExitPanel()
    {
        // 만약 마지막 버튼이 결정한 버튼 Index숫자와 다를때 마지막 버튼을 결정한 버튼으로 변경
        if(LastBtnIndex != GameManager.Enemy_defeatedIndex)
        {
            // 나머지 요소는 모두 false로 설정합니다.
            for (int i = 0; i < 18; i++) GameManager.MoveInStage[MoveDifficulty + MoveDifficultyInStage * 4][i] = false;

            GameManager.MoveInStage[MoveDifficulty + MoveDifficultyInStage * 4][LastBtnIndex] = GameManager.PinInStage[FixDifficulty + FixDifficultyInStage * 4][GameManager.Enemy_defeatedIndex];
            LastBtnIndex = GameManager.Enemy_defeatedIndex;
            
            UpdateInfoValue();
        }

        // 만약 마지막 선택 난이도가 결정 난이도와 다를때 선택 난이도를 결정 난이도로 설정
        if (FixDifficulty != MoveDifficulty) MoveDifficulty = FixDifficulty;
        if (FixDifficultyInStage != MoveDifficultyInStage) MoveDifficultyInStage = FixDifficultyInStage;

        EnemyLevelPanel.SetActive(false);
        EnemyDifficultyPanel.SetActive(false);
        EnemyLevelSelPanel.SetActive(false);
    }


    // 몹 난이도 관리 - 체크했음
    private void EnemyDifficultyOpen(int index)
    {
        MoveDifficulty = index;
        ChangeButtonclick = false;

        // 난이도에서 어떤 몹인지 선택

        if (MoveDifficultyInStage == 0) // 난이도에서 어떤 몹인지 선택
        {
            if (index == 0)  // 난이도만 선택
            {
                UpdateEnemyButtons("EasySlime", "민트색", "기본색", "투명기본색", 0);
                EnemyInfoFrame.color = ColorManager.ColorChange("라임색");
            }
            else if (index == 1)  // 난이도만 선택
            {
                UpdateEnemyButtons("NormalSlime", "민트색", "기본색", "투명기본색", 1);
                EnemyInfoFrame.color = ColorManager.ColorChange("하늘색");
            }
            else if (index == 2)  // 난이도만 선택
            {
                UpdateEnemyButtons("HardSlime", "민트색", "기본색", "투명기본색", 2);
                EnemyInfoFrame.color = ColorManager.ColorChange("빨간색");
            }
            else if (index == 3)  // 난이도만 선택
            {
                UpdateEnemyButtons("ExtremeSlime", "민트색", "기본색", "투명기본색", 3);
                EnemyInfoFrame.color = ColorManager.ColorChange("적홍색");
            }
        }

        if (MoveDifficultyInStage == 1) // 난이도에서 어떤 몹인지 선택
        {
            if (index == 0)  // 난이도만 선택
            {
                UpdateEnemyButtons("EasyWolf", "민트색", "기본색", "투명기본색", 4);
                EnemyInfoFrame.color = ColorManager.ColorChange("라임색");
            }
            else if (index == 1)  // 난이도만 선택
            {
                UpdateEnemyButtons("NormalWolf", "민트색", "기본색", "투명기본색", 5);
                EnemyInfoFrame.color = ColorManager.ColorChange("하늘색");
            }
            else if (index == 2)  // 난이도만 선택
            {
                UpdateEnemyButtons("HardWolf", "민트색", "기본색", "투명기본색", 6);
                EnemyInfoFrame.color = ColorManager.ColorChange("빨간색");
            }
            else if (index == 3)  // 난이도만 선택
            {
                UpdateEnemyButtons("ExtremeWolf", "민트색", "기본색", "투명기본색", 7);
                EnemyInfoFrame.color = ColorManager.ColorChange("적홍색");
            }
        }

        if (MoveDifficultyInStage == 2) 
        {
            if (index == 0)  // 난이도만 선택
            {
                UpdateEnemyButtons("EasyGolem", "민트색", "기본색", "투명기본색", 8);
                EnemyInfoFrame.color = ColorManager.ColorChange("골렘1번색");
            }
            else if (index == 1)  // 난이도만 선택
            {
                UpdateEnemyButtons("NormalGolem", "민트색", "기본색", "투명기본색", 9);
                EnemyInfoFrame.color = ColorManager.ColorChange("골렘2번색");
            }
            else if (index == 2)  // 난이도만 선택
            {
                UpdateEnemyButtons("HardGolem", "민트색", "기본색", "투명기본색", 10);
                EnemyInfoFrame.color = ColorManager.ColorChange("골렘3번색");
            }
            else if (index == 3)  // 난이도만 선택
            {
                UpdateEnemyButtons("ExtremeGolem", "민트색", "기본색", "투명기본색", 11);
                EnemyInfoFrame.color = ColorManager.ColorChange("골렘4번색");
            }


        }

        EnemyInfoText.text = DifficultyValue[index + MoveDifficultyInStage * 4];
        

        EnemyInfoImg.sprite = EnemyImg[index + MoveDifficultyInStage * 4].sprite;

        ExitSelBtn.gameObject.SetActive(true);
        ExitBtn.gameObject.SetActive(false);

        EnemyDifficultyPanel.SetActive(false);
        EnemyLevelSelPanel.SetActive(true);
        UpdateInfoValue();
    }

    // 버튼 색상 업데이트 0 ~ 17번 - 체크했음
    private void UpdateEnemyButtons(string enemyType, string defeatedColor, string interactableColor, string nonInteractableColor, int difficulty)
    {
        for (int i = 0; i < EnemyLevel_SelBtn.Length; i++)
        {
            int index_ = i; // 클로저로 인덱스를 보존

            if (GameManager.stageClearDict[enemyType][index_] == true)
            {
                // 클리어된 스테이지일 때 버튼 색상 변경
                ChangeButtonColor(EnemyLevel_SelBtn[index_], interactableColor);
                EnemyLevel_SelBtn[index_].interactable = true;
            }
            else
            {
                // 클리어되지 않은 스테이지일 때 버튼 색상 변경
                ChangeButtonColor(EnemyLevel_SelBtn[index_], nonInteractableColor);
                EnemyLevel_SelBtn[index_].interactable = false;
            }
        }
        
        // 만약 PinInStage[][] == true면 색상 변경 이것만! : 현재 선택된 스테이지라는 뜻
        if (GameManager.PinInStage[FixDifficulty + FixDifficultyInStage * 4][GameManager.Enemy_defeatedIndex] == true && difficulty == FixDifficulty + FixDifficultyInStage * 4)
        {
            // 현재 선택된 스테이지일 때 버튼 색상 변경
            ChangeButtonColor(EnemyLevel_SelBtn[GameManager.Enemy_defeatedIndex], defeatedColor);
        }
    }

    // 슬라임 선택 정보 관리 - 체크했음
    private void ChangeEnemyInfo(int index)
    {

        if (ChangeButtonclick == false) ChangeButtonclick = true;
        // index : 현재   LastBtnIndex : 마지막

        // 현재와 마지막이 같으면 리턴;
        if (index == LastBtnIndex && MoveDifficulty + MoveDifficultyInStage * 4 == FixDifficulty + FixDifficultyInStage * 4) return;

        // 이전 버튼의 색상을 변경합니다.
        if (GameManager.stageClearDict[EnemyReturnName(MoveDifficultyInStage, MoveDifficulty)][LastBtnIndex] == true) ChangeButtonColor(EnemyLevel_SelBtn[LastBtnIndex], "기본색");

        // 현재 선택한 버튼의 색상 변경
        ChangeButtonColor(EnemyLevel_SelBtn[index], "민트색");

        // 전 넘버는 false로
        GameManager.MoveInStage[MoveDifficulty + MoveDifficultyInStage * 4][LastBtnIndex] = false;

        // MoveInStage를 true 변경
        GameManager.MoveInStage[MoveDifficulty + MoveDifficultyInStage * 4][index] = true;

        // 현재 선택한 버튼을 이전에 선택한 버튼으로 설정
        LastBtnIndex = index;

        // 슬라임 정보를 사용하여 UI 업데이트
        UpdateInfoValue();
    }

    // 다음 넘어가는 보스 소환 - 체크했음
    private void PowerBossSummons()
    {
        MoveDifficultyInStage = FixDifficultyInStage;

        enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].ResetTrigger("Hit");
        enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].SetBool("Basic", false);

        GameManager.Enemy_defeatedIndex = LastBtnIndex;

        int _Defeat = GetMonsterIndex(FixDifficulty + FixDifficultyInStage * 4, GameManager.Enemy_defeatedIndex);


        for (int i = 0; i < BG.Length; i++)
        {
            if (FixDifficultyInStage == i) BG[i].SetActive(true);
            else BG[i].SetActive(false);
        }

        for (int i = 0; i < Enemy.Length; i++)
        {
            if (i == FixDifficulty + 4 * FixDifficultyInStage)
            {
                Debug.Log("확인해 : " + (i + 4 * FixDifficultyInStage));
                Enemy[i].SetActive(true);
            }
            else
            {
                Enemy[i].SetActive(false);
            }
        }


        Monster selectedBoss = GetMonsterByIndex(_Defeat);
        
        Debug.Log("난이도 : " + selectedBoss.Difficulty);

        EnemyTitle.text = EnemyTitleString[FixDifficulty + FixDifficultyInStage * 4];

        // 보스의 인덱스만 true로 설정합니다.
        for (int i = 0; i < 18; i++)
        {
            GameManager.PinInStage[MoveDifficulty + MoveDifficultyInStage * 4][i] = false;
            GameManager.MoveInStage[MoveDifficulty + MoveDifficultyInStage * 4][i] = false;
        }

        GameManager.PinInStage[FixDifficulty + FixDifficultyInStage * 4][GameManager.Enemy_defeatedIndex] = true;
        GameManager.MoveInStage[FixDifficulty + FixDifficultyInStage * 4][GameManager.Enemy_defeatedIndex] = true;

        // 보스 몬스터 속성 설정
        Image enemycolor = Enemy[FixDifficulty + FixDifficultyInStage * 4].GetComponent<Image>();

        enemycolor.color = ColorManager.ColorChange(GetMonsterColor(selectedBoss.Difficulty));

        EnemyName.text = selectedBoss.Name;
        maxHP = selectedBoss.MaxHealth;
        currentHP = selectedBoss.MaxHealth;
        experienceValue = selectedBoss.ExperienceValue;
        moneyReward = selectedBoss.MoneyReward;
        RedStone = selectedBoss.RedStoneReward;

        // 보스 몬스터 속성에 따른 보정 (예: 보스 레벨에 따른 HP 보정)
        if (GameManager.WarrantLevel[15] >= 1) maxHP *= (1 - GameManager.Warrant_Power[15] / 100f);
        EnemyLevelPanel.SetActive(false);
        EnemyLevelSelPanel.SetActive(false);
    }


    // 적 소환 - 체크했음
    private void EnemySummons()
    {
        if (GameManager.stageClearDict[EnemyReturnName(MoveDifficultyInStage, MoveDifficulty)][LastBtnIndex] == false) return;
        if (ChangeButtonclick == false && MoveDifficulty != FixDifficulty) return; // 선택 안했을때 리턴시키기
        
        // 보스 몬스터 정보 가져오기
        GameManager.Enemy_defeatedIndex = LastBtnIndex;

        for (int i = 0; i < 18; i++)
        {
            GameManager.PinInStage[MoveDifficulty + MoveDifficultyInStage * 4][i] = false;
            GameManager.MoveInStage[MoveDifficulty + MoveDifficultyInStage * 4][i] = false;
        }

        FixDifficulty = MoveDifficulty;
        FixDifficultyInStage = MoveDifficultyInStage;

        enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].ResetTrigger("Hit");
        enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].SetBool("Basic", false);
        EnemyTitle.text = EnemyTitleString[FixDifficulty + FixDifficultyInStage * 4];

        for (int i = 0; i < BG.Length; i++)
        {
            if (FixDifficultyInStage == i) BG[i].SetActive(true);
            else BG[i].SetActive(false);
        }

        for (int i = 0; i < Enemy.Length; i++)
        {
            if (i == FixDifficulty + 4 * FixDifficultyInStage)
            {
                Enemy[i].SetActive(true);
            }
            else
            {
                Enemy[i].SetActive(false);
            }
        }


        int _Defeat = GetMonsterIndex(FixDifficulty + FixDifficultyInStage * 4, GameManager.Enemy_defeatedIndex);


        Monster selectedBoss = GetMonsterByIndex(_Defeat);

        

        // 보스의 인덱스만 true로 설정합니다.
        GameManager.PinInStage[FixDifficulty + FixDifficultyInStage * 4][GameManager.Enemy_defeatedIndex] = true;
        GameManager.MoveInStage[FixDifficulty + FixDifficultyInStage * 4][GameManager.Enemy_defeatedIndex] = true;

        // 보스 몬스터 속성 설정
        Image enemycolor = Enemy[FixDifficulty + FixDifficultyInStage * 4].GetComponent<Image>();

        enemycolor.color = ColorManager.ColorChange(GetMonsterColor(selectedBoss.Difficulty));

        EnemyName.text = selectedBoss.Name;
        maxHP = selectedBoss.MaxHealth;
        currentHP = selectedBoss.MaxHealth;
        experienceValue = selectedBoss.ExperienceValue;
        moneyReward = selectedBoss.MoneyReward;
        RedStone = selectedBoss.RedStoneReward;

        // 보스 몬스터 속성에 따른 보정 (예: 보스 레벨에 따른 HP 보정)
        if (GameManager.WarrantLevel[15] >= 1) maxHP *= (1 - GameManager.Warrant_Power[15] / 100f);

        EnemyLevelPanel.SetActive(false);
        EnemyLevelSelPanel.SetActive(false);
    }


    // 보상 지급 - 체크했음
    [System.Obsolete]
    public void GrantRewards(int defeatedSlimeIndex)
    {
        // 슬라임 정보 가져오기

        int _Defeat = GetMonsterIndex(FixDifficulty + FixDifficultyInStage * 4, GameManager.Enemy_defeatedIndex);

        Monster currentMonster = GetMonsterByIndex(_Defeat);



        // 랜덤변수
        int Warrant_Random1 = Random.Range(0, 100);  // 경험치 N배 확률
        int Warrant_Random2 = Random.Range(0, 100);  // 골드 N배 확률
        int Warrant_Random3 = Random.Range(0, 100);  // 강화석 N배 확률
        int Warrant_Random4 = Random.Range(0, 100);  // 비급서 N배 확률
        int redStoneRandom = Random.Range(0, 100);  // 붉은 보석 확률
        float scrollDropChance = Random.Range(0, 100); // 비급서 드랍 확률

        // 보상 계산
        float experienceReward = currentMonster.ExperienceValue + (float)GameManager.AccessoryEquipExperience + MobScrollManager.MS_UpExp * (1 + ((float)GameManager.AccessoryOwnExperience / 100));
        long moneyReward = (long)((currentMonster.MoneyReward + MobScrollManager.MS_UpEarnGold) * (1 + (float)GameManager.Player_MoneyUp_EarnMoney / 100));
        //if (GameManager.WarrantLevel[12] >= 1) moneyReward *= (long)(1 + (float)GameManager.Warrant_Power[12] / 100);   // 수정하기
        if (GameManager.WarrantLevel[20] >= 1) moneyReward *= (long)(1 + (float)GameManager.Warrant_Power[12] / 100);
        if (GameManager.WarrantLevel[19] >= 1) experienceReward *= 1 + (float)GameManager.Warrant_Power[19] / 100;

        if (AdManager.AdPlaying[1] == true) moneyReward *= (long)(AdManager.AdPowerValue[1] / 50f);
        if (AdManager.AdPlaying[2] == true) experienceReward *= AdManager.AdPowerValue[2] / 50f;


        int redStoneReward = currentMonster.RedStoneReward;
        if (GameManager.WarrantLevel[16] >= 1 && Warrant_Random3 < 8) redStoneReward *= GameManager.Warrant_Power[16];

        float scrollChance = currentMonster.ScrollDrop;
        int mobScroll;

        mobScroll = 1;

        if (GameManager.WarrantLevel[0] >= 1    && Warrant_Random1 < 8) currentMonster.ExperienceValue *= 1 + (float)GameManager.Warrant_Power[0] / 100;        // 경험치 2배
        if (GameManager.WarrantLevel[11] >= 1    && Warrant_Random2 < 8) currentMonster.MoneyReward *= (int)(1 + (float)GameManager.Warrant_Power[11] / 100);
        if (GameManager.WarrantLevel[0] >= 1    && Warrant_Random1 < 8) currentMonster.ExperienceValue *= 1 + (float)GameManager.Warrant_Power[0] / 100;
        if (GameManager.WarrantLevel[0] >= 1    && Warrant_Random1 < 8) currentMonster.ExperienceValue *= 1 + (float)GameManager.Warrant_Power[0] / 100;
        if (GameManager.WarrantLevel[17] >= 1 && Warrant_Random4 < 8) mobScroll += GameManager.PlayerHaveMobScroll[FixDifficultyInStage];
        
        // 보상 지급
        GameManager.Player_CurExp += experienceReward;
        GameManager.Player_Money += moneyReward;

        // 비급서 드랍 여부 확인 및 처리
        if (scrollDropChance < scrollChance + 250)
        {
            GameManager.PlayerHaveMobScroll[FixDifficultyInStage] += mobScroll;
            Acquisition_MobScrollValue.text = mobScroll.ToString();
            AchievementManager.MonsterKillMobScroll += mobScroll;
            Instantiate(Scroll[FixDifficultyInStage], EnemyBox);
        }
        else Acquisition_MobScrollValue.text = "0";

        // 붉은 보석 지급 여부 확인 및 처리

        if (redStoneRandom < 250 + GameManager.Player_RedStone_Percent)
        {
            GameManager.Player_RedStone += redStoneReward;
            Acquisition_RedStoneValue.text = redStoneReward.ToString();
            AchievementManager.MonsterKillRedStone += redStoneReward;
            Instantiate(Redstone, EnemyBox);
        }
        else Acquisition_RedStoneValue.text = "0";


        // 통계

        AchievementManager.MonsterKill++;
        AchievementManager.MonsterKillGold += (int)moneyReward;
        AchievementManager.MonsterKillExp += (int)experienceReward;

        // UI 업데이트
        Acquisition_ExpValue.text = TextFormatter.GetThousandCommaText((int)experienceReward);
        Acquisition_MoneyValue.text = TextFormatter.GetThousandCommaText((int)moneyReward);

        // 현재 체력 초기화
        currentHP = maxHP;

        // 사망 애니메이션 재생 후 일정 시간 후에 다시 생성
        StartCoroutine(RespawnEnemy());
    }


    // 몬스터 받아와서 리턴시키기
    private int GetMonsterIndex(int difficulty, int level)
    {
        int index = difficulty * 18 + level;

        return index;
    }

    private string GetMonsterColor(int index)
    {
        string name;

        if (index <= 2) name = "하얀색";
        else if (index == 3) name = "빨간색";
        else if (index >= 4 && index < 7) name = "하얀색";
        else if (index == 8) name = "골렘1번색";
        else if (index == 9) name = "골렘2번색";
        else if (index == 10) name = "골렘3번색";
        else if (index == 11) name = "골렘4번색";
        else name = "하얀색";

        return name;
    }

    // 보스 정보 받아오기 - 번호따라서
    public Monster GetMonsterByIndex(int index)
    {
        // index에 해당하는 슬라임 정보 반환
        return monsters[index];
    }
    // 몹 이름 받아서 보내주기
    private string EnemyReturnName(int difficultyInstage, int index)
    {
        if (difficultyInstage == 0)
        {
            if (index == 0) return "EasySlime";
            else if (index == 1) return "NormalSlime";
            else if (index == 2) return "HardSlime";
            else return "ExtremeSlime";
        }
        else if (difficultyInstage == 1)
        {
            if (index == 0) return "EasyWolf";
            else if (index == 1) return "NormalWolf";
            else if (index == 2) return "HardWolf";
            else return "ExtremeWolf";
        }
        else
        {
            if (index == 0) return "EasyGolem";
            else if (index == 1) return "NormalGolem";
            else if (index == 2) return "HardGolem";
            else return "ExtremeGolem";
        }
    }
    // 적 리스폰 관리
    private IEnumerator RespawnEnemy()
    {
        Enemy_HPBar.gameObject.SetActive(false);
        EnemyName.gameObject.SetActive(false);
        GameManager.Enemy_alive = false;
        Image image = Enemy[FixDifficulty + FixDifficultyInStage * 4].GetComponent<Image>();
        enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].SetBool("Basic", false);
        // 사망 애니메이션 재생 후 일정 시간(예: 2초) 대기
        yield return new WaitForSeconds(1.1f);
        Color newColor = image.color;
        newColor.a = 1; // 새로운 알파값 설정
        enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].ResetTrigger("Death");
        enemyAnimators[FixDifficulty + FixDifficultyInStage * 4].ResetTrigger("Hit");
        Enemy_HPBar.gameObject.SetActive(true);
        EnemyName.gameObject.SetActive(true);
        GameManager.Enemy_alive = true;
    }

    // 체력 재생
    private IEnumerator RegenHP() // IEnumerator 반환 타입으로 변경
    {
        if (currentHP / 2 < maxHP) RegenValue = 80;
        else if (currentHP / 3 < maxHP) RegenValue = 70;
        else if (currentHP / 8 < maxHP) RegenValue = 50;
        else if (currentHP / 12 < maxHP) RegenValue = 30;
        else RegenValue = 100;

        Stop = !Stop;
        yield return new WaitForSeconds(0.1f);
        if (currentHP < maxHP && GameManager.WarrantLevel[6] >= 1) currentHP += maxHP / (RegenValue + GameManager.Warrant_Power[6]);
        else if(currentHP < maxHP) currentHP += maxHP / RegenValue;
        else currentHP = maxHP;
        Stop = !Stop;
    }


    // Info 정보 관리
    private void UpdateInfoValue()
    {
        Monster monster;

        int _Defeat = GetMonsterIndex(MoveDifficulty + MoveDifficultyInStage * 4, LastBtnIndex);

        monster = GetMonsterByIndex(_Defeat);

        double maxHealth = monster.MaxHealth;
        float moneyReward = monster.MoneyReward;
        float experienceValue = monster.ExperienceValue;
        int redStone = monster.RedStoneReward;
        float scrollDrop = monster.ScrollDrop;

        if (GameManager.WarrantLevel[15] >= 1)
        {
            maxHealth *= (1 - GameManager.Warrant_Power[15] / 100f);
        }

        if (MoveDifficultyInStage == 0)
        {
            if (MoveDifficulty == 3) EnemyInfoImg.color = ColorManager.ColorChange("빨간색");
            else EnemyInfoImg.color = ColorManager.ColorChange("하얀색");
        }
        else if (MoveDifficultyInStage == 2)
        {
            if (MoveDifficulty == 0) EnemyInfoImg.color = ColorManager.ColorChange("골렘1번색");
            if (MoveDifficulty == 1) EnemyInfoImg.color = ColorManager.ColorChange("골렘2번색");
            if (MoveDifficulty == 2) EnemyInfoImg.color = ColorManager.ColorChange("골렘3번색");
            if (MoveDifficulty == 3) EnemyInfoImg.color = ColorManager.ColorChange("골렘4번색");

        } 
        else EnemyInfoImg.color = ColorManager.ColorChange("하얀색");


        Enemy_InfoText[0].text = "<color=lightblue>" + TextFormatter.GetThousandCommaText((int)maxHealth) + "</color>";
        Enemy_InfoText[1].text = "<color=lightblue>" + TextFormatter.GetThousandCommaText((long)moneyReward) + "</color>";
        Enemy_InfoText[2].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_00(experienceValue) + "</color>";
        Enemy_InfoText[3].text = "<color=lightblue>" + TextFormatter.GetThousandCommaText(redStone) + "</color>";
        Enemy_InfoText[4].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText_00(scrollDrop) + "%</color>";
    }


    // 몹 리스트 열기 - 체크했음
    private void EnemyListPanel(int index)
    {
        MoveDifficultyInStage = index;

        // 적 난이도 텍스트 변경

        for (int i = 0; i < 4; i++)
        {
            Enemy_DifficultyText[i].text = DifficultyValue[index * 4 + i];
            EnemyDifficultyImg[i].sprite = EnemyImg[index * 4 + i].sprite;

        }

        // 비급서 이미지 텍스트 변경
        EnemyDifficultyImg[4].sprite = MS_Sprite[index];

        MobText.text = ScrollMobText[index];

        // 클리어 확인
        int[] ClearCheck;

        ClearCheck = new int[4];

        for (int i = 0; i < 18; i++)
        {
            if (index == 0)
            {
                if (GameManager.stageClearDict["EasySlime"][i] == true) ClearCheck[0]++;
                if (GameManager.stageClearDict["NormalSlime"][i] == true) ClearCheck[1]++;
                if (GameManager.stageClearDict["HardSlime"][i] == true) ClearCheck[2]++;
                if (GameManager.stageClearDict["ExtremeSlime"][i] == true) ClearCheck[3]++;
            }
            if (index == 1)
            {
                if (GameManager.stageClearDict["EasyWolf"][i] == true) ClearCheck[0]++;
                if (GameManager.stageClearDict["NormalWolf"][i] == true) ClearCheck[1]++;
                if (GameManager.stageClearDict["HardWolf"][i] == true) ClearCheck[2]++;
                if (GameManager.stageClearDict["ExtremeWolf"][i] == true) ClearCheck[3]++;
            }
            else if(index == 2)
            {
                if (GameManager.stageClearDict["EasyGolem"][i] == true) ClearCheck[0]++;
                if (GameManager.stageClearDict["NormalGolem"][i] == true) ClearCheck[1]++;
                if (GameManager.stageClearDict["HardGolem"][i] == true) ClearCheck[2]++;
                if (GameManager.stageClearDict["ExtremeGolem"][i] == true) ClearCheck[3]++;

            }
        }

        if(MoveDifficultyInStage == 0)
        {
            EnemyDifficultyImg[0].color = ColorManager.ColorChange("하얀색");
            EnemyDifficultyImg[1].color = ColorManager.ColorChange("하얀색");
            EnemyDifficultyImg[2].color = ColorManager.ColorChange("하얀색");
            EnemyDifficultyImg[3].color = ColorManager.ColorChange("빨간색");
        }
        else if(MoveDifficultyInStage == 1)
        {
            EnemyDifficultyImg[0].color = ColorManager.ColorChange("하얀색");
            EnemyDifficultyImg[1].color = ColorManager.ColorChange("하얀색");
            EnemyDifficultyImg[2].color = ColorManager.ColorChange("하얀색");
            EnemyDifficultyImg[3].color = ColorManager.ColorChange("하얀색");
        }
        else if (MoveDifficultyInStage == 2)
        {
            EnemyDifficultyImg[0].color = ColorManager.ColorChange("골렘1번색");
            EnemyDifficultyImg[1].color = ColorManager.ColorChange("골렘2번색");
            EnemyDifficultyImg[2].color = ColorManager.ColorChange("골렘3번색");
            EnemyDifficultyImg[3].color = ColorManager.ColorChange("골렘4번색");
        }




        for (int i = 0; i < 4; i++)
        {
            MobClearValue[i].text = ClearCheck[i] + " / 18";
            if (ClearCheck[i] > 0)
            {
                BrokingPanel[i].SetActive(false);
                EnemyDifficulty_SelBtn[i].interactable = true;
            }
            else
            {
                BrokingPanel[i].SetActive(true);
                EnemyDifficulty_SelBtn[i].interactable = false;
            }
        }
        ExitBtn.gameObject.SetActive(true);
        ExitSelBtn.gameObject.SetActive(false);



        if (MoveDifficultyInStage == 0) EnemyDifficulty_ReturnBtn.gameObject.SetActive(false);
        else EnemyDifficulty_ReturnBtn.gameObject.SetActive(true);

        if (MoveDifficultyInStage == Enemy.Length / 4 - 1) EnemyDifficulty_NextBtn.gameObject.SetActive(false);
        else EnemyDifficulty_NextBtn.gameObject.SetActive(true);


        EnemyLevelPanel.SetActive(true);
        EnemyDifficultyPanel.SetActive(true);
    }
    // ChangeSel 버튼 색 관리
    public void ChangeButtonColor(Button button, string color)
    {
        // 버튼의 이미지 컴포넌트 가져오기
        Image buttonImage = button.GetComponent<Image>();
        // 버튼의 이미지 색상 변경
        buttonImage.color = ColorManager.ColorChange(color);
    }


    // 몹 체력 바 Value 관리
    private void Handle()
    {
        Enemy_HPBar.value = (float)(currentHP / maxHP); // 최대 체력에 대한 현재 체력의 비율로 설정
    }

    // 몹 체력 바 Text 관리
    public void UpdateMonsterHealthText()
    {
        HP_Text();
    }

    // 체력 텍스트 관리
    private void HP_Text()
    {
        // 현재 체력이 음수가 되지 않도록 조정
        currentHP = Mathf.Clamp((float)currentHP, 0f, (float)maxHP);

        double healthPercentage = (currentHP / maxHP) * 100f;

        if (healthPercentage >= 10)
            Enemy_HPText.text = healthPercentage.ToString("00.00") + " %";
        else
            Enemy_HPText.text = healthPercentage.ToString("0.00") + " %";
    }


    // 다음 넘기기
    private void EnemyListPanelNext()
    {
        MoveDifficultyInStage++;

        if (MoveDifficultyInStage == Enemy.Length / 4 - 1) EnemyDifficulty_NextBtn.gameObject.SetActive(false);
        else EnemyDifficulty_NextBtn.gameObject.SetActive(true);
        EnemyListPanel(MoveDifficultyInStage);
    }

    // 이전으로 오기
    private void EnemyListPanelReturn()
    {
        MoveDifficultyInStage--;

        if (MoveDifficultyInStage == 0) EnemyDifficulty_ReturnBtn.gameObject.SetActive(false);
        else EnemyDifficulty_ReturnBtn.gameObject.SetActive(true);
        EnemyListPanel(MoveDifficultyInStage);
    }
}