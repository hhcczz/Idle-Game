using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private string enemyType;
    private string StageType;
    private int StageClear;
    private int MobStage;

    private string MobName;
    private string StageEndName;
    private string StageClearTitle;

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
            250,
            250,
            250,
            300,
            300,
            1000,
            300,
            500,
        };


        enemyType = "EasySlime";

        MobStage = 0;
        StageClear = 0;

        AchInPanel = 0;

        LeftText[0].text = enemyType;
        LeftText[7].text = StageType;

        AchMonsterClearStage = 1;
        AchBossClearStage = 0;
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
            //AchTitleValue[0].text = // 곡괭이 업그레이드 
            //AchTitleValue[1].text = // 광물 업그레이드
            //AchTitleValue[2].text = // 옵션 업그레이드
            //AchTitleValue[3].text = // 광물 1단계 클리어
            //AchTitleValue[4].text = // 광물 Easy 클리어
            //AchTitleValue[5].text = // 광물 클릭
            //AchTitleValue[7].text = // 광물 채광
            //AchTitleValue[8].text = // 제련소?
        }
        for(int i = 0; i < DiamondText.Length; i++)
        {
            DiamondText[i].text = "x " + DiamondGiveValue[index * 9 + i]; 
        }
        

    }

    private void LeftNameUpdate(int index)
    {
        if(index == 0)
        {
            switch (MobStage)
            {
                case 0:
                    MobName = "Easy Slime 클리어 :";
                    break;
                case 1:
                    MobName = "Normal Slime 클리어 :";
                    break;
                case 2:
                    MobName = "Hard Slime 클리어 :";
                    break;
                case 3:
                    MobName = "Extreme Slime 클리어 :";
                    break;
                default:
                    break;
            }

            switch (StageClear)
            {
                case 0:
                    StageEndName = "생명의 숲 - <color=lime>Easy</color> :";
                    break;
                case 1:
                    StageEndName = "생명의 숲 - <color=cyan>Normal</color> :";
                    break;
                case 2:
                    StageEndName = "생명의 숲 - <color=red>Hard</color> :";
                    break;
                case 3:
                    StageEndName = "생명의 숲 - <color=maroon>Extreme</color> :";
                    break;
                default:
                    break;
            }

            switch (StageClear)
            {
                case 0:
                    StageClearTitle = "<color=white>생명의 숲 - </color><color=lime>Easy</color>";
                    break;
                case 1:
                    StageClearTitle = "<color=white>생명의 숲 - </color><color=cyan>Normal</color>";
                    break;
                case 2:
                    StageClearTitle = "<color=white>생명의 숲 - </color><color=red>Hard</color>";
                    break;
                case 3:
                    StageClearTitle = "<color=white>생명의 숲 - </color><color=maroon>Extreme</color>";
                    break;
                default:
                    break;
            }


            LeftText[0].text = MobName;
            LeftText[1].text = StageEndName;
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
        
    }

    private void BtnUpdate(int index)
    {
        if(index == 0)
        {
            enemyType = MobStage switch
            {
                0 => "EasySlime",
                1 => "NormalSlime",
                2 => "HardSlime",
                3 => "ExtremeSlime",
                _ => "Unknown"
            };
            StageType = StageClear switch
            {
                0 => "NormalSlime",
                1 => "HardSlime",
                2 => "ExtremeSlime",
                _ => "Unknown"
            };

            if (AchMonsterClearStage == 18)
            {
                if (GameManager.stageClearDict[enemyType][17] == true) AchRecieveBtn[0].interactable = true;
                else AchRecieveBtn[0].interactable = false;
            }
            else if (GameManager.stageClearDict[enemyType][AchMonsterClearStage] == true) AchRecieveBtn[0].interactable = true;
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

            if (GameManager.stageClearDict[StageType][0] == true) AchRecieveBtn[6].interactable = true;
            else AchRecieveBtn[6].interactable = false;

            if (BossManager.ClearBoss[AchBossClearStage] == true) AchRecieveBtn[7].interactable = true;
            else AchRecieveBtn[7].interactable = false;

            if (ClearTutorialValue >= AchTutorialClearNeedValue) AchRecieveBtn[8].interactable = true;
            else AchRecieveBtn[8].interactable = false;


            for (int i = 0; i < AchRecieveBtn.Length; i++)
            {
                if (AchRecieveBtn[i].interactable == true) ChangeButtonColor(AchRecieveBtn[i], "초록색");
                else ChangeButtonColor(AchRecieveBtn[i], "빨간색");
            }
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


            for (int i = 0; i < AchRecieveBtn.Length; i++)
            {
                if (AchRecieveBtn[i].interactable == true) ChangeButtonColor(AchRecieveBtn[i], "초록색");
                else ChangeButtonColor(AchRecieveBtn[i], "빨간색");
            }
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
                enemyType = MobStage switch
                {
                    0 => "EasySlime",
                    1 => "NormalSlime",
                    2 => "HardSlime",
                    3 => "ExtremeSlime",
                    _ => "Unknown"
                };

                StageType = StageClear switch
                {
                    0 => "NormalSlime",
                    1 => "HardSlime",
                    2 => "ExtremeSlime",
                    _ => "Unknown"
                };

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


        TextUpdate(AchInPanel);
        BtnUpdate(AchInPanel);
    }
    private void StageClearName(string name)
    {
        // 슬라임 - 일반
        if(name == "Monster")
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
