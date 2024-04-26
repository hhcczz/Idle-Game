using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageBuyManager : MonoBehaviour
{
    private AudioSource audioSource; // AudioSource 변수 추가

    public Button[] PackageBuyBtn;

    public GameObject SuccessBuyPanel;
    public GameObject SuccessBG;

    public GameObject SuccessTitle;

    public GameObject SuccessAni;

    public Image SuccessImg;
    public Sprite[] SuccessSprite;
    public Text SuccessText;

    public Image[] SuccessRewardImg;
    public Text[] SuccessRewardText;
    public Image[] SuccessRewardFrame;

    public Text[] PackageText;
    public Image[] PackageFrame;
    public Image[] PackageShopImg;
    public Image[] PackageTitleImg;

    public Image[] PackageImg;

    public Sprite[] OpenPackageSprite;

    public Button SuccessOutBtn;

    private bool EasyPackageBuy;
    private bool NormalPackageBuy;
    private bool HardPackageBuy;
    private bool ExtremePackageBuy;

    private string[] SuccessTextString;

    private bool[] PackageBuyCheck;

    private BtnSoundManager btnsoundmanager;

    // Start is called before the first frame update
    void Start()
    {
        btnsoundmanager = BtnSoundManager.Instance;

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < PackageBuyBtn.Length; i++)
        {
            int index = i;

            PackageBuyBtn[index].onClick.AddListener(() => PackageBuy(index));
        }
        SuccessTextString = new string[4]
        {
            "<color=lime>Easy</color> - 패키지",
            "<color=cyan>Noraml</color> - 패키지",
            "<color=red>Hard</color> - 패키지",
            "<color=maroon>Hard</color> - 패키지",
        };
        PackageBuyCheck = new bool[4];
        SuccessOutBtn.onClick.AddListener(SuccessOut);

    }

    private void PackageBuy(int index)
    {
        int Check_ItemNum = 0;
        int Diamondnum = 0;
        int Goldnum = 0;
        int Mobscroll = 0;
        if(index == 0)
        {
            if(true) // 구입성공
            {
                EasyPackageBuy = true;
                Check_ItemNum = 11;
                Diamondnum = 10000;
                Goldnum = 100000;
                Mobscroll = 5000;
                StartCoroutine(PackageBuySuccess(index, Check_ItemNum, Diamondnum, Goldnum, Mobscroll));
            }
        }
        else if (index == 1)
        {
            if (true) // 구입성공
            {
                NormalPackageBuy = true;

                Check_ItemNum = 11;
                Diamondnum = 20000;
                Goldnum = 500000;
                Mobscroll = 7000;
                StartCoroutine(PackageBuySuccess(index, Check_ItemNum, Diamondnum, Goldnum, Mobscroll));
            }
        }
        else if (index == 2)
        {
            if (true) // 구입성공
            {
                HardPackageBuy = true;

                Check_ItemNum = 11;
                Diamondnum = 40000;
                Goldnum = 2000000;
                Mobscroll = 8000;
                StartCoroutine(PackageBuySuccess(index, Check_ItemNum, Diamondnum, Goldnum, Mobscroll));
            }
        }
        else if (index == 3)
        {
            if (true) // 구입성공
            {
                ExtremePackageBuy = true;

                Check_ItemNum = 11;
                Diamondnum = 80000;
                Goldnum = 10000000;
                Mobscroll = 10000;
                StartCoroutine(PackageBuySuccess(index, Check_ItemNum, Diamondnum, Goldnum, Mobscroll));
            }
        }
    }

    IEnumerator PackageBuySuccess(int index, int checkitemnum, int diamondnum, int goldnum, int mobscroll)
    {
        Debug.Log("실행됐음 패키지 오픈");
        Image button = PackageBuyBtn[index].GetComponent<Image>();
        button.color = ColorManager.ColorChange("기본색");

        Image image = SuccessAni.GetComponent<Image>();
        image.sprite = PackageTitleImg[index].sprite;

        SuccessTitle.SetActive(false);
        SuccessAni.SetActive(true);
        SuccessBuyPanel.SetActive(true);
        SuccessBG.SetActive(false);
        SuccessImg.gameObject.SetActive(false);
        SuccessOutBtn.gameObject.SetActive(false);
        PackageBuyBtn[index].interactable = false;
        PackageBuyCheck[index] = true;

        yield return new WaitForSeconds(1f);
        Debug.Log("1초지남 패키지 오픈");
        
        audioSource.PlayOneShot(btnsoundmanager.BtnSoundClip[1], 1f);
        audioSource.pitch = 2f;

        SuccessText.text = SuccessTextString[index];
        SuccessImg.sprite = SuccessSprite[index];

        SuccessAni.SetActive(false);
        SuccessImg.gameObject.SetActive(true);
        SuccessOutBtn.gameObject.SetActive(true);
        SuccessTitle.SetActive(true);


        for (int i = 0; i < SuccessRewardImg.Length; i++)
        {
            SuccessRewardImg[i].sprite = PackageShopImg[i + 6 * index].sprite;
            SuccessRewardText[i].text = PackageText[i + 6 * index].text;

            SuccessRewardFrame[i].sprite = PackageFrame[i + 6 * index].sprite;
            SuccessRewardFrame[i].color = PackageFrame[i + 6 * index].color;

            RectTransform rectTransform = SuccessRewardFrame[i].rectTransform;
            rectTransform.sizeDelta = new Vector2(130f, 120f);

            Outline[] Soutline = SuccessRewardFrame[i].GetComponentsInChildren<Outline>();
            Outline[] Foutline = PackageFrame[i + 6 * index].GetComponentsInChildren<Outline>();

            Soutline[0].effectColor = Foutline[0].effectColor;
            Soutline[1].effectColor = Foutline[1].effectColor;

        }

        Sprite WeaponselectedSprite = GameManager.itemSprites[checkitemnum];
        Sprite AccessoryselectedSprite = GameManager.AccessorySprites[checkitemnum];

        // Dictionary에서 해당 무기의 카운트 증가
        if (GameManager.itemCounts.ContainsKey(WeaponselectedSprite)) GameManager.itemCounts[WeaponselectedSprite]++;
        else GameManager.itemCounts[WeaponselectedSprite] = 1;

        // Dictionary에서 해당 악세서리의 카운트 증가
        if (GameManager.AccessoryCount.ContainsKey(AccessoryselectedSprite)) GameManager.AccessoryCount[AccessoryselectedSprite]++;
        else GameManager.AccessoryCount[AccessoryselectedSprite] = 1;

        // WeaponDrawn[0]이 true이고, WeaponOwnDamage가 증가하지 않은 경우에만 증가시킴
        if (GameManager.WeaponDrawn[checkitemnum] == true && !GameManager.weaponOwnDamageIncreased[checkitemnum])
        {
            GameManager.WeaponOwnDamage += GameManager.WeaponOwnDmg[checkitemnum];
            GameManager.weaponOwnDamageIncreased[checkitemnum] = true; // WeaponOwnDamage가 증가되었음을 표시
        }

        // AccessoryDrawn[0]이 true이고, AccessoryOwnExperience가 증가하지 않은 경우에만 증가시킴
        if (GameManager.AccessoryDrawn[checkitemnum] == true && !GameManager.AccessoryOwnExpIncreased[checkitemnum])
        {
            GameManager.AccessoryOwnExperience += GameManager.AccessoryOwnExp[checkitemnum];
            GameManager.AccessoryOwnExpIncreased[checkitemnum] = true; // AccessoryOwnDamage가 증가되었음을 표시
        }

        // 악세서리를 뽑았다고 표시
        GameManager.AccessoryDrawn[checkitemnum] = true;

        // 무기를 뽑았다고 표시
        GameManager.WeaponDrawn[checkitemnum] = true;

        GameManager.Player_Money += goldnum;
        GameManager.Player_Diamond += diamondnum;
        GameManager.PlayerHaveMobScroll[index] += mobscroll;



        PackageImg[index].sprite = OpenPackageSprite[index];
        SuccessBG.SetActive(true);

    }

    private void SuccessOut()
    {
        SuccessBuyPanel.SetActive(false);
    }
}
