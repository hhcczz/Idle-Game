using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;   //UI 클릭시 터치 이벤트 발생 방지.



[System.Serializable]

public class MinerManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject TextPrefab;

    private AudioSource audioSource; // AudioSource 변수 추가

    public AudioClip MiningSoundClip; // AudioClip 변수 선언

    [System.Obsolete]

    public RectTransform touchableArea; // 터치 가능한 영역을 지정하기 위한 RectTransform

    public Image[] Upstair_HaveImg;

    public Image[] MineUpgradeImg;
    public Sprite[] MineUpgradeSprite;

    public Image[] MineNeedImg_1;
    public Image[] MineNeedImg_2;
    public Image[] MineNeedImg_3;

    public Image[] MineFrame;

    public Text[] MineNeedItemText_1;               //  필요한 광물 텍스트  EX) 곡괭이 데미지 증가때 사용
    public Text[] MineNeedItemText_2;               //  필요한 광물 텍스트  EX) 곡괭이 크리티컬 데미지 증가때 사용
    public Text[] MineNeedItemText_3;               //  필요한 광물 텍스트  EX) 곡괭이 크리티컬 확률 증가때 사용

    public Text[] MineUpgradeInTitle;

    public Text[] MineLevelText;                    //  광산 레벨 텍스트
    public Text[] MineLeftText;                     //  광산 능력치 증가전 표시
    public Text[] MineRightText;                    //  광산 능력치 증가후 표시

    public Button[] MineLevelUpBtn;                 //  레벨업 버튼
    public Button[] MineSelBtn;
    public Button[] MineUpgradePanelOpenBtn;

    public Text[] Own_MineralText;                  //  가지고 있는 광물 텍스트
    public Text[] BasicInfo;                        //  광산 기본 정보

    public Text Rocks_Name;                         //  돌 이름
    public Slider Rocks_Slider;                     //  돌 체력 슬라이더
    public Text Rocks_HPText;                       //  돌 체력 텍스트

    public GameObject PickaxeUpgradePanel;
    public GameObject BasicPanel;
    public GameObject UpgradePanel;
    public Text UpgradeTitleText;

    public static float RocksNormalDamage;
    public static float RocksCriticalDamage;

    public GameObject effectPrefab; // 이펙트 프리팹
    public RectTransform parentRectTransform; // 부모 Rect Transform

    bool RocksInfoOpen = false;

    int index;

    public static int executions = 0;

    public GameObject lackBG;
    public Button lackoutBtn;

    private int InUpgradePanel = -1;

    private bool check = false;

    // 업그레이드 패널 선택 이름
    private string[] UpgradeSelTitleString = new string[3]
        {
            "곡괭이 강화",
            "광물 강화",
            "옵션 강화",
        };

    // 업그레이드 장비 업그레이드 이름
    private string[] UpgradeInTitleString = new string[9]
        {
            "곡괭이 공격력",
            "크리티컬 확률",
            "크리티컬 데미지",

            "광물 클릭 제한",
            "광물 약점",
            "강화 스크롤 확률",

            "데미지 증폭",
            "럭키 광물",
            "치명적 피해",
        };


    void Start()
    {
        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        InUpgradePanel = 0;

        RockManager.currentHP = RockManager.maxHP;

        lackoutBtn.onClick.AddListener(LackOut);

        for (int i = 0; i < MineSelBtn.Length; i++)
        {
            int index = i;

            MineSelBtn[index].onClick.AddListener(() => MineUpgradeChange(index));
            MineUpgradePanelOpenBtn[index].onClick.AddListener(() => UpgradeOpen(index));
        }
        
        for (int i = 0; i < 9; i++)
        {
            Upstair_HaveImg[i].sprite = GameManager.GemSprites[i];

            MineNeedImg_1[i].sprite = GameManager.GemSprites[i];
            MineNeedImg_2[i].sprite = GameManager.GemSprites[i];
            MineNeedImg_3[i].sprite = GameManager.GemSprites[i];
        }

        // 광산 레벨업 리스너
        for (int i = 0; i < MineLevelUpBtn.Length; i++)
        {
            int index = i;

            MineLevelUpBtn[index].onClick.AddListener(() => MineLevelUP(index));
        }

        UpdateHealth();
        UpgradeTitleText.text = "Upgrade";
        UpdateBasicInfo();
    }


    // 광물 체력 업데이트 관리
    private void UpdateHealth()
    {
        Rocks_Slider.value = (float)(RockManager.currentHP / RockManager.maxHP); // 최대 체력에 대한 현재 체력의 비율로 설정

        float healthPercentage = (float)(RockManager.currentHP / RockManager.maxHP) * 100f;

        if (healthPercentage >= 10)
            Rocks_HPText.text = healthPercentage.ToString("00.00") + " %";
        else
            Rocks_HPText.text = healthPercentage.ToString("0.00") + " %";
    }

    private void UpgradeOpen(int index)
    {
        InUpgradePanel = index;
        UpgradeTitleText.text = UpgradeSelTitleString[index];
        if (index == 0) UpdateMineLeftRightText(index, GameManager.Pickaxe_Damage, GameManager.Pickaxe_CriticalChance, GameManager.Pickaxe_CriticalDamage);
        else if(index == 1) UpdateMineLeftRightText(index, GameManager.Mineral_MI, GameManager.Mineral_HP, GameManager.Mineral_RS);
        else if (index == 2) UpdateMineLeftRightText(index, GameManager.Option_PMA, GameManager.Option_MB, GameManager.Option_PFD);

        for (int i = 0; i < MineUpgradeImg.Length; i++)
        {
            MineUpgradeImg[i].sprite = MineUpgradeSprite[i + 3 * index];
            MineUpgradeInTitle[i].text = UpgradeInTitleString[i + 3 * index];
            if (index == 2)
            {
                MineFrame[i].color = ColorManager.ColorChange("검정색");
                MineFrame[i].sprite = MineUpgradeSprite[10];
            }
            else
            {
                MineFrame[i].color = ColorManager.ColorChange("하얀색");
                MineFrame[i].sprite = MineUpgradeSprite[9];
            }

        }
        BasicPanel.SetActive(false);
        UpgradePanel.SetActive(true);
    }

    [System.Obsolete]
    void Update()
    {
        Own_MineralText[0].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarGrey) + "";
        Own_MineralText[1].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarBrown) + "";
        Own_MineralText[2].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarBlue) + "";
        Own_MineralText[3].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarGreen) + "";
        Own_MineralText[4].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarRed) + "";
        Own_MineralText[5].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarYellow) + "";
        Own_MineralText[6].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarPurple) + "";
        Own_MineralText[7].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarOrange) + "";
        Own_MineralText[8].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarDark) + "";

        if (RocksInfoOpen == true && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            RocksInfoOpen = false;
        }
        StartCoroutine(RockManager.RegenHP());
        UpdateHealth();
    }
    

    public static int maxExecutions = 3;

    // 클릭 초기화 1초에 N번 클릭가능
    public static IEnumerator ResetExecutionsAfterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            executions = 0;
            Debug.Log("광물 초기화!");
        }
    }

    // 클릭 포인터 받기
    [System.Obsolete]
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(touchableArea, eventData.position, eventData.pressEventCamera, out localPoint);

        // 터치 가능한 영역이면 HandleTouchInput 호출
        if (touchableArea.rect.Contains(localPoint))
        {
            // 최대 3번까지만 HandleTouchInput 실행
            if (executions >= maxExecutions + (int)GameManager.Mineral_MI) return;

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

        RocksNormalDamage = (float)GameManager.Pickaxe_Damage * (1 + (float)GameManager.Option_PMA / 100);
        RocksCriticalDamage = (float)(GameManager.Pickaxe_Damage + GameManager.Pickaxe_CriticalDamage) * (1 + (float)GameManager.Option_PMA / 100);

        int Random_Critical = Random.RandomRange(0, 100); // 크리티컬 확률 조건
        int FatalDamage = Random.RandomRange(0, 100); // 치명적 피해 확률 조건
        int Warrant = Random.RandomRange(0, 100);   //  권능 확률 조건

        float damageAmount = Random_Critical < GameManager.Pickaxe_CriticalChance ? RocksCriticalDamage : RocksNormalDamage;

        if (MineAdManager.AdPlaying[0] == true) damageAmount *= (float)(MineAdManager.AdPowerValue[0] / 50f);

        if (FatalDamage < GameManager.Option_PFD) RockManager.currentHP -= RockManager.currentHP / 3;    // 치명적 피해

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

        RockManager.currentHP -= damageAmount;

        int MineralBomb = Random.RandomRange(0, 100); // 광물 폭탄 확률 조건
        if (RockManager.currentHP <= 0)
        {
            if (GameManager.RockstageClearDict[RockManager.Instance.RockType][RockManager.Rock_defeatedIndex + 1] == false)
            {
                GameManager.RockstageClearDict[RockManager.Instance.RockType][RockManager.Rock_defeatedIndex + 1] = true;
            }
        
            if (MineralBomb < GameManager.Option_LevelMB) RockManager.Instance.GrantRewards(RockManager.Rock_defeatedIndex, 20);
            else RockManager.Instance.GrantRewards(RockManager.Rock_defeatedIndex, 1);
        }

        // 이펙트를 생성하고 부모 Rect Transform의 좌표계를 기준으로 위치를 설정합니다.
        GameObject effectInstance = Instantiate(effectPrefab, parentRectTransform);
        Destroy(effectInstance, 1f);
    }

    // 광물 부족 알림 패널
    private void LackOut()
    {
        lackBG.SetActive(false);
    }

    // 업그레이드 총합 정보 관리
    private void UpdateBasicInfo()
    {
        BasicInfo[0].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Pickaxe_Damage) + "";
        BasicInfo[1].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Pickaxe_CriticalChance) + "%";
        BasicInfo[2].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Pickaxe_CriticalDamage) + "%";

        BasicInfo[3].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Mineral_MI + maxExecutions) + "번";
        BasicInfo[4].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Mineral_HP) + "%";
        BasicInfo[5].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Mineral_RS) + "%";

        BasicInfo[6].text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Option_PMA) + "%";
        BasicInfo[7].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Option_MB) + "%";
        BasicInfo[8].text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Option_PFD) + "%";
    }

    private long ExchangeNeedStar(int level, string grade)
    {
        long ReturnValue;

        if (grade == "일반")
        {
            ReturnValue = level * 5;
        }
        else
        {
            ReturnValue = level * 15;
        }

        return ReturnValue;
    }

    // 레벨 체크하기
    private int[] DevideLevel = new int[9]
    {
        0,
        10,
        30,
        60,
        120,
        240,
        480,
        800,
        1500,
    };

    // 필요 아이템 개수 업데이트
    private void UpdateMineNeedItemText(Text[] texts, string grade, int level)
    {
        for(int i = 0; i < 9; i++)
        {
            if (level >= DevideLevel[i] && i != 0) texts[i].text = TextFormatter.GetThousandCommaText(ExchangeNeedStar(level - DevideLevel[i] + 1, grade)) + "";
            else if (i == 0) texts[0].text = TextFormatter.GetThousandCommaText(ExchangeNeedStar(level, grade)) + "";
            else texts[i].text = "0";
        }
        

    }

    // 업그레이드 좌우 값 변경
    private void UpdateMineLeftRightText(int index, decimal Value_1, decimal Value_2, decimal Value_3)
    {
        // 곡괭이 패널
        if (index == 0)
        {
            MineLeftText[0].text = TextFormatter.GetDecimalPointCommaText_0(Value_1) + "";
            MineLeftText[1].text = TextFormatter.GetDecimalPointCommaText_0(Value_2) + "%";
            MineLeftText[2].text = TextFormatter.GetDecimalPointCommaText_0(Value_3) + "%";

            MineRightText[0].text = TextFormatter.GetDecimalPointCommaText_0(Value_1 + 1m) + "";
            MineRightText[1].text = TextFormatter.GetDecimalPointCommaText_0(Value_2 + 0.1m) + "%";
            MineRightText[2].text = TextFormatter.GetDecimalPointCommaText_0(Value_3 + 1m) + "%";
        }
        // 광물 패널
        if (index == 1)
        {
            MineLeftText[0].text = TextFormatter.GetDecimalPointCommaText_0(Value_1) + "번";
            MineLeftText[1].text = TextFormatter.GetDecimalPointCommaText_0(Value_2) + "%";
            MineLeftText[2].text = TextFormatter.GetDecimalPointCommaText_00(Value_3) + "%";

            MineRightText[0].text = TextFormatter.GetDecimalPointCommaText_0(Value_1 + 0.2m) + "번";
            MineRightText[1].text = TextFormatter.GetDecimalPointCommaText_0(Value_2 + 0.1m) + "%";
            MineRightText[2].text = TextFormatter.GetDecimalPointCommaText_00(Value_3 + 0.01m) + "%";
        }
        // 옵션 패널
        if (index == 2)
        {
            MineLeftText[0].text = TextFormatter.GetDecimalPointCommaText_0(Value_1) + "%";
            MineLeftText[1].text = TextFormatter.GetDecimalPointCommaText_00(Value_2) + "%";
            MineLeftText[2].text = TextFormatter.GetDecimalPointCommaText_00(Value_3) + "%";

            MineRightText[0].text = TextFormatter.GetDecimalPointCommaText_0(Value_1 + 1.2m) + "%";
            MineRightText[1].text = TextFormatter.GetDecimalPointCommaText_00(Value_2 + 0.05m) + "%";
            MineRightText[2].text = TextFormatter.GetDecimalPointCommaText_00(Value_3 + 0.01m) + "%";
        }
    }

    // 곡괭이 강화 화면 변경
    private void MineUpgradeChange(int index)
    {
        if(InUpgradePanel == index)
        {
            BasicPanel.SetActive(true);
            UpgradePanel.SetActive(false);
            return;
        }

        InUpgradePanel = index;

        // 다시 누르면 Basic으로 돌아가는거 제작
        Debug.Log("체인지 버튼 누름");
        if (index == 0)
        {
            UpdateMineLevelText(GameManager.Pickaxe_DamageLv, GameManager.Pickaxe_CriticalChance_Level, GameManager.Pickaxe_CriticalDamage_Level);
            UpdateMineLeftRightText(index, GameManager.Pickaxe_Damage, GameManager.Pickaxe_CriticalChance, GameManager.Pickaxe_CriticalDamage);
            UpdateMineNeedItemText(MineNeedItemText_1, "일반", GameManager.Pickaxe_DamageLv);
            UpdateMineNeedItemText(MineNeedItemText_2, "고급", GameManager.Pickaxe_CriticalChance_Level);
            UpdateMineNeedItemText(MineNeedItemText_3, "일반", GameManager.Pickaxe_CriticalDamage_Level);
        }
        else if (index == 1)
        {
            UpdateMineLevelText(GameManager.Mineral_LevelMI, GameManager.Mineral_LevelHP, GameManager.Mineral_LevelRS);
            UpdateMineLeftRightText(index, GameManager.Mineral_MI, GameManager.Mineral_HP, GameManager.Mineral_RS);
            UpdateMineNeedItemText(MineNeedItemText_1, "일반", GameManager.Mineral_LevelMI);
            UpdateMineNeedItemText(MineNeedItemText_2, "일반", GameManager.Mineral_LevelHP);
            UpdateMineNeedItemText(MineNeedItemText_3, "고급", GameManager.Mineral_LevelRS);
        }
        else if (index == 2)
        {
            UpdateMineLevelText(GameManager.Mineral_LevelMI, GameManager.Mineral_LevelHP, GameManager.Mineral_LevelRS);
            UpdateMineLeftRightText(index, GameManager.Option_PMA, GameManager.Option_MB, GameManager.Option_PFD);
            UpdateMineNeedItemText(MineNeedItemText_1, "고급", GameManager.Option_LevelPMA);
            UpdateMineNeedItemText(MineNeedItemText_2, "고급", GameManager.Option_LevelMB);
            UpdateMineNeedItemText(MineNeedItemText_3, "고급", GameManager.Option_LevelPFD);
        }

        for (int i = 0; i < MineUpgradeImg.Length; i++)
        {
            MineUpgradeImg[i].sprite = MineUpgradeSprite[i + 3 * index];
            MineUpgradeInTitle[i].text = UpgradeInTitleString[i + 3 * index];

            if (index == 2)
            {
                MineFrame[i].color = ColorManager.ColorChange("검정색");
                MineFrame[i].sprite = MineUpgradeSprite[10];
            }
            else
            {
                MineFrame[i].color = ColorManager.ColorChange("하얀색");
                MineFrame[i].sprite = MineUpgradeSprite[9];
            }
        }
        
        UpgradeTitleText.text = UpgradeSelTitleString[index];

        UpdateBasicInfo();
    }

    private void UpdateMineLevelText(int Level_1, int Level_2, int Level_3)
    {
        MineLevelText[0].text = "Lv. " + TextFormatter.GetThousandCommaText(Level_1);
        MineLevelText[1].text = "Lv. " + TextFormatter.GetThousandCommaText(Level_2);
        MineLevelText[2].text = "Lv. " + TextFormatter.GetThousandCommaText(Level_3);
    }

    
    private bool BuyChecking(int level, string grade)
    {
        Debug.Log("검사 들어갔음 : " + level + " Grade : " + grade);
        if (level >= DevideLevel[0] && GameManager.HaveStarGrey < ExchangeNeedStar(level - DevideLevel[0]   - 0, grade)) check = true;
        if (level >= DevideLevel[1] && GameManager.HaveStarBrown < ExchangeNeedStar(level - DevideLevel[1]  + 1, grade)) check = true;
        if (level >= DevideLevel[2] && GameManager.HaveStarBlue < ExchangeNeedStar(level - DevideLevel[2]   + 1, grade)) check = true;
        if (level >= DevideLevel[3] && GameManager.HaveStarGreen < ExchangeNeedStar(level - DevideLevel[3]  + 1, grade)) check = true;
        if (level >= DevideLevel[4] && GameManager.HaveStarRed < ExchangeNeedStar(level - DevideLevel[4]    + 1, grade)) check = true;
        if (level >= DevideLevel[5] && GameManager.HaveStarYellow < ExchangeNeedStar(level - DevideLevel[5] + 1, grade)) check = true;
        if (level >= DevideLevel[6] && GameManager.HaveStarPurple < ExchangeNeedStar(level - DevideLevel[6] + 1, grade)) check = true;
        if (level >= DevideLevel[7] && GameManager.HaveStarOrange < ExchangeNeedStar(level - DevideLevel[7] + 1, grade)) check = true;
        if (level >= DevideLevel[8] && GameManager.HaveStarDark < ExchangeNeedStar(level - DevideLevel[8]   + 1, grade)) check = true;

        return check;
    }

    // 곡괭이 업그레이드

    private void TakeUpgradeStar(int level, string grade)
    {
        Debug.Log("뺏었음 : " + level + " Grade : " + grade);
        if (level >= DevideLevel[0]) GameManager.HaveStarGrey -= ExchangeNeedStar(level - DevideLevel[0]    + 0, grade);
        if (level >= DevideLevel[1]) GameManager.HaveStarBrown -= ExchangeNeedStar(level - DevideLevel[1]   + 1, grade);
        if (level >= DevideLevel[2]) GameManager.HaveStarBlue -= ExchangeNeedStar(level - DevideLevel[2]    + 1, grade);
        if (level >= DevideLevel[3]) GameManager.HaveStarGreen -= ExchangeNeedStar(level - DevideLevel[3]   + 1, grade);
        if (level >= DevideLevel[4]) GameManager.HaveStarRed -= ExchangeNeedStar(level - DevideLevel[4]     + 1, grade);
        if (level >= DevideLevel[5]) GameManager.HaveStarYellow -= ExchangeNeedStar(level - DevideLevel[5]  + 1, grade);
        if (level >= DevideLevel[6]) GameManager.HaveStarPurple -= ExchangeNeedStar(level - DevideLevel[6]  + 1, grade);
        if (level >= DevideLevel[7]) GameManager.HaveStarOrange -= ExchangeNeedStar(level - DevideLevel[7]  + 1, grade);
        if (level >= DevideLevel[8]) GameManager.HaveStarDark -= ExchangeNeedStar(level - DevideLevel[8]    + 1, grade);
    }

    private void MineLevelUP(int index)
    {
        check = false;
        Debug.Log("마인 패널 : " + InUpgradePanel);
        if (InUpgradePanel == 0)
        {
            if (index == 0)
            {
                Debug.Log("검사 들어옴  : " + index);
                if (BuyChecking(GameManager.Pickaxe_DamageLv, "일반") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Pickaxe_DamageLv, "일반");

                GameManager.Pickaxe_DamageLv++;
                GameManager.Pickaxe_Damage += 1m;

                UpdateMineNeedItemText(MineNeedItemText_1, "일반", GameManager.Pickaxe_DamageLv);
            }
            else if (index == 1)
            {
                Debug.Log("검사 들어옴  : " + index);

                if (BuyChecking(GameManager.Pickaxe_CriticalChance_Level, "고급") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Pickaxe_CriticalChance_Level, "고급");
                GameManager.Pickaxe_CriticalChance_Level++;
                GameManager.Pickaxe_CriticalChance += 0.1m;


                UpdateMineNeedItemText(MineNeedItemText_2, "고급", GameManager.Pickaxe_CriticalChance_Level);
            }
            else if (index == 2)
            {
                Debug.Log("검사 들어옴  : " + index);

                if (BuyChecking(GameManager.Pickaxe_CriticalDamage_Level, "일반") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Pickaxe_CriticalDamage_Level, "일반");

                GameManager.Pickaxe_CriticalDamage_Level++;
                GameManager.Pickaxe_CriticalDamage += 1m;

                UpdateMineNeedItemText(MineNeedItemText_3, "일반", GameManager.Pickaxe_CriticalDamage_Level);
            }

            UpdateMineLevelText(GameManager.Pickaxe_DamageLv, GameManager.Pickaxe_CriticalChance_Level, GameManager.Pickaxe_CriticalDamage_Level);
            UpdateMineLeftRightText(InUpgradePanel, GameManager.Pickaxe_Damage, GameManager.Pickaxe_CriticalChance, GameManager.Pickaxe_CriticalDamage);

        }
        if (InUpgradePanel == 1)
        {
            if (index == 0)
            {
                if (BuyChecking(GameManager.Mineral_LevelMI, "일반") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Mineral_LevelMI, "일반");

                GameManager.Mineral_LevelMI++;
                GameManager.Mineral_MI += 0.2m;

                UpdateMineNeedItemText(MineNeedItemText_1, "일반", GameManager.Mineral_LevelMI);
            }
            else if (index == 1)
            {
                if (BuyChecking(GameManager.Mineral_LevelHP, "고급") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Mineral_LevelHP, "고급");
                GameManager.Mineral_LevelHP++;
                GameManager.Mineral_HP += 0.1m;


                UpdateMineNeedItemText(MineNeedItemText_2, "고급", GameManager.Mineral_LevelHP);
            }
            else if (index == 2)
            {
                if (BuyChecking(GameManager.Mineral_LevelRS, "고급") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Mineral_LevelRS, "고급");

                GameManager.Mineral_LevelRS++;
                GameManager.Mineral_RS += 0.01m;

                UpdateMineNeedItemText(MineNeedItemText_3, "고급", GameManager.Mineral_LevelRS);
            }

            UpdateMineLevelText(GameManager.Mineral_LevelMI, GameManager.Mineral_LevelHP, GameManager.Mineral_LevelRS);
            UpdateMineLeftRightText(InUpgradePanel, GameManager.Mineral_MI, GameManager.Mineral_HP, GameManager.Mineral_RS);

        }
        if (InUpgradePanel == 2)
        {
            if (index == 0)
            {
                if (BuyChecking(GameManager.Option_LevelPMA, "고급") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Option_LevelPMA, "고급");

                GameManager.Option_LevelPMA++;
                GameManager.Option_PMA += 1.2m;

                UpdateMineNeedItemText(MineNeedItemText_1, "고급", GameManager.Option_LevelPMA);
            }
            else if (index == 1)
            {
                if (BuyChecking(GameManager.Option_LevelMB, "고급") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Option_LevelMB, "고급");
                GameManager.Option_LevelMB++;
                GameManager.Option_MB += 0.05m;


                UpdateMineNeedItemText(MineNeedItemText_2, "고급", GameManager.Option_LevelMB);
            }
            else if (index == 2)
            {
                if (BuyChecking(GameManager.Option_LevelPFD, "고급") == true)
                {
                    lackBG.SetActive(true);
                    return;
                }
                TakeUpgradeStar(GameManager.Option_LevelPFD, "고급");

                GameManager.Option_LevelPFD++;
                GameManager.Option_PFD += 0.01m;

                UpdateMineNeedItemText(MineNeedItemText_3, "고급", GameManager.Option_LevelPFD);
            }

            UpdateMineLevelText(GameManager.Option_LevelPMA, GameManager.Option_LevelMB, GameManager.Option_LevelPFD);
            UpdateMineLeftRightText(InUpgradePanel, GameManager.Option_PMA, GameManager.Option_MB, GameManager.Option_PFD);
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

}
