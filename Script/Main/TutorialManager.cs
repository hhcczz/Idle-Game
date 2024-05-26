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
 &  :
 &  :
 &  [Private]
 &  : TutorialBoxUpdate() - 튜토리얼의 MainSystem 조건을 주고받고 확인시킴
 &  : FadeOut(int index) - Fade Out 시켜줌
 &  : TutorialBoxUpdate() - 텍스트 업데이트
 &  : TutorialSuccess() - 튜토리얼 클리어
 &  : GiveReward(string Name, int index) - 스테이지별로 체크해서 골드, 강화스크롤, 다이아몬드, 붉은보석 체크해서 지급
 &  : GiveRewardBasedOnLevel(int level) - 스테이지별로 어떻게 줄지 결정
 &  : HandleTutorialFailure(int level) - 튜토리얼 실패 관리
 &  : CheckCondition(int level) - 튜토리얼 클리어 조건
 &  : TutorialRun() - 튜토리얼 실행
 &  : SetClearBallColor(bool conditionMet) - 튜토리얼 클리어 여부 확인 / 성공 : 초록 / 실패 : 빨간색
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
        Tutorial_Text[22] = "<size=25>유물 강화 <color=yellow><size=28>10</size></color>회 시도</size>";
        Tutorial_Text[23] = "<size=25>초급 슬라임 <color=lime>X</color> 클리어</size>";
        Tutorial_Text[24] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.30</color></size>";
        Tutorial_Text[25] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.30</color></size>";
        Tutorial_Text[26] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.30</color></size>";
        Tutorial_Text[27] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.30</color></size>";
        Tutorial_Text[28] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.30</color></size>";
        Tutorial_Text[29] = "<size=25>초급 슬라임 <color=lime>XV</color> 클리어</size>";
        Tutorial_Text[30] = "<size=25>무기 <color=yellow><size=28>70</size></color>회 소환 </size>";
        Tutorial_Text[31] = "<size=25>악세서리 <color=yellow><size=28>70</size></color>회 소환 </size>";
        Tutorial_Text[32] = "<size=25>레벨 달성 → <color=yellow>Lv.30</color></size>";
        Tutorial_Text[33] = "<size=25>초급 슬라임 <color=lime>BOSS</color> 클리어</size>";
        Tutorial_Text[34] = "<size=25>동네 비둘기 <color=lime>III</color> 클리어</size>";
        Tutorial_Text[35] = "<size=25>동네 비둘기 <color=lime>IV</color> 클리어</size>";
        Tutorial_Text[36] = "<size=22>골드 증가 업그레이드 → <color=yellow>Lv.50</color></size>";
        Tutorial_Text[37] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.50</color></size>";
        Tutorial_Text[38] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.50</color></size>";
        Tutorial_Text[39] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.50</color></size>";
        Tutorial_Text[40] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.50</color></size>";
        Tutorial_Text[41] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.50</color></size>";
        Tutorial_Text[42] = "<size=27>광고 <color=lime>3회</color> 시청</size>";
        Tutorial_Text[43] = "<size=25>무기 강화 <color=yellow><size=28>15</size></color>회 시도</size>";
        Tutorial_Text[44] = "<size=25>악세서리 강화 <color=yellow><size=28>15</size></color>회 시도</size>";
        Tutorial_Text[45] = "<size=25>동네 비둘기 <color=lime>V</color> 클리어</size>";
        Tutorial_Text[46] = "<size=25>동네 비둘기 <color=lime>VI</color> 클리어</size>";
        Tutorial_Text[47] = "<size=25>레벨 달성 → <color=yellow>Lv.40</color></size>";
        Tutorial_Text[48] = "<size=25>중급 슬라임 <color=lime>V</color> 클리어</size>";
        Tutorial_Text[49] = "<size=25>유물 강화 <color=yellow><size=28>15</size></color>회 시도</size>";
        Tutorial_Text[50] = "<size=22>골드 증가 업그레이드 → <color=yellow>Lv.70</color></size>";
        Tutorial_Text[51] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.70</color></size>";
        Tutorial_Text[52] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.70</color></size>";
        Tutorial_Text[53] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.70</color></size>";
        Tutorial_Text[54] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.70</color></size>";
        Tutorial_Text[55] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.70</color></size>";
        Tutorial_Text[56] = "<size=25>무기 <color=yellow><size=28>105</size></color>회 소환 </size>";
        Tutorial_Text[57] = "<size=25>악세서리 <color=yellow><size=28>105</size></color>회 소환 </size>";
        Tutorial_Text[58] = "<size=27>광고 <color=lime>6회</color> 시청</size>";
        Tutorial_Text[59] = "<size=25>무기 강화 <color=yellow><size=28>30</size></color>회 시도</size>";
        Tutorial_Text[60] = "<size=25>악세서리 강화 <color=yellow><size=28>30</size></color>회 시도</size>";
        Tutorial_Text[61] = "<size=25>동네 비둘기 <color=lime>VII</color> 클리어</size>";
        Tutorial_Text[62] = "<size=25>동네 비둘기 <color=lime>VIII</color> 클리어</size>";
        Tutorial_Text[63] = "<size=25>레벨 달성 → <color=yellow>Lv.60</color></size>";
        Tutorial_Text[64] = "<size=25>중급 슬라임 <color=lime>X</color> 클리어</size>";
        Tutorial_Text[65] = "<size=25>유물 강화 <color=yellow><size=28>20</size></color>회 시도</size>";
        Tutorial_Text[66] = "<size=22>골드 증가 업그레이드 → <color=yellow>Lv.100</color></size>";
        Tutorial_Text[67] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.100</color></size>";
        Tutorial_Text[68] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.100</color></size>";
        Tutorial_Text[69] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.100</color></size>";
        Tutorial_Text[70] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.100</color></size>";
        Tutorial_Text[71] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.100</color></size>";
        Tutorial_Text[72] = "<size=25>무기 <color=yellow><size=28>140</size></color>회 소환 </size>";
        Tutorial_Text[73] = "<size=25>악세서리 <color=yellow><size=28>140</size></color>회 소환 </size>";
        Tutorial_Text[74] = "<size=25>무기 강화 <color=yellow><size=28>45</size></color>회 시도</size>";
        Tutorial_Text[75] = "<size=25>악세서리 강화 <color=yellow><size=28>45</size></color>회 시도</size>";
        Tutorial_Text[76] = "<size=25>동네 비둘기 <color=lime>IX</color> 클리어</size>";
        Tutorial_Text[77] = "<size=25>동네 비둘기 <color=lime>X</color> 클리어</size>";
        Tutorial_Text[78] = "<size=25>레벨 달성 → <color=yellow>Lv.80</color></size>";
        Tutorial_Text[79] = "<size=25>중급 슬라임 <color=lime>XV</color> 클리어</size>";
        Tutorial_Text[80] = "<size=25>유물 강화 <color=yellow><size=28>25</size></color>회 시도</size>";
        Tutorial_Text[81] = "<size=22>골드 증가 업그레이드 → <color=yellow>Lv.130</color></size>";
        Tutorial_Text[82] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.130</color></size>";
        Tutorial_Text[83] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.130</color></size>";
        Tutorial_Text[84] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.130</color></size>";
        Tutorial_Text[85] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.130</color></size>";
        Tutorial_Text[86] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.130</color></size>";
        Tutorial_Text[87] = "<size=25>무기 <color=yellow><size=28>175</size></color>회 소환 </size>";
        Tutorial_Text[88] = "<size=25>악세서리 <color=yellow><size=28>175</size></color>회 소환 </size>";
        Tutorial_Text[89] = "<size=27>광고 <color=lime>9회</color> 시청</size>";
        Tutorial_Text[90] = "<size=25>무기 강화 <color=yellow><size=28>60</size></color>회 시도</size>";
        Tutorial_Text[91] = "<size=25>악세서리 강화 <color=yellow><size=28>60</size></color>회 시도</size>";
        Tutorial_Text[92] = "<size=25>동네 비둘기 <color=lime>XI</color> 클리어</size>";
        Tutorial_Text[93] = "<size=25>동네 비둘기 <color=lime>XII</color> 클리어</size>";
        Tutorial_Text[94] = "<size=25>레벨 달성 → <color=yellow>Lv.100</color></size>";
        Tutorial_Text[95] = "<size=25>중급 슬라임 <color=lime>BOSS</color> 클리어</size>";
        Tutorial_Text[96] = "<size=25>유물 강화 <color=yellow><size=28>30</size></color>회 시도</size>";
        Tutorial_Text[97] = "<size=22>골드 증가 업그레이드 → <color=yellow>Lv.160</color></size>";
        Tutorial_Text[98] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.160</color></size>";
        Tutorial_Text[99] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.160</color></size>";
        Tutorial_Text[100] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.160</color></size>";
        Tutorial_Text[101] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.160</color></size>";
        Tutorial_Text[102] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.160</color></size>";
        Tutorial_Text[103] = "<size=25>무기 <color=yellow><size=28>210</size></color>회 소환 </size>";
        Tutorial_Text[104] = "<size=25>악세서리 <color=yellow><size=28>210</size></color>회 소환 </size>";
        Tutorial_Text[105] = "<size=25>무기 강화 <color=yellow><size=28>75</size></color>회 시도</size>";
        Tutorial_Text[106] = "<size=25>악세서리 강화 <color=yellow><size=28>75</size></color>회 시도</size>";
        Tutorial_Text[107] = "<size=25>동네 비둘기 <color=lime>XIII</color> 클리어</size>";
        Tutorial_Text[108] = "<size=25>동네 비둘기 <color=lime>XIV</color> 클리어</size>";
        Tutorial_Text[109] = "<size=25>레벨 달성 → <color=yellow>Lv.150</color></size>";
        Tutorial_Text[110] = "<size=25>상급 슬라임 <color=lime>V</color> 클리어</size>";
        Tutorial_Text[111] = "<size=25>유물 강화 <color=yellow><size=28>35</size></color>회 시도</size>";
        Tutorial_Text[112] = "<size=22>골드 증가 업그레이드 → <color=yellow>Lv.200</color></size>";
        Tutorial_Text[113] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.200</color></size>";
        Tutorial_Text[114] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.200</color></size>";
        Tutorial_Text[115] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.200</color></size>";
        Tutorial_Text[116] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.200</color></size>";
        Tutorial_Text[117] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.200</color></size>";
        Tutorial_Text[118] = "<size=25>무기 <color=yellow><size=28>245</size></color>회 소환 </size>";
        Tutorial_Text[119] = "<size=25>악세서리 <color=yellow><size=28>245</size></color>회 소환 </size>";
        Tutorial_Text[120] = "<size=25>무기 강화 <color=yellow><size=28>100</size></color>회 시도</size>";
        Tutorial_Text[121] = "<size=25>악세서리 강화 <color=yellow><size=28>100</size></color>회 시도</size>";
        Tutorial_Text[122] = "<size=25>동네 비둘기 <color=lime>XV</color> 클리어</size>";
        Tutorial_Text[123] = "<size=25>동네 비둘기 <color=lime>XVI</color> 클리어</size>";
        Tutorial_Text[124] = "<size=25>레벨 달성 → <color=yellow>Lv.200</color></size>";
        Tutorial_Text[125] = "<size=25>상급 슬라임 <color=lime>X</color> 클리어</size>";
        Tutorial_Text[126] = "<size=25>유물 강화 <color=yellow><size=28>40</size></color>회 시도</size>";
        Tutorial_Text[127] = "<size=22>골드 증가 업그레이드 → <color=yellow>Lv.300</color></size>";
        Tutorial_Text[128] = "<size=22>공격력 업그레이드 → <color=yellow>Lv.300</color></size>";
        Tutorial_Text[129] = "<size=22>공격속도 업그레이드 → <color=yellow>Lv.300</color></size>";
        Tutorial_Text[130] = "<size=22>크리티컬 확률 업그레이드 → <color=yellow>Lv.300</color></size>";
        Tutorial_Text[131] = "<size=22>크리티컬 데미지 업그레이드 → <color=yellow>Lv.300</color></size>";
        Tutorial_Text[132] = "<size=22>방어력 관통 업그레이드 → <color=yellow>Lv.300</color></size>";
        Tutorial_Text[133] = "<size=25>무기 <color=yellow><size=28>280</size></color>회 소환 </size>";
        Tutorial_Text[134] = "<size=25>악세서리 <color=yellow><size=28>280</size></color>회 소환 </size>";
        Tutorial_Text[135] = "<size=25>무기 강화 <color=yellow><size=28>150</size></color>회 시도</size>";
        Tutorial_Text[136] = "<size=25>악세서리 강화 <color=yellow><size=28>150</size></color>회 시도</size>";
        Tutorial_Text[137] = "<size=25>동네 비둘기 <color=lime>XVII</color> 클리어</size>";
        Tutorial_Text[138] = "<size=25>동네 비둘기 <color=lime>XVIII</color> 클리어</size>";
        Tutorial_Text[139] = "<size=25>레벨 달성 → <color=yellow>Lv.300</color></size>";
        Tutorial_Text[140] = "<size=25>상급 슬라임 <color=lime>XV</color> 클리어</size>";

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
        Tutorial_Compensation[21] = 5;     // ReinforceScroll

        TutorialLevelText.text = "Tutorial Level. <color=lime><size=28>" + PlayerTutorialLevel + "</size></color>";
        TutorialTitle.text = Tutorial_Text[PlayerTutorialLevel];
        TutorialValue.text = Tutorial_Compensation[PlayerTutorialLevel] + "";
    }

    private void Update()
    {
        bool conditionMet = CheckCondition(PlayerTutorialLevel);
        SetClearBallColor(conditionMet);
    }
    // ClearBall 색상 설정 함수
    private void SetClearBallColor(bool conditionMet)
    {
        ClearBall.color = conditionMet ? ColorManager.ColorChange("초록색") : ColorManager.ColorChange("빨간색");
    }

    private void TutorialRun()
    {
        bool conditionMet = CheckCondition(PlayerTutorialLevel);
        if (conditionMet)
        {
            GiveRewardBasedOnLevel(PlayerTutorialLevel);
            TutorialSuccess();
        }
        else
        {
            HandleTutorialFailure(PlayerTutorialLevel);
        }
        TutorialBoxUpdate();
    }

    private bool CheckCondition(int level)
    {
        switch (level)
        {
            case 0:
                return GameManager.stageClearDict["EasySlime"][1];
            case 1:
                return GameManager.Player_MoneyUp_DamageLevel >= 3;
            case 2:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 3;
            case 3:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 3;
            case 4:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 3;
            case 5:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 3;
            case 6:
                return GameManager.stageClearDict["EasySlime"][3];
            case 7:
                return StatisticsManager.ImmutabilityWeaponCount >= 35;
            case 8:
                return StatisticsManager.ImmutabilityAccessoryCount >= 35;
            case 9:
                return GameManager.Player_Level >= 8;
            case 10:
                return GameManager.stageClearDict["EasySlime"][5];
            case 11:
                return StatisticsManager.RelicsTry >= 5;
            case 12:
                return GameManager.Player_MoneyUp_EarnMoneyLevel >= 10;
            case 13:
                return GameManager.Player_MoneyUp_DamageLevel >= 10;
            case 14:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 10;
            case 15:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 10;
            case 16:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 10;
            case 17:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 10;
            case 18:
                return BossManager.ClearBoss[0];
            case 19:
                return BossManager.ClearBoss[1];
            case 20:
                return StatisticsManager.ImmutabilityWeaponReinforceCount >= 5;
            case 21:
                return StatisticsManager.ImmutabilityAccessoryReinforceCount >= 5;
            case 22:
                return StatisticsManager.RelicsTry >= 10;
            case 23:
                return GameManager.stageClearDict["EasySlime"][10];
            case 24:
                return GameManager.Player_MoneyUp_DamageLevel >= 30;
            case 25:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 30;
            case 26:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 30;
            case 27:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 30;
            case 28:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 30;
            case 29:
                return GameManager.stageClearDict["EasySlime"][15];
            case 30:
                return StatisticsManager.ImmutabilityWeaponCount >= 70;
            case 31:
                return StatisticsManager.ImmutabilityAccessoryCount >= 70;
            case 32:
                return GameManager.Player_Level >= 30;
            case 33:
                return GameManager.stageClearDict["NormalSlime"][0];
            case 34:
                return BossManager.ClearBoss[2];
            case 35:
                return BossManager.ClearBoss[3];
            case 36:
                return GameManager.Player_MoneyUp_EarnMoneyLevel >= 50;
            case 37:
                return GameManager.Player_MoneyUp_DamageLevel >= 50;
            case 38:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 50;
            case 39:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 50;
            case 40:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 50;
            case 41:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 50; // 여기서부터
            case 42:
                return StatisticsManager.ImmutabilityMainAdCount >= 3;
            case 43:
                return StatisticsManager.ImmutabilityWeaponReinforceCount >= 15;
            case 44:
                return StatisticsManager.ImmutabilityAccessoryReinforceCount >= 15;
            case 45:
                return BossManager.ClearBoss[4];
            case 46:
                return BossManager.ClearBoss[5];
            case 47:
                return GameManager.Player_Level >= 40;
            case 48:
                return GameManager.stageClearDict["NormalSlime"][5];
            case 49:
                return StatisticsManager.RelicsTry >= 15;
            case 50:
                return GameManager.Player_MoneyUp_EarnMoneyLevel >= 70;
            case 51:
                return GameManager.Player_MoneyUp_DamageLevel >= 70;
            case 52:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 70;
            case 53:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 70;
            case 54:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 70;
            case 55:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 50;
            case 56:
                return StatisticsManager.ImmutabilityWeaponCount >= 105;
            case 57:
                return StatisticsManager.ImmutabilityAccessoryCount >= 105;
            case 58:
                return StatisticsManager.ImmutabilityMainAdCount >= 6;
            case 59:
                return StatisticsManager.ImmutabilityWeaponReinforceCount >= 30;
            case 60:
                return StatisticsManager.ImmutabilityAccessoryReinforceCount >= 30;
            case 61:
                return BossManager.ClearBoss[6];
            case 62:
                return BossManager.ClearBoss[7];
            case 63:
                return GameManager.Player_Level >= 60;
            case 64:
                return GameManager.stageClearDict["NormalSlime"][10];
            case 65:
                return StatisticsManager.RelicsTry >= 20;
            case 66:
                return GameManager.Player_MoneyUp_EarnMoneyLevel >= 100;
            case 67:
                return GameManager.Player_MoneyUp_DamageLevel >= 100;
            case 68:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 100;
            case 69:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 100;
            case 70:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 100;
            case 71:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 100;
            case 72:
                return StatisticsManager.ImmutabilityWeaponCount >= 140;
            case 73:
                return StatisticsManager.ImmutabilityAccessoryCount >= 140;
            case 74:
                return StatisticsManager.ImmutabilityWeaponReinforceCount >= 45;
            case 75:
                return StatisticsManager.ImmutabilityAccessoryReinforceCount >= 45;
            case 76:
                return BossManager.ClearBoss[8];
            case 77:
                return BossManager.ClearBoss[9];
            case 78:
                return GameManager.Player_Level >= 80;
            case 79:
                return GameManager.stageClearDict["NormalSlime"][15];
            case 80:
                return StatisticsManager.RelicsTry >= 25;
            case 81:
                return GameManager.Player_MoneyUp_EarnMoneyLevel >= 130;
            case 82:
                return GameManager.Player_MoneyUp_DamageLevel >= 130;
            case 83:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 130;
            case 84:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 130;
            case 85:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 130;
            case 86:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 130;
            case 87:
                return StatisticsManager.ImmutabilityWeaponCount >= 175;
            case 88:
                return StatisticsManager.ImmutabilityAccessoryCount >= 175;
            case 89:
                return StatisticsManager.ImmutabilityMainAdCount >= 9;
            case 90:
                return StatisticsManager.ImmutabilityWeaponReinforceCount >= 60;
            case 91:
                return StatisticsManager.ImmutabilityAccessoryReinforceCount >= 60;
            case 92:
                return BossManager.ClearBoss[10];
            case 93:
                return BossManager.ClearBoss[11];
            case 94:
                return GameManager.Player_Level >= 100;
            case 95:
                return GameManager.stageClearDict["HardSlime"][0];
            case 96:
                return StatisticsManager.RelicsTry >= 30;
            case 97:
                return GameManager.Player_MoneyUp_EarnMoneyLevel >= 160;
            case 98:
                return GameManager.Player_MoneyUp_DamageLevel >= 160;
            case 99:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 160;
            case 100:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 160;
            case 101:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 160;
            case 102:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 160;
            case 103:
                return StatisticsManager.ImmutabilityWeaponCount >= 210;
            case 104:
                return StatisticsManager.ImmutabilityAccessoryCount >= 210;
            case 105:
                return StatisticsManager.ImmutabilityWeaponReinforceCount >= 75;
            case 106:
                return StatisticsManager.ImmutabilityAccessoryReinforceCount >= 75;
            case 107:
                return BossManager.ClearBoss[12];
            case 108:
                return BossManager.ClearBoss[13];
            case 109:
                return GameManager.Player_Level >= 150;
            case 110:
                return GameManager.stageClearDict["HardSlime"][5];
            case 111:
                return StatisticsManager.RelicsTry >= 35;
            case 112:
                return GameManager.Player_MoneyUp_EarnMoneyLevel >= 200;
            case 113:
                return GameManager.Player_MoneyUp_DamageLevel >= 200;
            case 114:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 200;
            case 115:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 200;
            case 116:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 200;
            case 117:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 200;
            case 118:
                return StatisticsManager.ImmutabilityWeaponCount >= 245;
            case 119:
                return StatisticsManager.ImmutabilityAccessoryCount >= 245;
            case 120:
                return StatisticsManager.ImmutabilityWeaponReinforceCount >= 100;
            case 121:
                return StatisticsManager.ImmutabilityAccessoryReinforceCount >= 100;
            case 122:
                return BossManager.ClearBoss[14];
            case 123:
                return BossManager.ClearBoss[15];
            case 124:
                return GameManager.Player_Level >= 200;
            case 125:
                return GameManager.stageClearDict["HardSlime"][10];
            case 126:
                return StatisticsManager.RelicsTry >= 40;
            case 127:
                return GameManager.Player_MoneyUp_EarnMoneyLevel >= 300;
            case 128:
                return GameManager.Player_MoneyUp_DamageLevel >= 300;
            case 129:
                return GameManager.Player_MoneyUp_AttackSpeedLevel >= 300;
            case 130:
                return GameManager.Player_MoneyUp_CriticalChanceLevel >= 300;
            case 131:
                return GameManager.Player_MoneyUp_CriticalDamageLevel >= 300;
            case 132:
                return GameManager.Player_MoneyUp_ArmorPenetrationLevel >= 300;
            case 133:
                return StatisticsManager.ImmutabilityWeaponCount >= 280;
            case 134:
                return StatisticsManager.ImmutabilityAccessoryCount >= 280;
            default:
                return false;
        }
    }
    private void HandleTutorialFailure(int level)
    {
        switch (level)
        {
            // 슬라임 잡기
            case 0:
                AnnounceTitle.text = "초급 슬라임 <color=lime>I</color>을 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;

            case 6:
                AnnounceTitle.text = "초급 슬라임 <color=lime>III</color>을 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;

            case 10:
                AnnounceTitle.text = "초급 슬라임 <color=lime>V</color>을 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;

            case 23:
                AnnounceTitle.text = "초급 슬라임 <color=lime>X</color>을 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;

            case 29:
                AnnounceTitle.text = "초급 슬라임 <color=lime>XV</color>을 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;
            case 33:
                AnnounceTitle.text = "초급 슬라임 <color=lime>BOSS</color>를 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;
            case 48:
                AnnounceTitle.text = "중급 슬라임 <color=lime>V</color>를 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;
            case 64:
                AnnounceTitle.text = "중급 슬라임 <color=lime>X</color>를 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;
            case 79:
                AnnounceTitle.text = "중급 슬라임 <color=lime>XV</color>를 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;
            case 95:
                AnnounceTitle.text = "중급 슬라임 <color=lime>BOSS</color>를 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;
            case 110:
                AnnounceTitle.text = "상급 슬라임 <color=lime>V</color>를 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;
            case 125:
                AnnounceTitle.text = "상급 슬라임 <color=lime>X</color>를 클리어 해야합니다.";
                StartCoroutine(FadeOut(0));
                break;

            // 골드 강화 
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
            case 36:
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
            case 50:
            case 51:
            case 52:
            case 53:
            case 54:
            case 55:
            case 66:
            case 67:
            case 68:
            case 69:
            case 70:
            case 71:
            case 81:
            case 82:
            case 83:
            case 84:
            case 85:
            case 86:
            case 97:
            case 98:
            case 99:
            case 100:
            case 101:
            case 102:
            case 112:
            case 113:
            case 114:
            case 115:
            case 116:
            case 117:
            case 127:
            case 128:
            case 129:
            case 130:
            case 131:
            case 132:
                ChangeManager.Instance.OnClickChangeBtn(2);
                CharacterPanelChange.Instance.CharacherChangePanel(3);
                break;

            // 상점 무기, 악세서리 뽑기
            case 7:
            case 8:
            case 30:
            case 31:
            case 56:
            case 57:
            case 72:
            case 73:
            case 87:
            case 88:
            case 103:
            case 104:
            case 118:
            case 119:
            case 133:
            case 134:
                ChangeManager.Instance.OnClickChangeBtn(5);
                break;

            // 유물 강화하기
            case 11:
            case 22:
            case 49:
            case 65:
            case 80:
            case 96:
            case 111:
            case 126:
                ChangeManager.Instance.OnClickChangeBtn(4);
                break;

            // 보스 잡기
            case 18:
            case 19:
            case 34:
            case 35:
            case 45:
            case 46:
            case 61:
            case 62:
            case 76:
            case 77:
            case 92:
            case 93:
            case 107:
            case 108:
            case 122:
            case 123:
                bossManager.BossListOpen();
                break;

            // 광고 보기
            case 42:
            case 58:
            case 89:
                AdManager.Instance.AdOpen();
                break;

            // 무기, 악세서리 강화
            case 20:
            case 21:
            case 43:
            case 44:
            case 59:
            case 60:
            case 74:
            case 75:
            case 90:
            case 91:
            case 105:
            case 106:
            case 120:
            case 121:
                ChangeManager.Instance.OnClickChangeBtn(3);
                break;

            default:
                // Handle other levels if necessary
                break;
        }
    }
    private void GiveRewardBasedOnLevel(int level)
    {
        switch (level)
        {
            // 골드
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 8:
            case 9:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
            case 28:
            case 31:
            case 32:
            case 33:
            case 34:
            case 35:
            case 36:
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
            case 44:
            case 45:
            case 46:
            case 47:
            case 49:
            case 50:
            case 51:
            case 52:
            case 53:
            case 54:
            case 57:
            case 60:
            case 61:
            case 62:
            case 63:
            case 65:
            case 66:
            case 67:
            case 68:
            case 69:
            case 70:
            case 75:
            case 76:
            case 77:
            case 78:
            case 80:
            case 81:
            case 82:
            case 83:
            case 84:
            case 85:
            case 88:
            case 91:
            case 92:
            case 93:
            case 94:
            case 96:
            case 97:
            case 98:
            case 99:
            case 100:
            case 101:
            case 106:
            case 107:
            case 108:
            case 109:
            case 111:
            case 112:
            case 113:
            case 114:
            case 115:
            case 116:
            case 121:
            case 122:
            case 123:
            case 124:
            case 126:
            case 127:
            case 128:
            case 129:
            case 130:
            case 131:
            case 134:
                GiveReward("골드", level);
                break;

            // 강화스크롤
            case 10:
            case 21:
            case 48:
            case 64:
            case 79:
            case 95:
            case 110:
            case 125:
                GiveReward("강화스크롤", level);
                break;

            // 붉은 보석
            case 19:
            case 20:
            case 42:
            case 43:
            case 58:
            case 59:
            case 73:
            case 74:
            case 89:
            case 90:
            case 104:
            case 105:
            case 119:
            case 120:
                GiveReward("붉은보석", level);
                break;

            // 다이아몬드
            case 6:
            case 7:
            case 29:
            case 30:
            case 55:
            case 56:
            case 71:
            case 72:
            case 86:
            case 87:
            case 102:
            case 103:
            case 117:
            case 118:
            case 132:
            case 133:
                GiveReward("다이아몬드", level);
                break;
            default:
                GiveReward("골드", level);
                break;
        }
    }

    private void GiveReward(string Name, int index)
    {
        if (Name == "골드") GameManager.Player_Money += Tutorial_Compensation[index];
        else if(Name == "붉은보석") GameManager.Player_RedStone += Tutorial_Compensation[index];
        else if (Name == "다이아몬드") GameManager.Player_Diamond += Tutorial_Compensation[index];
        else if (Name == "강화스크롤") GameManager.RelicsReinforceScroll += Tutorial_Compensation[index];
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
