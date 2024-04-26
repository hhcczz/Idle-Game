using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDiamondBuySystem : MonoBehaviour
{
    private AudioSource audioSource; // AudioSource 변수 추가

    public Button[] DiamondBuyBtn;
    public Button[] GoldBuyBtn;

    public Image SellDiamondImg;
    public Image[] SellGoldImg;

    public Image SuccessImg;
    public Image SuccessBox;
    public Text SuccessImgText;
    public Text SuccessTitle;
    public Text SuccessValue;

    public GameObject SuccessPanel;

    public Button SuccessOutBtn;

    private BtnSoundManager btnsoundmanager;

    private int[] GoldValue;
    private int[] DiamondValue;

    private string[] SuccessTitleString;
    private string[] SuccessImgDiamondTextString;
    private string[] SuccessImgGoldTextString;

    // Start is called before the first frame update
    void Start()
    {
        btnsoundmanager = BtnSoundManager.Instance;

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        SuccessOutBtn.onClick.AddListener(SuccessOut);

        for (int i = 0; i < DiamondBuyBtn.Length; i++)
        {
            int index = i;

            DiamondBuyBtn[index].onClick.AddListener(() => BuyDiamond(index));
            GoldBuyBtn[index].onClick.AddListener(() => BuyGold(index));
        }

        GoldValue = new int[6]
        {
            100000,
            300000,
            1500000,
            4500000,
            9000000,
            20000000,
        };

        DiamondValue = new int[6]
        {
            3000,
            9000,
            27000,
            80000,
            150000,
            400000,
        };
        SuccessTitleString = new string[2]
        {
            "<color=cyan>다이아몬드</color> 구매 완료!",
            "<color=yellow>골드</color> 구매 완료!",
        };

        SuccessImgDiamondTextString = new string[6]
        {
            "<color=cyan>다이아몬드</color> 조각",
            "<color=cyan>다이아몬드</color> 뭉치",
            "<color=cyan>다이아몬드</color> 묶음",
            "<color=cyan>다이아몬드</color> 바구니",
            "<color=cyan>다이아몬드</color> 상자",
            "<color=cyan>다이아몬드</color> 폭탄",
        };
        SuccessImgGoldTextString = new string[6]
        {
            "<color=yellow>골드</color> 조각",
            "<color=yellow>골드</color> 뭉치",
            "<color=yellow>골드</color> 묶음",
            "<color=yellow>골드</color> 바구니",
            "<color=yellow>골드</color> 상자",
            "<color=yellow>골드</color> 폭탄",
        };


    }

    private void SuccessOut()
    {
        SuccessPanel.SetActive(false);
    }

    private void BuyDiamond(int index)
    {
        if (true) // 구매 성공시
        {
            GiveDiamond(index, DiamondValue[index]);
        }
    }

    private void GiveDiamond(int index, int diamond)
    {
        SuccessTitle.text = SuccessTitleString[0];
        SuccessImgText.text = SuccessImgDiamondTextString[index];
        SuccessValue.text = TextFormatter.GetThousandCommaText(DiamondValue[index]);
        SuccessBox.color = ColorManager.ColorChange("하늘색");
        SuccessImg.sprite = SellDiamondImg.sprite;

        RectTransform rectTransform = SuccessImg.rectTransform;
        rectTransform.sizeDelta = new Vector2(205f, 187f);

        SuccessPanel.SetActive(true);
        audioSource.PlayOneShot(btnsoundmanager.BtnSoundClip[1], 1f);
        audioSource.pitch = 2f;

        GameManager.Player_Diamond += diamond;
    }

    private void GiveGold(int index, int gold)
    {

        SuccessTitle.text = SuccessTitleString[1];
        SuccessImgText.text = SuccessImgGoldTextString[index];
        SuccessValue.text = TextFormatter.GetThousandCommaText(GoldValue[index]);
        SuccessBox.color = ColorManager.ColorChange("노랑색");
        SuccessImg.sprite = SellGoldImg[index].sprite;

        RectTransform rectTransform = SuccessImg.rectTransform;
        rectTransform.sizeDelta = new Vector2(205f, 187f);

        SuccessPanel.SetActive(true);
        audioSource.PlayOneShot(btnsoundmanager.BtnSoundClip[1], 1f);
        audioSource.pitch = 2f;

        GameManager.Player_Money += gold;
    }

    private void BuyGold(int index)
    {
        if (true) // 구매성공시
        {
            GiveGold(index, GoldValue[index]);
        }
    }
}
