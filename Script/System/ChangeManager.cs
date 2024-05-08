using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeManager : MonoBehaviour
{
    private static ChangeManager instance;

    public static ChangeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ChangeManager>();
                if (instance == null)
                {
                    Debug.LogError("ChangeManager 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }

    public Button MineBtn;
    public Button CharacterBtn;
    public Button EquipmentBtn;
    public Button RelicsBtn;
    public Button ShopBtn;

    public GameObject CharacterScroll;
    public GameObject EquipmentScroll;
    public GameObject ShopPanel;
    public GameObject MinePanel;
    public GameObject RelicsPanel;

    public GameObject[] MainObject;

    public Image[] WeaponImg;
    public Image[] InfoWeaponImg;

    public Image[] AccessoryImg;

    public static int InPanel = -1;       //  1 광산    2 캐릭터   3 장비    4 유물    5 상점

    // Start is called before the first frame update
    void Start()
    {
        if (MinePanel.activeSelf == true)               InPanel = 1;
        else if (CharacterScroll.activeSelf == true)    InPanel = 2;
        else if (EquipmentScroll.activeSelf == true)    InPanel = 3;
        else if (RelicsPanel.activeSelf == true)        InPanel = 4;
        else if (ShopPanel.activeSelf == true)          InPanel = 5;

        MineBtn.onClick.AddListener(()      => OnClickChangeBtn(1));
        CharacterBtn.onClick.AddListener(() => OnClickChangeBtn(2));
        EquipmentBtn.onClick.AddListener(() => OnClickChangeBtn(3));
        RelicsBtn.onClick.AddListener(()    => OnClickChangeBtn(4));
        ShopBtn.onClick.AddListener(()      => OnClickChangeBtn(5));

        for (int i = 0; i < GameManager.WeaponDrawn.Length; i++)
        {
            if (GameManager.WeaponDrawn[i] == true) WeaponImg[i].color = ColorManager.ColorChange("하얀색");
            else WeaponImg[i].color = ColorManager.ColorChange("검정색");
        }

        for (int i = 0; i < GameManager.AccessoryDrawn.Length; i++)
        {
            if (GameManager.AccessoryDrawn[i] == true) AccessoryImg[i].color = ColorManager.ColorChange("하얀색");
            else AccessoryImg[i].color = ColorManager.ColorChange("검정색");
        }

        OnClickChangeBtn(InPanel);
    }

    public void OnClickChangeBtn(int index)
    {
        InPanel = index;

        if (index == 1)         OnButtonClick(MinePanel, MineBtn.GetComponent<Image>());
        else if (index == 2)    OnButtonClick(CharacterScroll, CharacterBtn.GetComponent<Image>());
        else if (index == 3)    OnButtonClick(EquipmentScroll, EquipmentBtn.GetComponent<Image>());
        else if (index == 4)    OnButtonClick(RelicsPanel, RelicsBtn.GetComponent<Image>());
        else if (index == 5)    OnButtonClick(ShopPanel, ShopBtn.GetComponent<Image>());
    }

    private void SetPanelActive(GameObject panel)
    {
        ShopPanel.SetActive         (panel == ShopPanel);
        EquipmentScroll.SetActive   (panel == EquipmentScroll);
        MinePanel.SetActive         (panel == MinePanel);
        RelicsPanel.SetActive       (panel == RelicsPanel);
        CharacterScroll.SetActive   (panel == CharacterScroll);

        if(panel == ShopPanel || panel == MinePanel || panel == RelicsPanel)
        {
            for(int i = 0; i < MainObject.Length; i++) MainObject[i].SetActive(false);
        }
        else for (int i = 0; i < MainObject.Length; i++) MainObject[i].SetActive(true);
    }

    private void SetButtonsAlpha(Image clickedButtonImage)
    {
        float disableAlpha = GameManager.disable_ButtonAlpha;
        float enableAlpha = GameManager.enable_ButtonAlpha;

        Image[] buttons = { ShopBtn.GetComponent<Image>(), EquipmentBtn.GetComponent<Image>(), MineBtn.GetComponent<Image>(), RelicsBtn.GetComponent<Image>(), CharacterBtn.GetComponent<Image>() };

        foreach (Image buttonImage in buttons)
        {
            Color buttonColor = buttonImage.color;
            buttonColor.a = buttonImage == clickedButtonImage ? enableAlpha : disableAlpha;
            buttonImage.color = buttonColor;
        }

        for (int i = 0; i < GameManager.WeaponDrawn.Length; i++)
        {
            if (GameManager.WeaponDrawn[i] == true) WeaponImg[i].color = ColorManager.ColorChange("하얀색");
            else WeaponImg[i].color = ColorManager.ColorChange("검정색");
        }

        for (int i = 0; i < GameManager.AccessoryDrawn.Length; i++)
        {
            if (GameManager.AccessoryDrawn[i] == true) AccessoryImg[i].color = ColorManager.ColorChange("하얀색");
            else AccessoryImg[i].color = ColorManager.ColorChange("검정색");
        }

        for(int i = 0; i < CharacterMoneyShopManager.Instance.MoneyUpBtn.Length; i++)
             CharacterMoneyShopManager.Instance.MoneyUpTextUpdate(i);
    }

    private void OnButtonClick(GameObject panel, Image clickedButtonImage)
    {
        SetPanelActive(panel);
        SetButtonsAlpha(clickedButtonImage);
    }

}
