using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss {

    public string BossName { get; set; }
    public double BossMaxHealth { get; set; }
    public int DiamondReward { get; set; }
    public int BossArmor { get; set; }
    public int BossLevel { get; set; }
    public Boss(string name, double maxhealth, int diamond, int bossarmor, int bosslevel)
    {
        BossName = name;
        BossMaxHealth = maxhealth;
        DiamondReward = diamond;
        BossArmor = bossarmor;
        BossLevel = bosslevel;
    }
};


public class BossManager : MonoBehaviour
{
    private static BossManager instance;

    public static BossManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BossManager>();
                if (instance == null)
                {
                    Debug.LogError("BossManager 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }


    private List<Boss> boss = new();

    public Button BossListOpenBtn;
    public Button BossListOutBtn;
    public Button BossOutBtn;

    public Button BossNextBtn;
    public Button BossReturnBtn;
    public Button[] BossSummonsBtn;

    public GameObject BossList;
    public GameObject BossPanel;

    public GameObject BossBox;
    public GameObject HuntBox;

    public Image[] BossImg;
    public GameObject[] BossingPigeon;
    public Sprite[] BossSprite;
    public Text[] BossTitle;
    public Text[] BossArmor;
    public Text[] BossHP;
    public Text[] BossDiamondReward;
    public Text[] BossSummonsBtnText;

    public Text BossingTitle;
    public Text BossingHP;
    public Text BossingArmor;
    public Text BossingHPText;
    public Text BossingUTitle;
    public Slider BossingSlider;


    public Text HuntTitleText;
    public Text HuntDiamondReward;
    public Image HuntBossImg;
    public Button HuntOutBtn;

    public static bool BossAttackStart = false;

    public static int BossInStage = -1;
    public static int Bossindex;
    public static bool fighting = false;
    public static bool bossHit = false;
    public static double bossCurHealth;
    public static int bossArmor;
    public static double bossMaxHealth;

    public static bool[] ClearBoss = new bool[120];

    private string[] StageName = new string[4];

    void Awake()
    {
        InitializeMonsters();
    }
    private void InitializeMonsters()
    {
        Boss[] pigeon = new Boss[]
        {
            new Boss("공원 비둘기 <color=lime>I</color>",    300,  300, 5,  0),
            new Boss("공원 비둘기 <color=lime>II</color>",   500,  350, 10, 0),
            new Boss("공원 비둘기 <color=lime>III</color>",  700,  400, 15, 0),
            new Boss("공원 비둘기 <color=lime>IV</color>",   1000, 450, 20, 0),
            new Boss("공원 비둘기 <color=lime>V</color>",    1500, 500, 25, 0),
            new Boss("공원 비둘기 <color=lime>VI</color>",   2000, 550, 30, 0),

            new Boss("공원 비둘기 <color=lime>VII</color>",  2800, 600, 40, 0),
            new Boss("공원 비둘기 <color=lime>VIII</color>", 3600, 650, 50, 0),
            new Boss("공원 비둘기 <color=lime>IX</color>",   4400, 700, 60, 0),
            new Boss("공원 비둘기 <color=lime>X</color>",    5200, 750, 70, 0),
            new Boss("공원 비둘기 <color=lime>XI</color>",   6000, 800, 80, 0),
            new Boss("공원 비둘기 <color=red>R</color>",     8000, 850, 90, 0),

            new Boss("동네 비둘기 <color=lime>I</color>",    9200,  900,  100, 1),
            new Boss("동네 비둘기 <color=lime>II</color>",   10400, 950,  110, 1),
            new Boss("동네 비둘기 <color=lime>III</color>",  11600, 1000, 120, 1),
            new Boss("동네 비둘기 <color=lime>IV</color>",   12800, 1050, 130, 1),
            new Boss("동네 비둘기 <color=lime>V</color>",    14000, 1100, 140, 1),
            new Boss("동네 비둘기 <color=lime>VI</color>",   17000, 1150, 150, 1),

            new Boss("동네 비둘기 <color=lime>VII</color>",  19000, 1200, 160, 1),
            new Boss("동네 비둘기 <color=lime>VIII</color>", 21000, 1250, 170, 1),
            new Boss("동네 비둘기 <color=lime>XI</color>",   23000, 1300, 180, 1),
            new Boss("동네 비둘기 <color=lime>X</color>",    25000, 1350, 190, 1),
            new Boss("동네 비둘기 <color=lime>XI</color>",   27000, 1400, 200, 1),
            new Boss("동네 비둘기 <color=red>R</color>",     32000, 1450, 210, 1),

            new Boss("마을 비둘기 <color=lime>I</color>",    35000, 1500, 220, 2),
            new Boss("마을 비둘기 <color=lime>II</color>",   38000, 1550, 230, 2),
            new Boss("마을 비둘기 <color=lime>III</color>",  41000, 1600, 240, 2),
            new Boss("마을 비둘기 <color=lime>IV</color>",   44000, 1650, 250, 2),
            new Boss("마을 비둘기 <color=lime>V</color>",    47000, 1700, 260, 2),
            new Boss("마을 비둘기 <color=lime>VI</color>",   55000, 1750, 270, 2),

            new Boss("마을 비둘기 <color=lime>VII</color>",  59000, 1800, 280, 2),
            new Boss("마을 비둘기 <color=lime>VIII</color>", 63000, 1850, 290, 2),
            new Boss("마을 비둘기 <color=lime>XI</color>",   67000, 1900, 300, 2),
            new Boss("마을 비둘기 <color=lime>X</color>",    71000, 1950, 310, 2),
            new Boss("마을 비둘기 <color=lime>XI</color>",   75000, 2000, 320, 2),
            new Boss("마을 비둘기 <color=red>R</color>",     85000, 2050, 330, 2),

            new Boss("읍내 비둘기 <color=lime>I</color>",    91000,  2100, 340, 3),
            new Boss("읍내 비둘기 <color=lime>II</color>",   97000,  2150, 350, 3),
            new Boss("읍내 비둘기 <color=lime>III</color>",  103000, 2200, 360, 3),
            new Boss("읍내 비둘기 <color=lime>IV</color>",   109000, 2250, 370, 3),
            new Boss("읍내 비둘기 <color=lime>V</color>",    115000, 2300, 380, 3),
            new Boss("읍내 비둘기 <color=lime>VI</color>",   130000, 2350, 390, 3),

            new Boss("읍내 비둘기 <color=lime>VII</color>",  138000, 2400, 400, 3),
            new Boss("읍내 비둘기 <color=lime>VIII</color>", 146000, 2450, 410, 3),
            new Boss("읍내 비둘기 <color=lime>XI</color>",   154000, 2500, 420, 3),
            new Boss("읍내 비둘기 <color=lime>X</color>",    162000, 2550, 430, 3),
            new Boss("읍내 비둘기 <color=lime>XI</color>",   170000, 2600, 440, 3),
            new Boss("읍내 비둘기 <color=red>R</color>",     200000, 2650, 450, 3),

            new Boss("구청 비둘기 <color=lime>I</color>",    240000, 2700, 460, 4),
            new Boss("구청 비둘기 <color=lime>II</color>",   280000, 2750, 470, 4),
            new Boss("구청 비둘기 <color=lime>III</color>",  320000, 2800, 480, 4),
            new Boss("구청 비둘기 <color=lime>IV</color>",   360000, 2850, 490, 4),
            new Boss("구청 비둘기 <color=lime>V</color>",    400000, 2900, 500, 4),
            new Boss("구청 비둘기 <color=lime>VI</color>",   440000, 2950, 510, 4),

            new Boss("구청 비둘기 <color=lime>VII</color>",  480000, 3000, 520, 4),
            new Boss("구청 비둘기 <color=lime>VIII</color>", 520000, 3050, 530, 4),
            new Boss("구청 비둘기 <color=lime>XI</color>",   560000, 3100, 540, 4),
            new Boss("구청 비둘기 <color=lime>X</color>",    600000, 3150, 550, 4),
            new Boss("구청 비둘기 <color=lime>XI</color>",   640000, 3200, 560, 4),
            new Boss("구청 비둘기 <color=red>R</color>",     680000, 3250, 570, 4),

            new Boss("군청 비둘기 <color=lime>I</color>",    730000, 3300, 580, 5),
            new Boss("군청 비둘기 <color=lime>II</color>",   780000, 3350, 590, 5),
            new Boss("군청 비둘기 <color=lime>III</color>",  830000, 3400, 600, 5),
            new Boss("군청 비둘기 <color=lime>IV</color>",   880000, 3450, 610, 5),
            new Boss("군청 비둘기 <color=lime>V</color>",    930000, 3500, 620, 5),
            new Boss("군청 비둘기 <color=lime>VI</color>",   980000, 3550, 630, 5),

            new Boss("군청 비둘기 <color=lime>VII</color>",  1030000, 3600, 640, 5),
            new Boss("군청 비둘기 <color=lime>VIII</color>", 1080000, 3650, 650, 5),
            new Boss("군청 비둘기 <color=lime>XI</color>",   1130000, 3700, 660, 5),
            new Boss("군청 비둘기 <color=lime>X</color>",    1180000, 3750, 670, 5),
            new Boss("군청 비둘기 <color=lime>XI</color>",   1230000, 3800, 680, 5),
            new Boss("군청 비둘기 <color=red>R</color>",     1280000, 3850, 690, 5),

            new Boss("시청 비둘기 <color=lime>I</color>",    1350000, 3900, 700, 6),
            new Boss("시청 비둘기 <color=lime>II</color>",   1420000, 3950, 710, 6),
            new Boss("시청 비둘기 <color=lime>III</color>",  1490000, 4000, 720, 6),
            new Boss("시청 비둘기 <color=lime>IV</color>",   1560000, 4050, 730, 6),
            new Boss("시청 비둘기 <color=lime>V</color>",    1630000, 4100, 740, 6),
            new Boss("시청 비둘기 <color=lime>VI</color>",   1700000, 4150, 750, 6),

            new Boss("시청 비둘기 <color=lime>VII</color>",  1770000, 4200, 760, 6),
            new Boss("시청 비둘기 <color=lime>VIII</color>", 1840000, 4250, 770, 6),
            new Boss("시청 비둘기 <color=lime>XI</color>",   1910000, 4300, 780, 6),
            new Boss("시청 비둘기 <color=lime>X</color>",    1980000, 4350, 790, 6),
            new Boss("시청 비둘기 <color=lime>XI</color>",   2050000, 4400, 800, 6),
            new Boss("시청 비둘기 <color=red>R</color>",     2120000, 4450, 810, 6),

            new Boss("도시 비둘기 <color=lime>I</color>",    2210000, 4500, 820, 7),
            new Boss("도시 비둘기 <color=lime>II</color>",   2300000, 4550, 830, 7),
            new Boss("도시 비둘기 <color=lime>III</color>",  2390000, 4600, 840, 7),
            new Boss("도시 비둘기 <color=lime>IV</color>",   2480000, 4650, 850, 7),
            new Boss("도시 비둘기 <color=lime>V</color>",    2570000, 4700, 860, 7),
            new Boss("도시 비둘기 <color=lime>VI</color>",   2680000, 4750, 870, 7),

            new Boss("도시 비둘기 <color=lime>VII</color>",  2770000, 4800, 880, 7),
            new Boss("도시 비둘기 <color=lime>VIII</color>", 2860000, 4850, 890, 7),
            new Boss("도시 비둘기 <color=lime>XI</color>",   2950000, 4900, 900, 7),
            new Boss("도시 비둘기 <color=lime>X</color>",    3040000, 4950, 910, 7),
            new Boss("도시 비둘기 <color=lime>XI</color>",   3130000, 5000, 920, 7),
            new Boss("도시 비둘기 <color=red>R</color>",     3220000, 5050, 930, 7),

            new Boss("국회 비둘기 <color=lime>I</color>",    3320000, 5100, 940, 8),
            new Boss("국회 비둘기 <color=lime>II</color>",   3420000, 5150, 950, 8),
            new Boss("국회 비둘기 <color=lime>III</color>",  3520000, 5200, 960, 8),
            new Boss("국회 비둘기 <color=lime>IV</color>",   3620000, 5250, 970, 8),
            new Boss("국회 비둘기 <color=lime>V</color>",    3720000, 5300, 980, 8),
            new Boss("국회 비둘기 <color=lime>VI</color>",   3820000, 5350, 990, 8),

            new Boss("국회 비둘기 <color=lime>VII</color>",  3920000, 5400, 1000, 8),
            new Boss("국회 비둘기 <color=lime>VIII</color>", 4020000, 5450, 1010, 8),
            new Boss("국회 비둘기 <color=lime>XI</color>",   4120000, 5500, 1020, 8),
            new Boss("국회 비둘기 <color=lime>X</color>",    4220000, 5550, 1030, 8),
            new Boss("국회 비둘기 <color=lime>XI</color>",   4320000, 5600, 1040, 8),
            new Boss("국회 비둘기 <color=red>R</color>",     4420000, 5650, 1050, 8),

            new Boss("청와대 비둘기 <color=lime>I</color>",    4540000, 5700, 1060, 9),
            new Boss("청와대 비둘기 <color=lime>II</color>",   4660000, 5750, 1070, 9),
            new Boss("청와대 비둘기 <color=lime>III</color>",  4780000, 5800, 1080, 9),
            new Boss("청와대 비둘기 <color=lime>IV</color>",   4900000, 5850, 1090, 9),
            new Boss("청와대 비둘기 <color=lime>V</color>",    5020000, 5900, 1100, 9),
            new Boss("청와대 비둘기 <color=lime>VI</color>",   5140000, 5950, 1110, 9),

            new Boss("청와대 비둘기 <color=lime>VII</color>",  5260000, 6000, 1120, 9),
            new Boss("청와대 비둘기 <color=lime>VIII</color>", 5380000, 6050, 1130, 9),
            new Boss("청와대 비둘기 <color=lime>XI</color>",   5500000, 6100, 1140, 9),
            new Boss("청와대 비둘기 <color=lime>X</color>",    5620000, 6150, 1150, 9),
            new Boss("청와대 비둘기 <color=lime>XI</color>",   5740000, 6200, 1160, 9),
            new Boss("청와대 비둘기 <color=red>R</color>",     5860000, 6250, 1170, 9),


        };

        

        // 다른 몬스터들을 몬스터 리스트에 추가
        boss.AddRange(pigeon);
    }
    // Start is called before the first frame update
    private void Start()
    {
        BossListOpenBtn.onClick.AddListener(BossListOpen);
        BossListOutBtn.onClick.AddListener(BossListOut);
        BossOutBtn.onClick.AddListener(BossOut);
        BossNextBtn.onClick.AddListener(BossNext);
        BossReturnBtn.onClick.AddListener(BossReturn);
        HuntOutBtn.onClick.AddListener(HuntOut);

        BossInStage = 0;
        Bossindex = 0;


        BossingSlider.value = 1f;

        for (int i = 0; i < BossSummonsBtn.Length; i++)
        {
            int index = i;

            BossSummonsBtn[index].onClick.AddListener(() => BossSummons(index));
        }

        StageName = new string[10]
        {
            "평화로운 공원 한바퀴",
            "북적거리는 동네 한바퀴",
            "소란스러운 마을 한바퀴",
            "시끌시끌한 읍내 한바퀴",
            "조용한 구청 한바퀴",
            "복잡한 군청 한바퀴",
            "신나는 시청 한바퀴",
            "높은 건물 도시 한바퀴",
            "멀리보는 국회 한바퀴",
            "빛나는 청와대 한바퀴",

        };

    }

    private void Update()
    {
        if(fighting == true)
        {
            BossHPUpdate(Bossindex);
            StartCoroutine(RegenHP());
            if (bossCurHealth <= 0)
            {
                HuntReward();
            }
        }
    }
    private void HuntOut()
    {
        BossPanel.SetActive(false);
        HuntBox.SetActive(false);
        HuntOutBtn.gameObject.SetActive(false);
    }
    private void HuntReward()
    {
        Boss boss;

        boss = GetBossByIndex(Bossindex);

        fighting = false;
        GameManager.Player_Diamond += BossReward();
        ClearBoss[Bossindex] = true;
        BossBox.SetActive(false);
        HuntBox.SetActive(true);
        HuntOutBtn.gameObject.SetActive(true);
        HuntBossImg.sprite = BossSprite[boss.BossLevel];
        HuntTitleText.text = boss.BossName;
        HuntDiamondReward.text = "x " + boss.DiamondReward;
    }

    private void BossListUpdate()
    {
        Boss boss;

        // 0 1 2 3 4 5              // i + (N * 5)
        // 6 7 8 9 10 11            // i + (N * 5)
        // 12 13 14 15 16 17        // i + (N * 5)

        for (int i = 0; i < 6; i++)
        {
            boss = GetBossByIndex(i + (BossInStage * 5) + BossInStage);

            BossImg[i].sprite = BossSprite[boss.BossLevel];
            BossTitle[i].text = boss.BossName;
            BossHP[i].text = "체력 : "+ TextFormatter.GetThousandCommaText((long)boss.BossMaxHealth);
            BossArmor[i].text = "방어력 : " + TextFormatter.GetThousandCommaText(boss.BossArmor);
            BossDiamondReward[i].text = "x " + TextFormatter.GetThousandCommaText(boss.DiamondReward);

            Image button = BossSummonsBtn[i].GetComponent<Image>();

            if (ClearBoss[i + (BossInStage * 5) + BossInStage] == true)
            {
                BossSummonsBtnText[i].text = "성공";
                BossSummonsBtn[i].interactable = false;
                button.color = ColorManager.ColorChange("빨간색");
            }
            else
            {
                BossSummonsBtn[i].interactable = true;
                BossSummonsBtnText[i].text = "도전";
                button.color = ColorManager.ColorChange("민트색");
            }
        }

        Debug.Log("현재 보스 스테이지 : " + BossInStage);

        if (BossInStage == 0) BossReturnBtn.gameObject.SetActive(false);
        else BossReturnBtn.gameObject.SetActive(true);

        if (BossInStage == 19) BossNextBtn.gameObject.SetActive(false);
        else BossNextBtn.gameObject.SetActive(true);

    }

    public void BossListOpen()
    {
        BossListUpdate();
        BossBox.SetActive(true);
        BossList.SetActive(true);
    }
    private void BossListOut()
    {
        BossList.SetActive(false);
    }
    private void BossNext()
    {
        BossInStage++;
        BossListUpdate();
    }
    private void BossReturn()
    {
        BossInStage--;
        BossListUpdate();
    }
    private void BossOut()
    {
        BossPanel.SetActive(false);
        fighting = false;
    }

    private void BossSummons(int index)
    {
        BossList.SetActive(false);
        BossPanel.SetActive(true);
        BossAttackStart = false;
        fighting = true;

        Boss boss;
        boss = GetBossByIndex((BossInStage * 5) + index + BossInStage);
        Bossindex = (BossInStage * 5) + index + BossInStage;

        BossingTitle.text = boss.BossName;
        BossingUTitle.text = StageName[boss.BossLevel];
        BossingHP.text = "<color=red>체력</color> : " + TextFormatter.GetFloatPointCommaText_00((float)boss.BossMaxHealth);
        BossingArmor.text = "<color=teal>방어력</color> : " + TextFormatter.GetThousandCommaText(boss.BossArmor);

        for (int i = 0; i < BossingPigeon.Length; i++)
        {
            if (i == boss.BossLevel)
            {
                BossingPigeon[i].SetActive(true);
            }
            else
            {
                BossingPigeon[i].SetActive(false);
            }
        }

        bossCurHealth = boss.BossMaxHealth;
        bossMaxHealth = boss.BossMaxHealth;
        bossArmor = boss.BossArmor;

    }

    private void BossHPUpdate(int index)
    {
        Boss boss;

        boss = GetBossByIndex(index);


        // 현재 체력이 음수가 되지 않도록 조정
        bossCurHealth = Mathf.Clamp((float)bossCurHealth, 0f, (float)boss.BossMaxHealth);

        double healthPercentage = (bossCurHealth / boss.BossMaxHealth) * 100f;

        if (healthPercentage >= 10)
            BossingHPText.text = healthPercentage.ToString("00.00") + " %";
        else
            BossingHPText.text = healthPercentage.ToString("0.00") + " %";

        BossingSlider.value = (float)(bossCurHealth / boss.BossMaxHealth); // 최대 체력에 대한 현재 체력의 비율로 설정
    }

    private int BossReward()
    {
        Boss boss;
        boss = GetBossByIndex(Bossindex);

        return boss.DiamondReward;
    }

    private Boss GetBossByIndex(int index)
    {
        // index에 해당하는 슬라임 정보 반환
        return boss[index];
    }

    private IEnumerator RegenHP() // IEnumerator 반환 타입으로 변경
    {
        yield return new WaitForSeconds(0.1f);
        if(bossCurHealth < bossMaxHealth) bossCurHealth += bossMaxHealth / 300f;
        else bossCurHealth = bossMaxHealth;
    }
}
