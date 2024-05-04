using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranscendenceShopManager : MonoBehaviour
{
    public Button[] Trand_Info;
    public Button[] Trand_Buy;
    public Text[] Trand_BuyText;
    public Animator[] TrandEffect;

    public Image[] TrandImg;
    public Image[] TrandFrame;

    public GameObject[] Disable;
    public GameObject TrandInfoPanel;
    public Image TrandInfoImg;
    public Image TrandInfoFrame;
    public Text TrandInfoTitle;
    public Text TrandInfoExplain;
    public Text TrandInfoValue;
    public Button TrandOutBtn;
    public GameObject TrandInfoEffectOutline;
    public Animator TrandInfoEffect;

    Color[] OutlineColor =
    {
        new(152f / 255f, 146f / 255f, 173f / 255f), // #9892AD
        new(235f / 255f, 86f / 255f, 75f / 255f),   // #EB564B
        new(252f / 255f, 180f / 255f, 72f / 255f),  // #FCB448
        new(194f / 255f, 255f / 255f, 73f / 255f),  // #C2FF49
        new(6f / 255f, 120f / 255f, 38f / 255f),    // #067826
        new(156f / 255f, 12f / 255f, 156f / 255f),  // #9C0C9C
        new(103f / 255f, 205f / 255f, 252f / 255f), // #67CDFC
        new(252f / 255f, 20f / 255f, 0f / 255f),    // #FC1400
    };


    private int InTrandInfo = -1;
    private int BuyingMoney;

    private string[] TrandInfoTitleText;
    private string[] TrandInfoTitleExplainText;

    // Start is called before the first frame update
    void Start()
    {
        
        TrandInfoTitleText = new string[Trand_Info.Length]; // 배열 초기화
        TrandInfoTitleExplainText = new string[8]; // 배열 초기화

        for (int i = 0; i < Trand_Info.Length; i++)
        {
            int index = i;

            Trand_Info[index].onClick.AddListener(() => Trand_InfoOpen(index));
            Trand_Buy[index].onClick.AddListener(() => Trand_Buying(index));
        }

        BuyingMoney = 500000000;
        
        TrandOutBtn.onClick.AddListener(Trand_Out);
        
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(UpdateShopItems), 0f, 1f); // 0초 딜레이 후 1초마다 UpdateShopItems() 호출
    }

    private void UpdateShopItems()
    {
        for (int i = 0; i < Trand_Buy.Length; i++)
        {
            int index = i;
            Image buttonImage = Trand_Buy[index].GetComponent<Image>();

            if (GameManager.TrandOwned[index] == true)
            {
                TrandImg[index].color = new Color(1, 1, 1);
                Trand_BuyText[index].text = "획득 완료";
            }
            else
            {
                TrandImg[index].color = new Color(0, 0, 0);
                Trand_BuyText[index].text = "장비 획득";
            }
            if (GameManager.TrandOwned[index] == true) continue;

            if (GameManager.Player_Money >= BuyingMoney)
            {
                buttonImage.color = ColorManager.ColorChange("민트색");
                Trand_BuyText[index].text = "구매 가능";
            }
            else
            {
                buttonImage.color = ColorManager.ColorChange("기본색");
                Trand_BuyText[index].text = "<color=yellow>￦500,000,000</color>";
            }

        }
    }

    private void Trand_InfoOpen(int index)
    {
        TrandInfoTitleText[0] = "<color=#9892AD>고대 나무 망치</color>";
        TrandInfoTitleText[1] = "<color=#EB564B>태양의 수호 방패</color>";
        TrandInfoTitleText[2] = "<color=#FCB448>불멸의 비룡 건틀릿</color>";
        TrandInfoTitleText[3] = "<color=#C2FF49>신성한 조준의 창공</color>";
        TrandInfoTitleText[4] = "<color=#067826>역병의 마처럼</color>";
        TrandInfoTitleText[5] = "<color=#9C0C9C>여신의 축복 곡괭이</color>";
        TrandInfoTitleText[6] = "<color=#67CDFC>얼어붙은 신성한 수호검</color>";
        TrandInfoTitleText[7] = "<color=#FC1400>군주의 전투 망치</color>";

        TrandInfoTitleExplainText[0] = "고대 나무 망치는 고대 신전에서 발견된다.\n" +
                                       "이 무기는 그리프 종족의 전통적인 무기로, 굵은 나무와 단단한 돌로 만들어져 있다.\n" +
                                       "그 견고한 구조는 신들의 힘을 담고 있어서 놀라운 힘과 세기를 발휘한다.\n" +
                                       "전설에 따르면, 이 망치는 세계를 어둠의 위협으로부터 구원할 용맹한 영웅에게만 부여된다.\n" +
                                       "고대 나무 망치를 휘둘러 세상을 수호하는 용사는 그리프 종족과 신들의 축복을 받게 된다.";

        TrandInfoTitleExplainText[1] = "태양의 수호 방패는 고대 신들의 빛을 담은 전설적인 방패이다.\n" +
                                       "그들은 이 방패를 휘둘러 어둠을 물리치고 세상을 밝히는데 헌신했다.\n" +
                                       "황금빛으로 빛나며, 신들의 수호자들에게만 부여되었다.\n" +
                                       "그들은 이 방패를 향해 용맹과 희망을 심어주는 빛을 보았고, 전장에서 끝없는 힘을 발휘했다.\n" +
                                       "태양의 수호 방패는 고대 전쟁의 영광과 신들의 축복을 기억하는 상징으로 남아있다.";

        TrandInfoTitleExplainText[2] = "고대에는 죽지 않는 용처럼 보였던 인간이 불멸의 비룡 건틀릿을 소유했습니다.\n" +
                                       "이 건틀릿은 전설적인 힘을 지니고 있었으며, 용맹한 전사들이 전장에서 그 힘을 발휘했습니다.\n" +
                                       "비룡의 울음과 함께 휘둘러진 이 건틀릿은 영광과 용기를 선사하며,\n" +
                                       "전투의 열기 속에서도 불멸의 영광을 유지했습니다.\n" +
                                       "이제도 그 이름은 영웅들의 노래에 남아, 전설 속의 건틀릿으로 기억되고 있습니다.";

        TrandInfoTitleExplainText[3] = "신성한 조준의 창공은 그린 톤의 빛을 품고 있는 무기로 알려진다.\n" +
                                       "이 빛은 자연의 힘과 생명의 신비를 상징하고 있으며,\n" +
                                       "창공 주변에 피어나는 느낌을 초록빛으로 물들이며, 자연과의 조화와 균형을 상징한다. \n" +
                                       "이 창공은 적에게 투사자의 의지를 강화하고\n" +
                                       "전투 중에 신성한 가호와 보호를 제공하여 신성한 힘의 증거로 작용한다. \n" +
                                       "이러한 창공은 자연의 지혜와 힘을 소유한 자들에게 큰 힘을 부여한다.";

        TrandInfoTitleExplainText[4] = "그 어둠의 군주가 손길을 펼치며, 땅 위에 치명적인 역병을 퍼뜨렸다.\n" +
                                       "그의 마법은 죽음을 부르며, 모든 것을 부패와 파괴로 뒤덮었다.\n" +
                                       "땅은 고통에 몸부림치며 울부짖었고, 그 영향은 끝없는 고통으로 이어졌다.\n" +
                                       "이 끝없는 어둠 속에서, 역병의 마는 그를 따르는 자들에게 죽음의 축복을 안겨주었다.\n" +
                                       "그러나 그의 손에 쥐어진 무기는 단지 파괴와 고통을 낳을 뿐이었다.\n" +
                                       "이는 죽음의 노래를 지르며 쓰러진 이들의 고난스러운 명복이다.";

        TrandInfoTitleExplainText[5] = "빛나는 여신의 손에서 나온 이 곡괭이는 곧 영광과 축복의 상징이다.\n" +
                                       "그 반짝이는 칼날은 영원한 선악을 베어내고, 어둠을 밝히는 빛으로 변신한다.\n" +
                                       "그 빛은 희망을 안겨주며, 어둠을 밝히는 성스러운 불꽃으로서 악의 군중을 물리친다.\n" +
                                       "이 축복받은 곡괭이는 희망의 방패가 되어 이 세상을 지키는 여신의 은총을 드러낸다.\n" +
                                       "그 힘은 무한하며, 그 영광은 지금까지 이어져 온다.";

        TrandInfoTitleExplainText[6] = "한 나라의 제일검이었던 자가 악마와의 전쟁중 죽음을 맞이하였다.\n" +
                                       "그의 뜨거운 신념은 죽지않고 칼에 담겨져 얼어붙은 신성한 힘을 부여했다.\n" +
                                       "이 칼은 죽어서도 잃지 않는 그의 믿음의 상징으로, 어둠의 힘을 억누르며 신성한 빛을 발산한다.\n" +
                                       "그 얼음이 어둠의 존재를 얼어붙이고, 신성한 수호검의 힘이 여전히 세상을 지키는 데 사용된다.\n" +
                                       "이 칼은 지금까지 한 나라를 수호하던 영웅의 정신과 힘이 고스란히 담겨져 있다.";

        TrandInfoTitleExplainText[7] = "옛날, 모든 것을 걸었던 군주가 자신의 힘과 권력을 상징하는 무기로 사용한 전투 망치다.\n" +
                                       "이 망치는 그의 지배력과 힘의 상징으로, 전쟁터에서 그의 무모한 용기와 열정을 나타냈다.\n" +
                                       "그의 왼쪽 손에 들리면, 불타오르는 화염과 강력한 번개의 힘을 발산하여 적을 무너뜨리고\n" +
                                       "그의 오른쪽 손에 들리면, 강력한 굉음과 함께 전장을 휩쓸어 버린다.\n" +
                                       "오늘날에도 이 망치는 군주의 전설을 기억하며 전장에서 두려움을 일으킨다.";


        for (int i = 0; i < Disable.Length; i++)
        {
            int _index = i;

            Disable[_index].SetActive(false);
        }
        Image change_color = TrandInfoEffectOutline.GetComponent<Image>();

        if (GameManager.TrandOwned[index] == true) TrandInfoImg.color = ColorManager.ColorChange("하얀색");
        else TrandInfoImg.color = ColorManager.ColorChange("검정색");

        TrandInfoImg.sprite = TrandImg[index].sprite;
        TrandInfoFrame.sprite = TrandFrame[index].sprite;
        TrandInfoTitle.text = TrandInfoTitleText[index];
        TrandInfoExplain.text = TrandInfoTitleExplainText[index];
        TrandInfoValue.text = "";
        TrandInfoEffect.runtimeAnimatorController = TrandEffect[index].runtimeAnimatorController;
        if (index == 7) change_color.color = Color.red;
        else change_color.color = Color.white;
        Outline[] outlines = TrandInfoEffectOutline.GetComponentsInChildren<Outline>();

        foreach (Outline outline in outlines)
        {
            outline.effectColor = OutlineColor[index];
        }

        InTrandInfo = index;
        TrandInfoPanel.SetActive(true);
    }

    private void Trand_Out()
    {
        for (int i = 0; i < Disable.Length; i++)
        {
            int _index = i;

            Disable[_index].SetActive(true);
        }

        TrandInfoPanel.SetActive(false);
    }

    private void Trand_Buying(int index)
    {
        if (GameManager.Player_Money >= BuyingMoney)
        {
            GameManager.Player_Money -= BuyingMoney;

            Image buttonImage = Trand_Buy[index].GetComponent<Image>();
            buttonImage.color = ColorManager.ColorChange("구매가능");
            Trand_Buy[index].interactable = false;

            TrandImg[index].color = ColorManager.ColorChange("하얀색");

            GameManager.TrandOwned[index] = true;
            Trand_BuyText[index].text = "획득 완료";

            for(int i = 0; i < GameManager.TrandOwned.Length; i++)
            {
                Image _buttonImage = Trand_Buy[i].GetComponent<Image>();
                if (GameManager.TrandOwned[i] == true) continue;

                if (GameManager.Player_Money >= BuyingMoney) _buttonImage.color = ColorManager.ColorChange("구매가능");
                else _buttonImage.color = ColorManager.ColorChange("기본색");
            }
            
        }

    }
}
