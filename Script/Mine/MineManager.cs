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
    public float HaveStarGrey;
    public float HaveStarBrown;
    public float HaveStarBlue;
    public float HaveStarGreen;
    public float HaveStarRed;
    public float HaveStarYellow;
    public float HaveStarPurple;
    public float HaveStarOrange;
    public float HaveStarDark;
    public float ReinforceScroll;
     
    // 몬스터 정보를 설정하는 메서드
    public void SetRockInfo(string newName, float currentHealth, float maxHealth, float _HaveStarGrey, float _HaveStarBrown, float _HaveStarBlue, float _HaveStarGreen, float _HaveStarRed, float _HaveStarYellow,
        float _HaveStarPurple, float _HaveStarOrange, float _HaveStarDark, float _ReinforceScroll)
    {
        name = newName;
        Curhealth = currentHealth;
        Maxhealth = maxHealth;
        HaveStarGrey = _HaveStarGrey;
        HaveStarBrown = _HaveStarBrown;
        HaveStarBlue = _HaveStarBlue;
        HaveStarGreen = _HaveStarGreen;
        HaveStarRed = _HaveStarRed;
        HaveStarYellow = _HaveStarYellow;
        HaveStarPurple = _HaveStarPurple;
        HaveStarOrange = _HaveStarOrange;
        HaveStarDark = _HaveStarDark;
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

    public static float RocksCurHP;
    public static float RocksMaxHP;

    public static float RocksNormalDamage;
    public static float RocksCriticalDamage;

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

        InUpgradePanel = 0;

        RocksCurHP = RocksMaxHP;

        lackoutBtn.onClick.AddListener(LackOut);

        RockListOpenBtn.onClick.AddListener(() => Rock_LevelListPanel.SetActive(true));
        RockListOpenBtn.onClick.AddListener(() => Rock_DifficultyPanel.SetActive(true));
        RockListExitBtn.onClick.AddListener(RockListExitPanel);
        Rock_SummonsBtn.onClick.AddListener(Rock_Summons);

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

        // 광산 레벨업 리스너
        for (int i = 0; i < MineLevelUpBtn.Length; i++)
        {
            int index = i;

            MineLevelUpBtn[index].onClick.AddListener(() => MineLevelUP(index));
        }

        UpdateHealth();
        UpgradeTitleText.text = "Upgrade";
        UpdateBasicInfo();
        UpdateInfoValue();

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
        StartCoroutine(RegenHP());
        UpdateHealth();
    }
    private IEnumerator RegenHP() // IEnumerator 반환 타입으로 변경
    {
        yield return new WaitForSeconds(0.1f);
        if (RocksCurHP < RocksMaxHP) RocksCurHP += RocksMaxHP / 100f;
        else RocksCurHP = RocksMaxHP;
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
        Rock_InfoText[1].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarGrey) + "%</color>";
        Rock_InfoText[2].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarBrown) + "%</color>";
        Rock_InfoText[3].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarBlue) + "%</color>";
        Rock_InfoText[4].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarGreen) + "%</color>";
        Rock_InfoText[5].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarRed) + "%</color>";
        Rock_InfoText[6].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarYellow) + "%</color>";
        Rock_InfoText[7].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarPurple) + "%</color>";
        Rock_InfoText[8].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarOrange) + "%</color>";
        Rock_InfoText[9].text = "<color=lightblue>" + TextFormatter.GetFloatPointCommaText(rock.n_rocks[GameManager.Rock_InfoStage].HaveStarDark) + "%</color>";
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

    // 광물 업그레이드
    /*
    private void MineralLevelUP(int index)
    {
        bool check = false;
        //  광물 증가량 업그레이드
        if (index == 0)
        {
            if (GameManager.HaveStarGrey < GameManager.SpecialNeedHaveStarGrey[GameManager.Mineral_LevelMI - 1]) check = true;

            if (GameManager.Mineral_LevelMI >= 10)
                if (GameManager.HaveStarBrown < GameManager.SpecialNeedHaveStarBrown[GameManager.Mineral_LevelMI - 10]) check = true;
            if (GameManager.Mineral_LevelMI >= 30)
                if (GameManager.HaveStarBlue < GameManager.SpecialNeedHaveStarBlue[GameManager.Mineral_LevelMI - 30]) check = true;
            if (GameManager.Mineral_LevelMI >= 60)
                if (GameManager.HaveStarGreen < GameManager.SpecialNeedHaveStarGreen[GameManager.Mineral_LevelMI - 60]) check = true;
            if (GameManager.Mineral_LevelMI >= 120)
                if (GameManager.HaveStarRed < GameManager.SpecialNeedHaveStarRed[GameManager.Mineral_LevelMI - 120]) check = true;
            if (GameManager.Mineral_LevelMI >= 240)
                if (GameManager.HaveStarYellow < GameManager.SpecialNeedHaveStarYellow[GameManager.Mineral_LevelMI - 240]) check = true;
            if (GameManager.Mineral_LevelMI >= 480)
                if (GameManager.HaveStarPurple < GameManager.SpecialNeedHaveStarPurple[GameManager.Mineral_LevelMI - 480]) check = true;
            if (GameManager.Mineral_LevelMI >= 800)
                if (GameManager.HaveStarOrange < GameManager.SpecialNeedHaveStarOrange[GameManager.Mineral_LevelMI - 800]) check = true;
            if (GameManager.Mineral_LevelMI >= 1500)
                if (GameManager.HaveStarDark < GameManager.SpecialNeedHaveStarDark[GameManager.Mineral_LevelMI - 1500]) check = true;


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

            Mineral_NeeditemMI[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGrey[GameManager.Mineral_LevelMI - 1]) + "";
            GameManager.HaveStarGrey -= GameManager.SpecialNeedHaveStarGrey[GameManager.Mineral_LevelMI - 2];

            if (GameManager.Mineral_LevelMI >= 10)
            {
                Mineral_NeeditemMI[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBrown[GameManager.Mineral_LevelMI - 10]) + "";
                if (GameManager.Mineral_LevelMI != 10) GameManager.HaveStarBrown -= GameManager.SpecialNeedHaveStarBrown[GameManager.Mineral_LevelMI - 11];
            }

            if (GameManager.Mineral_LevelMI >= 30)
            {
                Mineral_NeeditemMI[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBlue[GameManager.Mineral_LevelMI - 30]) + "";
                if (GameManager.Mineral_LevelMI != 30) GameManager.HaveStarBlue -= GameManager.SpecialNeedHaveStarBlue[GameManager.Mineral_LevelMI - 31];
            }

            if (GameManager.Mineral_LevelMI >= 60)
            {
                Mineral_NeeditemMI[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGreen[GameManager.Mineral_LevelMI - 60]) + "";
                if (GameManager.Mineral_LevelMI != 60) GameManager.HaveStarGreen -= GameManager.SpecialNeedHaveStarGreen[GameManager.Mineral_LevelMI - 61];
            }

            if (GameManager.Mineral_LevelMI >= 120)
            {
                Mineral_NeeditemMI[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarRed[GameManager.Mineral_LevelMI - 120]) + "";
                if (GameManager.Mineral_LevelMI != 120) GameManager.HaveStarRed -= GameManager.SpecialNeedHaveStarRed[GameManager.Mineral_LevelMI - 121];
            }

            if (GameManager.Mineral_LevelMI >= 240)
            {
                Mineral_NeeditemMI[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarYellow[GameManager.Mineral_LevelMI - 240]) + "";
                if (GameManager.Mineral_LevelMI != 240) GameManager.HaveStarYellow -= GameManager.SpecialNeedHaveStarYellow[GameManager.Mineral_LevelMI - 241];
            }

            if (GameManager.Mineral_LevelMI >= 480)
            {
                Mineral_NeeditemMI[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarPurple[GameManager.Mineral_LevelMI - 480]) + "";
                if (GameManager.Mineral_LevelMI != 480) GameManager.HaveStarPurple -= GameManager.SpecialNeedHaveStarPurple[GameManager.Mineral_LevelMI - 481];
            }

            if (GameManager.Mineral_LevelMI >= 800)
            {
                Mineral_NeeditemMI[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarOrange[GameManager.Mineral_LevelMI - 800]) + "";
                if (GameManager.Mineral_LevelMI != 800) GameManager.HaveStarOrange -= GameManager.SpecialNeedHaveStarOrange[GameManager.Mineral_LevelMI - 801];
            }

            if (GameManager.Mineral_LevelMI >= 1500)
            {
                Mineral_NeeditemMI[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarDark[GameManager.Mineral_LevelMI - 1500]) + "";
                if (GameManager.Mineral_LevelMI != 1500) GameManager.HaveStarDark -= GameManager.SpecialNeedHaveStarDark[GameManager.Mineral_LevelMI - 1501];
            }
        }
        //  광물 약점 업그레이드
        else if (index == 1)
        {
            if (GameManager.HaveStarGrey < GameManager.SpecialNeedHaveStarGrey[GameManager.Mineral_LevelHP - 1]) check = true;

            if (GameManager.Mineral_LevelHP >= 10)
                if (GameManager.HaveStarBrown < GameManager.SpecialNeedHaveStarBrown[GameManager.Mineral_LevelHP - 10]) check = true;
            if (GameManager.Mineral_LevelHP >= 30)
                if (GameManager.HaveStarBlue < GameManager.SpecialNeedHaveStarBlue[GameManager.Mineral_LevelHP - 30]) check = true;
            if (GameManager.Mineral_LevelHP >= 60)
                if (GameManager.HaveStarGreen < GameManager.SpecialNeedHaveStarGreen[GameManager.Mineral_LevelHP - 60]) check = true;
            if (GameManager.Mineral_LevelHP >= 120)
                if (GameManager.HaveStarRed < GameManager.SpecialNeedHaveStarRed[GameManager.Mineral_LevelHP - 120]) check = true;
            if (GameManager.Mineral_LevelHP >= 240)
                if (GameManager.HaveStarYellow < GameManager.SpecialNeedHaveStarYellow[GameManager.Mineral_LevelHP - 240]) check = true;
            if (GameManager.Mineral_LevelHP >= 480)
                if (GameManager.HaveStarPurple < GameManager.SpecialNeedHaveStarPurple[GameManager.Mineral_LevelHP - 480]) check = true;
            if (GameManager.Mineral_LevelHP >= 800)
                if (GameManager.HaveStarOrange < GameManager.SpecialNeedHaveStarOrange[GameManager.Mineral_LevelHP - 800]) check = true;
            if (GameManager.Mineral_LevelHP >= 1500)
                if (GameManager.HaveStarDark < GameManager.SpecialNeedHaveStarDark[GameManager.Mineral_LevelHP - 1500]) check = true;


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

            Mineral_NeeditemHP[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGrey[GameManager.Mineral_LevelHP - 1]) + "";
            GameManager.HaveStarGrey -= GameManager.SpecialNeedHaveStarGrey[GameManager.Mineral_LevelHP - 2];

            if (GameManager.Mineral_LevelHP >= 10)
            {
                Mineral_NeeditemHP[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBrown[GameManager.Mineral_LevelHP - 10]) + "";
                if (GameManager.Mineral_LevelHP != 10) GameManager.HaveStarBrown -= GameManager.SpecialNeedHaveStarBrown[GameManager.Mineral_LevelHP - 11];
            }

            if (GameManager.Mineral_LevelHP >= 30)
            {
                Mineral_NeeditemHP[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBlue[GameManager.Mineral_LevelHP - 30]) + "";
                if (GameManager.Mineral_LevelHP != 30) GameManager.HaveStarBlue -= GameManager.SpecialNeedHaveStarBlue[GameManager.Mineral_LevelHP - 31];
            }

            if (GameManager.Mineral_LevelHP >= 60)
            {
                Mineral_NeeditemHP[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGreen[GameManager.Mineral_LevelHP - 60]) + "";
                if (GameManager.Mineral_LevelHP != 60) GameManager.HaveStarGreen -= GameManager.SpecialNeedHaveStarGreen[GameManager.Mineral_LevelHP - 61];
            }

            if (GameManager.Mineral_LevelHP >= 120)
            {
                Mineral_NeeditemHP[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarRed[GameManager.Mineral_LevelHP - 120]) + "";
                if (GameManager.Mineral_LevelHP != 120) GameManager.HaveStarRed -= GameManager.SpecialNeedHaveStarRed[GameManager.Mineral_LevelHP - 121];
            }

            if (GameManager.Mineral_LevelHP >= 240)
            {
                Mineral_NeeditemHP[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarYellow[GameManager.Mineral_LevelHP - 240]) + "";
                if (GameManager.Mineral_LevelHP != 240) GameManager.HaveStarYellow -= GameManager.SpecialNeedHaveStarYellow[GameManager.Mineral_LevelHP - 241];
            }

            if (GameManager.Mineral_LevelHP >= 480)
            {
                Mineral_NeeditemHP[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarPurple[GameManager.Mineral_LevelHP - 480]) + "";
                if (GameManager.Mineral_LevelHP != 480) GameManager.HaveStarPurple -= GameManager.SpecialNeedHaveStarPurple[GameManager.Mineral_LevelHP - 481];
            }

            if (GameManager.Mineral_LevelHP >= 800)
            {
                Mineral_NeeditemHP[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarOrange[GameManager.Mineral_LevelHP - 800]) + "";
                if (GameManager.Mineral_LevelHP != 800) GameManager.HaveStarOrange -= GameManager.SpecialNeedHaveStarOrange[GameManager.Mineral_LevelHP - 801];
            }

            if (GameManager.Mineral_LevelHP >= 1500)
            {
                Mineral_NeeditemHP[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarDark[GameManager.Mineral_LevelHP - 1500]) + "";
                if (GameManager.Mineral_LevelHP != 1500) GameManager.HaveStarDark -= GameManager.SpecialNeedHaveStarDark[GameManager.Mineral_LevelHP - 1501];
            }
        }
        //  강화 스크롤 업그레이드
        else if (index == 2)
        {
            if (GameManager.HaveStarGrey < GameManager.BasicNeedHaveStarGrey[GameManager.Mineral_LevelRS - 1]) check = true;

            if (GameManager.Mineral_LevelRS >= 10)
                if (GameManager.HaveStarBrown < GameManager.BasicNeedHaveStarBrown[GameManager.Mineral_LevelRS - 10]) check = true;
            if (GameManager.Mineral_LevelRS >= 30)
                if (GameManager.HaveStarBlue < GameManager.BasicNeedHaveStarBlue[GameManager.Mineral_LevelRS - 30]) check = true;
            if (GameManager.Mineral_LevelRS >= 60)
                if (GameManager.HaveStarGreen < GameManager.BasicNeedHaveStarGreen[GameManager.Mineral_LevelRS - 60]) check = true;
            if (GameManager.Mineral_LevelRS >= 120)
                if (GameManager.HaveStarRed < GameManager.BasicNeedHaveStarRed[GameManager.Mineral_LevelRS - 120]) check = true;
            if (GameManager.Mineral_LevelRS >= 240)
                if (GameManager.HaveStarYellow < GameManager.BasicNeedHaveStarYellow[GameManager.Mineral_LevelRS - 240]) check = true;
            if (GameManager.Mineral_LevelRS >= 480)
                if (GameManager.HaveStarPurple < GameManager.BasicNeedHaveStarPurple[GameManager.Mineral_LevelRS - 480]) check = true;
            if (GameManager.Mineral_LevelRS >= 800)
                if (GameManager.HaveStarOrange < GameManager.BasicNeedHaveStarOrange[GameManager.Mineral_LevelRS - 800]) check = true;
            if (GameManager.Mineral_LevelRS >= 1500)
                if (GameManager.HaveStarDark < GameManager.BasicNeedHaveStarDark[GameManager.Mineral_LevelRS - 1500]) check = true;

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

            Mineral_NeeditemRS[0].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarGrey[GameManager.Mineral_LevelRS - 1]) + "";
            GameManager.HaveStarGrey -= GameManager.BasicNeedHaveStarGrey[GameManager.Mineral_LevelRS - 2];

            if (GameManager.Mineral_LevelRS >= 10)
            {
                Mineral_NeeditemRS[1].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarBrown[GameManager.Mineral_LevelRS - 10]) + "";
                if (GameManager.Mineral_LevelRS != 10) GameManager.HaveStarBrown -= GameManager.BasicNeedHaveStarBrown[GameManager.Mineral_LevelRS - 11];
            }

            if (GameManager.Mineral_LevelRS >= 30)
            {
                Mineral_NeeditemRS[2].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarBlue[GameManager.Mineral_LevelRS - 30]) + "";
                if (GameManager.Mineral_LevelRS != 30) GameManager.HaveStarBlue -= GameManager.BasicNeedHaveStarBlue[GameManager.Mineral_LevelRS - 31];
            }

            if (GameManager.Mineral_LevelRS >= 60)
            {
                Mineral_NeeditemRS[3].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarGreen[GameManager.Mineral_LevelRS - 60]) + "";
                if (GameManager.Mineral_LevelRS != 60) GameManager.HaveStarGreen -= GameManager.BasicNeedHaveStarGreen[GameManager.Mineral_LevelRS - 61];
            }

            if (GameManager.Mineral_LevelRS >= 120)
            {
                Mineral_NeeditemRS[4].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarRed[GameManager.Mineral_LevelRS - 120]) + "";
                if (GameManager.Mineral_LevelRS != 120) GameManager.HaveStarRed -= GameManager.BasicNeedHaveStarRed[GameManager.Mineral_LevelRS - 121];
            }

            if (GameManager.Mineral_LevelRS >= 240)
            {
                Mineral_NeeditemRS[5].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarYellow[GameManager.Mineral_LevelRS - 240]) + "";
                if (GameManager.Mineral_LevelRS != 240) GameManager.HaveStarYellow -= GameManager.BasicNeedHaveStarYellow[GameManager.Mineral_LevelRS - 241];
            }

            if (GameManager.Mineral_LevelRS >= 480)
            {
                Mineral_NeeditemRS[6].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarPurple[GameManager.Mineral_LevelRS - 480]) + "";
                if (GameManager.Mineral_LevelRS != 480) GameManager.HaveStarPurple -= GameManager.BasicNeedHaveStarPurple[GameManager.Mineral_LevelRS - 481];
            }

            if (GameManager.Mineral_LevelRS >= 800)
            {
                Mineral_NeeditemRS[7].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarOrange[GameManager.Mineral_LevelRS - 800]) + "";
                if (GameManager.Mineral_LevelRS != 800) GameManager.HaveStarOrange -= GameManager.BasicNeedHaveStarOrange[GameManager.Mineral_LevelRS - 801];
            }

            if (GameManager.Mineral_LevelRS >= 1500)
            {
                Mineral_NeeditemRS[8].text = TextFormatter.GetThousandCommaText(GameManager.BasicNeedHaveStarDark[GameManager.Mineral_LevelRS - 1500]) + "";
                if (GameManager.Mineral_LevelRS != 1500) GameManager.HaveStarDark -= GameManager.BasicNeedHaveStarDark[GameManager.Mineral_LevelRS - 1501];
            }
        }
    }

    private void OptionLevelUP(int index)
    {
        bool check = false;
        //  옵션 데미지 증폭 업그레이드
        if (index == 0)
        {
            if (GameManager.HaveStarGrey < GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelPMA - 1]) check = true;

            if (GameManager.Option_LevelPMA >= 10)
                if (GameManager.HaveStarBrown < GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelPMA - 10]) check = true;
            if (GameManager.Option_LevelPMA >= 30)
                if (GameManager.HaveStarBlue < GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelPMA - 30]) check = true;
            if (GameManager.Option_LevelPMA >= 60)
                if (GameManager.HaveStarGreen < GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelPMA - 60]) check = true;
            if (GameManager.Option_LevelPMA >= 120)
                if (GameManager.HaveStarRed < GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelPMA - 120]) check = true;
            if (GameManager.Option_LevelPMA >= 240)
                if (GameManager.HaveStarYellow < GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelPMA - 240]) check = true;
            if (GameManager.Option_LevelPMA >= 480)
                if (GameManager.HaveStarPurple < GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelPMA - 480]) check = true;
            if (GameManager.Option_LevelPMA >= 800)
                if (GameManager.HaveStarOrange < GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelPMA - 800]) check = true;
            if (GameManager.Option_LevelPMA >= 1500)
                if (GameManager.HaveStarDark < GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelPMA - 1500]) check = true;

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

            Option_NeeditemPMA[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelPMA - 1]) + "";
            GameManager.HaveStarGrey -= GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelPMA - 2];

            if (GameManager.Option_LevelPMA >= 10)
            {
                Option_NeeditemPMA[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelPMA - 10]) + "";
                if (GameManager.Option_LevelPMA != 10) GameManager.HaveStarBrown -= GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelPMA - 11];
            }

            if (GameManager.Option_LevelPMA >= 30)
            {
                Option_NeeditemPMA[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelPMA - 30]) + "";
                if (GameManager.Option_LevelPMA != 30) GameManager.HaveStarBlue -= GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelPMA - 31];
            }

            if (GameManager.Option_LevelPMA >= 60)
            {
                Option_NeeditemPMA[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelPMA - 60]) + "";
                if (GameManager.Option_LevelPMA != 60) GameManager.HaveStarGreen -= GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelPMA - 61];
            }

            if (GameManager.Option_LevelPMA >= 120)
            {
                Option_NeeditemPMA[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelPMA - 120]) + "";
                if (GameManager.Option_LevelPMA != 120) GameManager.HaveStarRed -= GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelPMA - 121];
            }

            if (GameManager.Option_LevelPMA >= 240)
            {
                Option_NeeditemPMA[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelPMA - 240]) + "";
                if (GameManager.Option_LevelPMA != 240) GameManager.HaveStarYellow -= GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelPMA - 241];
            }

            if (GameManager.Option_LevelPMA >= 480)
            {
                Option_NeeditemPMA[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelPMA - 480]) + "";
                if (GameManager.Option_LevelPMA != 480) GameManager.HaveStarPurple -= GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelPMA - 481];
            }

            if (GameManager.Option_LevelPMA >= 800)
            {
                Option_NeeditemPMA[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelPMA - 800]) + "";
                if (GameManager.Option_LevelPMA != 800) GameManager.HaveStarOrange -= GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelPMA - 801];
            }

            if (GameManager.Option_LevelPMA >= 1500)
            {
                Option_NeeditemPMA[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelPMA - 1500]) + "";
                if (GameManager.Option_LevelPMA != 1500) GameManager.HaveStarDark -= GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelPMA - 1501];
            }
        }
        //  옵션 럭키 광물 업그레이드
        else if (index == 1)
        {
            if (GameManager.HaveStarGrey < GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelMB - 1]) check = true;

            if (GameManager.Option_LevelMB >= 10)
                if (GameManager.HaveStarBrown < GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelMB - 10]) check = true;
            if (GameManager.Option_LevelMB >= 30)
                if (GameManager.HaveStarBlue < GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelMB - 30]) check = true;
            if (GameManager.Option_LevelMB >= 60)
                if (GameManager.HaveStarGreen < GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelMB - 60]) check = true;
            if (GameManager.Option_LevelMB >= 120)
                if (GameManager.HaveStarRed < GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelMB - 120]) check = true;
            if (GameManager.Option_LevelMB >= 240)
                if (GameManager.HaveStarYellow < GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelMB - 240]) check = true;
            if (GameManager.Option_LevelMB >= 480)
                if (GameManager.HaveStarPurple < GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelMB - 480]) check = true;
            if (GameManager.Option_LevelMB >= 800)
                if (GameManager.HaveStarOrange < GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelMB - 800]) check = true;
            if (GameManager.Option_LevelMB >= 1500)
                if (GameManager.HaveStarDark < GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelMB - 1500]) check = true;

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

            Option_NeeditemMB[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelMB - 1]) + "";
            GameManager.HaveStarGrey -= GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelMB - 2];

            if (GameManager.Option_LevelMB >= 10)
            {
                Option_NeeditemMB[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelMB - 10]) + "";
                if (GameManager.Option_LevelMB != 10) GameManager.HaveStarBrown -= GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelMB - 11];
            }

            if (GameManager.Option_LevelMB >= 30)
            {
                Option_NeeditemMB[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelMB - 30]) + "";
                if (GameManager.Option_LevelMB != 30) GameManager.HaveStarBlue -= GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelMB - 31];
            }

            if (GameManager.Option_LevelMB >= 60)
            {
                Option_NeeditemMB[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelMB - 60]) + "";
                if (GameManager.Option_LevelMB != 60) GameManager.HaveStarGreen -= GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelMB - 61];
            }

            if (GameManager.Option_LevelMB >= 120)
            {
                Option_NeeditemMB[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelMB - 120]) + "";
                if (GameManager.Option_LevelMB != 120) GameManager.HaveStarRed -= GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelMB - 121];
            }

            if (GameManager.Option_LevelMB >= 240)
            {
                Option_NeeditemMB[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelMB - 240]) + "";
                if (GameManager.Option_LevelMB != 240) GameManager.HaveStarYellow -= GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelMB - 241];
            }

            if (GameManager.Option_LevelMB >= 480)
            {
                Option_NeeditemMB[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelMB - 480]) + "";
                if (GameManager.Option_LevelMB != 480) GameManager.HaveStarPurple -= GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelMB - 481];
            }

            if (GameManager.Option_LevelMB >= 800)
            {
                Option_NeeditemMB[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelMB - 800]) + "";
                if (GameManager.Option_LevelMB != 800) GameManager.HaveStarOrange -= GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelMB - 801];
            }

            if (GameManager.Option_LevelMB >= 1500)
            {
                Option_NeeditemMB[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelMB - 1500]) + "";
                if (GameManager.Option_LevelMB != 1500) GameManager.HaveStarDark -= GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelMB - 1501];
            }
        }
        //  옵션 치명적 피해 업그레이드
        else if (index == 2)
        {
            if (GameManager.HaveStarGrey < GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelPFD - 1]) check = true;

            if (GameManager.Option_LevelPFD >= 10)
                if (GameManager.HaveStarBrown < GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelPFD - 10]) check = true;
            if (GameManager.Option_LevelPFD >= 30)
                if (GameManager.HaveStarBlue < GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelPFD - 30]) check = true;
            if (GameManager.Option_LevelPFD >= 60)
                if (GameManager.HaveStarGreen < GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelPFD - 60]) check = true;
            if (GameManager.Option_LevelPFD >= 120)
                if (GameManager.HaveStarRed < GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelPFD - 120]) check = true;
            if (GameManager.Option_LevelPFD >= 240)
                if (GameManager.HaveStarYellow < GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelPFD - 240]) check = true;
            if (GameManager.Option_LevelPFD >= 480)
                if (GameManager.HaveStarPurple < GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelPFD - 480]) check = true;
            if (GameManager.Option_LevelPFD >= 800)
                if (GameManager.HaveStarOrange < GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelPFD - 800]) check = true;
            if (GameManager.Option_LevelPFD >= 1500)
                if (GameManager.HaveStarDark < GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelPFD - 1500]) check = true;

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

            Option_NeeditemPFD[0].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelPFD - 1]) + "";
            GameManager.HaveStarGrey -= GameManager.SpecialNeedHaveStarGrey[GameManager.Option_LevelPFD - 2];

            if (GameManager.Option_LevelPFD >= 10)
            {
                Option_NeeditemPFD[1].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelPFD - 10]) + "";
                if (GameManager.Option_LevelPFD != 10) GameManager.HaveStarBrown -= GameManager.SpecialNeedHaveStarBrown[GameManager.Option_LevelPFD - 11];
            }

            if (GameManager.Option_LevelPFD >= 30)
            {
                Option_NeeditemPFD[2].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelPFD - 30]) + "";
                if (GameManager.Option_LevelPFD != 30) GameManager.HaveStarBlue -= GameManager.SpecialNeedHaveStarBlue[GameManager.Option_LevelPFD - 31];
            }

            if (GameManager.Option_LevelPFD >= 60)
            {
                Option_NeeditemPFD[3].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelPFD - 60]) + "";
                if (GameManager.Option_LevelPFD != 60) GameManager.HaveStarGreen -= GameManager.SpecialNeedHaveStarGreen[GameManager.Option_LevelPFD - 61];
            }

            if (GameManager.Option_LevelPFD >= 120)
            {
                Option_NeeditemPFD[4].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelPFD - 120]) + "";
                if (GameManager.Option_LevelPFD != 120) GameManager.HaveStarRed -= GameManager.SpecialNeedHaveStarRed[GameManager.Option_LevelPFD - 121];
            }

            if (GameManager.Option_LevelPFD >= 240)
            {
                Option_NeeditemPFD[5].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelPFD - 240]) + "";
                if (GameManager.Option_LevelPFD != 240) GameManager.HaveStarYellow -= GameManager.SpecialNeedHaveStarYellow[GameManager.Option_LevelPFD - 241];
            }

            if (GameManager.Option_LevelPFD >= 480)
            {
                Option_NeeditemPFD[6].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelPFD - 480]) + "";
                if (GameManager.Option_LevelPFD != 480) GameManager.HaveStarPurple -= GameManager.SpecialNeedHaveStarPurple[GameManager.Option_LevelPFD - 481];
            }

            if (GameManager.Option_LevelPFD >= 800)
            {
                Option_NeeditemPFD[7].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelPFD - 800]) + "";
                if (GameManager.Option_LevelPFD != 800) GameManager.HaveStarOrange -= GameManager.SpecialNeedHaveStarOrange[GameManager.Option_LevelPFD - 801];
            }

            if (GameManager.Option_LevelPFD >= 1500)
            {
                Option_NeeditemPFD[8].text = TextFormatter.GetThousandCommaText(GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelPFD - 1500]) + "";
                if (GameManager.Option_LevelPFD != 1500) GameManager.HaveStarDark -= GameManager.SpecialNeedHaveStarDark[GameManager.Option_LevelPFD - 1501];
            }
        }
    }
    */
    private int Save_index = 0;
    private int Check = 0;

    [System.Obsolete]
    public static void GrantRewards(int Rock_defeatedIndex, int Bonus)
    {
        float Reward_RocksRandom = Random.value;

        int Reward_HaveStarGrey = 0;
        int Reward_HaveStarBrown = 0;
        int Reward_HaveStarBlue = 0;
        int Reward_HaveStarGreen = 0;
        int Reward_HaveStarRed = 0;
        int Reward_HaveStarYellow = 0;
        int Reward_HaveStarPurple = 0;
        int Reward_HaveStarOrange = 0;
        int Reward_HaveStarDark = 0;
        int Reward_ReinforceScroll = 0;


        // 광물의 정보를 가져와서 해당 보상을 계산

        float[] dustProbabilities = {
            rock.n_rocks[Rock_defeatedIndex].HaveStarGrey,
            rock.n_rocks[Rock_defeatedIndex].HaveStarBrown,
            rock.n_rocks[Rock_defeatedIndex].HaveStarBlue,
            rock.n_rocks[Rock_defeatedIndex].HaveStarGreen,
            rock.n_rocks[Rock_defeatedIndex].HaveStarRed,
            rock.n_rocks[Rock_defeatedIndex].HaveStarYellow,
            rock.n_rocks[Rock_defeatedIndex].HaveStarPurple,
            rock.n_rocks[Rock_defeatedIndex].HaveStarOrange,
            rock.n_rocks[Rock_defeatedIndex].HaveStarDark,
            rock.n_rocks[Rock_defeatedIndex].ReinforceScroll
        };

        int[] dustRewards = new int[dustProbabilities.Length];
        float cumulativeProbability = 0;

        for (int i = 0; i < dustProbabilities.Length; i++)
        {
            cumulativeProbability += dustProbabilities[i] / 100f;
            if (Reward_RocksRandom < cumulativeProbability)
            {
                dustRewards[i] = GameManager.Rock_defeatedIndex + 1;
                break; // 보상이 결정되면 루프 종료
            }
        }

        // 각 보상 변수에 값 할당
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

        Debug.Log("광물 확률 : " + rock.n_rocks[Rock_defeatedIndex].HaveStarGrey / 100f + " | " + Reward_RocksRandom);
        Debug.Log("광물 확률 : " + rock.n_rocks[Rock_defeatedIndex].HaveStarBrown / 100f + " | " + Reward_RocksRandom);
        Debug.Log("광물 확률 : " + rock.n_rocks[Rock_defeatedIndex].HaveStarBlue / 100f + " | " + Reward_RocksRandom);

        // 권능

        if(GameManager.WarrantLevel[9] >= 1) Bonus += GameManager.Warrant_Power[9];
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
        //GameManager.ReinforceScroll += Reward_ReinforceScroll;

        RocksCurHP = RocksMaxHP;
        
    }


    
}
