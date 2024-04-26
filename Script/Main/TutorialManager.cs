using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   TutorialManager.cs
 * Desc :   튜토리얼의 모든것이 담겨있는 CS
 *         
 *
 & Functions
 &  [Public]
 &  : 
 &  : 
 &
 &  [Protected]
 &  : TutorialBoxUpdate() - 튜토리얼의 MainSystem 조건을 주고받고 확인시킴
 &  : 
 &  : 
 &  : 
 &
 &  [Private]
 &  : 
 *
 */

public static class Tutorialcontents
{
    public static int TutorialNum = 30;
}

public class TutorialManager : MonoBehaviour
{
    public Button TutorialBox;
    public Text TutorialTitle;
    public Text TutorialValue;
    public Text TutorialLevelText;

    public Image CompensationImg;
    public Sprite[] CompensationSprite;

    public GameObject Announce;
    public Text AnnounceTitle;
    public Sprite[] AnnounceSprite;
    public Image AnnounceImg;
    public Image ClearBall;

    private string[] Tutorial_Text;
    private int[] Tutorial_Compensation;

    public static int PlayerTutorialLevel;
    
    private bool isFading = false;
    private float fadeSpeed = 0.25f; // 서서히 사라지는 속도

    public static bool tutorialclear = false;

    private BossManager bossManager;
    private BtnSoundManager btnsoundmanager;

    private void Awake()
    {
        bossManager = BossManager.Instance;
        btnsoundmanager = BtnSoundManager.Instance;
    }


    // Start is called before the first frame update
    void Start()
    {
        TutorialBox.onClick.AddListener(TutorialRun);

        Tutorial_Text = new string[Tutorialcontents.TutorialNum];
        Tutorial_Compensation = new int[Tutorialcontents.TutorialNum];

        PlayerTutorialLevel = 0;
        Tutorial_Text[0] = "<size=25>초급 슬라임 <color=lime>I</color> 클리어</size>";
        Tutorial_Text[1] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.3</color></size>";
        Tutorial_Text[2] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.3</color></size>";
        Tutorial_Text[3] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.3</color></size>";
        Tutorial_Text[4] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.3</color></size>";
        Tutorial_Text[5] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.3</color></size>";
        Tutorial_Text[6] = "<size=25>초급 슬라임 <color=lime>III</color> 클리어</size>";
        Tutorial_Text[7] = "<size=25>무기 <color=yellow><size=28>35</size></color>회 소환 </size>";
        Tutorial_Text[8] = "<size=25>악세서리 <color=yellow><size=28>35</size></color>회 소환 </size>";
        Tutorial_Text[9] = "<size=25>레벨 달성 → <color=yellow>Lv.8</color></size>";
        Tutorial_Text[10] = "<size=25>초급 슬라임 <color=lime>V</color> 클리어</size>";
        Tutorial_Text[11] = "<size=25>유물 강화 <color=yellow><size=28>5</size></color>회 시도</size>";
        Tutorial_Text[12] = "<size=22>골드 증가 업그레이드 → <color=yellow>Lv.10</color></size>";
        Tutorial_Text[13] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.10</color></size>";
        Tutorial_Text[14] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.10</color></size>";
        Tutorial_Text[15] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.10</color></size>";
        Tutorial_Text[16] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.10</color></size>";
        Tutorial_Text[17] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.10</color></size>";
        Tutorial_Text[18] = "<size=25>동네 비둘기 <color=lime>I</color> 클리어</size>";
        Tutorial_Text[19] = "<size=25>동네 비둘기 <color=lime>II</color> 클리어</size>";
        Tutorial_Text[20] = "<size=25>무기 강화 <color=yellow><size=28>5</size></color>회 시도</size>";
        Tutorial_Text[21] = "<size=25>악세서리 강화 <color=yellow><size=28>5</size></color>회 시도</size>";


        Tutorial_Compensation[0] = 50;      // Money
        Tutorial_Compensation[1] = 50;      // Money
        Tutorial_Compensation[2] = 50;      // Money
        Tutorial_Compensation[3] = 50;      // Money
        Tutorial_Compensation[4] = 50;      // Money
        Tutorial_Compensation[5] = 50;      // Money
        Tutorial_Compensation[6] = 1500;    // Diamond
        Tutorial_Compensation[7] = 1500;    // Diamond
        Tutorial_Compensation[8] = 300;     // Money
        Tutorial_Compensation[9] = 300;     // Money
        Tutorial_Compensation[10] = 5;     // ReinforceScroll
        Tutorial_Compensation[11] = 300;     // Money
        Tutorial_Compensation[12] = 300;     // Money
        Tutorial_Compensation[13] = 300;     // Money
        Tutorial_Compensation[14] = 300;     // Money
        Tutorial_Compensation[15] = 300;     // Money
        Tutorial_Compensation[16] = 300;     // Money
        Tutorial_Compensation[17] = 500;     // Money
        Tutorial_Compensation[18] = 500;     // Money
        Tutorial_Compensation[19] = 100;     // RedStone
        Tutorial_Compensation[20] = 100;     // RedStone
        Tutorial_Compensation[21] = 500;     // Money

        TutorialLevelText.text = "Tutorial Level. <color=lime><size=28>" + PlayerTutorialLevel + "</size></color>";
        TutorialTitle.text = Tutorial_Text[PlayerTutorialLevel];
        TutorialValue.text = Tutorial_Compensation[PlayerTutorialLevel] + "";
    }

    private void Update()
    {
        bool conditionMet = false;
        switch (PlayerTutorialLevel)
        {
            case 0:
                conditionMet = GameManager.stageClearDict["EasySlime"][1];
                break;
            case 1:
                conditionMet = GameManager.Player_MoneyUp_DamageLevel >= 3;
                break;
            case 2:
                conditionMet = GameManager.Player_MoneyUp_AttackSpeedLevel >= 3;
                break;
            case 3:
                conditionMet = GameManager.Player_MoneyUp_CriticalChanceLevel >= 3;
                break;
            case 4:
                conditionMet = GameManager.Player_MoneyUp_CriticalDamageLevel >= 3;
                break;
            case 5:
                conditionMet = GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 3;
                break;
            case 6:
                conditionMet = GameManager.stageClearDict["EasySlime"][3];
                break;
            case 7:
                conditionMet = StatisticsManager.ImmutabilityWeaponCount >= 35;
                break;
            case 8:
                conditionMet = StatisticsManager.ImmutabilityAccessoryCount >= 35;
                break;
            case 9:
                conditionMet = GameManager.Player_Level >= 8;
                break;
            case 10:
                conditionMet = GameManager.stageClearDict["EasySlime"][5];
                break;
            case 11:
                conditionMet = StatisticsManager.RelicsTry >= 5;
                break;
            case 12:
                conditionMet = GameManager.Player_MoneyUp_EarnMoneyLevel >= 10;
                break;
            case 13:
                conditionMet = GameManager.Player_MoneyUp_DamageLevel >= 10;
                break;
            case 14:
                conditionMet = GameManager.Player_MoneyUp_AttackSpeedLevel >= 10;
                break;
            case 15:
                conditionMet = GameManager.Player_MoneyUp_CriticalChanceLevel >= 10;
                break;
            case 16:
                conditionMet = GameManager.Player_MoneyUp_CriticalDamageLevel >= 10;
                break;
            case 17:
                conditionMet = GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 10;
                break;
            case 18:
                conditionMet = BossManager.ClearBoss[0];
                break;
            case 19:
                conditionMet = BossManager.ClearBoss[1];
                break;
            case 20:
                conditionMet = StatisticsManager.ImmutabilityWeaponReinforceCount >= 5;
                break;
            case 21:
                conditionMet = StatisticsManager.ImmutabilityAccessoryReinforceCount >= 5;
                break;

            default:
                break;
        }
        SetClearBallColor(conditionMet);
    }
    // ClearBall 색상 설정 함수
    void SetClearBallColor(bool conditionMet)
    {
        ClearBall.color = conditionMet ? ColorManager.ColorChange("초록색") : ColorManager.ColorChange("빨간색");
    }

    private void TutorialRun()
    {
        if (PlayerTutorialLevel == 0)               // 골드강화 슬라임 I 잡기
        {
            AnnounceTitle.text = "초급 슬라임 <color=lime>I</color> 을 잡으셔야 합니다.";
            if (GameManager.stageClearDict["EasySlime"][1] == true)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else StartCoroutine(FadeOut(0));
        }
        else if (PlayerTutorialLevel == 1)          // 골드강화 공격력 Lv.3
        {
            if (GameManager.Player_MoneyUp_DamageLevel >= 3)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 2)          // 골드강화 공격 속도 Lv.3
        {
            if (GameManager.Player_MoneyUp_AttackSpeedLevel >= 3)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 3)          // 골드강화 크리티컬 확률 Lv.3
        {
            if (GameManager.Player_MoneyUp_CriticalChanceLevel >= 3)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 4)          // 골드강화 크리티컬 데미지 Lv.3
        {
            if (GameManager.Player_MoneyUp_CriticalDamageLevel >= 3)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 5)          // 골드강화 방어력 관통 Lv.3
        {
            if (GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 3)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 6)          // 슬라임 III 잡기
        {
            AnnounceTitle.text = "초급 슬라임 <color=lime>III</color> 을 잡으셔야 합니다.";
            if (GameManager.stageClearDict["EasySlime"][3] == true)
            {
                GameManager.Player_Diamond += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else StartCoroutine(FadeOut(0));
        }
        else if (PlayerTutorialLevel == 7)          //  무기 1,500번 뽑기
        {
            if (StatisticsManager.ImmutabilityWeaponCount >= 35)
            {
                GameManager.Player_Diamond += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else ChangeManager.Instance.OnClickChangeBtn(5);
        }
        else if (PlayerTutorialLevel == 8)          // 악세서리 1,500번 뽑기
        {
            if (StatisticsManager.ImmutabilityAccessoryCount >= 35)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else ChangeManager.Instance.OnClickChangeBtn(5);
        }
        else if (PlayerTutorialLevel == 9)          // 레벨 Lv.8 달성
        {
            if (GameManager.Player_Level >= 8)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
        }
        else if (PlayerTutorialLevel == 10)          // 슬라임 V 잡기
        {
            AnnounceTitle.text = "초급 슬라임 <color=lime>V</color> 을 잡으셔야 합니다.";
            if (GameManager.stageClearDict["EasySlime"][5] == true)
            {
                GameManager.RelicsReinforceScroll += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else StartCoroutine(FadeOut(0));
        }
        else if (PlayerTutorialLevel == 11)          // 유물강화 5회시도
        {
            if (StatisticsManager.RelicsTry >= 5)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else ChangeManager.Instance.OnClickChangeBtn(4);
        }

        else if (PlayerTutorialLevel == 12)          // 골드강화 공격력 Lv.10
        {
            if (GameManager.Player_MoneyUp_EarnMoneyLevel >= 10)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 13)          // 골드강화 공격력 Lv.10
        {
            if (GameManager.Player_MoneyUp_DamageLevel >= 10)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 14)          // 골드강화 공격 속도 Lv.10
        {
            if (GameManager.Player_MoneyUp_AttackSpeedLevel >= 10)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 15)          // 골드강화 크리티컬 확률 Lv.10
        {
            if (GameManager.Player_MoneyUp_CriticalChanceLevel >= 10)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 16)          // 골드강화 크리티컬 데미지 Lv.10
        {
            if (GameManager.Player_MoneyUp_CriticalDamageLevel >= 10)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 17)          // 골드강화 방어력 관통 Lv.10
        {
            if (GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 10)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else
            {
                ChangeManager.Instance.OnClickChangeBtn(2);
                BasicCharacterInfo.Instance.CharacherChangePanel(3);
            }
        }
        else if (PlayerTutorialLevel == 18)          // 공원 비둘기 I 잡기
        {
            if (BossManager.ClearBoss[0] == true)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else bossManager.BossListOpen();
        }
        else if (PlayerTutorialLevel == 19)          // 공원 비둘기 II 잡기
        {
            if (BossManager.ClearBoss[1] == true)
            {
                GameManager.Player_RedStone += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else bossManager.BossListOpen();
        }
        else if (PlayerTutorialLevel == 20)          // 무기 강화 5회
        {
            if (StatisticsManager.ImmutabilityWeaponReinforceCount >= 5)
            {
                GameManager.Player_RedStone += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else ChangeManager.Instance.OnClickChangeBtn(3);
        }
        else if (PlayerTutorialLevel == 21)          // 악세서리 강화 5회
        {
            if (StatisticsManager.ImmutabilityWeaponReinforceCount >= 5)
            {
                GameManager.Player_Money += Tutorial_Compensation[PlayerTutorialLevel];
                TutorialSuccess();
            }
            else ChangeManager.Instance.OnClickChangeBtn(3);
        }
        TutorialBoxUpdate();
    }

    private void TutorialSuccess()
    {
        tutorialclear = true;
        PlayerTutorialLevel++;
        AchievementManager.ClearTutorialValue++;
        btnsoundmanager.TutorialSuccessPlay();
        tutorialclear = false;
    }

    // 텍스트 업데이트
    private void TutorialBoxUpdate()
    {
        TutorialTitle.text = Tutorial_Text[PlayerTutorialLevel];
        TutorialValue.text = TextFormatter.GetThousandCommaText(Tutorial_Compensation[PlayerTutorialLevel]) + "";
        CompensationImg.sprite = CompensationSprite[PlayerTutorialLevel];
        TutorialLevelText.text = "Tutorial Level. <color=lime><size=28>" + PlayerTutorialLevel + "</size></color>";
    }

    // 메세지 띄우기
    IEnumerator FadeOut(int index)
    {
        Announce.SetActive(true);
        AnnounceImg.sprite = AnnounceSprite[index];
        
        Image an = Announce.GetComponent<Image>();
        Vector2 PosAn = Announce.transform.position;
        isFading = true;
        float alpha = 150f / 255f; // 최대 알파값
        TutorialBox.interactable = false;
        while (alpha > 50f / 255f)
        {
            alpha -= fadeSpeed * Time.deltaTime; // 알파값 감소
            Color newColor = an.color;
            newColor.a = alpha; // 새로운 알파값 설정
            an.color = newColor; // 패널의 색상 업데이트
            Announce.transform.Translate(new Vector2(0, 1) * Time.deltaTime);
            yield return null;
        }
        Announce.transform.position = PosAn;
        TutorialBox.interactable = true;

        // 알파값이 0 이하가 되면 패널 비활성화
        Announce.SetActive(false);
        isFading = false; // 페이드 아웃 종료
    }

}
