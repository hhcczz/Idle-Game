using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaSystem : MonoBehaviour
{

    [SerializeField]
    public Button diamond;

    public GameObject gachaPanel;
    public Button closeButton;
    public Text WeaponText;
    public Text WeaponLevelText;
    public Text AdText;
    public Text WeaponAdDayText;
    public Text[] WeaponNeedDiamondText;

    public Button[] WeaponDrawBtn;
    public Slider WeaponSlider;

    public Image[] resultImages;                        //  Result Image 배열
    public GameObject[] resultBG;                       //  Result BG 배열
    private Image BGimageComponent;                     // 대상 GameObject의 Image 컴포넌트

    public Button[] WeaponLevelReceiveBtn;              // 무기 레벨 천장 무기 획득 버튼
    public Text[] WeaponProbility_Left;
    public Text[] WeaponProbility_Right;


    public Text AccessoryAdText;
    public Text AccessoryText;
    public Text AccessoryLevelText;
    public Text AccessoryAdDayText;
    public Text[] AccessoryNeedDiamondText;

    public Button[] AccessoryDrawBtn;
    public Slider AccessorySlider;

    public Button[] AccessoryLevelReceiveBtn;              // 무기 레벨 천장 무기 획득 버튼
    public Text[] AccessoryProbility_Left;
    public Text[] AccessoryProbility_Right;

    public GameObject[] ShopObject;

    public GameObject ReceivePanel;
    public Text[] ReceiveText;
    public Image ReceiveFrame;
    public Image ReceiveImg;
    public Image ReceiveTitleImg;

    public Image RecevieEffect;

    public Sprite[] ReceiveTitleSprite;

    public Button ReceiveOutBtn;

    public Button[] AdDrawBtn;

    private int Weapon_AdDayCount = 3;
    private int Accessory_AdDayCount = 3;
    private int absoulteAdDayCount = 3;

    bool GachaRun = false;

    private int thisInDrawingState = -1;    //  1 = 무기  2 = 악세서리

    private int NeedDiamondValue500;
    private int NeedDiamondValue1500;


    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip GachaSoundClip; // AudioClip 변수 선언

    private void Start()
    {
        if (GameManager.Weapon_AdCount == 0) GameManager.Weapon_AdCount = 11; // 또는 다른 초기값으로 설정
        if (GameManager.Accessory_AdCount == 0) GameManager.Accessory_AdCount = 11; // 또는 다른 초기값으로 설정

        Debug.Log("개수 : " + GameManager.Weapon_AdCount);

        if (GameManager.WarrantLevel[26] >= 1)
        {
            Weapon_AdDayCount = 3 + GameManager.Warrant_Power[26];
            Accessory_AdDayCount = 3 + GameManager.Warrant_Power[26];
            absoulteAdDayCount = 3 + GameManager.Warrant_Power[26];
        }
        else
        {
            Weapon_AdDayCount = 3;
            Accessory_AdDayCount = 3;
            absoulteAdDayCount = 3;
        }


        NeedDiamondValue500 = 500;
        NeedDiamondValue1500 = 1500;

        int numWeapons = GameManager.itemSprites.Length;
        int numAccessory = GameManager.AccessorySprites.Length;

        GameManager.WeaponDrawn = new bool[numWeapons];
        GameManager.AccessoryDrawn = new bool[numAccessory];

        ReceiveOutBtn.onClick.AddListener(ReceiveOut);

        for (int i = 0; i < WeaponDrawBtn.Length; i++)
        {
            int index = i;

            WeaponDrawBtn[index].onClick.AddListener(() => ChangeDrawCount(index));
            AccessoryDrawBtn[index].onClick.AddListener(() => ChangeDrawCount(index));

            WeaponDrawBtn[index].onClick.AddListener(() => { thisInDrawingState = 1; });
            AccessoryDrawBtn[index].onClick.AddListener(() => { thisInDrawingState = 2; });
        }

        for (int i = 0; i < WeaponLevelReceiveBtn.Length; i++)
        {
            int index = i;

            WeaponLevelReceiveBtn[index].onClick.AddListener(() => ReceiveWeapon(index));
            AccessoryLevelReceiveBtn[index].onClick.AddListener(() => ReceiveAccessory(index));
        }

        for(int i = 0; i < AdDrawBtn.Length; i++)
        {
            int index = i;

            AdDrawBtn[index].onClick.AddListener(() => AdPlayingDraw(index));
        }

        if (GameManager.WarrantLevel[14] >= 1)
        {
            NeedDiamondValue1500 = (int)(1500 * (float)(1 - GameManager.Warrant_Power[14] / 100f));
            NeedDiamondValue500 = (int)(500 * (float)(1 - GameManager.Warrant_Power[14] / 100f));
        }

        Debug.Log("Before NEED 1500 : " + NeedDiamondValue1500);
        Debug.Log("Before NEED 500 : " + NeedDiamondValue500);
        for (int i = 0; i < 4; i++)
        {
            int index = i;

            if(index <= 1)  WeaponNeedDiamondText[index].text = NeedDiamondValue1500 + "";
            else            WeaponNeedDiamondText[index].text = NeedDiamondValue500 + "";

            if (index <= 1) AccessoryNeedDiamondText[index].text = NeedDiamondValue1500 + "";
            else            AccessoryNeedDiamondText[index].text = NeedDiamondValue500 + "";

        }

        Debug.Log("After NEED 1500 : " + NeedDiamondValue1500);
        Debug.Log("After NEED 500 : " + NeedDiamondValue500);


        closeButton.onClick.AddListener(CloseGachaPanel);
        diamond.onClick.AddListener(Givediamond);

        WeaponSlider.value = GameManager.WeaponCounts / GameManager.WeaponMaxCounts[GameManager.WeaponShopLevel];
        AccessorySlider.value = GameManager.AccessoryCounts / GameManager.AccessoryMaxCounts[GameManager.AccessoryShopLevel];

        // WeaponBar 배열의 각 요소를 직접 인스펙터에서 할당
        // WeaponBar[0]에는 "무기1"의 슬라이더, WeaponBar[1]에는 "무기2"의 슬라이더, ...
        // 이런 식으로 할당해주세요.

        // GameManager.itemCounts 초기화
        //GameManager.itemCounts = new Dictionary<Sprite, int>();
        //GameManager.AccessoryCount = new Dictionary<Sprite, int>();
        //
        //foreach (Sprite itemSprite in GameManager.itemSprites)
        //{
        //    GameManager.itemCounts[itemSprite] = 0;
        //}
        //
        //foreach (Sprite itemSprite in GameManager.AccessorySprites)
        //{
        //    GameManager.AccessoryCount[itemSprite] = 0;
        //}

        if (GameManager.WeaponShopLevel == 7) WeaponText.text = "MAX";
        else WeaponText.text = GameManager.WeaponCounts + "/" + GameManager.WeaponMaxCounts[GameManager.WeaponShopLevel - 1];
        AccessoryText.text = GameManager.AccessoryCounts + "/" + GameManager.AccessoryMaxCounts[GameManager.AccessoryShopLevel - 1];

        UpdateButtonInteractivity();
        UpdateWeaponProbility();
    }

    private void OnEnable()
    {
        if (gameObject.activeSelf == true)
        {
            if (GameManager.WarrantLevel[14] >= 1)
            {
                NeedDiamondValue1500 = (int)(1500 * (float)(1 - GameManager.Warrant_Power[14] / 100f));
                NeedDiamondValue500 = (int)(500 * (float)(1 - GameManager.Warrant_Power[14] / 100f));
            }

            for (int i = 0; i < 4; i++)
            {
                int index = i;

                if (index <= 1) WeaponNeedDiamondText[index].text = NeedDiamondValue1500 + "";
                else WeaponNeedDiamondText[index].text = NeedDiamondValue500 + "";

                if (index <= 1) AccessoryNeedDiamondText[index].text = NeedDiamondValue1500 + "";
                else AccessoryNeedDiamondText[index].text = NeedDiamondValue500 + "";

            }

        }
        

    }

    private void ReceiveOut()
    {
        ReceivePanel.SetActive(false);
    }

    int ReceiveNum = 0;

    private void ReceiveWeapon(int index)
    {
        // 색상 이름 배열 선언 (index에 따른 색상 이름)
        string[] colorNames = { "무기2번색", "무기3번색", "무기4번색", "무기5번색", "무기6번색", "무기6번색" };

        Outline[] outline = ReceiveFrame.GetComponentsInChildren<Outline>();

        ReceiveTitleImg.sprite = ReceiveTitleSprite[0];
        if (index == 0) ReceiveNum = 7;
        else if (index == 1) ReceiveNum = 11;
        else if (index == 2) ReceiveNum = 15;
        else if (index == 3) ReceiveNum = 19;
        else if (index == 4) ReceiveNum = 20;
        else if (index == 5) ReceiveNum = 21;
        ReceiveFrame.color = ColorManager.ColorChange(colorNames[index]);
        outline[0].effectColor = ColorManager.ColorChange(colorNames[index]);
        outline[1].effectColor = ColorManager.ColorChange(colorNames[index]);
        RecevieEffect.color = ColorManager.ColorChange(colorNames[index]);

        Sprite selectedSprite = GameManager.itemSprites[ReceiveNum];
        if (GameManager.itemCounts.ContainsKey(selectedSprite)) GameManager.itemCounts[selectedSprite]++;
        else GameManager.itemCounts[selectedSprite] = 1;
        GameManager.WeaponDrawn[ReceiveNum] = true;
        Image buttonImage = WeaponLevelReceiveBtn[index].GetComponent<Image>();       // 버튼의 이미지 컴포넌트 가져오기
        buttonImage.color = ColorManager.ColorChange("빨간색");
        if (GameManager.WeaponDrawn[ReceiveNum] == true && !GameManager.weaponOwnDamageIncreased[ReceiveNum])
        {
            GameManager.WeaponOwnDamage += GameManager.WeaponOwnDmg[ReceiveNum];
            GameManager.weaponOwnDamageIncreased[ReceiveNum] = true; // WeaponOwnDamage가 증가되었음을 표시
        }

        ReceiveImg.sprite = GameManager.itemSprites[ReceiveNum];
        ReceiveText[0].text = GameManager.WeaponTitle[ReceiveNum];
        ReceiveText[1].text = "+ " + GameManager.WeaponEquipDmg[ReceiveNum];
        ReceiveText[2].text = "+ " + GameManager.WeaponOwnDmg[ReceiveNum] + "%";
        ReceivePanel.SetActive(true);

        WeaponLevelReceiveBtn[index].interactable = false;
    }

    private void ReceiveAccessory(int index)
    {
        // 색상 이름 배열 선언 (index에 따른 색상 이름)
        string[] colorNames = { "무기2번색", "무기3번색", "무기4번색", "무기5번색", "무기6번색", "무기6번색" };

        Outline[] outline = ReceiveFrame.GetComponentsInChildren<Outline>();

        ReceiveTitleImg.sprite = ReceiveTitleSprite[1];
        if (index == 0) ReceiveNum = 7;
        else if (index == 1) ReceiveNum = 11;
        else if (index == 2) ReceiveNum = 15;
        else if (index == 3) ReceiveNum = 19;
        else if (index == 4) ReceiveNum = 20;
        else if (index == 5) ReceiveNum = 21;
        ReceiveFrame.color = ColorManager.ColorChange(colorNames[index]);
        outline[0].effectColor = ColorManager.ColorChange(colorNames[index]);
        outline[1].effectColor = ColorManager.ColorChange(colorNames[index]);
        RecevieEffect.color = ColorManager.ColorChange(colorNames[index]);

        Sprite selectedSprite = GameManager.AccessorySprites[ReceiveNum];
        if (GameManager.AccessoryCount.ContainsKey(selectedSprite)) GameManager.AccessoryCount[selectedSprite]++;
        else GameManager.AccessoryCount[selectedSprite] = 1;
        GameManager.AccessoryDrawn[ReceiveNum] = true;
        Image buttonImage = AccessoryLevelReceiveBtn[index].GetComponent<Image>();       // 버튼의 이미지 컴포넌트 가져오기
        buttonImage.color = ColorManager.ColorChange("빨간색");
        if (GameManager.AccessoryDrawn[ReceiveNum] == true && !GameManager.AccessoryOwnExpIncreased[ReceiveNum])
        {
            GameManager.AccessoryOwnExperience += GameManager.AccessoryOwnExp[ReceiveNum];
            GameManager.AccessoryOwnExpIncreased[ReceiveNum] = true; // WeaponOwnDamage가 증가되었음을 표시
        }

        ReceiveImg.sprite = GameManager.AccessorySprites[ReceiveNum];
        ReceiveText[0].text = GameManager.AccessoryTitle[ReceiveNum];
        ReceiveText[1].text = "+ " + GameManager.AccessoryEquipExp[ReceiveNum];
        ReceiveText[2].text = "+ " + GameManager.AccessoryOwnExp[ReceiveNum] + "%";
        ReceivePanel.SetActive(true);

        AccessoryLevelReceiveBtn[index].interactable = false;
    }


    private void UpdateWeaponProbility()
    {
        Debug.Log("상점 레벨 : " + GameManager.WeaponShopLevel);
        if (GameManager.WeaponShopLevel == 7)
        {
            WeaponProbility_Left[0].text = GameManager.WeaponLevelProbilityValue_Common[GameManager.WeaponShopLevel] + "%";
            WeaponProbility_Right[0].text = GameManager.WeaponLevelProbilityValue_Common[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[1].text = GameManager.WeaponLevelProbilityValue_Rare[GameManager.WeaponShopLevel] + "%";
            WeaponProbility_Right[1].text = GameManager.WeaponLevelProbilityValue_Rare[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[2].text = GameManager.WeaponLevelProbilityValue_UnCommon[GameManager.WeaponShopLevel] + "%";
            WeaponProbility_Right[2].text = GameManager.WeaponLevelProbilityValue_UnCommon[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[3].text = GameManager.WeaponLevelProbilityValue_Artifact[GameManager.WeaponShopLevel] + "%";
            WeaponProbility_Right[3].text = GameManager.WeaponLevelProbilityValue_Artifact[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[4].text = GameManager.WeaponLevelProbilityValue_Mythical[GameManager.WeaponShopLevel] + "%";
            WeaponProbility_Right[4].text = GameManager.WeaponLevelProbilityValue_Mythical[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[5].text = GameManager.WeaponLevelProbilityValue_Legend[GameManager.WeaponShopLevel] + "%";
            WeaponProbility_Right[5].text = GameManager.WeaponLevelProbilityValue_Legend[GameManager.WeaponShopLevel] + "%";
        }
        else
        {
            WeaponProbility_Left[0].text = GameManager.WeaponLevelProbilityValue_Common[GameManager.WeaponShopLevel - 1] + "%";
            WeaponProbility_Right[0].text = GameManager.WeaponLevelProbilityValue_Common[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[1].text = GameManager.WeaponLevelProbilityValue_Rare[GameManager.WeaponShopLevel - 1] + "%";
            WeaponProbility_Right[1].text = GameManager.WeaponLevelProbilityValue_Rare[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[2].text = GameManager.WeaponLevelProbilityValue_UnCommon[GameManager.WeaponShopLevel - 1] + "%";
            WeaponProbility_Right[2].text = GameManager.WeaponLevelProbilityValue_UnCommon[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[3].text = GameManager.WeaponLevelProbilityValue_Artifact[GameManager.WeaponShopLevel - 1] + "%";
            WeaponProbility_Right[3].text = GameManager.WeaponLevelProbilityValue_Artifact[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[4].text = GameManager.WeaponLevelProbilityValue_Mythical[GameManager.WeaponShopLevel - 1] + "%";
            WeaponProbility_Right[4].text = GameManager.WeaponLevelProbilityValue_Mythical[GameManager.WeaponShopLevel] + "%";

            WeaponProbility_Left[5].text = GameManager.WeaponLevelProbilityValue_Legend[GameManager.WeaponShopLevel - 1] + "%";
            WeaponProbility_Right[5].text = GameManager.WeaponLevelProbilityValue_Legend[GameManager.WeaponShopLevel] + "%";
        }
        if (GameManager.AccessoryShopLevel == 7)
        {
            AccessoryProbility_Left[0].text = GameManager.AccessoryLevelProbilityValue_Common[GameManager.AccessoryShopLevel] + "%";
            AccessoryProbility_Right[0].text = GameManager.AccessoryLevelProbilityValue_Common[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[1].text = GameManager.AccessoryLevelProbilityValue_Rare[GameManager.AccessoryShopLevel] + "%";
            AccessoryProbility_Right[1].text = GameManager.AccessoryLevelProbilityValue_Rare[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[2].text = GameManager.AccessoryLevelProbilityValue_UnCommon[GameManager.AccessoryShopLevel] + "%";
            AccessoryProbility_Right[2].text = GameManager.AccessoryLevelProbilityValue_UnCommon[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[3].text = GameManager.AccessoryLevelProbilityValue_Artifact[GameManager.AccessoryShopLevel] + "%";
            AccessoryProbility_Right[3].text = GameManager.AccessoryLevelProbilityValue_Artifact[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[4].text = GameManager.AccessoryLevelProbilityValue_Mythical[GameManager.AccessoryShopLevel] + "%";
            AccessoryProbility_Right[4].text = GameManager.AccessoryLevelProbilityValue_Mythical[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[5].text = GameManager.AccessoryLevelProbilityValue_Legend[GameManager.AccessoryShopLevel] + "%";
            AccessoryProbility_Right[5].text = GameManager.AccessoryLevelProbilityValue_Legend[GameManager.AccessoryShopLevel] + "%";
        }
        else
        {

            AccessoryProbility_Left[0].text = GameManager.AccessoryLevelProbilityValue_Common[GameManager.AccessoryShopLevel - 1] + "%";
            AccessoryProbility_Right[0].text = GameManager.AccessoryLevelProbilityValue_Common[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[1].text = GameManager.AccessoryLevelProbilityValue_Rare[GameManager.AccessoryShopLevel - 1] + "%";
            AccessoryProbility_Right[1].text = GameManager.AccessoryLevelProbilityValue_Rare[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[2].text = GameManager.AccessoryLevelProbilityValue_UnCommon[GameManager.AccessoryShopLevel - 1] + "%";
            AccessoryProbility_Right[2].text = GameManager.AccessoryLevelProbilityValue_UnCommon[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[3].text = GameManager.AccessoryLevelProbilityValue_Artifact[GameManager.AccessoryShopLevel - 1] + "%";
            AccessoryProbility_Right[3].text = GameManager.AccessoryLevelProbilityValue_Artifact[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[4].text = GameManager.AccessoryLevelProbilityValue_Mythical[GameManager.AccessoryShopLevel - 1] + "%";
            AccessoryProbility_Right[4].text = GameManager.AccessoryLevelProbilityValue_Mythical[GameManager.AccessoryShopLevel] + "%";

            AccessoryProbility_Left[5].text = GameManager.AccessoryLevelProbilityValue_Legend[GameManager.AccessoryShopLevel - 1] + "%";
            AccessoryProbility_Right[5].text = GameManager.AccessoryLevelProbilityValue_Legend[GameManager.AccessoryShopLevel] + "%";
        }
    }

    //  나중에 다이아몬드 획득하면 코드 실행으로 최적화하기
    private void Update()
    {
        if (GameManager.WeaponShopLevel == 7) WeaponSlider.value = 1f;
        else WeaponSlider.value = GameManager.WeaponCounts / GameManager.WeaponMaxCounts[GameManager.WeaponShopLevel - 1];

        if (GameManager.WeaponShopLevel == 7) AccessorySlider.value = 1f;
        else AccessorySlider.value = GameManager.AccessoryCounts / GameManager.AccessoryMaxCounts[GameManager.AccessoryShopLevel - 1];


        UpdateButtonInteractivity();
    }

    [System.Obsolete]
    private void AdPlayingDraw(int index)
    {
        AdManager.Instance.ShowAd();
        int drawCount = 0;
        if (GameManager.Weapon_AdCount == 0) GameManager.Weapon_AdCount = 11; // 또는 다른 초기값으로 설정
        if (GameManager.Accessory_AdCount == 0) GameManager.Accessory_AdCount = 11; // 또는 다른 초기값으로 설정

        if (index == 0)
        {
            drawCount = GameManager.Weapon_AdCount;
            Weapon_AdDayCount--;
            GameManager.Weapon_AdCount++;
            thisInDrawingState = 1;
        }
        else if(index == 1)
        {
            drawCount = GameManager.Accessory_AdCount;
            Accessory_AdDayCount--;
            GameManager.Accessory_AdCount++;
            thisInDrawingState = 2;
        }
        closeButton.interactable = false;
        gachaPanel.SetActive(true);
        if (!GachaRun) StartCoroutine(RunGacha(drawCount));
        AdManager.Instance.LoadRewardedAd();
    }
    
    void ChangeDrawCount(int index)
    {
        int drawCount = 0;
        int Free = Random.Range(0, 100);
        switch (index)
        {
            case 0:
                drawCount = 11;
                if (Free < GameManager.Warrant_Power[21])
                {
                    Debug.Log("무료!");
                }
                else GameManager.Player_Diamond -= NeedDiamondValue500;
                break;
            case 1:
                drawCount = 35;
                if (Free < GameManager.Warrant_Power[21])
                {
                    Debug.Log("무료!");
                }
                else GameManager.Player_Diamond -= NeedDiamondValue1500;
                break;
            case 2:
                drawCount = 11;
                if (Free < GameManager.Warrant_Power[21])
                {
                    Debug.Log("무료!");
                }
                else GameManager.Player_Diamond -= NeedDiamondValue500;
                break;
            case 3:
                drawCount = 35;
                if (Free < GameManager.Warrant_Power[21])
                {
                    Debug.Log("무료!");
                }
                else GameManager.Player_Diamond -= NeedDiamondValue1500;
                break;
        }

        closeButton.interactable = false;

        gachaPanel.SetActive(true);
        if (!GachaRun)
        {
            StartCoroutine(RunGacha(drawCount));
        }

    }

    private void CloseGachaPanel()
    {
        for (int i = 0; i < ShopObject.Length; i++)
        {
            ShopObject[i].SetActive(true);
        }

        gachaPanel.SetActive(false);
        ResetResultImages();
    }

    private IEnumerator RunGacha(int index)
    {
        GachaRun = true;
        for (int i =0; i < ShopObject.Length; i++)
        {
            ShopObject[i].SetActive(false);
        }
        for (int i = 0; i < 35; i++)
        {
            resultBG[i].SetActive(false);
        }

        for(int i = 0; i < 4; i++)
        {
            int _index = i;

            WeaponDrawBtn[_index].interactable = false;
            AccessoryDrawBtn[_index].interactable = false;
        }

        yield return new WaitForSeconds(0.025f);

        if (thisInDrawingState == 1)
        {
            WeaponDrawBtn[2].gameObject.SetActive(true);
            WeaponDrawBtn[3].gameObject.SetActive(true);

            AccessoryDrawBtn[2].gameObject.SetActive(false);
            AccessoryDrawBtn[3].gameObject.SetActive(false);

            GameManager.WeaponCounts += index;
            StatisticsManager.ImmutabilityWeaponCount += index;

            if (GameManager.WeaponCounts >= GameManager.WeaponMaxCounts[GameManager.WeaponShopLevel - 1] && GameManager.WeaponShopLevel != 7)
            {
                GameManager.WeaponCounts -= GameManager.WeaponMaxCounts[GameManager.WeaponShopLevel - 1];
                GameManager.WeaponShopLevel++;
                UpdateWeaponProbility();
                Image buttonImage = WeaponLevelReceiveBtn[GameManager.WeaponShopLevel - 2].GetComponent<Image>();       // 버튼의 이미지 컴포넌트 가져오기
                WeaponLevelReceiveBtn[GameManager.WeaponShopLevel - 2].interactable = true;

                // 버튼의 이미지 색상 변경

                buttonImage.color = ColorManager.ColorChange("초록색");
            }

            if (GameManager.WeaponShopLevel == 7) WeaponText.text = GameManager.WeaponCounts + " / MAX";
            else WeaponText.text = GameManager.WeaponCounts + "/" + GameManager.WeaponMaxCounts[GameManager.WeaponShopLevel - 1];

            if (GameManager.WeaponShopLevel == 7) WeaponLevelText.text = "Lv. MAX";
            else WeaponLevelText.text = "Lv." + GameManager.WeaponShopLevel;
            AdText.text = GameManager.Weapon_AdCount + "회 소환";
            WeaponAdDayText.text = "(" + Weapon_AdDayCount + "/" + absoulteAdDayCount + ")";

            // 각 무기 레벨에 대한 확률 배열
            float[] weaponLevelProbabilities = {
                GameManager.WeaponLevelProbilityValue_Common[GameManager.WeaponShopLevel - 1],
                GameManager.WeaponLevelProbilityValue_Rare[GameManager.WeaponShopLevel - 1],
                GameManager.WeaponLevelProbilityValue_UnCommon[GameManager.WeaponShopLevel - 1],
                GameManager.WeaponLevelProbilityValue_Artifact[GameManager.WeaponShopLevel - 1],
                GameManager.WeaponLevelProbilityValue_Mythical[GameManager.WeaponShopLevel - 1],
                GameManager.WeaponLevelProbilityValue_Legend[GameManager.WeaponShopLevel - 1]
                };

            for (int i = 0; i < index; i++)
            {
                // AudioSource 컴포넌트 초기화
                audioSource = GetComponent<AudioSource>();

                yield return new WaitForSeconds(0.025f);
                audioSource.pitch = 1.2f;

                audioSource.PlayOneShot(GachaSoundClip, 0.7f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.

                // 각 무기 레벨에 대한 누적 확률 계산
                float[] cumulativeProbabilities = new float[weaponLevelProbabilities.Length];
                float cumulativeProbability = 0f;
                for (int j = 0; j < weaponLevelProbabilities.Length; j++)
                {
                    cumulativeProbability += weaponLevelProbabilities[j];
                    cumulativeProbabilities[j] = cumulativeProbability;
                }
                float randomValue = Random.value * 100f;
                int randomIndex = 0;

                // 누적 확률을 사용하여 무기 선택
                for (int j = 0; j < cumulativeProbabilities.Length; j++)
                {
                    if (randomValue <= cumulativeProbabilities[j])
                    {
                        // 해당 레벨의 무기를 선택
                        randomIndex = Random.Range(j * 4, (j + 1) * 4);
                        break;
                    }
                }

                Sprite selectedSprite = GameManager.itemSprites[randomIndex];

                // Dictionary에서 해당 무기의 카운트 증가
                if (GameManager.itemCounts.ContainsKey(selectedSprite))
                {
                    GameManager.itemCounts[selectedSprite]++;
                }
                else
                {
                    GameManager.itemCounts[selectedSprite] = 1;
                }

                // 무기를 뽑았다고 표시
                GameManager.WeaponDrawn[randomIndex] = true;



                // Result Image에 무기 이미지 표시
                if (i < index)
                {
                    resultImages[i].sprite = selectedSprite;
                    BGimageComponent = resultBG[i].GetComponent<Image>();
                    Outline[] outline = resultBG[i].GetComponentsInChildren<Outline>();

                    if (randomIndex <= 3)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기1번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기1번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기1번색");

                    }
                    else if (randomIndex >= 4 && randomIndex <= 7)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기2번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기2번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기2번색");
                    }
                    else if (randomIndex >= 8 && randomIndex <= 11)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기3번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기3번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기3번색");
                    }
                    else if (randomIndex >= 12 && randomIndex <= 15)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기4번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기4번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기4번색");
                    }
                    else if (randomIndex >= 16 && randomIndex <= 19)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기5번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기5번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기5번색");
                    }
                    else if (randomIndex >= 20 && randomIndex <= 23)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기6번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기6번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기6번색");
                    }
                    resultBG[i].SetActive(true);


                    // WeaponDrawn[0]이 true이고, WeaponOwnDamage가 증가하지 않은 경우에만 증가시킴
                    if (GameManager.WeaponDrawn[randomIndex] == true && !GameManager.weaponOwnDamageIncreased[randomIndex])
                    {
                        GameManager.WeaponOwnDamage += GameManager.WeaponOwnDmg[randomIndex];
                        GameManager.weaponOwnDamageIncreased[randomIndex] = true; // WeaponOwnDamage가 증가되었음을 표시
                    }
                }
            }
        }
        else if(thisInDrawingState == 2)
        {
            AccessoryDrawBtn[2].gameObject.SetActive(true);
            AccessoryDrawBtn[3].gameObject.SetActive(true);

            WeaponDrawBtn[2].gameObject.SetActive(false);
            WeaponDrawBtn[3].gameObject.SetActive(false);


            GameManager.AccessoryCounts += index;
            StatisticsManager.ImmutabilityAccessoryCount += index;

            if (GameManager.AccessoryCounts >= GameManager.AccessoryMaxCounts[GameManager.AccessoryShopLevel - 1] && GameManager.AccessoryShopLevel != 7)
            {
                GameManager.AccessoryCounts -= GameManager.AccessoryMaxCounts[GameManager.AccessoryShopLevel - 1];
                GameManager.AccessoryShopLevel++;
                UpdateWeaponProbility();
                Image buttonImage = AccessoryLevelReceiveBtn[GameManager.AccessoryShopLevel - 2].GetComponent<Image>();       // 버튼의 이미지 컴포넌트 가져오기
                AccessoryLevelReceiveBtn[GameManager.AccessoryShopLevel - 2].interactable = true;

                // 버튼의 이미지 색상 변경
                buttonImage.color = ColorManager.ColorChange("초록색");
            }

            if (GameManager.AccessoryShopLevel == 7) AccessoryText.text = GameManager.AccessoryCounts + " / MAX";
            else AccessoryText.text = GameManager.AccessoryCounts + "/" + GameManager.AccessoryMaxCounts[GameManager.AccessoryShopLevel - 1];

            if (GameManager.AccessoryShopLevel == 7) AccessoryLevelText.text = "Lv. MAX";
            else AccessoryLevelText.text = "Lv." + GameManager.AccessoryShopLevel;



            AccessoryAdText.text = GameManager.Accessory_AdCount + "회 소환";
            AccessoryAdDayText.text = "(" + Accessory_AdDayCount + "/" + absoulteAdDayCount + ")";

            // 각 무기 레벨에 대한 확률 배열
            float[] AccessoryLevelProbabilities = {
                GameManager.AccessoryLevelProbilityValue_Common[GameManager.AccessoryShopLevel - 1],
                GameManager.AccessoryLevelProbilityValue_Rare[GameManager.AccessoryShopLevel - 1],
                GameManager.AccessoryLevelProbilityValue_UnCommon[GameManager.AccessoryShopLevel - 1],
                GameManager.AccessoryLevelProbilityValue_Artifact[GameManager.AccessoryShopLevel - 1],
                GameManager.AccessoryLevelProbilityValue_Mythical[GameManager.AccessoryShopLevel - 1],
                GameManager.AccessoryLevelProbilityValue_Legend[GameManager.AccessoryShopLevel - 1]
                };

            for (int i = 0; i < index; i++)
            {
                // AudioSource 컴포넌트 초기화
                audioSource = GetComponent<AudioSource>();

                yield return new WaitForSeconds(0.025f);
                audioSource.pitch = 1.2f;

                audioSource.PlayOneShot(GachaSoundClip, 0.7f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.

                // 각 무기 레벨에 대한 누적 확률 계산
                float[] cumulativeProbabilities = new float[AccessoryLevelProbabilities.Length];
                float cumulativeProbability = 0f;
                for (int j = 0; j < AccessoryLevelProbabilities.Length; j++)
                {
                    cumulativeProbability += AccessoryLevelProbabilities[j];
                    cumulativeProbabilities[j] = cumulativeProbability;
                }
                float randomValue = Random.value * 100f;
                int randomIndex = 0;

                // 누적 확률을 사용하여 악세서리 선택
                for (int j = 0; j < cumulativeProbabilities.Length; j++)
                {
                    if (randomValue <= cumulativeProbabilities[j])
                    {
                        // 해당 레벨의 무기를 선택
                        randomIndex = Random.Range(j * 4, (j + 1) * 4);
                        break;
                    }
                }

                Sprite selectedSprite = GameManager.AccessorySprites[randomIndex];

                // Dictionary에서 해당 악세서리의 카운트 증가
                if (GameManager.AccessoryCount.ContainsKey(selectedSprite))
                {
                    GameManager.AccessoryCount[selectedSprite]++;
                }
                else
                {
                    GameManager.AccessoryCount[selectedSprite] = 1;
                }

                // 악세서리를 뽑았다고 표시
                GameManager.AccessoryDrawn[randomIndex] = true;

                // Result Image에 악세서리 이미지 표시
                if (i < index)
                {
                    resultImages[i].sprite = selectedSprite;
                    BGimageComponent = resultBG[i].GetComponent<Image>();
                    Outline[] outline = resultBG[i].GetComponentsInChildren<Outline>();

                    if (randomIndex <= 3)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기1번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기1번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기1번색");
                    }
                    else if (randomIndex >= 4 && randomIndex <= 7)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기2번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기2번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기2번색");
                    }
                    else if (randomIndex >= 8 && randomIndex <= 11)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기3번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기3번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기3번색");
                    }
                    else if (randomIndex >= 12 && randomIndex <= 15)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기4번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기4번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기4번색");
                    }
                    else if (randomIndex >= 16 && randomIndex <= 19)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기5번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기5번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기5번색");
                    }
                    else if (randomIndex >= 20 && randomIndex <= 23)
                    {
                        BGimageComponent.color = ColorManager.ColorChange("무기6번색");
                        outline[0].effectColor = ColorManager.ColorChange("무기6번색");
                        outline[1].effectColor = ColorManager.ColorChange("무기6번색");
                    }
                    resultBG[i].SetActive(true);


                    // WeaponDrawn[0]이 true이고, AccessoryOwnDamage가 증가하지 않은 경우에만 증가시킴
                    if (GameManager.AccessoryDrawn[randomIndex] == true && !GameManager.AccessoryOwnExpIncreased[randomIndex])
                    {
                        GameManager.AccessoryOwnExperience += GameManager.AccessoryOwnExp[randomIndex];
                        GameManager.AccessoryOwnExpIncreased[randomIndex] = true; // AccessoryOwnDamage가 증가되었음을 표시
                    }
                }
            }
        }

        GachaRun = false;
        closeButton.interactable = true;

        UpdateButtonInteractivity();

        while (gachaPanel.activeSelf)
        {
            yield return null;
        }

    }
    private void UpdateButtonInteractivity()
    {
        if(Weapon_AdDayCount == 0)
        {
            AdDrawBtn[0].interactable = false;
        }
        if (Accessory_AdDayCount == 0)
        {
            AdDrawBtn[1].interactable = false;
        }

        if (GameManager.Player_Diamond >= NeedDiamondValue500 && GachaRun == false)
        {

            if (GameManager.Player_Diamond >= NeedDiamondValue1500 && GachaRun == false)
            {
                WeaponDrawBtn[0].interactable = true;
                WeaponDrawBtn[1].interactable = true;
                WeaponDrawBtn[2].interactable = true;
                WeaponDrawBtn[3].interactable = true;

                AccessoryDrawBtn[0].interactable = true;
                AccessoryDrawBtn[1].interactable = true;
                AccessoryDrawBtn[2].interactable = true;
                AccessoryDrawBtn[3].interactable = true;
            }
            else
            {
                WeaponDrawBtn[0].interactable = true;
                WeaponDrawBtn[1].interactable = false;
                WeaponDrawBtn[2].interactable = true;
                WeaponDrawBtn[3].interactable = false;

                AccessoryDrawBtn[0].interactable = true;
                AccessoryDrawBtn[1].interactable = false;
                AccessoryDrawBtn[2].interactable = true;
                AccessoryDrawBtn[3].interactable = false;
            }
        }
        else
        {
            WeaponDrawBtn[0].interactable = false;
            WeaponDrawBtn[1].interactable = false;
            WeaponDrawBtn[2].interactable = false;
            WeaponDrawBtn[3].interactable = false;

            AccessoryDrawBtn[0].interactable = false;
            AccessoryDrawBtn[1].interactable = false;
            AccessoryDrawBtn[2].interactable = false;
            AccessoryDrawBtn[3].interactable = false;
        }


        WeaponNeedDiamondText[0].text = NeedDiamondValue1500 + "";
        WeaponNeedDiamondText[1].text = NeedDiamondValue1500 + "";
        WeaponNeedDiamondText[2].text = NeedDiamondValue500 + "";
        WeaponNeedDiamondText[3].text = NeedDiamondValue500 + "";

        AccessoryNeedDiamondText[0].text = NeedDiamondValue1500 + "";
        AccessoryNeedDiamondText[1].text = NeedDiamondValue1500 + "";
        AccessoryNeedDiamondText[2].text = NeedDiamondValue500 + "";
        AccessoryNeedDiamondText[3].text = NeedDiamondValue500 + "";
    }


    private void ResetResultImages()
    {
        foreach (Image resultImage in resultImages)
        {
            resultImage.sprite = null;
        }
    }
    void Givediamond()
    {
        GameManager.Player_Diamond += 800;
        GameManager.Player_Money += 300000000;
        GameManager.RelicsReinforceScroll += 10;
        GameManager.Player_RedStone += 40000;
        GameManager.HaveStarGrey += 50000;
        GameManager.HaveStarBrown += 50000;
        GameManager.HaveStarBlue += 50000;
        GameManager.HaveStarGreen += 50000;
        GameManager.HaveStarRed += 50000;
        GameManager.HaveStarYellow += 50000;
        GameManager.HaveStarPurple += 50000;
        GameManager.HaveStarOrange += 50000;
        GameManager.HaveStarDark += 50000;
        GameManager.PlayerHaveMobScroll[0] += 5000;
        GameManager.PlayerHaveMobScroll[1] += 5000;
        GameManager.PlayerHaveMobScroll[2] += 5000;
        GameManager.PlayerHaveMobScroll[3] += 5000;
        GameManager.PlayerHaveMobScroll[4] += 5000;
        GameManager.PlayerHaveMobScroll[5] += 5000;
        GameManager.PlayerHaveMobScroll[6] += 5000;
        GameManager.PlayerHaveMobScroll[7] += 5000;
    }
}
