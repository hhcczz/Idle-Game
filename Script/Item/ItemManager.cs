using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{ 
    public Button InfoPanelExitBtn;
    public GameObject EquipInfoPanel;


    public Text[] EquipMiddleTitle;
    public Button[] Equipment_WeaponBtn;
    public Button EquipWeaponPlayerBtn;
    public Button EquipInfoPanelNextBtn;
    public Button EquipInfoPanelReturnBtn;
    public GameObject[] EquipingWeaponImg;
    public Image[] WeaponImg;
    public Image[] WeaponInfoImg;

    public Text EquipInfoPanelTitle;
    public Text RedStone;
    public Text EquipValue;
    public Text HaveValue;
    public GameObject[] GradeImg;

    public Button Weapon_Batchsynthesis;
    public Slider[] WeaponBar;
    public Slider[] InfoWeaponBar;

    public Text[] itemCountTexts;                       // 무기 카운트 Text 배열
    public Text[] InfoitemCountTexts;                   // 무기 정보 카운트 Text 배열

    // 악세서리

    public Button[] Equipment_AccessoryBtn;
    public Button EquipAccessoryPlayerBtn;
    public GameObject[] EquipingAccessoryImg;
    public Image[] AccessoryImg;

    public Button Accessory_Batchsynthesis;
    public Slider[] AccessoryBar;

    public Text[] AccessoryitemCountTexts;                       // 무기 카운트 Text 배열

    public Button Weapon;
    public Button Accessory;
    public Button Warrant;
    public Button Trand;

    public GameObject WeaponPanel;
    public GameObject AccessoryPanel;
    public GameObject WarrantPanel;
    public GameObject TrandPanel;

    private string[] equipmiddlename = new string[4];

    private int Weapon_currentIndex = 0; // 현재 선택된 인덱스를 저장하는 변수
    public static int Weapon_currentEquipIndex = -1;

    private int Accessory_currentIndex = 0; // 현재 선택된 인덱스를 저장하는 변수
    public static int Accessory_currentEquipIndex = -1;

    private int thisInfoPanel = -1;       // 0 : Weapon // 1 : Accessory // 2 : Warrant

    public Button ReinforceBtn;                 // 강화 버튼
    public Text NeedReinStoneText;              // 필요 보석 텍스트
    public Text ReinforceValueText;             // 강화 레벨 텍스트

    public Text[] WeaponReinText;
    public Text[] AccessoryReinText;


    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip ItemSoundClip; // AudioClip 변수 선언

    public GameObject LackPanel;
    public Button lackOutBtn;

    public GameObject NoDrawnLackPanel;
    public Button NoDrawnLackOutBtn;

    public GameObject ReinLackPanel;
    public Button ReinlackOutBtn;

    private void Start()
    {
        SaveLoadManager.Instance.LoadEquip();
        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        if (WeaponPanel.activeSelf == true) thisInfoPanel = 1;
        else if (AccessoryPanel.activeSelf == true) thisInfoPanel = 2;
        else if (WarrantPanel.activeSelf == true) thisInfoPanel = 3;
        else if (TrandPanel.activeSelf == true) thisInfoPanel = 4;


        for (int i = 0; i < Equipment_WeaponBtn.Length; i++)
        {
            int index = i;

            Equipment_WeaponBtn[index].onClick.AddListener(() => OpenEquipInfoPanel(index));
        }

        for (int i = 0; i < Equipment_AccessoryBtn.Length; i++)
        {
            int index = i;

            Equipment_AccessoryBtn[index].onClick.AddListener(() => OpenEquipInfoPanel(index));
        }

        EquipWeaponPlayerBtn.onClick.AddListener        (() => EquipPlayerInfoPanel(Weapon_currentIndex));
        EquipAccessoryPlayerBtn.onClick.AddListener     (() => EquipPlayerInfoPanel(Accessory_currentIndex));

        InfoPanelExitBtn.onClick.AddListener(CloseEquipInfoPanel);
        lackOutBtn.onClick.AddListener(LackOut);
        NoDrawnLackOutBtn.onClick.AddListener(NoDrawnLackOut);
        ReinlackOutBtn.onClick.AddListener(ReinLackOut);

        RedStone.text = TextFormatter.GetThousandCommaText(GameManager.Player_RedStone) + "";

        EquipInfoPanelNextBtn.onClick.AddListener       (() => EquipInfoNextInfo());
        EquipInfoPanelReturnBtn.onClick.AddListener     (() => EquipInfoReturnInfo());

        Weapon_Batchsynthesis.onClick.AddListener       (System_Batchsynthesis);
        Accessory_Batchsynthesis.onClick.AddListener    (System_Batchsynthesis);

        // Panel 변경
        Weapon.onClick.AddListener      (() => OnClickChangeBtn(1));
        Accessory.onClick.AddListener   (() => OnClickChangeBtn(2));
        Warrant.onClick.AddListener     (() => OnClickChangeBtn(3));
        Trand.onClick.AddListener       (() => OnClickChangeBtn(4));

        ReinforceBtn.onClick.AddListener(() => ItemReinforce());

        equipmiddlename[0] = "무기 장착 효과";
        equipmiddlename[1] = "무기 보유 효과";
        equipmiddlename[2] = "악세서리 장착 효과";
        equipmiddlename[3] = "악세서리 보유 효과";

        for (int i = 0; i < GameConstants.WeaponNum; i++)
        {
            WeaponImg[i].sprite = GameManager.itemSprites[i];
            AccessoryImg[i].sprite = GameManager.AccessorySprites[i];

            if (Weapon_currentEquipIndex != i) EquipingWeaponImg[i].SetActive(false);
            else EquipingWeaponImg[i].SetActive(true);

            if (Accessory_currentEquipIndex != i) EquipingAccessoryImg[i].SetActive(false);
            else EquipingAccessoryImg[i].SetActive(true);
        }


        for (int i = 0; i < WeaponReinText.Length; i++)
        {
            if (EquipReinforceManager.WeaponReinValue[i] == 0) WeaponReinText[i].text = "";
            else WeaponReinText[i].text = "+ " + EquipReinforceManager.WeaponReinValue[i];

            if (EquipReinforceManager.AccessoryReinValue[i] == 0) AccessoryReinText[i].text = "";
            else AccessoryReinText[i].text = "+ " + EquipReinforceManager.AccessoryReinValue[i];
        }


    }

    private void Update()
    {
        if(Weapon_currentEquipIndex != -1) GameManager.WeaponEquipDamage = GameManager.WeaponEquipDmg[Weapon_currentEquipIndex];
        if(Accessory_currentEquipIndex != -1) GameManager.AccessoryEquipExperience = GameManager.AccessoryEquipExp[Accessory_currentEquipIndex];

        GameManager.WeaponOwnDamage = 0;
        for(int i = 0; i < GameConstants.WeaponNum; i++)
        {
            if(GameManager.WeaponDrawn[i] == true) GameManager.WeaponOwnDamage += GameManager.WeaponOwnDmg[i];
        }
    }

    private void OnEnable()
    {
        if (WeaponPanel.activeSelf == true) thisInfoPanel = 1;
        else if (AccessoryPanel.activeSelf == true) thisInfoPanel = 2;
        else if (WarrantPanel.activeSelf == true) thisInfoPanel = 3;
        else if (TrandPanel.activeSelf == true) thisInfoPanel = 4;

        OnClickChangeBtn(thisInfoPanel);
    }

    private void LackOut()
    {
        LackPanel.SetActive(false);
    }
    private void ReinLackOut()
    {
        ReinLackPanel.SetActive(false);
    }

    private void NoDrawnLackOut()
    {
        NoDrawnLackPanel.SetActive(false);
    }


    void OpenEquipInfoPanel(int index)
    {
        if (index == 0) EquipInfoPanelReturnBtn.gameObject.SetActive(false);
        else EquipInfoPanelReturnBtn.gameObject.SetActive(true);

        if (index == 23) EquipInfoPanelNextBtn.gameObject.SetActive(false);
        else EquipInfoPanelNextBtn.gameObject.SetActive(true);

        UpdateItemCountText();
        UpdateWeaponBarValue();

        if (thisInfoPanel == 1)
        {
            Weapon_currentIndex = index; // 현재 선택된 인덱스 저장
            ReinTextUpdate();
            EquipAccessoryPlayerBtn.gameObject.SetActive(false);
            EquipWeaponPlayerBtn.gameObject.SetActive(true);

            EquipMiddleTitle[0].text = equipmiddlename[0];
            EquipMiddleTitle[1].text = equipmiddlename[1];

            for (int i = 0; i < GameConstants.WeaponNum; i++) WeaponInfoImg[i].sprite = GameManager.itemSprites[i];

            for (int i = 0; i < GameManager.WeaponDrawn.Length; i++)
            {
                if (GameManager.WeaponDrawn[i] == true) WeaponInfoImg[i].color = ColorManager.ColorChange("하얀색");
                else WeaponInfoImg[i].color = ColorManager.ColorChange("검정색");
            }
            
            Debug.Log(index + "번 버튼 클릭! : ");

            // 장착 버튼 관리
            if (GameManager.WeaponDrawn[index] == false)
            {
                EquipWeaponPlayerBtn.interactable = false;
            }
            else
            {
                EquipWeaponPlayerBtn.interactable = true;
            }

            EquipInfoPanel.SetActive(true);
            GradeImg[index].SetActive(true);

            RedStone.text = TextFormatter.GetThousandCommaText(GameManager.Player_RedStone) + "";
            EquipValue.text = "공격력 : <color=cyan>+ " + GameManager.WeaponEquipDmg[index] + "</color>  →   <color=cyan>+ " + (GameManager.WeaponEquipDmg[index] + EquipReinforceManager.IncreaseWeaponValue[index]) + "</color>";
            HaveValue.text = "공격력 : <color=cyan>+ " + GameManager.WeaponOwnDmg[index] + "%</color>  →   <color=cyan>+ " + (GameManager.WeaponOwnDmg[index] + (decimal)EquipReinforceManager.IncreaseWeaponValue[index] / 10) + "</color>%"; ;
            
            EquipInfoPanelTitle.text = GameManager.WeaponTitle[index] + "";
        }
        else if (thisInfoPanel == 2) //  악세서리 정보 열기
        {
            Accessory_currentIndex = index; // 현재 선택된 인덱스 저장

            EquipWeaponPlayerBtn.gameObject.SetActive(false);
            EquipAccessoryPlayerBtn.gameObject.SetActive(true);
            ReinTextUpdate();
            EquipMiddleTitle[0].text = equipmiddlename[2];
            EquipMiddleTitle[1].text = equipmiddlename[3];

            for (int i = 0; i < GameConstants.AccessoryNum; i++) WeaponInfoImg[i].sprite = GameManager.AccessorySprites[i];

            for (int i = 0; i < GameManager.AccessoryDrawn.Length; i++)
            {
                if (GameManager.AccessoryDrawn[i] == true) WeaponInfoImg[i].color = ColorManager.ColorChange("하얀색");
                else WeaponInfoImg[i].color = ColorManager.ColorChange("검정색");
            }

            
            Debug.Log(index + "번 버튼 클릭! : ");

            // 장착 버튼 관리
            if (GameManager.AccessoryDrawn[index] == false)
            {
                EquipAccessoryPlayerBtn.interactable = false;
            }
            else
            {
                EquipAccessoryPlayerBtn.interactable = true;
            }

            EquipInfoPanel.SetActive(true);
            GradeImg[index].SetActive(true);

            RedStone.text = TextFormatter.GetThousandCommaText(GameManager.Player_RedStone) + "";
            EquipValue.text = "경험치 : <color=cyan>+ " + GameManager.AccessoryEquipExp[index] + "%</color>  →   <color=cyan>+ " + (GameManager.AccessoryEquipExp[index] + EquipReinforceManager.IncreaseAccessoryValue[index]) + "</color>%";
            HaveValue.text = "경험치 : <color=cyan>+ " + GameManager.AccessoryOwnExp[index] + "%</color>  →   <color=cyan>+ " + (GameManager.AccessoryOwnExp[index] + (decimal)EquipReinforceManager.IncreaseAccessoryValue[index] / 10) + "</color>%"; ;
            
            EquipInfoPanelTitle.text = GameManager.AccessoryTitle[index] + "";
        }

    }

    private void ItemReinforce()
    {
        if(thisInfoPanel == 1)
        {
            if (GameManager.WeaponDrawn[Weapon_currentIndex] == true)
            {
                if (EquipReinforceManager.ReinforceItem(Weapon_currentIndex, 1) == -1)
                {
                    ReinLackPanel.SetActive(true);
                }
                ReinTextUpdate();
            }
            else NoDrawnLackPanel.SetActive(true);
        }
        else if(thisInfoPanel == 2)
        {
            if (GameManager.AccessoryDrawn[Accessory_currentIndex] == true)
            {
                if (EquipReinforceManager.ReinforceItem(Accessory_currentIndex, 2) == -1)
                {
                    ReinLackPanel.SetActive(true);
                }
                ReinTextUpdate();
            }
            else NoDrawnLackPanel.SetActive(true);

        }
        EquipSave saveData = new();
        SaveLoadManager.Instance.SaveEquip(saveData);
    }

    private void OnClickChangeBtn(int index)
    {
        if (index == 1) OnButtonClick(WeaponPanel, Weapon.GetComponent<Image>());
        else if (index == 2) OnButtonClick(AccessoryPanel, Accessory.GetComponent<Image>());
        else if (index == 3) OnButtonClick(WarrantPanel, Warrant.GetComponent<Image>());
        else if (index == 4) OnButtonClick(TrandPanel, Trand.GetComponent<Image>());

        thisInfoPanel = index;
        UpdateItemCountText();
        UpdateWeaponBarValue();
    }

    private void OnButtonClick(GameObject panel, Image clickedButtonImage)
    {
        SetPanelActive(panel);
        SetButtonsAlpha(clickedButtonImage);
    }

    private void SetPanelActive(GameObject panel)
    {
        WeaponPanel.SetActive       (panel == WeaponPanel);
        AccessoryPanel.SetActive    (panel == AccessoryPanel);
        WarrantPanel.SetActive      (panel == WarrantPanel);
        TrandPanel.SetActive        (panel == TrandPanel);
    }

    private void SetButtonsAlpha(Image clickedButtonImage)
    {
        float disableAlpha = GameManager.disable_ButtonAlpha;
        float enableAlpha = GameManager.enable_ButtonAlpha;

        Image[] buttons = { Weapon.GetComponent<Image>(), Accessory.GetComponent<Image>(), Warrant.GetComponent<Image>(), Trand.GetComponent<Image>() };

        foreach (Image buttonImage in buttons)
        {
            Color buttonColor = buttonImage.color;
            buttonColor.a = buttonImage == clickedButtonImage ? enableAlpha : disableAlpha;
            buttonImage.color = buttonColor;
        }
    }


    //  무기 정보 열기

    void CloseEquipInfoPanel()
    {
        Debug.Log("클로즈 버튼 클릭!");
        GradeImg[Weapon_currentIndex].SetActive(false);
        GradeImg[Accessory_currentIndex].SetActive(false);
        EquipInfoPanel.SetActive(false);

        for (int i = 0; i < WeaponReinText.Length; i++)
        {
            if (EquipReinforceManager.WeaponReinValue[i] == 0) WeaponReinText[i].text = "";
            else WeaponReinText[i].text = "+ " + EquipReinforceManager.WeaponReinValue[i];

            if (EquipReinforceManager.AccessoryReinValue[i] == 0) AccessoryReinText[i].text = "";
            else AccessoryReinText[i].text = "+ " + EquipReinforceManager.AccessoryReinValue[i];
        }

    }

    void EquipInfoNextInfo()
    {
        if(thisInfoPanel == 1)
        {
            if (Weapon_currentIndex < Equipment_WeaponBtn.Length - 1) // 다음 인덱스가 배열 범위 내에 있는지 확인
            {
                GradeImg[Weapon_currentIndex].SetActive(false);
                OpenEquipInfoPanel(Weapon_currentIndex + 1);
            }
        }
        else if(thisInfoPanel == 2)
        {
            if (Accessory_currentIndex < Equipment_AccessoryBtn.Length - 1) // 다음 인덱스가 배열 범위 내에 있는지 확인
            {
                GradeImg[Accessory_currentIndex].SetActive(false);
                OpenEquipInfoPanel(Accessory_currentIndex + 1);
            }
        }
    }

    void EquipInfoReturnInfo()
    {
        if(thisInfoPanel == 1)
        {
            if (Weapon_currentIndex > 0) // 이전 인덱스가 0보다 큰지 확인
            {
                GradeImg[Weapon_currentIndex].SetActive(false);
                OpenEquipInfoPanel(Weapon_currentIndex - 1);
            }
        }
        else if(thisInfoPanel == 2)
        {
            if (Accessory_currentIndex > 0) // 이전 인덱스가 0보다 큰지 확인
            {
                GradeImg[Accessory_currentIndex].SetActive(false);
                OpenEquipInfoPanel(Accessory_currentIndex - 1);
            }
        }
    }

    void EquipPlayerInfoPanel(int index)
    {
        // 무기 장착 코드
        if(thisInfoPanel == 1)
        {
            Debug.Log("무기 장착 전 index : " + index);
            Debug.Log("무기 장착 전 Weapon_currentIndex : " + Weapon_currentIndex);

            if(Weapon_currentEquipIndex != -1) EquipingWeaponImg[Weapon_currentEquipIndex].SetActive(false);
            EquipingWeaponImg[index].SetActive(true);

            Weapon_currentEquipIndex = index;

            Debug.Log("무기 장착 후 index : " + index);
            Debug.Log("무기 장착 후 Weapon_currentEquipIndex : " + Weapon_currentEquipIndex);
        }
        else if(thisInfoPanel == 2)
        {
            Debug.Log("악세서리 장착 전 index : " + index);
            Debug.Log("악세서리 장착 전 Accessory_currentIndex : " + Accessory_currentIndex);

            if(Accessory_currentEquipIndex != -1) EquipingAccessoryImg[Accessory_currentEquipIndex].SetActive(false);
            EquipingAccessoryImg[index].SetActive(true);

            Accessory_currentEquipIndex = index;

            Debug.Log("악세서리 장착 후 index : " + index);
            Debug.Log("악세서리 장착 후 Accessory_currentEquipIndex : " + Accessory_currentEquipIndex);
        }
        GradeImg[index].SetActive(false);
        EquipInfoPanel.SetActive(false);

        EquipSave saveData = new();
        SaveLoadManager.Instance.SaveEquip(saveData);
    }


    void System_Batchsynthesis()
    {
        Debug.Log("합성 시작!");
        audioSource.PlayOneShot(ItemSoundClip, 1f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
        if (thisInfoPanel == 1)  // 무기
        {
            bool hasSynthesis = false; // 합성 여부를 나타내는 변수

            // 스프라이트별로 반복하며 합성 진행
            for (int i = 0; i < GameConstants.WeaponNum - 1; i++)
            {
                Sprite currentSprite = GameManager.itemSprites[i];
                Sprite nextSprite = GameManager.itemSprites[i + 1];

                // 현재 스프라이트의 카운트가 5 이상인 경우
                while (GameManager.itemCounts.ContainsKey(currentSprite) && GameManager.itemCounts[currentSprite] >= 5)
                {
                    hasSynthesis = true; // 합성이 있었음을 표시

                    // 현재 스프라이트 카운트에서 5를 뺌
                    GameManager.itemCounts[currentSprite] -= 5;

                    // 다음 스프라이트의 카운트를 증가시킴
                    if (GameManager.itemCounts.ContainsKey(nextSprite))
                    {
                        GameManager.itemCounts[nextSprite]++;
                    }
                    else
                    {
                        GameManager.itemCounts[nextSprite] = 1;
                    }

                    if (GameManager.WeaponDrawn[i + 1] == false)
                    {
                        WeaponImg[i + 1].color = ColorManager.ColorChange("하얀색");
                        WeaponInfoImg[i + 1].color = ColorManager.ColorChange("하얀색");
                        GameManager.WeaponDrawn[i + 1] = true;
                    }
                }
            }

            if (!hasSynthesis) // 합성이 없을 경우
            {
                LackPanel.SetActive(true);
                return;
            }
            
        }
        else if (thisInfoPanel == 2)  // 악세서리
        {
            bool hasSynthesis = false; // 합성 여부를 나타내는 변수

            // 스프라이트별로 반복하며 합성 진행
            for (int i = 0; i < GameConstants.AccessoryNum - 1; i++)
            {
                Sprite currentSprite = GameManager.AccessorySprites[i];
                Sprite nextSprite = GameManager.AccessorySprites[i + 1];

                // 현재 스프라이트의 카운트가 5 이상인 경우
                while (GameManager.AccessoryCount.ContainsKey(currentSprite) && GameManager.AccessoryCount[currentSprite] >= 5)
                {
                    hasSynthesis = true; // 합성이 있었음을 표시

                    // 현재 스프라이트 카운트에서 5를 뺌
                    GameManager.AccessoryCount[currentSprite] -= 5;

                    // 다음 스프라이트의 카운트를 증가시킴
                    if (GameManager.AccessoryCount.ContainsKey(nextSprite))
                    {
                        GameManager.AccessoryCount[nextSprite]++;
                    }
                    else
                    {
                        GameManager.AccessoryCount[nextSprite] = 1;
                    }

                    if (GameManager.AccessoryDrawn[i + 1] == false)
                    {
                        AccessoryImg[i + 1].color = ColorManager.ColorChange("하얀색");
                        WeaponInfoImg[i + 1].color = ColorManager.ColorChange("하얀색");
                        GameManager.AccessoryDrawn[i + 1] = true;
                    }
                }
            }

            if (!hasSynthesis) // 합성이 없을 경우
            {
                LackPanel.SetActive(true);
                return;
            }
        }

        // 무기 합성 후 UI 업데이트
        UpdateItemCountText();
        UpdateWeaponBarValue();
        Debug.Log("합성 완료!");

        EquipSave saveData = new();
        SaveLoadManager.Instance.SaveEquip(saveData);
    }

    public void UpdateWeaponBarValue()
    {
        if(thisInfoPanel == 1)
        {
            foreach (var kvp in GameManager.itemCounts)
            {
                Sprite sprite = kvp.Key;
                int count = kvp.Value;

                int weaponIndex = System.Array.IndexOf(GameManager.itemSprites, sprite);

                // 해당 무기에 대한 슬라이더 값을 증가시킴
                if (weaponIndex >= 0 && weaponIndex < WeaponBar.Length)
                {
                    // 무기 개수가 최대 5개일 때 슬라이더가 꽉 차도록 함
                    WeaponBar[weaponIndex].value = Mathf.Clamp01((float)count / 5);
                    InfoWeaponBar[weaponIndex].value = Mathf.Clamp01((float)count / 5);
                }
            }
        }
        else if(thisInfoPanel == 2)
        {
            foreach (var kvp in GameManager.AccessoryCount)
            {
                Sprite sprite = kvp.Key;
                int count = kvp.Value;

                int AccessoryIndex = System.Array.IndexOf(GameManager.AccessorySprites, sprite);

                // 해당 무기에 대한 슬라이더 값을 증가시킴
                if (AccessoryIndex >= 0 && AccessoryIndex < AccessoryBar.Length)
                {
                    // 무기 개수가 최대 5개일 때 슬라이더가 꽉 차도록 함
                    AccessoryBar[AccessoryIndex].value = Mathf.Clamp01((float)count / 5);
                    InfoWeaponBar[AccessoryIndex].value = Mathf.Clamp01((float)count / 5);
                }
            }
        }
    }

    private void UpdateItemCountText()
    {
        if (thisInfoPanel == 1)
        {
            // 각 무기에 대한 카운트를 Text에 표시
            for (int i = 0; i < 24; i++)
            {
                int index = i;

                if (GameManager.itemCounts.ContainsKey(GameManager.itemSprites[index]))
                {
                    itemCountTexts[index].text = GameManager.itemCounts[GameManager.itemSprites[index]] + "/5";
                    InfoitemCountTexts[index].text = GameManager.itemCounts[GameManager.itemSprites[index]] + "/5";
                }
            }
        }
        else if(thisInfoPanel == 2)
        {
            // 각 악세서리에 대한 카운트를 Text에 표시
            for (int i = 0; i < GameManager.AccessorySprites.Length; i++)
            {
                Sprite currentSprite = GameManager.AccessorySprites[i];
                if (GameManager.AccessoryCount.ContainsKey(currentSprite))
                {
                    AccessoryitemCountTexts[i].text = GameManager.AccessoryCount[currentSprite] + "/5";
                    InfoitemCountTexts[i].text = GameManager.AccessoryCount[currentSprite] + "/5";
                }
            }
        }
    }
    private void ReinTextUpdate()
    {
        Debug.Log("현재 인덱스 : " + Weapon_currentIndex);
        if (thisInfoPanel == 1)
        {
            EquipValue.text = "공격력 : <color=cyan>+ " + GameManager.WeaponEquipDmg[Weapon_currentIndex] + "</color>  →   <color=cyan>+ " + (GameManager.WeaponEquipDmg[Weapon_currentIndex] + EquipReinforceManager.IncreaseWeaponValue[Weapon_currentIndex]) + "</color>";
            HaveValue.text = "공격력 : <color=cyan>+ " + GameManager.WeaponOwnDmg[Weapon_currentIndex] + "%</color>  →   <color=cyan>+ " + (GameManager.WeaponOwnDmg[Weapon_currentIndex] + (decimal)EquipReinforceManager.IncreaseWeaponValue[Weapon_currentIndex] / 10) + "</color>%";

            NeedReinStoneText.text = "강화석 필요 개수 : " + TextFormatter.GetThousandCommaText(EquipReinforceManager.ReinforceNeedTextUpdate(Weapon_currentIndex, 1));
            ReinforceValueText.text = "+ " + EquipReinforceManager.WeaponReinValue[Weapon_currentIndex];
        }
        else if(thisInfoPanel == 2)
        {
            EquipValue.text = "경험치 : <color=cyan>+ " + GameManager.AccessoryEquipExp[Accessory_currentIndex] + "%</color>  →   <color=cyan>+ " + (GameManager.AccessoryEquipExp[Accessory_currentIndex] + EquipReinforceManager.IncreaseAccessoryValue[Accessory_currentIndex]) + "</color>%";
            HaveValue.text = "경험치 : <color=cyan>+ " + GameManager.AccessoryOwnExp[Accessory_currentIndex] + "%</color>  →   <color=cyan>+ " + (GameManager.AccessoryOwnExp[Accessory_currentIndex] + (decimal)EquipReinforceManager.IncreaseAccessoryValue[Accessory_currentIndex] / 10) + "</color>%";

            NeedReinStoneText.text = "강화석 필요 개수 : " + TextFormatter.GetThousandCommaText(EquipReinforceManager.ReinforceNeedTextUpdate(Accessory_currentIndex, 2));
            ReinforceValueText.text = "+ " + EquipReinforceManager.AccessoryReinValue[Accessory_currentIndex];
        }
        RedStone.text = TextFormatter.GetThousandCommaText(GameManager.Player_RedStone) + "";
    }
}
