using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * 업적 고칠것
 * 1. 스테이지 클리어 18단계 보스 단계에서 무조건 클리어라고뜸
 * 2. 이름 다시 다 지을것 stageclear 난잡함
 * 3. 광산도 똑같음
 * 4. 몬스터든 광산이든 클리어 부분 고치기
 */

public class AchievementManager : MonoBehaviour
{
    public Text[] AchTitleValue;
    public Button[] AchRecieveBtn;

    public Text[] LeftText;
    public Text[] DiamondText;

    public Button AchPanelOpenBtn;
    public Button AchPanelCloseBtn;
    public Button[] AchSelectBtn;
    public GameObject AchPanel;

    public static int MonsterKill;
    public static int MonsterKillGold;
    public static int MonsterKillExp;
    public static int MonsterKillRedStone;
    public static int MonsterKillMobScroll;
    public static int ClearTutorialValue;

    private string[] AchDiamond;

    private int[] AchClear_Monster;
    private int[] AchClear_Equipment;
    private int[] AchClear_Mine;

    private string AchMineClearName;
    private int AchMineClearStage;

    private string AchMonsterClearStageName;
    private string AchBossClearStageName;
    private int AchMonsterClearStage;
    private int AchBossClearStage;
    private int AchMonsterKillNeedValue;
    private int AchMonsterKillNeedGold;
    private int AchMonsterKillNeedExp;
    private int AchMonsterKillNeedRedStone;
    private int AchMonsterKillNeedMobScroll;
    private int AchTutorialClearNeedValue;

    private int AchNeedWeaponCount;
    private int AchNeedWeaponReinforceCount;
    private int AchNeedWeaponHave;
    private int AchNeedAccessoryCount;
    private int AchNeedAccessoryReinforceCount;
    private int AchNeedAccessoryHave;
    private int AchNeedRelicsTry;
    private int AchNeedWarrantLevel;
    private int AchTrandCount;
    private int AchNeedTrandCount;

    private int AchNeedPickaxeUpgradeCount;
    private int AchNeedMineralUpgradeCount;
    private int AchNeedOptionUpgradeCount;
    private int AchNeedMineClickCount;
    private int AchNeedMineBreakCount;
    private int AchNeedMineAdCount;

    private string enemyType;
    private string StageType;

    private string MineType;
    private string MineStageType;

    private int StageClear;
    private int MobStage;
    private int MineClear;
    private int MineStage;

    private string MobName;
    private string MineName;
    private string StageClearTitle;
    private string MineClearTitle;

    private int AchInPanel;

    private int[] DiamondGiveValue;

    // Start is called before the first frame update
    void Start()
    {
        AchClear_Monster = new int[9];
        AchClear_Equipment = new int[9];
        AchClear_Mine = new int[9];

        DiamondGiveValue = new int[27]
        {
            500,
            250,
            250,
            250,
            300,
            300,
            1000,
            300,
            500,


            500,
            500,
            500,
            500,
            500,
            500,
            300,
            1500,
            5000,


            500,
            500,
            500,
            100,
            100,
            1500,
            300,
            500,
            500,
        };


        enemyType = "EasySlime";
        MineType = "하급 돌덩이";

        MobStage = 0;
        StageClear = 0;

        MineStage = 0;
        MineClear = 0;


        AchInPanel = 0;

        AchMonsterClearStage = 1;
        AchBossClearStage = 0;

        AchMineClearStage = 1;

        AchMonsterKillNeedValue = 30 * (1 + AchClear_Monster[1]);
        AchMonsterKillNeedGold = 5000 * (1 + AchClear_Monster[2]);
        AchMonsterKillNeedExp = 2500 * (1 + AchClear_Monster[3]);
        AchMonsterKillNeedRedStone = 120 * (1 + AchClear_Monster[4]);
        AchMonsterKillNeedMobScroll = 50 * (1 + AchClear_Monster[5]);
        AchTutorialClearNeedValue = 10 * (1 + AchClear_Monster[8]);

        AchNeedWeaponCount = 100 * (1 + AchClear_Equipment[0]);
        AchNeedWeaponReinforceCount = 20 * (1 + AchClear_Equipment[1]);
        AchNeedWeaponHave = 1 + AchClear_Equipment[2];
        AchNeedAccessoryCount = 100 * (1 + AchClear_Equipment[3]);
        AchNeedAccessoryReinforceCount = 20 * (1 + AchClear_Equipment[4]);
        AchNeedAccessoryHave = 1 + AchClear_Equipment[5];
        AchNeedRelicsTry = 10 * (1 + AchClear_Equipment[6]);
        AchNeedWarrantLevel = 1 + AchClear_Equipment[7];
        AchTrandCount = 1 + AchClear_Equipment[8];

        AchNeedPickaxeUpgradeCount =    30 * (1 + AchClear_Mine[0]);
        AchNeedMineralUpgradeCount =    30 * (1 + AchClear_Mine[1]);
        AchNeedOptionUpgradeCount =     30 * (1 + AchClear_Mine[2]);
        AchNeedMineClickCount =         300 * (1 + AchClear_Mine[3]);
        AchNeedMineBreakCount =         200 * (1 + AchClear_Mine[4]);
        AchNeedMineAdCount = 3 * (1 + AchClear_Mine[7]);
        //AchTrandCount = 1 + AchClear_Equipment[8];

        for (int i = 0; i < AchRecieveBtn.Length; i++)
        {
            int index = i;

            AchRecieveBtn[index].onClick.AddListener(() => AchRecive(index));
        }

        for(int i = 0; i < AchSelectBtn.Length; i++)
        {
            int index = i;

            AchSelectBtn[index].onClick.AddListener(() => AchSel(index));
        }

        AchPanelOpenBtn.onClick.AddListener(() => AchOpen(AchInPanel));
        AchPanelCloseBtn.onClick.AddListener(AchClose);
    }

    private void AchSel(int index)
    {
        AchInPanel = index;

        for (int i = 0; i < AchSelectBtn.Length; i++)
        {
            Image image = AchSelectBtn[i].GetComponent<Image>();
            if (i == AchInPanel) image.color = ColorManager.ColorChange("민트색");
            else image.color = ColorManager.ColorChange("기본색");
        }


        TextUpdate(index);
        LeftNameUpdate(index);
        BtnUpdate(index);
    }

    private void TextUpdate(int index)
    {
        StageClearName("Monster");
        StageClearName("BOSS");
        StageClearName("Mineral");
        LeftNameUpdate(AchInPanel);
        if(index == 0)
        {
            AchTitleValue[0].text = AchMonsterClearStageName;
            AchTitleValue[1].text = TextFormatter.GetThousandCommaText(MonsterKill) + " / " + TextFormatter.GetThousandCommaText(AchMonsterKillNeedValue) + "마리";
            AchTitleValue[2].text = TextFormatter.GetThousandCommaText(AchMonsterKillNeedGold) + " Gold";
            AchTitleValue[3].text = TextFormatter.GetThousandCommaText(AchMonsterKillNeedExp) + " Exp";
            AchTitleValue[4].text = TextFormatter.GetThousandCommaText(MonsterKillRedStone) + " / " + TextFormatter.GetThousandCommaText(AchMonsterKillNeedRedStone) + " 개";
            AchTitleValue[5].text = TextFormatter.GetThousandCommaText(MonsterKillMobScroll) + " / " + TextFormatter.GetThousandCommaText(AchMonsterKillNeedMobScroll) + " 개";
            AchTitleValue[6].text = StageClearTitle;
            AchTitleValue[7].text = AchBossClearStageName;
            AchTitleValue[8].text = TextFormatter.GetThousandCommaText(ClearTutorialValue) + " / " + TextFormatter.GetThousandCommaText(AchTutorialClearNeedValue) + " 회";
        }
        else if(index == 1)
        {
            AchTitleValue[0].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityWeaponCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedWeaponCount) + "회";
            AchTitleValue[1].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityWeaponReinforceCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedWeaponReinforceCount) + "회";
            AchTitleValue[2].text = TextFormatter.GetThousandCommaText(StatisticsManager.WeaponHaveNum()) + " / " + TextFormatter.GetThousandCommaText(AchNeedWeaponHave) + "개";
            AchTitleValue[3].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityAccessoryCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedAccessoryCount) + "회";
            AchTitleValue[4].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityAccessoryReinforceCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedAccessoryReinforceCount) + "회";
            AchTitleValue[5].text = TextFormatter.GetThousandCommaText(StatisticsManager.AccessoryHaveNum()) + " / " + TextFormatter.GetThousandCommaText(AchNeedAccessoryHave) + "개";
            AchTitleValue[6].text = TextFormatter.GetThousandCommaText(StatisticsManager.RelicsTry) + " / " + TextFormatter.GetThousandCommaText(AchNeedRelicsTry) + "회";
            AchTitleValue[7].text = TextFormatter.GetThousandCommaText(StatisticsManager.WarrantHaveNum(AchNeedWarrantLevel)) + " / 20개";
            AchTitleValue[8].text = TextFormatter.GetThousandCommaText(StatisticsManager.TrandHaveNum()) + " / " + AchTrandCount + "개";
        }
        else if (index == 2)
        {
            AchTitleValue[0].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityPickaxeUpgradeCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedPickaxeUpgradeCount) + "회";
            AchTitleValue[1].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityMineralUpgradeCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedMineralUpgradeCount) + "회";
            AchTitleValue[2].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityOptionUpgradeCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedOptionUpgradeCount) + "회";
            AchTitleValue[3].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityMineClickCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedMineClickCount) + "회";
            AchTitleValue[4].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityMineBreakCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedMineBreakCount) + "회";
            AchTitleValue[5].text = MineClearTitle;
            AchTitleValue[6].text = AchMineClearName;
            AchTitleValue[7].text = TextFormatter.GetThousandCommaText(StatisticsManager.ImmutabilityMineAdCount) + " / " + TextFormatter.GetThousandCommaText(AchNeedMineAdCount) + "회";
            //AchTitleValue[8].text = // ??
        }
        for (int i = 0; i < DiamondText.Length; i++)
        {
            DiamondText[i].text = "x " + DiamondGiveValue[index * 9 + i]; 
        }
        
        
    }

    private string ReturnLeftName(int index)
    {
        if (index == 0)         return "<color=lime>Easy Slime</color> 클리어 :";
        else if(index == 1)     return "<color=cyan>Normal Slime</color> 클리어 :";
        else if (index == 2)    return "<color=red>Hard Slime</color> 클리어 :";
        else if (index == 3)    return "<color=maroon>Extreme Slime</color> 클리어 :";

        else if (index == 4)    return "<color=lime>Easy Wolf</color> 클리어 :";
        else if (index == 5)    return "<color=cyan>Normal Wolf</color> 클리어 :";
        else if (index == 6)    return "<color=red>Hard Wolf</color> 클리어 :";
        else if (index == 7)    return "<color=maroon>Extreme Wolf</color> 클리어 :";

        else if (index == 8)    return "<color=lime>Easy Golem</color> 클리어 :";
        else if (index == 9)    return "<color=cyan>Normal Golem</color> 클리어 :";
        else if (index == 10)   return "<color=yellow>Hard Golem</color> 클리어 :";
        else if (index == 11)   return "<color=#FF7474>Extreme Golem</color> 클리어 :";

        else if (index == 12)   return "<color=lime><size=35>Easy MushRoom</size></color> 클리어 :";
        else if (index == 13)   return "<color=cyan><size=35>Normal MushRoom</size></color> 클리어 :";
        else if (index == 14)   return "<color=yellow><size=35>Hard MushRoom</size></color> 클리어 :";
        else if (index == 15)   return "<color=#FF7474><size=35>Extreme MushRoom</size></color> 클리어 :";
                                        
        else if (index == 16)   return "<color=lime>Easy Skull</color> 클리어 :";
        else if (index == 17)   return "<color=cyan>Normal Skull</color> 클리어 :";
        else if (index == 18)   return "<color=yellow>Hard Skull</color> 클리어 :";
        else if (index == 19)   return "<color=#FF7474>Extreme Skull</color> 클리어 :";
                                       
        else if (index == 20)   return "<color=lime>Easy Goblin</color> 클리어 :";
        else if (index == 21)   return "<color=cyan>Normal Goblin</color> 클리어 :";
        else if (index == 22)   return "<color=yellow>Hard Goblin</color> 클리어 :";
        else if (index == 23)   return "<color=#FF7474>Extreme Goblin</color> 클리어 :";
                                        
        else if (index == 24)   return "<color=lime><size=35>Easy FlyingEye</size></color> 클리어 :";
        else if (index == 25)   return "<color=cyan><size=35>Normal FlyingEye</size></color> 클리어 :";
        else if (index == 26)   return "<color=yellow><size=35>Hard FlyingEye</size></color> 클리어 :";
        else                    return "<color=#FF7474><size=35>Extreme FlyingEye</size></color> 클리어 :";
    }


    private string ReturnLeftColorName(int index)
    {
        if (index == 0)         return "<color=white>넓은 들판 - </color><color=lime>Easy</color>";
        else if (index == 1)    return "<color=white>넓은 들판 - </color><color=cyan>Normal</color>";
        else if (index == 2)    return "<color=white>넓은 들판 - </color><color=red>Hard</color>";
        else if (index == 3)    return "<color=white>넓은 들판 - </color><color=maroon>Extreme</color>";

        else if (index == 4)    return "<color=white>깊은 초원 - </color><color=lime>Easy</color>";
        else if (index == 5)    return "<color=white>깊은 초원 - </color><color=cyan>Normal</color>";
        else if (index == 6)    return "<color=white>깊은 초원 - </color><color=red>Hard</color>";
        else if (index == 7)    return "<color=white>깊은 초원 - </color><color=maroon>Extreme</color>";

        else if (index == 8)    return "<color=white>정글 사원 - </color><color=lime>Easy</color>";
        else if (index == 9)    return "<color=white>정글 사원 - </color><color=cyan>Normal</color>";
        else if (index == 10)   return "<color=white>정글 사원 - </color><color=yellow>Hard</color>";
        else if (index == 11)   return "<color=white>정글 사원 - </color><color=#FF7474>Extreme</color>";

        else if (index == 12)   return "<color=white>버섯 군락 - </color><color=lime>Easy</color>";
        else if (index == 13)   return "<color=white>버섯 군락 - </color><color=cyan>Normal</color>";
        else if (index == 14)   return "<color=white>버섯 군락 - </color><color=yellow>Hard</color>";
        else if (index == 15)   return "<color=white>버섯 군락 - </color><color=#FF7474>Extreme</color>";

        else if (index == 16)   return "<color=white>설산의 숲 - </color><color=lime>Easy</color>";
        else if (index == 17)   return "<color=white>설산의 숲 - </color><color=cyan>Normal</color>";
        else if (index == 18)   return "<color=white>설산의 숲 - </color><color=yellow>Hard</color>";
        else if (index == 19)   return "<color=white>설산의 숲 - </color><color=#FF7474>Extreme</color>";

        else if (index == 20)   return "<color=white>깊은 설산 - </color><color=lime>Easy</color>";
        else if (index == 21)   return "<color=white>깊은 설산 - </color><color=cyan>Normal</color>";
        else if (index == 22)   return "<color=white>깊은 설산 - </color><color=yellow>Hard</color>";
        else if (index == 23)   return "<color=white>깊은 설산 - </color><color=#FF7474>Extreme</color>";

        else if (index == 24)   return "<color=white>비명의 숲 - </color><color=lime>Easy</color>";
        else if (index == 25)   return "<color=white>비명의 숲 - </color><color=cyan>Normal</color>";
        else if (index == 26)   return "<color=white>비명의 숲 - </color><color=yellow>Hard</color>";
        else                    return "<color=white>비명의 숲 - </color><color=#FF7474>Extreme</color>";
    }


    private void LeftNameUpdate(int index)
    {
        if(index == 0)
        {
            MobName = ReturnLeftName(MobStage);

            StageClearTitle = ReturnLeftColorName(StageClear);


            LeftText[0].text = MobName;
            LeftText[1].text = "몬스터 처치 :";
            LeftText[2].text = "적 처치 골드 획득 :";
            LeftText[3].text = "적 처치 경험치 획득 :";
            LeftText[4].text = "적 처치 붉은 보석 획득 :";
            LeftText[5].text = "적 처치 비급서 획득 :";
            LeftText[6].text = "스테이지 클리어 :";
            LeftText[7].text = "보스 클리어 :";
            LeftText[8].text = "튜토리얼 클리어 :";

        }
        else if(index == 1)
        {
            LeftText[0].text = "무기 뽑기 횟수 :";
            LeftText[1].text = "무기 강화 횟수 :";
            LeftText[2].text = "무기 획득 개수 :";
            LeftText[3].text = "악세서리 뽑기 횟수 :";
            LeftText[4].text = "악세서리 강화 횟수 :";
            LeftText[5].text = "악세서리 획득 개수 :";
            LeftText[6].text = "유물 강화 횟수 :";
            LeftText[7].text = "Lv.<color=lime>" + AchNeedWarrantLevel + " </color> 권능 획득 :";
            LeftText[8].text = "초월 장비 획득 :";
        }
        else if(index == 2)
        {
            switch (MineStage)
            {
                case 0:
                    MineName = "하급 돌덩이 클리어 :";
                    break;
                case 1:
                    MineName = "중급 돌덩이 클리어 :";
                    break;
                case 2:
                    MineName = "상급 돌덩이 클리어 :";
                    break;
                case 3:
                    MineName = "최상급 돌덩이 클리어 :";
                    break;
                default:
                    break;
            }
            switch (MineClear)
            {
                case 0:
                    MineClearTitle = "<color=white>작은 광산 - </color><color=lime>Easy</color>";
                    break;
                case 1:
                    MineClearTitle = "<color=white>작은 광산 - </color><color=cyan>Normal</color>";
                    break;
                case 2:
                    MineClearTitle = "<color=white>작은 광산 - </color><color=red>Hard</color>";
                    break;
                case 3:
                    MineClearTitle = "<color=white>작은 광산 - </color><color=maroon>Extreme</color>";
                    break;
                default:
                    break;
            }

            LeftText[0].text = "곡괭이 업그레이드 횟수 : ";
            LeftText[1].text = "광물 업그레이드 횟수 : ";
            LeftText[2].text = "옵션 업그레이드 횟수 : ";
            LeftText[3].text = "광산 클릭 횟수 : ";
            LeftText[4].text = "광물 채광 횟수 : ";
            LeftText[5].text = "스테이지 클리어 :";
            LeftText[6].text = MineName;
            LeftText[7].text = "광산 광고 횟수 :";
            LeftText[8].text = "만들어야함 :";
        }
        
    }

    private string ReturnEnemyName(int index)
    {
        if (index == 0)         return "EasySlime";
        else if (index == 1)    return "NormalSlime";
        else if (index == 2)    return "HardSlime";
        else if (index == 3)    return "ExtremeSlime";

        else if (index == 4)    return "EasyWolf";
        else if (index == 5)    return "NormalWolf";
        else if (index == 6)    return "HardWolf";
        else if (index == 7)    return "ExtremeWolf";

        else if (index == 8)    return "EasyGolem";
        else if (index == 9)    return "NormalGolem";
        else if (index == 10)   return "HardGolem";
        else if (index == 11)   return "ExtremeGolem";

        else if (index == 12)   return "EasyMushRoom";
        else if (index == 13)   return "NormalMushRoom";
        else if (index == 14)   return "HardMushRoom";
        else if (index == 15)   return "ExtremeMushRoom";

        else if (index == 16)   return "EasySkull";
        else if (index == 17)   return "NormalSkull";
        else if (index == 18)   return "HardSkull";
        else if (index == 19)   return "ExtremeSkull";

        else if (index == 20)   return "EasyGoblin";
        else if (index == 21)   return "NormalGoblin";
        else if (index == 22)   return "HardGoblin";
        else if (index == 23)   return "ExtremeGoblin";

        else if (index == 24)   return "EasyFlyingEye";
        else if (index == 25)   return "NormalFlyingEye";
        else if (index == 26)   return "HardFlyingEye";
        else                    return "ExtremeFlyingEye";

    }
    private string ReturnRockName(int index)
    {
        if (index == 0) return "하급 돌덩이";
        else if (index == 1) return "중급 돌덩이";
        else if (index == 2) return "상급 돌덩이";
        else return "최상급 돌덩이";

    }

    private void BtnUpdate(int index)
    {
        if(index == 0)
        {
            if (AchMonsterClearStage == 18)
            {
                if (GameManager.stageClearDict[ReturnEnemyName(MobStage + 1)][0] == true) AchRecieveBtn[0].interactable = true;
                else AchRecieveBtn[0].interactable = false;
            }
            else if (GameManager.stageClearDict[ReturnEnemyName(MobStage)][AchMonsterClearStage] == true) AchRecieveBtn[0].interactable = true;
            else AchRecieveBtn[0].interactable = false;

            if (MonsterKill >= AchMonsterKillNeedValue) AchRecieveBtn[1].interactable = true;
            else AchRecieveBtn[1].interactable = false;

            if (MonsterKillGold >= AchMonsterKillNeedGold) AchRecieveBtn[2].interactable = true;
            else AchRecieveBtn[2].interactable = false;

            if (MonsterKillExp >= AchMonsterKillNeedExp) AchRecieveBtn[3].interactable = true;
            else AchRecieveBtn[3].interactable = false;

            if (MonsterKillRedStone >= AchMonsterKillNeedRedStone) AchRecieveBtn[4].interactable = true;
            else AchRecieveBtn[4].interactable = false;

            if (MonsterKillMobScroll >= AchMonsterKillNeedMobScroll) AchRecieveBtn[5].interactable = true;
            else AchRecieveBtn[5].interactable = false;

            if (GameManager.stageClearDict[ReturnEnemyName(StageClear + 1)][0] == true) AchRecieveBtn[6].interactable = true;
            else AchRecieveBtn[6].interactable = false;

            if (BossManager.ClearBoss[AchBossClearStage] == true) AchRecieveBtn[7].interactable = true;
            else AchRecieveBtn[7].interactable = false;

            if (ClearTutorialValue >= AchTutorialClearNeedValue) AchRecieveBtn[8].interactable = true;
            else AchRecieveBtn[8].interactable = false;


        }
        else if(index == 1)
        {
             
            if (AchNeedWeaponCount <= StatisticsManager.ImmutabilityWeaponCount) AchRecieveBtn[0].interactable = true;
            else AchRecieveBtn[0].interactable = false;

            if (AchNeedWeaponReinforceCount <= StatisticsManager.ImmutabilityWeaponReinforceCount) AchRecieveBtn[1].interactable = true;
            else AchRecieveBtn[1].interactable = false;

            if (AchNeedWeaponHave <= StatisticsManager.WeaponHaveNum()) AchRecieveBtn[2].interactable = true;
            else AchRecieveBtn[2].interactable = false;

            if (AchNeedAccessoryCount <= StatisticsManager.ImmutabilityAccessoryCount) AchRecieveBtn[3].interactable = true;
            else AchRecieveBtn[3].interactable = false;

            if (AchNeedAccessoryReinforceCount <= StatisticsManager.ImmutabilityAccessoryReinforceCount) AchRecieveBtn[4].interactable = true;
            else AchRecieveBtn[4].interactable = false;

            if (AchNeedAccessoryHave <= StatisticsManager.AccessoryHaveNum()) AchRecieveBtn[5].interactable = true;
            else AchRecieveBtn[5].interactable = false;

            if (AchNeedRelicsTry <= StatisticsManager.RelicsTry) AchRecieveBtn[6].interactable = true;
            else AchRecieveBtn[6].interactable = false;

            if (20 <= StatisticsManager.WarrantHaveNum(AchNeedWarrantLevel)) AchRecieveBtn[7].interactable = true;
            else AchRecieveBtn[7].interactable = false;

            if (AchTrandCount <= StatisticsManager.TrandHaveNum()) AchRecieveBtn[8].interactable = true;
            else AchRecieveBtn[8].interactable = false;

        }
        else if(index == 2)
        {

            if (AchNeedPickaxeUpgradeCount <= StatisticsManager.ImmutabilityPickaxeUpgradeCount) AchRecieveBtn[0].interactable = true;
            else AchRecieveBtn[0].interactable = false;

            if (AchNeedMineralUpgradeCount <= StatisticsManager.ImmutabilityMineralUpgradeCount) AchRecieveBtn[1].interactable = true;
            else AchRecieveBtn[1].interactable = false;

            if (AchNeedOptionUpgradeCount <= StatisticsManager.ImmutabilityOptionUpgradeCount) AchRecieveBtn[2].interactable = true;
            else AchRecieveBtn[2].interactable = false;

            if (AchNeedMineClickCount <= StatisticsManager.ImmutabilityMineClickCount) AchRecieveBtn[3].interactable = true;
            else AchRecieveBtn[3].interactable = false;

            if (AchNeedMineBreakCount <= StatisticsManager.ImmutabilityMineBreakCount) AchRecieveBtn[4].interactable = true;
            else AchRecieveBtn[4].interactable = false;


            if (GameManager.RockstageClearDict[ReturnRockName(MineClear + 1)][0] == true) AchRecieveBtn[5].interactable = true;
            else AchRecieveBtn[5].interactable = false;

            if (AchMineClearStage == 18)
            {
                if (GameManager.RockstageClearDict[ReturnRockName(MineStage + 1)][0] == true) AchRecieveBtn[6].interactable = true;
                else AchRecieveBtn[6].interactable = false;
            }
            else if (GameManager.RockstageClearDict[ReturnRockName(MineStage)][AchMineClearStage] == true) AchRecieveBtn[6].interactable = true;
            else AchRecieveBtn[6].interactable = false;


            if (AchNeedMineAdCount <= StatisticsManager.ImmutabilityMineAdCount) AchRecieveBtn[7].interactable = true;
            else AchRecieveBtn[7].interactable = false;

            //if (ClearTutorialValue >= AchTutorialClearNeedValue) AchRecieveBtn[8].interactable = true;
            //else AchRecieveBtn[8].interactable = false;

        }

        for (int i = 0; i < AchRecieveBtn.Length; i++)
        {
            if (AchRecieveBtn[i].interactable == true) ChangeButtonColor(AchRecieveBtn[i], "초록색");
            else ChangeButtonColor(AchRecieveBtn[i], "빨간색");
        }
    }

    private void AchOpen(int index)
    {
        TextUpdate(index);
        BtnUpdate(index);

        for (int i = 0; i < AchSelectBtn.Length; i++)
        {
            Image image = AchSelectBtn[i].GetComponent<Image>();
            if (i == index) image.color = ColorManager.ColorChange("민트색");
            else image.color = ColorManager.ColorChange("기본색");
        }

        AchPanel.SetActive(true);

    }
    private void AchClose()
    {
        AchPanel.SetActive(false);
    }

    private void AchRecive(int index)
    {
        if(AchInPanel == 0)
        {
            if (index == 0)
            {
                enemyType = ReturnEnemyName(MobStage);
                StageType = ReturnEnemyName(StageClear + 1);

                if (AchMonsterClearStage == 18)
                {
                    AchMonsterClearStage = 1;
                    MobStage++;
                }
                else if (GameManager.stageClearDict[enemyType][AchMonsterClearStage] == true)
                {
                    AchMonsterClearStage++;
                }
            }

            AchClear_Monster[index]++;

            if (index == 1) AchMonsterKillNeedValue = 10 * (1 + AchClear_Monster[index]);

            if (index == 2) AchMonsterKillNeedGold = 1000 * (1 + AchClear_Monster[index]);

            if (index == 3) AchMonsterKillNeedExp = 2000 * (1 + AchClear_Monster[index]);

            if (index == 4) AchMonsterKillNeedRedStone = 100 * (1 + AchClear_Monster[index]);

            if (index == 5) AchMonsterKillNeedMobScroll = 100 * (1 + AchClear_Monster[index]);

            if (index == 6) StageClear++;

            if (index == 7) AchBossClearStage++;

            if (index == 8) AchTutorialClearNeedValue = 10 * (1 + AchClear_Monster[index]);

            GameManager.Player_Diamond += DiamondGiveValue[AchInPanel * 9 + index];
            
        }
        else if(AchInPanel == 1)
        {
            AchClear_Equipment[index]++;

            if (index == 0) AchNeedWeaponCount = 100 * (1 + AchClear_Equipment[index]);

            if (index == 1) AchNeedWeaponReinforceCount = 20 * (1 + AchClear_Equipment[index]);

            if (index == 2) AchNeedWeaponHave = 1 + AchClear_Equipment[index];

            if (index == 3) AchNeedAccessoryCount = 100 * (1 + AchClear_Equipment[index]);

            if (index == 4) AchNeedAccessoryReinforceCount = 20 * (1 + AchClear_Equipment[index]);

            if (index == 5) AchNeedAccessoryHave = 1 + AchClear_Equipment[index];

            if (index == 6) AchNeedRelicsTry = 10 * AchClear_Equipment[index];

            if (index == 7) AchNeedWarrantLevel = 1 + AchClear_Equipment[index];

            if (index == 8)
            {
                AchTrandCount = 1 + AchClear_Equipment[index];
                AchNeedTrandCount++;
            }

            GameManager.Player_Diamond += DiamondGiveValue[AchInPanel * 9 + index];
        }
        else if(AchInPanel == 2)
        {
            if (index == 6)
            {
                if (AchMineClearStage == 18)
                {
                    AchMineClearStage = 1;
                    MineStage++;
                }
                else if (GameManager.RockstageClearDict[ReturnRockName(MineStage)][AchMineClearStage] == true)
                {
                    AchMineClearStage++;
                }
            }

            AchClear_Mine[index]++;

            if (index == 0) AchNeedPickaxeUpgradeCount = 30 * (1 + AchClear_Mine[index]);

            if (index == 1) AchNeedMineralUpgradeCount = 30 * (1 + AchClear_Mine[index]);
            
            if (index == 2) AchNeedOptionUpgradeCount = 30 * (1 + AchClear_Mine[index]);
            
            if (index == 3) AchNeedMineClickCount = 300 * (1 + AchClear_Mine[index]);
            
            if (index == 4) AchNeedMineBreakCount = 200 * (1 + AchClear_Mine[index]);
            
            if (index == 5) MineClear++;
            
            if (index == 7) AchNeedMineAdCount = 3 * (1 + AchClear_Mine[index]);

            if (index == 8) AchTutorialClearNeedValue = 10 * (1 + AchClear_Monster[index]);

            GameManager.Player_Diamond += DiamondGiveValue[AchInPanel * 9 + index];
        }


        TextUpdate(AchInPanel);
        BtnUpdate(AchInPanel);
    }
    private void StageClearName(string name)
    {
        // 슬라임 - 일반
        if (name == "Monster")
        {
            AchMonsterClearStageName = (AchMonsterClearStage - 2) switch
            {
                -1 => "Lv. I",
                0 => "Lv. II",
                1 => "Lv. III",
                2 => "Lv. IV",
                3 => "Lv. V",
                4 => "Lv. VI",
                5 => "Lv. VII",
                6 => "Lv. VIII",
                7 => "Lv. IX",
                8 => "Lv. X",
                9 => "Lv. XI",
                10 => "Lv. XII",
                11 => "Lv. XIII",
                12 => "Lv. XIV",
                13 => "Lv. XV",
                14 => "Lv. XVI",
                15 => "Lv. XVII",
                16 => "Lv. BOSS",
                _ => "Unknown"
            };

        }
        // 비둘기 - 보스
        else if (name == "BOSS")
        {
            switch (AchBossClearStage)
            {
                case 0:
                    AchBossClearStageName = "동네 비둘기 - I";
                    break;
                case 1:
                    AchBossClearStageName = "동네 비둘기 - II";
                    break;
                case 2:
                    AchBossClearStageName = "동네 비둘기 - III";
                    break;
                case 3:
                    AchBossClearStageName = "동네 비둘기 - IV";
                    break;
                case 4:
                    AchBossClearStageName = "동네 비둘기 - V";
                    break;
                case 5:
                    AchBossClearStageName = "동네 비둘기 - VI";
                    break;
                case 6:
                    AchBossClearStageName = "동네 비둘기 - VII";
                    break;
                case 7:
                    AchBossClearStageName = "동네 비둘기 - VIII";
                    break;
                case 8:
                    AchBossClearStageName = "동네 비둘기 - IX";
                    break;
                case 9:
                    AchBossClearStageName = "동네 비둘기 - X";
                    break;
                case 10:
                    AchBossClearStageName = "동네 비둘기 - XI";
                    break;
                case 11:
                    AchBossClearStageName = "동네 비둘기 - R";
                    break;
                case 12:
                    AchBossClearStageName = "공원 비둘기 - I";
                    break;
                case 13:
                    AchBossClearStageName = "공원 비둘기 - II";
                    break;
                case 14:
                    AchBossClearStageName = "공원 비둘기 - III";
                    break;
                case 15:
                    AchBossClearStageName = "공원 비둘기 - IV";
                    break;
                case 16:
                    AchBossClearStageName = "공원 비둘기 - V";
                    break;
                case 17:
                    AchBossClearStageName = "공원 비둘기 - VI";
                    break;
                case 18:
                    AchBossClearStageName = "공원 비둘기 - VII";
                    break;
                case 19:
                    AchBossClearStageName = "공원 비둘기 - VIII";
                    break;
                case 20:
                    AchBossClearStageName = "공원 비둘기 - IX";
                    break;
                case 21:
                    AchBossClearStageName = "공원 비둘기 - X";
                    break;
                case 22:
                    AchBossClearStageName = "공원 비둘기 - XI";
                    break;
                case 23:
                    AchBossClearStageName = "공원 비둘기 - R";
                    break;
                default:
                    break;
            }
        }
        // 광산 - 일반
        else if (name == "Mineral")
        {
            AchMineClearName = (AchMineClearStage - 2) switch
            {
                -1 => "Lv. I",
                0 => "Lv. II",
                1 => "Lv. III",
                2 => "Lv. IV",
                3 => "Lv. V",
                4 => "Lv. VI",
                5 => "Lv. VII",
                6 => "Lv. VIII",
                7 => "Lv. IX",
                8 => "Lv. X",
                9 => "Lv. XI",
                10 => "Lv. XII",
                11 => "Lv. XIII",
                12 => "Lv. XIV",
                13 => "Lv. XV",
                14 => "Lv. XVI",
                15 => "Lv. XVII",
                16 => "Lv. BOSS",
                _ => "Unknown"
            };
        }
    }

    // ChangeSel 버튼 색 관리
    public void ChangeButtonColor(Button button, string color)
    {
        // 버튼의 이미지 컴포넌트 가져오기
        Image buttonImage = button.GetComponent<Image>();
        // 버튼의 이미지 색상 변경
        buttonImage.color = ColorManager.ColorChange(color);
    }
}
