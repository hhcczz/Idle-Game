using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;   //UI 클릭시 터치 이벤트 발생 방지.

public struct NormalRock
{
    public string name;
    public float Curhealth;
    public float Maxhealth;
    public float BrownDust;
    public float OrangeDust;
    public float LimeDust;
    public float BlackDust;
    public float RedDust;
    public float YellowDust;
    public float DirtDust;
    public float BlueDust;
    public float PurpleDust;
    public float ReinforceScroll;
     
    // 몬스터 정보를 설정하는 메서드
    public void SetRockInfo(string newName, float currentHealth, float maxHealth, float _BrownDust, float _OrangeDust, float _LimeDust, float _BlackDust, float _RedDust, float _YellowDust,
        float _DirtDust, float _BlueDust, float _PurpleDust, float _ReinforceScroll)
    {
        name = newName;
        Curhealth = currentHealth;
        Maxhealth = maxHealth;
        BrownDust = _BrownDust;
        OrangeDust = _OrangeDust;
        LimeDust = _LimeDust;
        BlackDust = _BlackDust;
        RedDust = _RedDust;
        YellowDust = _YellowDust;
        DirtDust = _DirtDust;
        BlueDust = _BlueDust;
        PurpleDust = _PurpleDust;
        ReinforceScroll = _ReinforceScroll;
    }
}

[System.Serializable]
public struct Rock
{
    public NormalRock[] n_rocks;
}

public class MinerManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject TextPrefab;

    private AudioSource audioSource; // AudioSource 변수 추가

    public AudioClip MiningSoundClip; // AudioClip 변수 선언

    [System.Obsolete]

    public RectTransform touchableArea; // 터치 가능한 영역을 지정하기 위한 RectTransform

    public Image[] Upstair_HaveImg;

    public Image[] PickaxeNeedImg_1;
    public Image[] PickaxeNeedImg_2;
    public Image[] PickaxeNeedImg_3;

    public Image[] MineralNeedImg_1;
    public Image[] MineralNeedImg_2;
    public Image[] MineralNeedImg_3;

    public Image[] OptionNeedImg_1;
    public Image[] OptionNeedImg_2;
    public Image[] OptionNeedImg_3;

    public Text[] Own_MineralText;                  //  가지고 있는 광물 텍스트
    public Text[] BasicInfo;                        //  광산 기본 정보

    public Text[] Pickaxe_DamageNeeditem;           //  필요한 광물 텍스트  EX) 곡괭이 데미지 증가때 사용
    public Text[] Pickaxe_CriDamageNeeditem;        //  필요한 광물 텍스트  EX) 곡괭이 크리티컬 데미지 증가때 사용
    public Text[] Pickaxe_CriChanceNeeditem;        //  필요한 광물 텍스트  EX) 곡괭이 크리티컬 확률 증가때 사용

    public Text[] Mineral_NeeditemMI;               //  광물 증가량      Mineral acquisition amount
    public Text[] Mineral_NeeditemHP;               //  광물 약점        Mineral Weakness
    public Text[] Mineral_NeeditemRS;               //  강화 스크롤      Reinforce Scroll

    public Text[] Option_NeeditemPMA;               //  데미지 증폭      Pickaxe Damage amplification Scroll
    public Text[] Option_NeeditemMB;                //  럭키 광물        Mineral Bomb
    public Text[] Option_NeeditemPFD;               //  치명적 피해      Pickaxe Fatal Damage

    public Text Rocks_Name;                         //  돌 이름
    public Slider Rocks_Slider;                     //  돌 체력 슬라이더
    public Text Rocks_HPText;                       //  돌 체력 텍스트

    public Button[] PickaxeLevelUpBtn;              //  곡괭이 레벨업
    public Button[] MineralLevelUpBtn;              //  광물 레벨업
    public Button[] OptionLevelUpBtn;               //  옵션 레벨업

    public Text[] PickaxeLevelText;                 //  곡괭이 레벨 텍스트
    public Text[] PickaxeLeftText;                  //  곡괭이 능력치 증가전 표시
    public Text[] PickaxeRightText;                 //  곡괭이 능력치 증가후 표시

    public Text[] MineralLevelText;                 //  광물 레벨 텍스트
    public Text[] MineralLeftText;                  //  광물 능력치 증가전 표시
    public Text[] MineralRightText;                 //  광물 능력치 증가후 표시

    public Text[] OptionLevelText;                  //  옵션 레벨 텍스트
    public Text[] OptionLeftText;                   //  옵션 능력치 증가전 표시
    public Text[] OptionRightText;                  //  옵션 능력치 증가후 표시

    public Button[] PickaxeUpgradeBtn;
    public Button[] MineralUpgradeBtn;
    public Button[] OptionUpgradeBtn;
    public GameObject PickaxeUpgradePanel;
    public GameObject MineralUpgradePanel;
    public GameObject OptionUpgradePanel;
    public GameObject BasicPanel;
    public GameObject UpgradePanel;
    public Text UpgradeTitleText;

    //public GameObject OptionUpgradePanel;

    //public GameObject TouchPanel;  // 특정 GameObject로 터치 영역을 지정합니다.
    //public RectTransform TouchpanelRect;  // 패널의 RectTransform을 Unity 에디터에서 할당해야 함

    public static float RocksCurHP;
    public static float RocksMaxHP;

    public static float RocksNormalDamage;
    public static float RocksCriticalDamage;
    private bool isPickaxeActive = false;
    private bool isMineralActive = false;
    private bool isOptionActive = false;

    // private bool isOptionActive = false;

    public GameObject effectPrefab; // 이펙트 프리팹
    public RectTransform parentRectTransform; // 부모 Rect Transform

    bool RocksInfoOpen = false;

    public static Rock rock;

    public Text[] Rock_DifficultyText;             //  적 난이도 텍스트
    public Text[] Rock_InfoText;                    //  적 정보 텍스트

    public Button RockListOpenBtn;        //  적 레벨 패널 오픈 버튼
    public GameObject Rock_LevelSelPanel;          //  적 난이도 선택 패널
    public GameObject Rock_LevelListPanel;             //  적 레벨 패널
    public GameObject Rock_DifficultyPanel;             //  적 난이도 패널
    public Button[] Rock_DifficultyBtn;                 //  적 난이도 버튼
    public Button[] Rock_LevelSelBtn;                 //  적 레벨 버튼
    public Button Rock_SummonsBtn;                 //  적 소환 버튼
    public Button RockListExitBtn;

    private int lastIndex;

    int index;

    public static int executions = 0;

    public GameObject lackBG;
    public Button lackoutBtn;

    void Start()
    {
        // 돌 생성 및 설정
        rock.n_rocks = new NormalRock[12];

        rock.n_rocks[0].SetRockInfo("일반 돌덩이 <color=lime>I</color>", 6, 6, 100, 0, 0f, 0f, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[1].SetRockInfo("일반 돌덩이 <color=lime>II</color>", 10, 10, 99f, 1, 0, 0, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[2].SetRockInfo("일반 돌덩이 <color=lime>III</color>", 20, 20, 98f, 2f, 0, 0, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[3].SetRockInfo("일반 돌덩이 <color=lime>IV</color>", 32, 32, 90, 97f, 3f, 0, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[4].SetRockInfo("일반 돌덩이 <color=lime>V</color>", 44, 44, 90, 96f, 4f, 0, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[5].SetRockInfo("일반 돌덩이 <color=lime>VI</color>", 60, 60, 95f, 4f, 1f, 0, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[6].SetRockInfo("일반 돌덩이 <color=lime>VII</color>", 80, 80, 94f, 5f, 1f, 0, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[7].SetRockInfo("일반 돌덩이 <color=lime>VIII</color>", 100, 100, 93f, 6f, 1f, 0, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[8].SetRockInfo("일반 돌덩이 <color=lime>IX</color>", 120, 120, 92f, 7f, 1f, 0, 0, 0, 0, 0, 0, 0);
        rock.n_rocks[9].SetRockInfo("일반 돌덩이 <color=lime>IX</color>", 120, 120, 91f, 7.8f, 1.1f, 0, 0, 0, 0, 0, 0, 0.1f);
        rock.n_rocks[10].SetRockInfo("일반 돌덩이 <color=lime>X</color>", 150, 150, 90f, 8.6f, 1.2f, 0, 0, 0, 0, 0, 0, 0.2f);
        rock.n_rocks[11].SetRockInfo("일반 돌덩이 <color=lime>XI</color>", 180, 180, 88f, 9.6f, 2, 0.2f, 0, 0, 0, 0, 0, 0.2f);

        

        ChangeRockInfo(0);
        Rock_Summons(); // 첫 번째 적 생성

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        RocksCurHP = RocksMaxHP;

        lackoutBtn.onClick.AddListener(LackOut);

        RockListOpenBtn.onClick.AddListener(() => Rock_LevelListPanel.SetActive(true));
        RockListOpenBtn.onClick.AddListener(() => Rock_DifficultyPanel.SetActive(true));
        RockListExitBtn.onClick.AddListener(RockListExitPanel);
        Rock_SummonsBtn.onClick.AddListener(Rock_Summons);

        PickaxeUpgradeBtn[0].onClick.AddListener(PickaxeChange);
        PickaxeUpgradeBtn[1].onClick.AddListener(PickaxeChange);

        MineralUpgradeBtn[0].onClick.AddListener(MineralChange);
        MineralUpgradeBtn[1].onClick.AddListener(MineralChange);

        OptionUpgradeBtn[0].onClick.AddListener(OptionChange);
        OptionUpgradeBtn[1].onClick.AddListener(OptionChange);

        for (int i = 0; i < 9; i++)
        {
            Upstair_HaveImg[i].sprite = GameManager.GemSprites[i];

            PickaxeNeedImg_1[i].sprite = GameManager.GemSprites[i];
            PickaxeNeedImg_2[i].sprite = GameManager.GemSprites[i];
            PickaxeNeedImg_3[i].sprite = GameManager.GemSprites[i];

            MineralNeedImg_1[i].sprite = GameManager.GemSprites[i];
            MineralNeedImg_2[i].sprite = GameManager.GemSprites[i];
            MineralNeedImg_3[i].sprite = GameManager.GemSprites[i];

            OptionNeedImg_1[i].sprite = GameManager.GemSprites[i];
            OptionNeedImg_2[i].sprite = GameManager.GemSprites[i];
            OptionNeedImg_3[i].sprite = GameManager.GemSprites[i];
        }

        for (int i = 0; i < Rock_LevelSelBtn.Length; i++)
        {
            int index = i; // 클로저로 인덱스를 보존

            Rock_LevelSelBtn[index].onClick.AddListener(() => ChangeRockInfo(index));
            if (GameManager.RockStageClear[index] == true) Rock_LevelSelBtn[index].interactable = true;
        }

        for (int i = 0; i < Rock_DifficultyBtn.Length; i++)
        {
            int index = i;

            Rock_DifficultyBtn[index].onClick.AddListener(() => RockDifficultyOpen(index));
        }

        // 곡괭이 레벨업 리스너
        for (int i = 0; i < PickaxeLevelUpBtn.Length; i++)
        {
            int index = i;

            PickaxeLevelUpBtn[i].onClick.AddListener(() => PickaxeLevelUP(index));
        }

        // 광물 레벨업 리스너
        for (int i = 0; i < MineralLevelUpBtn.Length; i++)
        {
            int index = i;

            MineralLevelUpBtn[i].onClick.AddListener(() => MineralLevelUP(index));
        }

        // 옵션 레벨업 리스너
        for (int i = 0; i < OptionLevelUpBtn.Length; i++)
        {
            int index = i;

            OptionLevelUpBtn[i].onClick.AddListener(() => OptionLevelUP(index));
        }

        
        UpdateHealth();

        Pickaxe_DamageNeeditem[0].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBrownDust[GameManager.Pickaxe_DamageLv - 1]) + "";
        if (GameManager.Pickaxe_DamageLv >= 10) Pickaxe_DamageNeeditem[1].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedOrangeDust[GameManager.Pickaxe_DamageLv - 1]) + "";
        if (GameManager.Pickaxe_DamageLv >= 30) Pickaxe_DamageNeeditem[2].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedLimeDust[GameManager.Pickaxe_DamageLv - 1]) + "";
        if (GameManager.Pickaxe_DamageLv >= 60) Pickaxe_DamageNeeditem[3].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlackDust[GameManager.Pickaxe_DamageLv - 1]) + "";
        if (GameManager.Pickaxe_DamageLv >= 120) Pickaxe_DamageNeeditem[4].text =  TextFormatter.GetThousandCommaText(GameManager.BasicNeedRedDust[GameManager.Pickaxe_DamageLv - 1]) + "";
        if (GameManager.Pickaxe_DamageLv >= 240) Pickaxe_DamageNeeditem[5].text =  TextFormatter.GetThousandCommaText(GameManager.BasicNeedYellowDust[GameManager.Pickaxe_DamageLv - 1]) + "";
        if (GameManager.Pickaxe_DamageLv >= 480) Pickaxe_DamageNeeditem[6].text =  TextFormatter.GetThousandCommaText(GameManager.BasicNeedDirtDust[GameManager.Pickaxe_DamageLv - 1]) + "";
        if (GameManager.Pickaxe_DamageLv >= 800) Pickaxe_DamageNeeditem[7].text =  TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlueDust[GameManager.Pickaxe_DamageLv - 1]) + "";
        if (GameManager.Pickaxe_DamageLv >= 1500) Pickaxe_DamageNeeditem[8].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedPurpleDust[GameManager.Pickaxe_DamageLv - 1]) + "";

        Pickaxe_CriDamageNeeditem[0].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBrownDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalDamage_Level >= 10) Pickaxe_CriChanceNeeditem[1].text =    TextFormatter.GetThousandCommaText(GameManager.BasicNeedOrangeDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalDamage_Level >= 30) Pickaxe_CriChanceNeeditem[2].text =    TextFormatter.GetThousandCommaText(GameManager.BasicNeedLimeDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalDamage_Level >= 60) Pickaxe_CriChanceNeeditem[3].text =    TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlackDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalDamage_Level >= 120) Pickaxe_CriChanceNeeditem[4].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedRedDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalDamage_Level >= 240) Pickaxe_CriChanceNeeditem[5].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedYellowDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalDamage_Level >= 480) Pickaxe_CriChanceNeeditem[6].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedDirtDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalDamage_Level >= 800) Pickaxe_CriChanceNeeditem[7].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlueDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalDamage_Level >= 1500) Pickaxe_CriChanceNeeditem[8].text =  TextFormatter.GetThousandCommaText(GameManager.BasicNeedPurpleDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";

        Pickaxe_CriChanceNeeditem[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalChance_Level >= 10) Pickaxe_CriChanceNeeditem[1].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalChance_Level >= 30) Pickaxe_CriChanceNeeditem[2].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalChance_Level >= 60) Pickaxe_CriChanceNeeditem[3].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalChance_Level >= 120) Pickaxe_CriChanceNeeditem[4].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalChance_Level >= 240) Pickaxe_CriChanceNeeditem[5].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalChance_Level >= 480) Pickaxe_CriChanceNeeditem[6].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalChance_Level >= 800) Pickaxe_CriChanceNeeditem[7].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
        if (GameManager.Pickaxe_CriticalChance_Level >= 1500) Pickaxe_CriChanceNeeditem[8].text =  TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";

        // 광물

        Mineral_NeeditemMI[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Mineral_LevelMI - 1]) + "";
        if (GameManager.Mineral_LevelMI >= 10) Mineral_NeeditemMI[1].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Mineral_LevelMI - 1]) + "";
        if (GameManager.Mineral_LevelMI >= 30) Mineral_NeeditemMI[2].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Mineral_LevelMI - 1]) + "";
        if (GameManager.Mineral_LevelMI >= 60) Mineral_NeeditemMI[3].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Mineral_LevelMI - 1]) + "";
        if (GameManager.Mineral_LevelMI >= 120) Mineral_NeeditemMI[4].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Mineral_LevelMI - 1]) + "";
        if (GameManager.Mineral_LevelMI >= 240) Mineral_NeeditemMI[5].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Mineral_LevelMI - 1]) + "";
        if (GameManager.Mineral_LevelMI >= 480) Mineral_NeeditemMI[6].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Mineral_LevelMI - 1]) + "";
        if (GameManager.Mineral_LevelMI >= 800) Mineral_NeeditemMI[7].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Mineral_LevelMI - 1]) + "";
        if (GameManager.Mineral_LevelMI >= 1500) Mineral_NeeditemMI[8].text =  TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Mineral_LevelMI - 1]) + "";

        Mineral_NeeditemHP[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Mineral_LevelHP - 1]) + "";
        if (GameManager.Mineral_LevelHP >= 10) Mineral_NeeditemHP[1].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Mineral_LevelHP - 1]) + "";
        if (GameManager.Mineral_LevelHP >= 30) Mineral_NeeditemHP[2].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Mineral_LevelHP - 1]) + "";
        if (GameManager.Mineral_LevelHP >= 60) Mineral_NeeditemHP[3].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Mineral_LevelHP - 1]) + "";
        if (GameManager.Mineral_LevelHP >= 120) Mineral_NeeditemHP[4].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Mineral_LevelHP - 1]) + "";
        if (GameManager.Mineral_LevelHP >= 240) Mineral_NeeditemHP[5].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Mineral_LevelHP - 1]) + "";
        if (GameManager.Mineral_LevelHP >= 480) Mineral_NeeditemHP[6].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Mineral_LevelHP - 1]) + "";
        if (GameManager.Mineral_LevelHP >= 800) Mineral_NeeditemHP[7].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Mineral_LevelHP - 1]) + "";
        if (GameManager.Mineral_LevelHP >= 1500) Mineral_NeeditemHP[8].text =  TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Mineral_LevelHP - 1]) + "";

        Mineral_NeeditemRS[0].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedOrangeDust[GameManager.Mineral_LevelRS - 1]) + "";
        if (GameManager.Mineral_LevelRS >= 10) Mineral_NeeditemRS[1].text =    TextFormatter.GetThousandCommaText(GameManager.BasicNeedOrangeDust[GameManager.Mineral_LevelRS - 1]) + "";
        if (GameManager.Mineral_LevelRS >= 30) Mineral_NeeditemRS[2].text =    TextFormatter.GetThousandCommaText(GameManager.BasicNeedLimeDust[GameManager.Mineral_LevelRS - 1]) + "";
        if (GameManager.Mineral_LevelRS >= 60) Mineral_NeeditemRS[3].text =    TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlackDust[GameManager.Mineral_LevelRS - 1]) + "";
        if (GameManager.Mineral_LevelRS >= 120) Mineral_NeeditemRS[4].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedRedDust[GameManager.Mineral_LevelRS - 1]) + "";
        if (GameManager.Mineral_LevelRS >= 240) Mineral_NeeditemRS[5].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedYellowDust[GameManager.Mineral_LevelRS - 1]) + "";
        if (GameManager.Mineral_LevelRS >= 480) Mineral_NeeditemRS[6].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedDirtDust[GameManager.Mineral_LevelRS - 1]) + "";
        if (GameManager.Mineral_LevelRS >= 800) Mineral_NeeditemRS[7].text =   TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlueDust[GameManager.Mineral_LevelRS - 1]) + "";
        if (GameManager.Mineral_LevelRS >= 1500) Mineral_NeeditemRS[8].text =  TextFormatter.GetThousandCommaText(GameManager.BasicNeedPurpleDust[GameManager.Mineral_LevelRS - 1]) + "";

        // 옵션

        Option_NeeditemPMA[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPMA - 1]) + "";
        if (GameManager.Option_LevelPMA >= 10) Option_NeeditemPMA[1].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPMA - 1]) + "";
        if (GameManager.Option_LevelPMA >= 30) Option_NeeditemPMA[2].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Option_LevelPMA - 1]) + "";
        if (GameManager.Option_LevelPMA >= 60) Option_NeeditemPMA[3].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Option_LevelPMA - 1]) + "";
        if (GameManager.Option_LevelPMA >= 120) Option_NeeditemPMA[4].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Option_LevelPMA - 1]) + "";
        if (GameManager.Option_LevelPMA >= 240) Option_NeeditemPMA[5].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Option_LevelPMA - 1]) + "";
        if (GameManager.Option_LevelPMA >= 480) Option_NeeditemPMA[6].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Option_LevelPMA - 1]) + "";
        if (GameManager.Option_LevelPMA >= 800) Option_NeeditemPMA[7].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Option_LevelPMA - 1]) + "";
        if (GameManager.Option_LevelPMA >= 1500) Option_NeeditemPMA[8].text =  TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelPMA - 1]) + "";

        Option_NeeditemMB[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelMB - 1]) + "";
        if (GameManager.Option_LevelMB >= 10) Option_NeeditemMB[1].text =      TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelMB - 1]) + "";
        if (GameManager.Option_LevelMB >= 30) Option_NeeditemMB[2].text =      TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Option_LevelMB - 1]) + "";
        if (GameManager.Option_LevelMB >= 60) Option_NeeditemMB[3].text =      TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Option_LevelMB - 1]) + "";
        if (GameManager.Option_LevelMB >= 120) Option_NeeditemMB[4].text =     TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Option_LevelMB - 1]) + "";
        if (GameManager.Option_LevelMB >= 240) Option_NeeditemMB[5].text =     TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Option_LevelMB - 1]) + "";
        if (GameManager.Option_LevelMB >= 480) Option_NeeditemMB[6].text =     TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Option_LevelMB - 1]) + "";
        if (GameManager.Option_LevelMB >= 800) Option_NeeditemMB[7].text =     TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Option_LevelMB - 1]) + "";
        if (GameManager.Option_LevelMB >= 1500) Option_NeeditemMB[8].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelMB - 1]) + "";

        Option_NeeditemPFD[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPFD - 1]) + "";
        if (GameManager.Option_LevelPFD >= 10) Option_NeeditemPFD[1].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPFD - 1]) + "";
        if (GameManager.Option_LevelPFD >= 30) Option_NeeditemPFD[2].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Option_LevelPFD - 1]) + "";
        if (GameManager.Option_LevelPFD >= 60) Option_NeeditemPFD[3].text =    TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Option_LevelPFD - 1]) + "";
        if (GameManager.Option_LevelPFD >= 120) Option_NeeditemPFD[4].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Option_LevelPFD - 1]) + "";
        if (GameManager.Option_LevelPFD >= 240) Option_NeeditemPFD[5].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Option_LevelPFD - 1]) + "";
        if (GameManager.Option_LevelPFD >= 480) Option_NeeditemPFD[6].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Option_LevelPFD - 1]) + "";
        if (GameManager.Option_LevelPFD >= 800) Option_NeeditemPFD[7].text =   TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Option_LevelPFD - 1]) + "";
        if (GameManager.Option_LevelPFD >= 1500) Option_NeeditemPFD[8].text =  TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelPFD - 1]) + "";


        PickaxeLevelText[0].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Pickaxe_DamageLv);
        PickaxeLevelText[1].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Pickaxe_CriticalChance_Level);
        PickaxeLevelText[2].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Pickaxe_CriticalDamage_Level);

        PickaxeLeftText[0].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_Damage) + "";
        PickaxeLeftText[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Pickaxe_CriticalChance) + "%";
        PickaxeLeftText[2].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_CriticalDamage) + "%";

        PickaxeRightText[0].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_Damage + 1f) + "";
        PickaxeRightText[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Pickaxe_CriticalChance + 0.1m) + "%";
        PickaxeRightText[2].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_CriticalDamage + 1f) + "%";

        MineralLevelText[0].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Mineral_LevelMI);
        MineralLevelText[1].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Mineral_LevelHP);
        MineralLevelText[2].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Mineral_LevelRS);

        MineralLeftText[0].text = TextFormatter.GetThousandCommaText(GameManager.Mineral_MI) + "";
        MineralLeftText[1].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Mineral_HP) + "%";
        MineralLeftText[2].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Mineral_RS) + "%";

        MineralRightText[0].text = TextFormatter.GetThousandCommaText(GameManager.Mineral_MI + 1) + "";
        MineralRightText[1].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Mineral_HP + 0.01m) + "%";
        MineralRightText[2].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Mineral_RS + 0.1m) + "%";

        OptionLevelText[0].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Option_LevelPMA);
        OptionLevelText[1].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Option_LevelMB);
        OptionLevelText[2].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Option_LevelPFD);

        OptionLeftText[0].text = TextFormatter.GetThousandCommaText((long)GameManager.Option_PMA) + "%";
        OptionLeftText[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Option_MB) + "%";
        OptionLeftText[2].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Option_PFD) + "%";

        OptionRightText[0].text = TextFormatter.GetThousandCommaText((long)GameManager.Option_PMA + 1) + "%";
        OptionRightText[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Option_MB + 0.1m) + "%";
        OptionRightText[2].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Option_PFD + 0.01m) + "%";

        UpgradeTitleText.text = "Upgrade";
        UpdateBasicInfo();
        UpdateInfoValue();
        
    }

    [System.Obsolete]
    void Update()
    {
        Own_MineralText[0].text = TextFormatter.GetThousandCommaText(GameManager.BrownDust) + "";
        Own_MineralText[1].text = TextFormatter.GetThousandCommaText(GameManager.OrangeDust) + "";
        Own_MineralText[2].text = TextFormatter.GetThousandCommaText(GameManager.LimeDust) + "";
        Own_MineralText[3].text = TextFormatter.GetThousandCommaText(GameManager.BlackDust) + "";
        Own_MineralText[4].text = TextFormatter.GetThousandCommaText(GameManager.RedDust) + "";
        Own_MineralText[5].text = TextFormatter.GetThousandCommaText(GameManager.YellowDust) + "";
        Own_MineralText[6].text = TextFormatter.GetThousandCommaText(GameManager.DirtDust) + "";
        Own_MineralText[7].text = TextFormatter.GetThousandCommaText(GameManager.BlueDust) + "";
        Own_MineralText[8].text = TextFormatter.GetThousandCommaText(GameManager.PurpleDust) + "";

        if (RocksInfoOpen == true && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            RocksInfoOpen = false;
        }
        StartCoroutine(RegenHP());
        UpdateHealth();
    }
    private IEnumerator RegenHP() // IEnumerator 반환 타입으로 변경
    {
        yield return new WaitForSeconds(0.1f);
        if (RocksCurHP < RocksMaxHP) RocksCurHP += RocksMaxHP / 100f;
        else RocksCurHP = RocksMaxHP;
    }

    readonly int maxExecutions = 3;
    public static IEnumerator ResetExecutionsAfterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            executions = 0;
            Debug.Log("광물 초기화!");
        }
    }
    [System.Obsolete]
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(touchableArea, eventData.position, eventData.pressEventCamera, out localPoint);

        // 터치 가능한 영역이면 HandleTouchInput 호출
        if (touchableArea.rect.Contains(localPoint))
        {
            // 최대 3번까지만 HandleTouchInput 실행
            if (executions > maxExecutions) return;

            HandleTouchInput();

            if (MineAdManager.AdPlaying[1] == true)
            {
                for (int i = 0; i < MineAdManager.AdPowerValue[1]; i++)
                {
                    HandleTouchInput();
                }
            }
            
        }
    }
    

    // 광물 클릭 관리
    [System.Obsolete]
    public void HandleTouchInput()
    {
        executions++;
        audioSource.PlayOneShot(MiningSoundClip, 1f);

        RocksNormalDamage = (GameManager.Pickaxe_Damage) * (1 + (float)GameManager.Option_PMA / 100);
        RocksCriticalDamage = (GameManager.Pickaxe_Damage + GameManager.Pickaxe_CriticalDamage) * (1 + (float)GameManager.Option_PMA / 100);

        int Random_Critical = Random.RandomRange(0, 100); // 크리티컬 확률 조건
        int FatalDamage = Random.RandomRange(0, 100); // 치명적 피해 확률 조건
        int Warrant = Random.RandomRange(0, 100);   //  권능 확률 조건

        float damageAmount = Random_Critical < GameManager.Pickaxe_CriticalChance ? RocksCriticalDamage : RocksNormalDamage;

        if (MineAdManager.AdPlaying[0] == true) damageAmount *= (float)(MineAdManager.AdPowerValue[0] / 50f);

        if (FatalDamage < GameManager.Option_PFD) RocksCurHP -= RocksCurHP / 3;    // 치명적 피해

        if (GameManager.WarrantLevel[7] >= 1 && Warrant <= 1) damageAmount *= GameManager.Warrant_Power[7];
        if (GameManager.WarrantLevel[8] >= 1) damageAmount += GameManager.Warrant_Power[8];

        // touchableArea의 RectTransform을 가져옵니다.
        RectTransform touchableRectTransform = touchableArea.GetComponent<RectTransform>();

        // 랜덤한 x와 y 좌표를 생성합니다.
        float randomX = Random.Range(touchableRectTransform.rect.min.x + 20, touchableRectTransform.rect.max.x - 20);
        float randomY = Random.Range(touchableRectTransform.rect.min.y + 20, touchableRectTransform.rect.max.y - 20);

        // 랜덤한 좌표에 TextPrefab을 생성하고 parentRectTransform을 부모로 설정합니다.
        GameObject textInstance = Instantiate(TextPrefab, touchableRectTransform);
        textInstance.transform.localPosition = new Vector2(randomX, randomY); // 생성된 텍스트의 위치를 설정합니다.

        // TextPrefab의 Text 컴포넌트를 가져옵니다.
        Text textComponent = textInstance.GetComponent<Text>();

        // 데미지 값을 Text에 설정합니다.
        textComponent.text = damageAmount.ToString();

        Color color = Random_Critical < GameManager.Pickaxe_CriticalChance ? Color.blue : Color.white;

        textComponent.color = color;

        RocksCurHP -= damageAmount;

        int MineralBomb = Random.RandomRange(0, 100); // 광물 폭탄 확률 조건
        if (RocksCurHP <= 0)
        {
            if (GameManager.RockStageClear[GameManager.Rock_defeatedIndex + 1] == false)
            {
                GameManager.RockStageClear[GameManager.Rock_defeatedIndex + 1] = true;
                Rock_LevelSelBtn[GameManager.Rock_defeatedIndex + 1].interactable = true;
            }

            if (MineralBomb < GameManager.Option_LevelMB) GrantRewards(GameManager.Rock_defeatedIndex, 20);
            else GrantRewards(GameManager.Rock_defeatedIndex, 1);

        }

        // 이펙트를 생성하고 부모 Rect Transform의 좌표계를 기준으로 위치를 설정합니다.
        GameObject effectInstance = Instantiate(effectPrefab, parentRectTransform);
        Destroy(effectInstance, 1f);
    }

    private void LackOut()
    {
        lackBG.SetActive(false);
    }

    // 업그레이드 총합 정보 관리
    private void UpdateBasicInfo()
    {
        BasicInfo[0].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_Damage) + "";
        BasicInfo[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Pickaxe_CriticalChance) + "%";
        BasicInfo[2].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_CriticalDamage) + "";

        BasicInfo[3].text = TextFormatter.GetThousandCommaText(GameManager.Mineral_MI) + "";
        BasicInfo[4].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Mineral_HP) + "%";
        BasicInfo[5].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Mineral_RS) + "%";

        BasicInfo[6].text = TextFormatter.GetThousandCommaText((long)GameManager.Option_PMA) + "%";
        BasicInfo[7].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Option_MB) + "%";
        BasicInfo[8].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Option_PFD) + "%";
    }

    // 광물 체력 업데이트 관리
    private void UpdateHealth()
    {
        Rocks_Slider.value = RocksCurHP / RocksMaxHP; // 최대 체력에 대한 현재 체력의 비율로 설정

        float healthPercentage = (RocksCurHP / RocksMaxHP) * 100f;

        if (healthPercentage >= 10)
            Rocks_HPText.text = healthPercentage.ToString("00.00") + " %";
        else
            Rocks_HPText.text = healthPercentage.ToString("0.00") + " %";
    }

    // 광물 정보 업데이트 관리
    private void UpdateInfoValue()
    {
        Rock_InfoText[0].text = "<color=lightblue>" + TextFormatter.GetThousandCommaText((long)rock.n_rocks[GameManager.Rock_InfoStage].Maxhealth) + "</color>";
        Rock_InfoText[1].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].BrownDust) + "%</color>";
        Rock_InfoText[2].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].OrangeDust) + "%</color>";
        Rock_InfoText[3].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].LimeDust) + "%</color>";
        Rock_InfoText[4].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].BlackDust) + "%</color>";
        Rock_InfoText[5].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].RedDust) + "%</color>";
        Rock_InfoText[6].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].YellowDust) + "%</color>";
        Rock_InfoText[7].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].DirtDust) + "%</color>";
        Rock_InfoText[8].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].BlueDust) + "%</color>";
        Rock_InfoText[9].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].PurpleDust) + "%</color>";
        Rock_InfoText[10].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].ReinforceScroll) + "%</color>";
    }

    // 돌 선택 색깔 변경 관리
    public void ChangeButtonColor(Button button, Color color)
    {
        // 버튼의 이미지 컴포넌트 가져오기
        Image buttonImage = button.GetComponent<Image>();

        // 버튼의 이미지 색상 변경
        buttonImage.color = color;
    }

    // 돌 선택 정보 관리
    private void ChangeRockInfo(int index)
    {
        if (GameManager.Rock_Stage_Difficulty == 0)
        {
            NormalRock selectedRock = rock.n_rocks[index];
        }

        if (index == lastIndex) return;

        // 이전에 선택한 버튼이 있을 경우, 이전 버튼의 색상을 기본 색상으로 변경
        if (lastIndex != -1)
        {
            ChangeButtonColor(Rock_LevelSelBtn[lastIndex], ColorManager.ColorChange("기본색"));
        }

        // 현재 선택한 버튼의 색상 변경
        ChangeButtonColor(Rock_LevelSelBtn[index], ColorManager.ColorChange("민트색"));

        // 현재 선택한 버튼을 이전에 선택한 버튼으로 설정
        lastIndex = index;

        GameManager.Rock_InfoStage = index;
        // 슬라임 정보를 사용하여 UI 업데이트

        UpdateInfoValue();
    }

    // 돌 리스트 나가기
    void RockListExitPanel()
    {
        if (GameManager.Rock_InfoStage != GameManager.Rock_defeatedIndex)
        {
            GameManager.Rock_InfoStage = GameManager.Rock_defeatedIndex;
            lastIndex = GameManager.Rock_defeatedIndex;
            UpdateInfoValue();
        }

        Rock_LevelListPanel.SetActive(false);
        Rock_DifficultyPanel.SetActive(false);
        Rock_LevelSelPanel.SetActive(false);
    }

    // 돌 소환
    private void Rock_Summons()
    {
        GameManager.Rock_defeatedIndex = GameManager.Rock_InfoStage;

        NormalRock selectedRock = rock.n_rocks[GameManager.Rock_defeatedIndex];

        Rocks_Name.text = selectedRock.name;
        RocksMaxHP = selectedRock.Maxhealth * (1 - (float)GameManager.Mineral_HP / 100);
        RocksCurHP = selectedRock.Maxhealth * (1 - (float)GameManager.Mineral_HP / 100);

        // 현재 선택한 버튼의 색상 변경
        //ChangeButtonColor(Rock_LevelSelBtn[GameManager.], ColorManager.ColorChange("민트색"));

        Save_index = 0;
        Rock_LevelListPanel.SetActive(false);
        Rock_LevelSelPanel.SetActive(false);
        UpdateHealth();
    }

    // 돌 난이도
    private void RockDifficultyOpen(int index)
    {
        if (index == 0)
        {
            GameManager.Rock_Stage_Difficulty = 0;
        }

        for (int i = 0; i < Rock_LevelSelBtn.Length; i++)
        {
            int index_ = i; // 클로저로 인덱스를 보존

            if (GameManager.Rock_defeatedIndex == index_) ChangeButtonColor(Rock_LevelSelBtn[index_], ColorManager.ColorChange("민트색"));

            else if (Rock_LevelSelBtn[index_].interactable == true && GameManager.Rock_defeatedIndex != index_) ChangeButtonColor(Rock_LevelSelBtn[index_], ColorManager.ColorChange("기본색"));
            else ChangeButtonColor(Rock_LevelSelBtn[index_], ColorManager.ColorChange("투명기본색"));
        }

        Rock_DifficultyPanel.SetActive(false);
        Rock_LevelSelPanel.SetActive(true);
    }

    // 곡괭이 강화 화면 변경
    private void PickaxeChange()
    {
        UpgradeTitleText.text = "곡괭이 강화";

        isPickaxeActive = !isPickaxeActive;

        PickaxeUpgradePanel.SetActive(isPickaxeActive);
        UpgradePanel.SetActive(isPickaxeActive);
        BasicPanel.SetActive(!isPickaxeActive);

        if (isMineralActive)
        {
            MineralUpgradePanel.SetActive(false);
            isMineralActive = false;
            
        }
        if (isOptionActive)
        {
            OptionUpgradePanel.SetActive(false);
            isOptionActive = false;
        }

        UpdateBasicInfo();
    }

    // 광물 강화 화면 변경
    private void MineralChange()
    {
        UpgradeTitleText.text = "광물 강화";

        isMineralActive = !isMineralActive;

        MineralUpgradePanel.SetActive(isMineralActive);
        UpgradePanel.SetActive(isMineralActive);
        BasicPanel.SetActive(!isMineralActive);

        if (isPickaxeActive)
        {
            PickaxeUpgradePanel.SetActive(false);
            isPickaxeActive = false;

        }
        if (isOptionActive)
        {
            OptionUpgradePanel.SetActive(false);
            isOptionActive = false;
        }

        UpdateBasicInfo();
    }

    // 옵션 강화 화면 변경
    private void OptionChange()
    {
        UpgradeTitleText.text = "옵션 강화";

        isOptionActive = !isOptionActive;
        OptionUpgradePanel.SetActive(isOptionActive);
        UpgradePanel.SetActive(isOptionActive); // UpgradePanel의 활성/비활성 상태를 isOptionActive에 따라 역으로 설정
        BasicPanel.SetActive(!isOptionActive);

        if (isPickaxeActive)
        {
            PickaxeUpgradePanel.SetActive(false);
            isPickaxeActive = false;

        }
        if (isMineralActive)
        {
            MineralUpgradePanel.SetActive(false);
            isMineralActive = false;
        }

        UpdateBasicInfo();
    }
    // 곡괭이 업그레이드
    private void PickaxeLevelUP(int index)
    {
        bool check = false;

        //  곡괭이 데미지 증가
        if (index == 0)
        {
            if (GameManager.BrownDust < GameManager.BasicNeedBrownDust[GameManager.Pickaxe_DamageLv - 1]) check = true;

            if (GameManager.Pickaxe_DamageLv >= 10)
                if (GameManager.OrangeDust < GameManager.BasicNeedOrangeDust[GameManager.Pickaxe_DamageLv - 10]) check = true;
            if (GameManager.Pickaxe_DamageLv >= 30)
                if (GameManager.LimeDust < GameManager.BasicNeedLimeDust[GameManager.Pickaxe_DamageLv - 30]) check = true;
            if (GameManager.Pickaxe_DamageLv >= 60)
                if (GameManager.BlackDust < GameManager.BasicNeedBlackDust[GameManager.Pickaxe_DamageLv - 60]) check = true;
            if (GameManager.Pickaxe_DamageLv >= 120)
                if (GameManager.RedDust < GameManager.BasicNeedRedDust[GameManager.Pickaxe_DamageLv - 120]) check = true;
            if (GameManager.Pickaxe_DamageLv >= 240)
                if (GameManager.YellowDust < GameManager.BasicNeedYellowDust[GameManager.Pickaxe_DamageLv - 240]) check = true;
            if (GameManager.Pickaxe_DamageLv >= 480)
                if (GameManager.DirtDust < GameManager.BasicNeedDirtDust[GameManager.Pickaxe_DamageLv - 480]) check = true;
            if (GameManager.Pickaxe_DamageLv >= 800)
                if (GameManager.BlueDust < GameManager.BasicNeedBlueDust[GameManager.Pickaxe_DamageLv - 800]) check = true;
            if (GameManager.Pickaxe_DamageLv >= 1500)
                if (GameManager.PurpleDust < GameManager.BasicNeedPurpleDust[GameManager.Pickaxe_DamageLv - 1500]) check = true;

            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Pickaxe_DamageLv++;
            GameManager.Pickaxe_Damage += 1f;


            PickaxeLevelText[0].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Pickaxe_DamageLv);
            PickaxeLeftText[0].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_Damage) + "";
            PickaxeRightText[0].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_Damage + 1f) + "";

            Pickaxe_DamageNeeditem[0].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBrownDust[GameManager.Pickaxe_DamageLv - 1]) + "";
            GameManager.BrownDust -= GameManager.BasicNeedBrownDust[GameManager.Pickaxe_DamageLv - 2];

            if (GameManager.Pickaxe_DamageLv >= 10)
            {
                Pickaxe_DamageNeeditem[1].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedOrangeDust[GameManager.Pickaxe_DamageLv - 10]) + "";
                if (GameManager.Pickaxe_DamageLv != 10) GameManager.OrangeDust -= GameManager.BasicNeedOrangeDust[GameManager.Pickaxe_DamageLv - 11];
            }

            if (GameManager.Pickaxe_DamageLv >= 30)
            {
                Pickaxe_DamageNeeditem[2].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedLimeDust[GameManager.Pickaxe_DamageLv - 30]) + "";
                if (GameManager.Pickaxe_DamageLv != 30) GameManager.LimeDust -= GameManager.BasicNeedLimeDust[GameManager.Pickaxe_DamageLv - 31];
            }

            if (GameManager.Pickaxe_DamageLv >= 60)
            {
                Pickaxe_DamageNeeditem[3].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlackDust[GameManager.Pickaxe_DamageLv - 60]) + "";
                if (GameManager.Pickaxe_DamageLv != 60) GameManager.BlackDust -= GameManager.BasicNeedBlackDust[GameManager.Pickaxe_DamageLv - 61];
            }

            if (GameManager.Pickaxe_DamageLv >= 120)
            {
                Pickaxe_DamageNeeditem[4].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedRedDust[GameManager.Pickaxe_DamageLv - 120]) + "";
                if (GameManager.Pickaxe_DamageLv != 120) GameManager.RedDust -= GameManager.BasicNeedRedDust[GameManager.Pickaxe_DamageLv - 121];
            }

            if (GameManager.Pickaxe_DamageLv >= 240)
            {
                Pickaxe_DamageNeeditem[5].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedYellowDust[GameManager.Pickaxe_DamageLv - 240]) + "";
                if (GameManager.Pickaxe_DamageLv != 240) GameManager.YellowDust -= GameManager.BasicNeedYellowDust[GameManager.Pickaxe_DamageLv - 241];
            }

            if (GameManager.Pickaxe_DamageLv >= 480)
            {
                Pickaxe_DamageNeeditem[6].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedDirtDust[GameManager.Pickaxe_DamageLv - 480]) + "";
                if (GameManager.Pickaxe_DamageLv != 480) GameManager.DirtDust -= GameManager.BasicNeedDirtDust[GameManager.Pickaxe_DamageLv - 481];
            }

            if (GameManager.Pickaxe_DamageLv >= 800)
            {
                Pickaxe_DamageNeeditem[7].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlueDust[GameManager.Pickaxe_DamageLv - 800]) + "";
                if (GameManager.Pickaxe_DamageLv != 800) GameManager.BlueDust -= GameManager.BasicNeedBlueDust[GameManager.Pickaxe_DamageLv - 801];
            }

            if (GameManager.Pickaxe_DamageLv >= 1500)
            {
                Pickaxe_DamageNeeditem[8].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedPurpleDust[GameManager.Pickaxe_DamageLv - 1500]) + "";
                if (GameManager.Pickaxe_DamageLv != 1500) GameManager.PurpleDust -= GameManager.BasicNeedPurpleDust[GameManager.Pickaxe_DamageLv - 1501];
            }

        }
        //  곡괭이 크리티컬 확률 증가
        else if (index == 1)
        {
            if (GameManager.BrownDust < GameManager.SpecialNeedBrownDust[GameManager.Pickaxe_CriticalChance_Level - 1]) check = true;

            if (GameManager.Pickaxe_CriticalChance_Level >= 10)
                if (GameManager.OrangeDust < GameManager.SpecialNeedOrangeDust[GameManager.Pickaxe_CriticalChance_Level - 10]) check = true;
            if (GameManager.Pickaxe_CriticalChance_Level >= 30)
                if (GameManager.LimeDust < GameManager.SpecialNeedLimeDust[GameManager.Pickaxe_CriticalChance_Level - 30]) check = true;
            if (GameManager.Pickaxe_CriticalChance_Level >= 60)
                if (GameManager.BlackDust < GameManager.SpecialNeedBlackDust[GameManager.Pickaxe_CriticalChance_Level - 60]) check = true;
            if (GameManager.Pickaxe_CriticalChance_Level >= 120)
                if (GameManager.RedDust < GameManager.SpecialNeedRedDust[GameManager.Pickaxe_CriticalChance_Level - 120]) check = true;
            if (GameManager.Pickaxe_CriticalChance_Level >= 240)
                if (GameManager.YellowDust < GameManager.SpecialNeedYellowDust[GameManager.Pickaxe_CriticalChance_Level - 240]) check = true;
            if (GameManager.Pickaxe_CriticalChance_Level >= 480)
                if (GameManager.DirtDust < GameManager.SpecialNeedDirtDust[GameManager.Pickaxe_CriticalChance_Level - 480]) check = true;
            if (GameManager.Pickaxe_CriticalChance_Level >= 800)
                if (GameManager.BlueDust < GameManager.SpecialNeedBlueDust[GameManager.Pickaxe_CriticalChance_Level - 800]) check = true;
            if (GameManager.Pickaxe_CriticalChance_Level >= 1500)
                if (GameManager.PurpleDust < GameManager.SpecialNeedPurpleDust[GameManager.Pickaxe_CriticalChance_Level - 1500]) check = true;

            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Pickaxe_CriticalChance_Level++;
            GameManager.Pickaxe_CriticalChance += 0.1m;

            PickaxeLevelText[1].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Pickaxe_CriticalChance_Level);
            PickaxeLeftText[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Pickaxe_CriticalChance) + "%";
            PickaxeRightText[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Pickaxe_CriticalChance + 0.1m) + "%";

            Pickaxe_CriChanceNeeditem[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Pickaxe_CriticalChance_Level - 1]) + "";
            GameManager.BrownDust -= GameManager.SpecialNeedBrownDust[GameManager.Pickaxe_CriticalChance_Level - 2];


            if (GameManager.Pickaxe_CriticalChance_Level >= 10)
            {
                Pickaxe_CriChanceNeeditem[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Pickaxe_CriticalChance_Level - 10]) + "";
                if (GameManager.Pickaxe_CriticalChance_Level != 10) GameManager.OrangeDust -= GameManager.SpecialNeedOrangeDust[GameManager.Pickaxe_CriticalChance_Level - 11];
            }

            if (GameManager.Pickaxe_CriticalChance_Level >= 30)
            {
                Pickaxe_CriChanceNeeditem[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Pickaxe_CriticalChance_Level - 30]) + "";
                if (GameManager.Pickaxe_CriticalChance_Level != 30) GameManager.LimeDust -= GameManager.SpecialNeedLimeDust[GameManager.Pickaxe_CriticalChance_Level - 31];
            }

            if (GameManager.Pickaxe_CriticalChance_Level >= 60)
            {
                Pickaxe_CriChanceNeeditem[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Pickaxe_CriticalChance_Level - 60]) + "";
                if (GameManager.Pickaxe_CriticalChance_Level != 60) GameManager.BlackDust -= GameManager.SpecialNeedBlackDust[GameManager.Pickaxe_CriticalChance_Level - 61];
            }

            if (GameManager.Pickaxe_CriticalChance_Level >= 120)
            {
                Pickaxe_CriChanceNeeditem[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Pickaxe_CriticalChance_Level - 120]) + "";
                if (GameManager.Pickaxe_CriticalChance_Level != 120) GameManager.RedDust -= GameManager.SpecialNeedRedDust[GameManager.Pickaxe_CriticalChance_Level - 121];
            }

            if (GameManager.Pickaxe_CriticalChance_Level >= 240)
            {
                Pickaxe_CriChanceNeeditem[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Pickaxe_CriticalChance_Level - 240]) + "";
                if (GameManager.Pickaxe_CriticalChance_Level != 240) GameManager.YellowDust -= GameManager.SpecialNeedYellowDust[GameManager.Pickaxe_CriticalChance_Level - 241];
            }

            if (GameManager.Pickaxe_CriticalChance_Level >= 480)
            {
                Pickaxe_CriChanceNeeditem[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Pickaxe_CriticalChance_Level - 480]) + "";
                if (GameManager.Pickaxe_CriticalChance_Level != 480) GameManager.DirtDust -= GameManager.SpecialNeedDirtDust[GameManager.Pickaxe_CriticalChance_Level - 481];
            }

            if (GameManager.Pickaxe_CriticalChance_Level >= 800)
            {
                Pickaxe_CriChanceNeeditem[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Pickaxe_CriticalChance_Level - 800]) + "";
                if (GameManager.Pickaxe_CriticalChance_Level != 800) GameManager.BlueDust -= GameManager.SpecialNeedBlueDust[GameManager.Pickaxe_CriticalChance_Level - 801];
            }

            if (GameManager.Pickaxe_CriticalChance_Level >= 1500)
            {
                Pickaxe_CriChanceNeeditem[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Pickaxe_CriticalChance_Level - 1500]) + "";
                if (GameManager.Pickaxe_CriticalChance_Level != 1500) GameManager.PurpleDust -= GameManager.SpecialNeedPurpleDust[GameManager.Pickaxe_CriticalChance_Level - 1501];
            }
        }
        //  곡괭이 크리티컬 데미지 증가
        else if (index == 2)
        {
            if (GameManager.BrownDust < GameManager.BasicNeedBrownDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) check = true;

            if (GameManager.Pickaxe_CriticalDamage_Level >= 10)
                if (GameManager.OrangeDust < GameManager.BasicNeedOrangeDust[GameManager.Pickaxe_CriticalDamage_Level - 10]) check = true;
            if (GameManager.Pickaxe_CriticalDamage_Level >= 30)
                if (GameManager.LimeDust < GameManager.BasicNeedLimeDust[GameManager.Pickaxe_CriticalDamage_Level - 30]) check = true;
            if (GameManager.Pickaxe_CriticalDamage_Level >= 60)
                if (GameManager.BlackDust < GameManager.BasicNeedBlackDust[GameManager.Pickaxe_CriticalDamage_Level - 60]) check = true;
            if (GameManager.Pickaxe_CriticalDamage_Level >= 120)
                if (GameManager.RedDust < GameManager.BasicNeedRedDust[GameManager.Pickaxe_CriticalDamage_Level - 120]) check = true;
            if (GameManager.Pickaxe_CriticalDamage_Level >= 240)
                if (GameManager.YellowDust < GameManager.BasicNeedYellowDust[GameManager.Pickaxe_CriticalDamage_Level - 240]) check = true;
            if (GameManager.Pickaxe_CriticalDamage_Level >= 480)
                if (GameManager.DirtDust < GameManager.BasicNeedDirtDust[GameManager.Pickaxe_CriticalDamage_Level - 480]) check = true;
            if (GameManager.Pickaxe_CriticalDamage_Level >= 800)
                if (GameManager.BlueDust < GameManager.BasicNeedBlueDust[GameManager.Pickaxe_CriticalDamage_Level - 800]) check = true;
            if (GameManager.Pickaxe_CriticalDamage_Level >= 1500)
                if (GameManager.PurpleDust < GameManager.BasicNeedPurpleDust[GameManager.Pickaxe_CriticalDamage_Level - 1500]) check = true;

            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Pickaxe_CriticalDamage_Level++;
            GameManager.Pickaxe_CriticalDamage += 1f;

            PickaxeLevelText[2].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Pickaxe_CriticalDamage_Level);
            PickaxeLeftText[2].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_CriticalDamage) + "%";
            PickaxeRightText[2].text = TextFormatter.GetFloatPointCommaText_0(GameManager.Pickaxe_CriticalDamage + 1f) + "%";

            Pickaxe_CriDamageNeeditem[0].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBrownDust[GameManager.Pickaxe_CriticalDamage_Level - 1]) + "";
            GameManager.BrownDust -= GameManager.BasicNeedBrownDust[GameManager.Pickaxe_CriticalDamage_Level - 2];


            if (GameManager.Pickaxe_CriticalDamage_Level >= 10)
            {
                Pickaxe_CriDamageNeeditem[1].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedOrangeDust[GameManager.Pickaxe_CriticalDamage_Level - 10]) + "";
                if (GameManager.Pickaxe_CriticalDamage_Level != 10) GameManager.OrangeDust -= GameManager.BasicNeedOrangeDust[GameManager.Pickaxe_CriticalDamage_Level - 11];
            }

            if (GameManager.Pickaxe_CriticalDamage_Level >= 30)
            {
                Pickaxe_CriDamageNeeditem[2].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedLimeDust[GameManager.Pickaxe_CriticalDamage_Level - 30]) + "";
                if (GameManager.Pickaxe_CriticalDamage_Level != 30) GameManager.LimeDust -= GameManager.BasicNeedLimeDust[GameManager.Pickaxe_CriticalDamage_Level - 31];
            }

            if (GameManager.Pickaxe_CriticalDamage_Level >= 60)
            {
                Pickaxe_CriDamageNeeditem[3].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlackDust[GameManager.Pickaxe_CriticalDamage_Level - 60]) + "";
                if (GameManager.Pickaxe_CriticalDamage_Level != 60) GameManager.BlackDust -= GameManager.BasicNeedBlackDust[GameManager.Pickaxe_CriticalDamage_Level - 61];
            }

            if (GameManager.Pickaxe_CriticalDamage_Level >= 120)
            {
                Pickaxe_CriDamageNeeditem[4].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedRedDust[GameManager.Pickaxe_CriticalDamage_Level - 120]) + "";
                if (GameManager.Pickaxe_CriticalDamage_Level != 120) GameManager.RedDust -= GameManager.BasicNeedRedDust[GameManager.Pickaxe_CriticalDamage_Level - 121];
            }

            if (GameManager.Pickaxe_CriticalDamage_Level >= 240)
            {
                Pickaxe_CriDamageNeeditem[5].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedYellowDust[GameManager.Pickaxe_CriticalDamage_Level - 240]) + "";
                if (GameManager.Pickaxe_CriticalDamage_Level != 240) GameManager.YellowDust -= GameManager.BasicNeedYellowDust[GameManager.Pickaxe_CriticalDamage_Level - 241];
            }

            if (GameManager.Pickaxe_CriticalDamage_Level >= 480)
            {
                Pickaxe_CriDamageNeeditem[6].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedDirtDust[GameManager.Pickaxe_CriticalDamage_Level - 480]) + "";
                if (GameManager.Pickaxe_CriticalDamage_Level != 480) GameManager.DirtDust -= GameManager.BasicNeedDirtDust[GameManager.Pickaxe_CriticalDamage_Level - 481];
            }

            if (GameManager.Pickaxe_CriticalDamage_Level >= 800)
            {
                Pickaxe_CriDamageNeeditem[7].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlueDust[GameManager.Pickaxe_CriticalDamage_Level - 800]) + "";
                if (GameManager.Pickaxe_CriticalDamage_Level != 800) GameManager.BlueDust -= GameManager.BasicNeedBlueDust[GameManager.Pickaxe_CriticalDamage_Level - 801];
            }

            if (GameManager.Pickaxe_CriticalDamage_Level >= 1500)
            {
                Pickaxe_CriDamageNeeditem[8].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedPurpleDust[GameManager.Pickaxe_CriticalDamage_Level - 1500]) + "";
                if (GameManager.Pickaxe_CriticalDamage_Level != 1500) GameManager.PurpleDust -= GameManager.BasicNeedPurpleDust[GameManager.Pickaxe_CriticalDamage_Level - 1501];
            }
        }
    }
    
    // 광물 고민
    /*
     * 광산 레벨업을 하려면 광물이필요
     * 광물은 갈색 먼지 20개로 시작
     * 갈색 먼지 20개로 레벨업을 하면 다음 갈색 먼지 20개가 필요함
     * 그러면 20 * 곡괭이 레벨 하면 되는건가?
     * 그런데 20개는 너무많음
     * 광물 상자를 만들어서 미션을 깨면 주는식으로 할까?
     * 좋은거 같기도하고 광산은 무조건 Passive활성화를 목표를 두고 하는걸로
     * Text는 어떻게해? 똑같이 20 * 곡괭이 레벨을 시키면 되지않을까 일단 해보기
     * 갈색 먼지이 너무 많이 필요할거같음 하지만 쾌감을 줄수있지않을까 잘 분배하면
     * 버리는 
     * 
     */

    // 광물 업그레이드
    private void MineralLevelUP(int index)
    {
        bool check = false;
        //  광물 증가량 업그레이드
        if (index == 0)
        {
            if (GameManager.BrownDust < GameManager.SpecialNeedBrownDust[GameManager.Mineral_LevelMI - 1]) check = true;

            if (GameManager.Mineral_LevelMI >= 10)
                if (GameManager.OrangeDust < GameManager.SpecialNeedOrangeDust[GameManager.Mineral_LevelMI - 10]) check = true;
            if (GameManager.Mineral_LevelMI >= 30)
                if (GameManager.LimeDust < GameManager.SpecialNeedLimeDust[GameManager.Mineral_LevelMI - 30]) check = true;
            if (GameManager.Mineral_LevelMI >= 60)
                if (GameManager.BlackDust < GameManager.SpecialNeedBlackDust[GameManager.Mineral_LevelMI - 60]) check = true;
            if (GameManager.Mineral_LevelMI >= 120)
                if (GameManager.RedDust < GameManager.SpecialNeedRedDust[GameManager.Mineral_LevelMI - 120]) check = true;
            if (GameManager.Mineral_LevelMI >= 240)
                if (GameManager.YellowDust < GameManager.SpecialNeedYellowDust[GameManager.Mineral_LevelMI - 240]) check = true;
            if (GameManager.Mineral_LevelMI >= 480)
                if (GameManager.DirtDust < GameManager.SpecialNeedDirtDust[GameManager.Mineral_LevelMI - 480]) check = true;
            if (GameManager.Mineral_LevelMI >= 800)
                if (GameManager.BlueDust < GameManager.SpecialNeedBlueDust[GameManager.Mineral_LevelMI - 800]) check = true;
            if (GameManager.Mineral_LevelMI >= 1500)
                if (GameManager.PurpleDust < GameManager.SpecialNeedPurpleDust[GameManager.Mineral_LevelMI - 1500]) check = true;


            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Mineral_LevelMI++;
            GameManager.Mineral_MI += 1;


            MineralLevelText[0].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Mineral_LevelMI);
            MineralLeftText[0].text = TextFormatter.GetThousandCommaText(GameManager.Mineral_MI) + "";
            MineralRightText[0].text = TextFormatter.GetThousandCommaText(GameManager.Mineral_MI + 1) + "";

            Mineral_NeeditemMI[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Mineral_LevelMI - 1]) + "";
            GameManager.BrownDust -= GameManager.SpecialNeedBrownDust[GameManager.Mineral_LevelMI - 2];

            if (GameManager.Mineral_LevelMI >= 10)
            {
                Mineral_NeeditemMI[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Mineral_LevelMI - 10]) + "";
                if (GameManager.Mineral_LevelMI != 10) GameManager.OrangeDust -= GameManager.SpecialNeedOrangeDust[GameManager.Mineral_LevelMI - 11];
            }

            if (GameManager.Mineral_LevelMI >= 30)
            {
                Mineral_NeeditemMI[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Mineral_LevelMI - 30]) + "";
                if (GameManager.Mineral_LevelMI != 30) GameManager.LimeDust -= GameManager.SpecialNeedLimeDust[GameManager.Mineral_LevelMI - 31];
            }

            if (GameManager.Mineral_LevelMI >= 60)
            {
                Mineral_NeeditemMI[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Mineral_LevelMI - 60]) + "";
                if (GameManager.Mineral_LevelMI != 60) GameManager.BlackDust -= GameManager.SpecialNeedBlackDust[GameManager.Mineral_LevelMI - 61];
            }

            if (GameManager.Mineral_LevelMI >= 120)
            {
                Mineral_NeeditemMI[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Mineral_LevelMI - 120]) + "";
                if (GameManager.Mineral_LevelMI != 120) GameManager.RedDust -= GameManager.SpecialNeedRedDust[GameManager.Mineral_LevelMI - 121];
            }

            if (GameManager.Mineral_LevelMI >= 240)
            {
                Mineral_NeeditemMI[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Mineral_LevelMI - 240]) + "";
                if (GameManager.Mineral_LevelMI != 240) GameManager.YellowDust -= GameManager.SpecialNeedYellowDust[GameManager.Mineral_LevelMI - 241];
            }

            if (GameManager.Mineral_LevelMI >= 480)
            {
                Mineral_NeeditemMI[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Mineral_LevelMI - 480]) + "";
                if (GameManager.Mineral_LevelMI != 480) GameManager.DirtDust -= GameManager.SpecialNeedDirtDust[GameManager.Mineral_LevelMI - 481];
            }

            if (GameManager.Mineral_LevelMI >= 800)
            {
                Mineral_NeeditemMI[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Mineral_LevelMI - 800]) + "";
                if (GameManager.Mineral_LevelMI != 800) GameManager.BlueDust -= GameManager.SpecialNeedBlueDust[GameManager.Mineral_LevelMI - 801];
            }

            if (GameManager.Mineral_LevelMI >= 1500)
            {
                Mineral_NeeditemMI[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Mineral_LevelMI - 1500]) + "";
                if (GameManager.Mineral_LevelMI != 1500) GameManager.PurpleDust -= GameManager.SpecialNeedPurpleDust[GameManager.Mineral_LevelMI - 1501];
            }
        }
        //  광물 약점 업그레이드
        else if (index == 1)
        {
            if (GameManager.BrownDust < GameManager.SpecialNeedBrownDust[GameManager.Mineral_LevelHP - 1]) check = true;

            if (GameManager.Mineral_LevelHP >= 10)
                if (GameManager.OrangeDust < GameManager.SpecialNeedOrangeDust[GameManager.Mineral_LevelHP - 10]) check = true;
            if (GameManager.Mineral_LevelHP >= 30)
                if (GameManager.LimeDust < GameManager.SpecialNeedLimeDust[GameManager.Mineral_LevelHP - 30]) check = true;
            if (GameManager.Mineral_LevelHP >= 60)
                if (GameManager.BlackDust < GameManager.SpecialNeedBlackDust[GameManager.Mineral_LevelHP - 60]) check = true;
            if (GameManager.Mineral_LevelHP >= 120)
                if (GameManager.RedDust < GameManager.SpecialNeedRedDust[GameManager.Mineral_LevelHP - 120]) check = true;
            if (GameManager.Mineral_LevelHP >= 240)
                if (GameManager.YellowDust < GameManager.SpecialNeedYellowDust[GameManager.Mineral_LevelHP - 240]) check = true;
            if (GameManager.Mineral_LevelHP >= 480)
                if (GameManager.DirtDust < GameManager.SpecialNeedDirtDust[GameManager.Mineral_LevelHP - 480]) check = true;
            if (GameManager.Mineral_LevelHP >= 800)
                if (GameManager.BlueDust < GameManager.SpecialNeedBlueDust[GameManager.Mineral_LevelHP - 800]) check = true;
            if (GameManager.Mineral_LevelHP >= 1500)
                if (GameManager.PurpleDust < GameManager.SpecialNeedPurpleDust[GameManager.Mineral_LevelHP - 1500]) check = true;


            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Mineral_LevelHP++;
            GameManager.Mineral_HP += 0.01m;


            MineralLevelText[1].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Mineral_LevelHP);
            MineralLeftText[1].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Mineral_HP) + "%";
            MineralRightText[1].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Mineral_HP + 0.01m) + "%";

            Mineral_NeeditemHP[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Mineral_LevelHP - 1]) + "";
            GameManager.BrownDust -= GameManager.SpecialNeedBrownDust[GameManager.Mineral_LevelHP - 2];

            if (GameManager.Mineral_LevelHP >= 10)
            {
                Mineral_NeeditemHP[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Mineral_LevelHP - 10]) + "";
                if (GameManager.Mineral_LevelHP != 10) GameManager.OrangeDust -= GameManager.SpecialNeedOrangeDust[GameManager.Mineral_LevelHP - 11];
            }

            if (GameManager.Mineral_LevelHP >= 30)
            {
                Mineral_NeeditemHP[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Mineral_LevelHP - 30]) + "";
                if (GameManager.Mineral_LevelHP != 30) GameManager.LimeDust -= GameManager.SpecialNeedLimeDust[GameManager.Mineral_LevelHP - 31];
            }

            if (GameManager.Mineral_LevelHP >= 60)
            {
                Mineral_NeeditemHP[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Mineral_LevelHP - 60]) + "";
                if (GameManager.Mineral_LevelHP != 60) GameManager.BlackDust -= GameManager.SpecialNeedBlackDust[GameManager.Mineral_LevelHP - 61];
            }

            if (GameManager.Mineral_LevelHP >= 120)
            {
                Mineral_NeeditemHP[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Mineral_LevelHP - 120]) + "";
                if (GameManager.Mineral_LevelHP != 120) GameManager.RedDust -= GameManager.SpecialNeedRedDust[GameManager.Mineral_LevelHP - 121];
            }

            if (GameManager.Mineral_LevelHP >= 240)
            {
                Mineral_NeeditemHP[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Mineral_LevelHP - 240]) + "";
                if (GameManager.Mineral_LevelHP != 240) GameManager.YellowDust -= GameManager.SpecialNeedYellowDust[GameManager.Mineral_LevelHP - 241];
            }

            if (GameManager.Mineral_LevelHP >= 480)
            {
                Mineral_NeeditemHP[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Mineral_LevelHP - 480]) + "";
                if (GameManager.Mineral_LevelHP != 480) GameManager.DirtDust -= GameManager.SpecialNeedDirtDust[GameManager.Mineral_LevelHP - 481];
            }

            if (GameManager.Mineral_LevelHP >= 800)
            {
                Mineral_NeeditemHP[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Mineral_LevelHP - 800]) + "";
                if (GameManager.Mineral_LevelHP != 800) GameManager.BlueDust -= GameManager.SpecialNeedBlueDust[GameManager.Mineral_LevelHP - 801];
            }

            if (GameManager.Mineral_LevelHP >= 1500)
            {
                Mineral_NeeditemHP[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Mineral_LevelHP - 1500]) + "";
                if (GameManager.Mineral_LevelHP != 1500) GameManager.PurpleDust -= GameManager.SpecialNeedPurpleDust[GameManager.Mineral_LevelHP - 1501];
            }
        }
        //  강화 스크롤 업그레이드
        else if (index == 2)
        {
            if (GameManager.BrownDust < GameManager.BasicNeedBrownDust[GameManager.Mineral_LevelRS - 1]) check = true;

            if (GameManager.Mineral_LevelRS >= 10)
                if (GameManager.OrangeDust < GameManager.BasicNeedOrangeDust[GameManager.Mineral_LevelRS - 10]) check = true;
            if (GameManager.Mineral_LevelRS >= 30)
                if (GameManager.LimeDust < GameManager.BasicNeedLimeDust[GameManager.Mineral_LevelRS - 30]) check = true;
            if (GameManager.Mineral_LevelRS >= 60)
                if (GameManager.BlackDust < GameManager.BasicNeedBlackDust[GameManager.Mineral_LevelRS - 60]) check = true;
            if (GameManager.Mineral_LevelRS >= 120)
                if (GameManager.RedDust < GameManager.BasicNeedRedDust[GameManager.Mineral_LevelRS - 120]) check = true;
            if (GameManager.Mineral_LevelRS >= 240)
                if (GameManager.YellowDust < GameManager.BasicNeedYellowDust[GameManager.Mineral_LevelRS - 240]) check = true;
            if (GameManager.Mineral_LevelRS >= 480)
                if (GameManager.DirtDust < GameManager.BasicNeedDirtDust[GameManager.Mineral_LevelRS - 480]) check = true;
            if (GameManager.Mineral_LevelRS >= 800)
                if (GameManager.BlueDust < GameManager.BasicNeedBlueDust[GameManager.Mineral_LevelRS - 800]) check = true;
            if (GameManager.Mineral_LevelRS >= 1500)
                if (GameManager.PurpleDust < GameManager.BasicNeedPurpleDust[GameManager.Mineral_LevelRS - 1500]) check = true;

            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Mineral_LevelRS++;
            GameManager.Mineral_RS += 0.1m;


            MineralLevelText[2].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Mineral_LevelRS);
            MineralLeftText[2].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Mineral_RS) + "%";
            MineralRightText[2].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Mineral_RS + 0.1m) + "%";

            Mineral_NeeditemRS[0].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBrownDust[GameManager.Mineral_LevelRS - 1]) + "";
            GameManager.BrownDust -= GameManager.BasicNeedBrownDust[GameManager.Mineral_LevelRS - 2];

            if (GameManager.Mineral_LevelRS >= 10)
            {
                Mineral_NeeditemRS[1].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedOrangeDust[GameManager.Mineral_LevelRS - 10]) + "";
                if (GameManager.Mineral_LevelRS != 10) GameManager.OrangeDust -= GameManager.BasicNeedOrangeDust[GameManager.Mineral_LevelRS - 11];
            }

            if (GameManager.Mineral_LevelRS >= 30)
            {
                Mineral_NeeditemRS[2].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedLimeDust[GameManager.Mineral_LevelRS - 30]) + "";
                if (GameManager.Mineral_LevelRS != 30) GameManager.LimeDust -= GameManager.BasicNeedLimeDust[GameManager.Mineral_LevelRS - 31];
            }

            if (GameManager.Mineral_LevelRS >= 60)
            {
                Mineral_NeeditemRS[3].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlackDust[GameManager.Mineral_LevelRS - 60]) + "";
                if (GameManager.Mineral_LevelRS != 60) GameManager.BlackDust -= GameManager.BasicNeedBlackDust[GameManager.Mineral_LevelRS - 61];
            }

            if (GameManager.Mineral_LevelRS >= 120)
            {
                Mineral_NeeditemRS[4].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedRedDust[GameManager.Mineral_LevelRS - 120]) + "";
                if (GameManager.Mineral_LevelRS != 120) GameManager.RedDust -= GameManager.BasicNeedRedDust[GameManager.Mineral_LevelRS - 121];
            }

            if (GameManager.Mineral_LevelRS >= 240)
            {
                Mineral_NeeditemRS[5].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedYellowDust[GameManager.Mineral_LevelRS - 240]) + "";
                if (GameManager.Mineral_LevelRS != 240) GameManager.YellowDust -= GameManager.BasicNeedYellowDust[GameManager.Mineral_LevelRS - 241];
            }

            if (GameManager.Mineral_LevelRS >= 480)
            {
                Mineral_NeeditemRS[6].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedDirtDust[GameManager.Mineral_LevelRS - 480]) + "";
                if (GameManager.Mineral_LevelRS != 480) GameManager.DirtDust -= GameManager.BasicNeedDirtDust[GameManager.Mineral_LevelRS - 481];
            }

            if (GameManager.Mineral_LevelRS >= 800)
            {
                Mineral_NeeditemRS[7].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedBlueDust[GameManager.Mineral_LevelRS - 800]) + "";
                if (GameManager.Mineral_LevelRS != 800) GameManager.BlueDust -= GameManager.BasicNeedBlueDust[GameManager.Mineral_LevelRS - 801];
            }

            if (GameManager.Mineral_LevelRS >= 1500)
            {
                Mineral_NeeditemRS[8].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedPurpleDust[GameManager.Mineral_LevelRS - 1500]) + "";
                if (GameManager.Mineral_LevelRS != 1500) GameManager.PurpleDust -= GameManager.BasicNeedPurpleDust[GameManager.Mineral_LevelRS - 1501];
            }
        }
    }

    private void OptionLevelUP(int index)
    {
        bool check = false;
        //  옵션 데미지 증폭 업그레이드
        if (index == 0)
        {
            if (GameManager.BrownDust < GameManager.SpecialNeedBrownDust[GameManager.Option_LevelPMA - 1]) check = true;

            if (GameManager.Option_LevelPMA >= 10)
                if (GameManager.OrangeDust < GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPMA - 10]) check = true;
            if (GameManager.Option_LevelPMA >= 30)
                if (GameManager.LimeDust < GameManager.SpecialNeedLimeDust[GameManager.Option_LevelPMA - 30]) check = true;
            if (GameManager.Option_LevelPMA >= 60)
                if (GameManager.BlackDust < GameManager.SpecialNeedBlackDust[GameManager.Option_LevelPMA - 60]) check = true;
            if (GameManager.Option_LevelPMA >= 120)
                if (GameManager.RedDust < GameManager.SpecialNeedRedDust[GameManager.Option_LevelPMA - 120]) check = true;
            if (GameManager.Option_LevelPMA >= 240)
                if (GameManager.YellowDust < GameManager.SpecialNeedYellowDust[GameManager.Option_LevelPMA - 240]) check = true;
            if (GameManager.Option_LevelPMA >= 480)
                if (GameManager.DirtDust < GameManager.SpecialNeedDirtDust[GameManager.Option_LevelPMA - 480]) check = true;
            if (GameManager.Option_LevelPMA >= 800)
                if (GameManager.BlueDust < GameManager.SpecialNeedBlueDust[GameManager.Option_LevelPMA - 800]) check = true;
            if (GameManager.Option_LevelPMA >= 1500)
                if (GameManager.PurpleDust < GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelPMA - 1500]) check = true;

            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Option_LevelPMA++;
            GameManager.Option_PMA += 1;


            OptionLevelText[0].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Option_LevelPMA);
            OptionLeftText[0].text = TextFormatter.GetThousandCommaText((long)GameManager.Option_PMA) + "%";
            OptionRightText[0].text = TextFormatter.GetThousandCommaText((long)GameManager.Option_PMA + 1) + "%";

            Option_NeeditemPMA[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Option_LevelPMA - 1]) + "";
            GameManager.BrownDust -= GameManager.SpecialNeedBrownDust[GameManager.Option_LevelPMA - 2];

            if (GameManager.Option_LevelPMA >= 10)
            {
                Option_NeeditemPMA[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPMA - 10]) + "";
                if (GameManager.Option_LevelPMA != 10) GameManager.OrangeDust -= GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPMA - 11];
            }

            if (GameManager.Option_LevelPMA >= 30)
            {
                Option_NeeditemPMA[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Option_LevelPMA - 30]) + "";
                if (GameManager.Option_LevelPMA != 30) GameManager.LimeDust -= GameManager.SpecialNeedLimeDust[GameManager.Option_LevelPMA - 31];
            }

            if (GameManager.Option_LevelPMA >= 60)
            {
                Option_NeeditemPMA[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Option_LevelPMA - 60]) + "";
                if (GameManager.Option_LevelPMA != 60) GameManager.BlackDust -= GameManager.SpecialNeedBlackDust[GameManager.Option_LevelPMA - 61];
            }

            if (GameManager.Option_LevelPMA >= 120)
            {
                Option_NeeditemPMA[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Option_LevelPMA - 120]) + "";
                if (GameManager.Option_LevelPMA != 120) GameManager.RedDust -= GameManager.SpecialNeedRedDust[GameManager.Option_LevelPMA - 121];
            }

            if (GameManager.Option_LevelPMA >= 240)
            {
                Option_NeeditemPMA[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Option_LevelPMA - 240]) + "";
                if (GameManager.Option_LevelPMA != 240) GameManager.YellowDust -= GameManager.SpecialNeedYellowDust[GameManager.Option_LevelPMA - 241];
            }

            if (GameManager.Option_LevelPMA >= 480)
            {
                Option_NeeditemPMA[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Option_LevelPMA - 480]) + "";
                if (GameManager.Option_LevelPMA != 480) GameManager.DirtDust -= GameManager.SpecialNeedDirtDust[GameManager.Option_LevelPMA - 481];
            }

            if (GameManager.Option_LevelPMA >= 800)
            {
                Option_NeeditemPMA[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Option_LevelPMA - 800]) + "";
                if (GameManager.Option_LevelPMA != 800) GameManager.BlueDust -= GameManager.SpecialNeedBlueDust[GameManager.Option_LevelPMA - 801];
            }

            if (GameManager.Option_LevelPMA >= 1500)
            {
                Option_NeeditemPMA[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelPMA - 1500]) + "";
                if (GameManager.Option_LevelPMA != 1500) GameManager.PurpleDust -= GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelPMA - 1501];
            }
        }
        //  옵션 럭키 광물 업그레이드
        else if (index == 1)
        {
            if (GameManager.BrownDust < GameManager.SpecialNeedBrownDust[GameManager.Option_LevelMB - 1]) check = true;

            if (GameManager.Option_LevelMB >= 10)
                if (GameManager.OrangeDust < GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelMB - 10]) check = true;
            if (GameManager.Option_LevelMB >= 30)
                if (GameManager.LimeDust < GameManager.SpecialNeedLimeDust[GameManager.Option_LevelMB - 30]) check = true;
            if (GameManager.Option_LevelMB >= 60)
                if (GameManager.BlackDust < GameManager.SpecialNeedBlackDust[GameManager.Option_LevelMB - 60]) check = true;
            if (GameManager.Option_LevelMB >= 120)
                if (GameManager.RedDust < GameManager.SpecialNeedRedDust[GameManager.Option_LevelMB - 120]) check = true;
            if (GameManager.Option_LevelMB >= 240)
                if (GameManager.YellowDust < GameManager.SpecialNeedYellowDust[GameManager.Option_LevelMB - 240]) check = true;
            if (GameManager.Option_LevelMB >= 480)
                if (GameManager.DirtDust < GameManager.SpecialNeedDirtDust[GameManager.Option_LevelMB - 480]) check = true;
            if (GameManager.Option_LevelMB >= 800)
                if (GameManager.BlueDust < GameManager.SpecialNeedBlueDust[GameManager.Option_LevelMB - 800]) check = true;
            if (GameManager.Option_LevelMB >= 1500)
                if (GameManager.PurpleDust < GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelMB - 1500]) check = true;

            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Option_LevelMB++;
            GameManager.Option_MB += 0.1m;


            OptionLevelText[1].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Option_LevelMB);
            OptionLeftText[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Option_MB) + "%";
            OptionRightText[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Option_MB + 0.1m) + "%";

            Option_NeeditemMB[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Option_LevelMB - 1]) + "";
            GameManager.BrownDust -= GameManager.SpecialNeedBrownDust[GameManager.Option_LevelMB - 2];

            if (GameManager.Option_LevelMB >= 10)
            {
                Option_NeeditemMB[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelMB - 10]) + "";
                if (GameManager.Option_LevelMB != 10) GameManager.OrangeDust -= GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelMB - 11];
            }

            if (GameManager.Option_LevelMB >= 30)
            {
                Option_NeeditemMB[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Option_LevelMB - 30]) + "";
                if (GameManager.Option_LevelMB != 30) GameManager.LimeDust -= GameManager.SpecialNeedLimeDust[GameManager.Option_LevelMB - 31];
            }

            if (GameManager.Option_LevelMB >= 60)
            {
                Option_NeeditemMB[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Option_LevelMB - 60]) + "";
                if (GameManager.Option_LevelMB != 60) GameManager.BlackDust -= GameManager.SpecialNeedBlackDust[GameManager.Option_LevelMB - 61];
            }

            if (GameManager.Option_LevelMB >= 120)
            {
                Option_NeeditemMB[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Option_LevelMB - 120]) + "";
                if (GameManager.Option_LevelMB != 120) GameManager.RedDust -= GameManager.SpecialNeedRedDust[GameManager.Option_LevelMB - 121];
            }

            if (GameManager.Option_LevelMB >= 240)
            {
                Option_NeeditemMB[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Option_LevelMB - 240]) + "";
                if (GameManager.Option_LevelMB != 240) GameManager.YellowDust -= GameManager.SpecialNeedYellowDust[GameManager.Option_LevelMB - 241];
            }

            if (GameManager.Option_LevelMB >= 480)
            {
                Option_NeeditemMB[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Option_LevelMB - 480]) + "";
                if (GameManager.Option_LevelMB != 480) GameManager.DirtDust -= GameManager.SpecialNeedDirtDust[GameManager.Option_LevelMB - 481];
            }

            if (GameManager.Option_LevelMB >= 800)
            {
                Option_NeeditemMB[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Option_LevelMB - 800]) + "";
                if (GameManager.Option_LevelMB != 800) GameManager.BlueDust -= GameManager.SpecialNeedBlueDust[GameManager.Option_LevelMB - 801];
            }

            if (GameManager.Option_LevelMB >= 1500)
            {
                Option_NeeditemMB[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelMB - 1500]) + "";
                if (GameManager.Option_LevelMB != 1500) GameManager.PurpleDust -= GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelMB - 1501];
            }
        }
        //  옵션 치명적 피해 업그레이드
        else if (index == 2)
        {
            if (GameManager.BrownDust < GameManager.SpecialNeedBrownDust[GameManager.Option_LevelPFD - 1]) check = true;

            if (GameManager.Option_LevelPFD >= 10)
                if (GameManager.OrangeDust < GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPFD - 10]) check = true;
            if (GameManager.Option_LevelPFD >= 30)
                if (GameManager.LimeDust < GameManager.SpecialNeedLimeDust[GameManager.Option_LevelPFD - 30]) check = true;
            if (GameManager.Option_LevelPFD >= 60)
                if (GameManager.BlackDust < GameManager.SpecialNeedBlackDust[GameManager.Option_LevelPFD - 60]) check = true;
            if (GameManager.Option_LevelPFD >= 120)
                if (GameManager.RedDust < GameManager.SpecialNeedRedDust[GameManager.Option_LevelPFD - 120]) check = true;
            if (GameManager.Option_LevelPFD >= 240)
                if (GameManager.YellowDust < GameManager.SpecialNeedYellowDust[GameManager.Option_LevelPFD - 240]) check = true;
            if (GameManager.Option_LevelPFD >= 480)
                if (GameManager.DirtDust < GameManager.SpecialNeedDirtDust[GameManager.Option_LevelPFD - 480]) check = true;
            if (GameManager.Option_LevelPFD >= 800)
                if (GameManager.BlueDust < GameManager.SpecialNeedBlueDust[GameManager.Option_LevelPFD - 800]) check = true;
            if (GameManager.Option_LevelPFD >= 1500)
                if (GameManager.PurpleDust < GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelPFD - 1500]) check = true;

            if (check == true)
            {
                lackBG.SetActive(true);
                return;
            }

            GameManager.Option_LevelPFD++;
            GameManager.Option_PFD += 0.01m;


            OptionLevelText[2].text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Option_LevelPFD);
            OptionLeftText[2].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Option_PFD) + "%";
            OptionRightText[2].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Option_PFD + 0.01m) + "%";

            Option_NeeditemPFD[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBrownDust[GameManager.Option_LevelPFD - 1]) + "";
            GameManager.BrownDust -= GameManager.SpecialNeedBrownDust[GameManager.Option_LevelPFD - 2];

            if (GameManager.Option_LevelPFD >= 10)
            {
                Option_NeeditemPFD[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPFD - 10]) + "";
                if (GameManager.Option_LevelPFD != 10) GameManager.OrangeDust -= GameManager.SpecialNeedOrangeDust[GameManager.Option_LevelPFD - 11];
            }

            if (GameManager.Option_LevelPFD >= 30)
            {
                Option_NeeditemPFD[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedLimeDust[GameManager.Option_LevelPFD - 30]) + "";
                if (GameManager.Option_LevelPFD != 30) GameManager.LimeDust -= GameManager.SpecialNeedLimeDust[GameManager.Option_LevelPFD - 31];
            }

            if (GameManager.Option_LevelPFD >= 60)
            {
                Option_NeeditemPFD[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlackDust[GameManager.Option_LevelPFD - 60]) + "";
                if (GameManager.Option_LevelPFD != 60) GameManager.BlackDust -= GameManager.SpecialNeedBlackDust[GameManager.Option_LevelPFD - 61];
            }

            if (GameManager.Option_LevelPFD >= 120)
            {
                Option_NeeditemPFD[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedRedDust[GameManager.Option_LevelPFD - 120]) + "";
                if (GameManager.Option_LevelPFD != 120) GameManager.RedDust -= GameManager.SpecialNeedRedDust[GameManager.Option_LevelPFD - 121];
            }

            if (GameManager.Option_LevelPFD >= 240)
            {
                Option_NeeditemPFD[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedYellowDust[GameManager.Option_LevelPFD - 240]) + "";
                if (GameManager.Option_LevelPFD != 240) GameManager.YellowDust -= GameManager.SpecialNeedYellowDust[GameManager.Option_LevelPFD - 241];
            }

            if (GameManager.Option_LevelPFD >= 480)
            {
                Option_NeeditemPFD[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedDirtDust[GameManager.Option_LevelPFD - 480]) + "";
                if (GameManager.Option_LevelPFD != 480) GameManager.DirtDust -= GameManager.SpecialNeedDirtDust[GameManager.Option_LevelPFD - 481];
            }

            if (GameManager.Option_LevelPFD >= 800)
            {
                Option_NeeditemPFD[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedBlueDust[GameManager.Option_LevelPFD - 800]) + "";
                if (GameManager.Option_LevelPFD != 800) GameManager.BlueDust -= GameManager.SpecialNeedBlueDust[GameManager.Option_LevelPFD - 801];
            }

            if (GameManager.Option_LevelPFD >= 1500)
            {
                Option_NeeditemPFD[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelPFD - 1500]) + "";
                if (GameManager.Option_LevelPFD != 1500) GameManager.PurpleDust -= GameManager.SpecialNeedPurpleDust[GameManager.Option_LevelPFD - 1501];
            }
        }
    }

    private int Save_index = 0;
    private int Check = 0;

    [System.Obsolete]
    public static void GrantRewards(int Rock_defeatedIndex, int Bonus)
    {
        float Reward_RocksRandom = Random.value;

        int Reward_BrownDust;
        int Reward_OrangeDust;
        int Reward_LimeDust;
        int Reward_BlackDust;
        int Reward_RedDust;
        int Reward_YellowDust;
        int Reward_DirtDust;
        int Reward_BlueDust;
        int Reward_PurpleDust;
        int Reward_ReinforceScroll;

        Reward_BrownDust = 0;
        Reward_OrangeDust = 0;
        Reward_LimeDust = 0;
        Reward_BlackDust = 0;
        Reward_RedDust = 0;
        Reward_YellowDust = 0;
        Reward_DirtDust = 0;
        Reward_BlueDust = 0;
        Reward_PurpleDust = 0;
        Reward_ReinforceScroll = 0;

        // 광물의 정보를 가져와서 해당 보상을 계산

        float[] dustProbabilities = {
            rock.n_rocks[Rock_defeatedIndex].BrownDust,
            rock.n_rocks[Rock_defeatedIndex].OrangeDust,
            rock.n_rocks[Rock_defeatedIndex].LimeDust,
            rock.n_rocks[Rock_defeatedIndex].BlackDust,
            rock.n_rocks[Rock_defeatedIndex].RedDust,
            rock.n_rocks[Rock_defeatedIndex].YellowDust,
            rock.n_rocks[Rock_defeatedIndex].DirtDust,
            rock.n_rocks[Rock_defeatedIndex].BlueDust,
            rock.n_rocks[Rock_defeatedIndex].PurpleDust,
            rock.n_rocks[Rock_defeatedIndex].ReinforceScroll
        };

        int[] dustRewards = new int[dustProbabilities.Length];
        float cumulativeProbability = 0;

        for (int i = 0; i < dustProbabilities.Length; i++)
        {
            cumulativeProbability += dustProbabilities[i] / 100f;
            if (Reward_RocksRandom < cumulativeProbability)
            {
                dustRewards[i] = GameManager.Rock_defeatedIndex + 1 + GameManager.Mineral_MI;
                break; // 보상이 결정되면 루프 종료
            }
        }

        // 각 보상 변수에 값 할당
        Reward_BrownDust = dustRewards[0];
        Reward_OrangeDust = dustRewards[1];
        Reward_LimeDust = dustRewards[2];
        Reward_BlackDust = dustRewards[3];
        Reward_RedDust = dustRewards[4];
        Reward_YellowDust = dustRewards[5];
        Reward_DirtDust = dustRewards[6];
        Reward_BlueDust = dustRewards[7];
        Reward_PurpleDust = dustRewards[8];
        Reward_ReinforceScroll = dustRewards[9];

        Debug.Log("광물 확률 : " + rock.n_rocks[Rock_defeatedIndex].BrownDust / 100f + " | " + Reward_RocksRandom);
        Debug.Log("광물 확률 : " + rock.n_rocks[Rock_defeatedIndex].OrangeDust / 100f + " | " + Reward_RocksRandom);
        Debug.Log("광물 확률 : " + rock.n_rocks[Rock_defeatedIndex].LimeDust / 100f + " | " + Reward_RocksRandom);

        // 권능

        if(GameManager.WarrantLevel[9] >= 1) Bonus += GameManager.Warrant_Power[9];
        if (MineAdManager.AdPlaying[2] == true) Bonus += MineAdManager.AdPowerValue[2];

        // 플레이어에게 보상 지급
        GameManager.BrownDust += Reward_BrownDust * Bonus;
        GameManager.OrangeDust += Reward_OrangeDust * Bonus;
        GameManager.LimeDust += Reward_LimeDust * Bonus;
        GameManager.BlackDust += Reward_BlackDust * Bonus;
        GameManager.RedDust += Reward_RedDust * Bonus;
        GameManager.YellowDust += Reward_YellowDust * Bonus;
        GameManager.DirtDust += Reward_DirtDust * Bonus;
        GameManager.BlueDust += Reward_BlueDust * Bonus;
        GameManager.PurpleDust += Reward_PurpleDust * Bonus;
        //GameManager.ReinforceScroll += Reward_ReinforceScroll;

        RocksCurHP = RocksMaxHP;
        
    }


    
}
