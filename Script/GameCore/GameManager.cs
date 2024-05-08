using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameConstants
{
    public static int WeaponNum = 24;
    public static int AccessoryNum = 24;
    public static int WarrantNum = 29;
    public static int MobNum = 28;
}

public class GameManager : MonoBehaviour
{
    

    public static decimal Player_Damage = 1m;                        //  플레이어 기본 데미지

    // 음악

    public static int BGS_Number = 0;

    // 플레이어 기본 세팅

    public static decimal Player_AttackSpeed = 1.00m;                  //  플레이어 기본 공격속도 * Time.deltaTime;
    public static decimal Player_CriticalChance = 0;                 //  플레이어 기본 크리티컬 퍼센트
    public static decimal Player_CriticalDamage = 0;                  //  플레이어 기본 크리티컬 데미지
    public static int Player_ArmorPenetration = 0;                 //  플레이어 기본 방관 수치
    public static int Player_PassivePoint = 0;                     //  플레이어 기본 패시브 포인트                 |       줄여서 PP

    public static long Player_MoneyUp_EarnMoney = 0;                //  플레이어 돈 업글 돈 획득량 증가
    public static decimal Player_MoneyUp_Damage = 0m;                   //  플레이어 돈 업글 데미지 증가
    public static decimal Player_MoneyUp_AttackSpeed = 0.00m;              //  플레이어 돈 업글 공격속도 증가
    public static decimal Player_MoneyUp_CriticalChance = 0m;           //  플레이어 돈 업글 크리티컬 확률 증가
    public static decimal Player_MoneyUp_CriticalDamage = 0m;           //  플레이어 돈 업글 크리티컬 데미지 증가
    public static int Player_MoneyUp_ArmorPenetration = 0;              //  플레이어 돈 업글 방어력 관통 증가

    public static int Player_Level = 1;                         //  플레이어 기본 레벨
    public static float Player_MaxExp = 3f;                         //  플레이어 최대 경험치
    public static float Player_CurExp = 0;                         //  플레이어 현재 경험치
    public static long Player_Money = 0;                           //  플레이어 현재 소지금
    public static int Player_Diamond = 0;                          //  플레이어 현재 다이아몬드
    public static float Player_RedStone_Percent = 12.5f;                //  플레이어 다이아몬드 획득 퍼센트
    public static int Player_RedStone = 0;                         //  플레이어 붉은 돌

    public static int RelicsReinforceScroll = 0;                         // 유물 강화 스크롤
    public static int[] PlayerHaveMobScroll = new int[8];

    // 보스

    // 몹
    public static bool Enemy_alive = true;
    public static bool Enemy_Hit = false;

    public static Dictionary<string, bool[]> stageClearDict = new();
    public static bool[][] MoveInStage = new bool[GameConstants.MobNum][];
    public static bool[][] PinInStage = new bool[GameConstants.MobNum][];
    public static int Enemy_defeatedIndex = 0;

    public static int Enemy_Stage_Difficulty = 0;

    public static string Player_InStage { get; set; } // 전역 변수로 InStage 설정

    // 플레이어 업그레이드

    public static int Player_PPUP_DamageLevel = 1;              //  플레이어 업그레이드 데미지 레벨
    public static decimal Player_PPUP_AttackSpeedLevel = 1m;      //  플레이어 업그레이드 공격속도 레벨
    public static decimal Player_PPUP_CriticalChanceLevel = 1m;    //  플레이어 업그레이드 크리티컬 퍼센트 레벨
    public static int Player_PPUP_CriticalDamageLevel = 1;     //  플레이어 업그레이드 크리티컬 데미지 레벨
    public static int Player_PPUP_ArmorPenetrationLevel = 1;    //  플레이어 업그레이드 방관 레벨
           
    public static int Player_MoneyUp_EarnMoneyLevel = 1;    //  플레이어 돈 업그레이드 크리티컬 퍼센트 레벨
    public static int Player_MoneyUp_DamageLevel = 1;              //  플레이어 돈 업그레이드 데미지 레벨
    public static int Player_MoneyUp_AttackSpeedLevel = 1;      //  플레이어 돈 업그레이드 공격속도 레벨
    public static int Player_MoneyUp_CriticalChanceLevel = 1;    //  플레이어 돈 업그레이드 크리티컬 퍼센트 레벨
    public static int Player_MoneyUp_CriticalDamageLevel = 1;     //  플레이어 돈 업그레이드 크리티컬 데미지 레벨
    public static int Player_MoneyUp_ArmorPenetrationLevel = 1;    //  플레이어 돈 업그레이드 방관 레벨
        
           
    public static long[] NeedMoney;
    public static int[] NeedMoney_Level;


    // 악세서리
    public static Dictionary<Sprite, int> AccessoryCount;
    public static Sprite[] AccessorySprites;
          
    public static float AccessoryCounts = 0f;
    public static int[] AccessoryMaxCounts;
    public static int AccessoryShopLevel = 1;
    public static bool[] AccessoryDrawn = new bool[GameConstants.AccessoryNum];
         
    public static decimal AccessoryEquipExperience;
    public static decimal AccessoryOwnExperience;
        
    public static string[] AccessoryTitle = new string[GameConstants.AccessoryNum];
         
    public static decimal[] AccessoryEquipExp = new decimal[GameConstants.AccessoryNum];
    public static decimal[] AccessoryOwnExp = new decimal[GameConstants.AccessoryNum];
          
    public static bool[] AccessoryOwnExpIncreased = new bool[GameConstants.WeaponNum];

    public static float[] AccessoryLevelProbilityValue_Common;          // 무기 레벨당 확률 - 일반
    public static float[] AccessoryLevelProbilityValue_Rare;            // 무기 레벨당 확률 - 레어
    public static float[] AccessoryLevelProbilityValue_UnCommon;        // 무기 레벨당 확률 - 고급
    public static float[] AccessoryLevelProbilityValue_Artifact;        // 무기 레벨당 확률 - 유물
    public static float[] AccessoryLevelProbilityValue_Mythical;        // 무기 레벨당 확률 - 신화
    public static float[] AccessoryLevelProbilityValue_Legend;          // 무기 레벨당 확률 - 전설
          
    public static int Accessory_AdCount;

    // 무기

    public static Dictionary<Sprite, int> itemCounts;

    public static Sprite[] itemSprites;

    public static float WeaponCounts = 0f;                            //  상점 무기 뽑은 숫자
    public static int[] WeaponMaxCounts;                         //  상점 무기 뽑기 최대 숫자
    public static int WeaponShopLevel = 1;                             //  무기 뽑기 레벨
    public static bool[] WeaponDrawn = new bool[GameConstants.WeaponNum]; // 무기를 뽑았는지 여부를 저장하는 배열
    public static decimal WeaponEquipDamage;
    public static decimal WeaponOwnDamage = 0m;
           
    public static string[] WeaponTitle = new string[GameConstants.WeaponNum];
            
    public static decimal[] WeaponEquipDmg = new decimal[GameConstants.WeaponNum];
    public static decimal[] WeaponOwnDmg = new decimal[GameConstants.WeaponNum];
          
    public static bool[] weaponOwnDamageIncreased = new bool[GameConstants.WeaponNum]; // 무기 소유 데미지가 증가했는지 여부를 추적하는 변수

    public static float[] WeaponLevelProbilityValue_Common;          // 무기 레벨당 확률 - 일반
    public static float[] WeaponLevelProbilityValue_Rare;            // 무기 레벨당 확률 - 레어
    public static float[] WeaponLevelProbilityValue_UnCommon;        // 무기 레벨당 확률 - 고급
    public static float[] WeaponLevelProbilityValue_Artifact;        // 무기 레벨당 확률 - 유물
    public static float[] WeaponLevelProbilityValue_Mythical;        // 무기 레벨당 확률 - 신화
    public static float[] WeaponLevelProbilityValue_Legend;          // 무기 레벨당 확률 - 전설

    public static int Weapon_AdCount;

    // 몹 스크롤

    // 상점 

    public static float[] PackageSpecialUpDamage;
    public static bool[] PackageBuyCheck;

    // 권능

    public static Sprite[] WarrantSprites;
    public static Sprite[] GemSprites;

    public static bool[] WarrantOwned = new bool[GameConstants.WarrantNum];
    public static int[] WarrantLevel = new int[GameConstants.WarrantNum];
          
    public static int[] Warrant_Power = new int[GameConstants.WarrantNum];

    // 초월장비

    public static bool[] TrandOwned = new bool[8];

    // 유물
    public static float Relics_Rank = 1;
    public static decimal Player_RelicsDamageAmplification = 5;

    // 광산

    public static Dictionary<string, bool[]> RockstageClearDict = new();

    public static decimal Pickaxe_Damage = 1;                   //  광산 곡괭이 데미지 올리기
    public static decimal Pickaxe_CriticalDamage = 1;          //  광산 곡괭이 크리티컬 데미지 올리기
    public static decimal Pickaxe_CriticalChance = 0m;       //  광산 곡괭이 크리티컬 퍼센트 올리기
            
    public static int Pickaxe_DamageLv = 1;                    //  광산 곡괭이 데미지 레벨
    public static int Pickaxe_CriticalDamage_Level = 1;           //  광산 곡괭이 크리티컬 데미지 레벨
    public static int Pickaxe_CriticalChance_Level = 1;          //  광산 곡괭이 크리티컬 퍼센트 레벨
            
    public static int Mineral_LevelMI = 1;                     //  광물 획득량 증가 레벨
    public static int Mineral_LevelHP = 1;                     //  광물 약점 레벨
    public static int Mineral_LevelRS = 1;                     //  광물 스크롤 레벨
            
    public static int Option_LevelPMA = 1;                     //  옵션 데미지 증폭           Pickaxe Damage amplification Scroll 
    public static int Option_LevelMB = 1;                      //  옵션 럭키 광물             Mineral Bomb
    public static int Option_LevelPFD = 1;                     //  옵션 치명적 피해           Pickaxe Fatal Damage
            
    public static decimal Mineral_MI = 0;                          //  광물 증가량 업그레이드
    public static decimal Mineral_HP = 0m;                     //  광물 체력 감소 업그레이드
    public static decimal Mineral_RS = 0m;                     //  광물 스크롤 획득 업그레이드
            
    public static decimal Option_PMA = 0m;                     //  옵션 데미지 증폭 업그레이드
    public static decimal Option_MB = 0m;                      //  옵션 럭키 광물 업그레이드
    public static decimal Option_PFD = 0m;                     //  옵션 치명적 피해 업그레이드

    public static long NeedStarGrey;
    public static long NeedStarBrown;
    public static long NeedStarBlue;
    public static long NeedStarGreen;
    public static long NeedStarRed;
    public static long NeedStarYellow;
    public static long NeedStarPurple;
    public static long NeedStarOrange;
    public static long NeedStarDark;

    public static long HaveStarGrey;
    public static long HaveStarBrown;
    public static long HaveStarBlue;
    public static long HaveStarGreen;
    public static long HaveStarRed;
    public static long HaveStarYellow;
    public static long HaveStarPurple;
    public static long HaveStarOrange;
    public static long HaveStarDark;

    public static float disable_ButtonAlpha = 100f / 255f;
    public static float enable_ButtonAlpha = 1f;

    private void Awake()
    {
        

        PackageBuyCheck = new bool[4];
        // 상점
        PackageSpecialUpDamage = new float[4]
        {
            50f,
            100f,
            200f,
            400f,
        };

        //

        BGS_Number = 0;
        NeedMoney = new long[99999];
        NeedMoney_Level = new int[6];

        DontDestroyOnLoad(gameObject);

        // 각 배열을 초기화합니다.
        for (int i = 0; i < GameConstants.MobNum; i++)
        {
            MoveInStage[i] = new bool[18];
            PinInStage[i] = new bool[18];
        }

        PlayerHaveMobScroll = new int[8];

        // 무기

        WeaponMaxCounts = new int[8];
        WeaponMaxCounts[0] = 35;
        WeaponMaxCounts[1] = 35;
        WeaponMaxCounts[2] = 35;
        WeaponMaxCounts[3] = 35;
        WeaponMaxCounts[4] = 35;
        WeaponMaxCounts[5] = 35;
        WeaponMaxCounts[6] = 35;
        WeaponMaxCounts[7] = 2113333333; // MAX

        //WeaponMaxCounts[0] = 100;
        //WeaponMaxCounts[1] = 600;
        //WeaponMaxCounts[2] = 1500;
        //WeaponMaxCounts[3] = 5000;
        //WeaponMaxCounts[4] = 20000;
        //WeaponMaxCounts[5] = 40000;
        //WeaponMaxCounts[6] = 80000;

        WeaponLevelProbilityValue_Common = new float[8];

        WeaponLevelProbilityValue_Common[0] = 95.00f;      //  95.00%
        WeaponLevelProbilityValue_Common[1] = 85.00f;      //  85.00%
        WeaponLevelProbilityValue_Common[2] = 70.00f;      //  70.00%
        WeaponLevelProbilityValue_Common[3] = 53.00f;      //  53.00%
        WeaponLevelProbilityValue_Common[4] = 30.00f;      //  30.00%
        WeaponLevelProbilityValue_Common[5] = 15.00f;      //  15.00%
        WeaponLevelProbilityValue_Common[6] = 10.00f;      //  10.00%
        WeaponLevelProbilityValue_Common[7] = 10.00f;      //  10.00%           //  MAX

        WeaponLevelProbilityValue_Rare = new float[8];

        WeaponLevelProbilityValue_Rare[0] = 4.80f;     //  99.80%
        WeaponLevelProbilityValue_Rare[1] = 14.20f;    //  99.20%
        WeaponLevelProbilityValue_Rare[2] = 28.00f;    //  98.00%
        WeaponLevelProbilityValue_Rare[3] = 44.00f;    //  97.00%    
        WeaponLevelProbilityValue_Rare[4] = 62.00f;    //  92.00%
        WeaponLevelProbilityValue_Rare[5] = 71.18f;    //  86.18%
        WeaponLevelProbilityValue_Rare[6] = 67.18f;    //  77.18%
        WeaponLevelProbilityValue_Rare[7] = 67.18f;    //  77.18%           //  MAX

        WeaponLevelProbilityValue_UnCommon = new float[8];

        WeaponLevelProbilityValue_UnCommon[0] = 0.19f;         //  99.990%
        WeaponLevelProbilityValue_UnCommon[1] = 0.75f;         //  99.950%
        WeaponLevelProbilityValue_UnCommon[2] = 1.705f;        //  99.705%
        WeaponLevelProbilityValue_UnCommon[3] = 2.40f;         //  99.400%
        WeaponLevelProbilityValue_UnCommon[4] = 6.00f;         //  98.000%
        WeaponLevelProbilityValue_UnCommon[5] = 10.80f;        //  96.980%
        WeaponLevelProbilityValue_UnCommon[6] = 17.773f;       //  94.953%
        WeaponLevelProbilityValue_UnCommon[7] = 17.773f;       //  94.953%           //  MAX

        WeaponLevelProbilityValue_Artifact = new float[8];

        WeaponLevelProbilityValue_Artifact[0] = 0.01f;     //  100%
        WeaponLevelProbilityValue_Artifact[1] = 0.05f;     //  100%
        WeaponLevelProbilityValue_Artifact[2] = 0.29f;     //  99.995%
        WeaponLevelProbilityValue_Artifact[3] = 0.58f;     //  99.980%
        WeaponLevelProbilityValue_Artifact[4] = 1.905f;    //  99.905%
        WeaponLevelProbilityValue_Artifact[5] = 2.80f;     //  99.780%
        WeaponLevelProbilityValue_Artifact[6] = 4.4f;      //  99.353%
        WeaponLevelProbilityValue_Artifact[7] = 4.4f;      //  99.353%           //  MAX

        WeaponLevelProbilityValue_Mythical = new float[8];

        WeaponLevelProbilityValue_Mythical[0] = 0f;        //  100%
        WeaponLevelProbilityValue_Mythical[1] = 0f;        //  100%
        WeaponLevelProbilityValue_Mythical[2] = 0.004f;    //  99.999%
        WeaponLevelProbilityValue_Mythical[3] = 0.018f;    //  99.998%
        WeaponLevelProbilityValue_Mythical[4] = 0.09f;     //  99.995%
        WeaponLevelProbilityValue_Mythical[5] = 0.21f;     //  99.990%
        WeaponLevelProbilityValue_Mythical[6] = 0.63f;     //  99.983%
        WeaponLevelProbilityValue_Mythical[7] = 0.63f;     //  99.983%           //  MAX

        WeaponLevelProbilityValue_Legend = new float[8];

        WeaponLevelProbilityValue_Legend[0] = 0f;          //  100%
        WeaponLevelProbilityValue_Legend[1] = 0f;          //  100%
        WeaponLevelProbilityValue_Legend[2] = 0.001f;      //  100%
        WeaponLevelProbilityValue_Legend[3] = 0.002f;      //  100%
        WeaponLevelProbilityValue_Legend[4] = 0.005f;      //  100%
        WeaponLevelProbilityValue_Legend[5] = 0.01f;       //  100%
        WeaponLevelProbilityValue_Legend[6] = 0.017f;      //  100%
        WeaponLevelProbilityValue_Legend[7] = 0.017f;      //  100%           //  MAX




        // 무기 기본 장착 데미지
        WeaponEquipDmg[0] = 1;
        WeaponEquipDmg[1] = 2;
        WeaponEquipDmg[2] = 3;
        WeaponEquipDmg[3] = 4;

        WeaponEquipDmg[4] = 8;
        WeaponEquipDmg[5] = 10;
        WeaponEquipDmg[6] = 20;
        WeaponEquipDmg[7] = 30;

        WeaponEquipDmg[8] = 60;
        WeaponEquipDmg[9] = 90;
        WeaponEquipDmg[10] = 120;
        WeaponEquipDmg[11] = 150;

        WeaponEquipDmg[12] = 250;
        WeaponEquipDmg[13] = 350;
        WeaponEquipDmg[14] = 450;
        WeaponEquipDmg[15] = 550;

        WeaponEquipDmg[16] = 1650;
        WeaponEquipDmg[17] = 1950;
        WeaponEquipDmg[18] = 2250;
        WeaponEquipDmg[19] = 2550;

        WeaponEquipDmg[20] = 8000;
        WeaponEquipDmg[21] = 9000;
        WeaponEquipDmg[22] = 10000;
        WeaponEquipDmg[23] = 15000;

        // 무기 기본 보유 데미지
        WeaponOwnDmg[0] = 0.1m;
        WeaponOwnDmg[1] = 0.2m;
        WeaponOwnDmg[2] = 0.3m;
        WeaponOwnDmg[3] = 0.4m;

        WeaponOwnDmg[4] = 1.0m;
        WeaponOwnDmg[5] = 1.6m;
        WeaponOwnDmg[6] = 2.2m;
        WeaponOwnDmg[7] = 2.8m;

        WeaponOwnDmg[8] = 4.0m;
        WeaponOwnDmg[9] = 5.2m;
        WeaponOwnDmg[10] = 6.4m;
        WeaponOwnDmg[11] = 7.6m;

        WeaponOwnDmg[12] = 10.0m;
        WeaponOwnDmg[13] = 12.4m;
        WeaponOwnDmg[14] = 14.8m;
        WeaponOwnDmg[15] = 17.6m;

        WeaponOwnDmg[16] = 25.0m;
        WeaponOwnDmg[17] = 50.0m;
        WeaponOwnDmg[18] = 75.0m;
        WeaponOwnDmg[19] = 100.0m;

        WeaponOwnDmg[20] = 200.0m;
        WeaponOwnDmg[21] = 300.0m;
        WeaponOwnDmg[22] = 400.0m;
        WeaponOwnDmg[23] = 500.0m;

        // 무기 이름
        WeaponTitle[0] = "<color=grey>나무 검</color>";
        WeaponTitle[1] = "<color=grey>돌 검</color>";
        WeaponTitle[2] = "<color=grey>철 스피어</color>";
        WeaponTitle[3] = "<color=grey>암살자의 단검</color>";
        WeaponTitle[4] = "<color=orange>천상 칼리스</color>";
        WeaponTitle[5] = "<color=orange>강철 단검</color>";
        WeaponTitle[6] = "<color=orange>은신자의 비명검</color>";
        WeaponTitle[7] = "<color=orange>급속 스틸레토</color>";
        WeaponTitle[8] = "<color=yellow>사냥꾼의 삼각검</color>";
        WeaponTitle[9] = "<color=yellow>서리검</color>";
        WeaponTitle[10] = "<color=yellow>맹독 단검</color>";
        WeaponTitle[11] = "<color=yellow>화염 검</color>";
        WeaponTitle[12] = "<color=lime>딥오션 크러셔</color>";
        WeaponTitle[13] = "<color=lime>태양의 기운</color>";
        WeaponTitle[14] = "<color=lime>녹색 백 헌터</color>";
        WeaponTitle[15] = "<color=lime>서리의 격노</color>";
        WeaponTitle[16] = "<color=darkblue>화염 파수꾼 둔기</color>";
        WeaponTitle[17] = "<color=darkblue>무채색 장검</color>";
        WeaponTitle[18] = "<color=darkblue>라이트닝 스피어</color>";
        WeaponTitle[19] = "<color=darkblue>그림자 반달도</color>";
        WeaponTitle[20] = "<color=red>빛나는 신검</color>";
        WeaponTitle[21] = "<color=red>청명한 뇌우칼</color>";
        WeaponTitle[22] = "<color=red>번개의 파수검 </color>";
        WeaponTitle[23] = "<color=red>죽음의 독칼</color>";


        // 악세서리

        AccessoryMaxCounts = new int[8];
        AccessoryMaxCounts[0] = 35;
        AccessoryMaxCounts[1] = 35;
        AccessoryMaxCounts[2] = 35;
        AccessoryMaxCounts[3] = 35;
        AccessoryMaxCounts[4] = 35;
        AccessoryMaxCounts[5] = 35;
        AccessoryMaxCounts[6] = 35;
        AccessoryMaxCounts[7] = 2113333333; // MAX

        //AccessoryMaxCounts[0] = 100;
        //AccessoryMaxCounts[1] = 600;
        //AccessoryMaxCounts[2] = 1500;
        //AccessoryMaxCounts[3] = 5000;
        //AccessoryMaxCounts[4] = 20000;
        //AccessoryMaxCounts[5] = 40000;
        //AccessoryMaxCounts[6] = 80000;
        //AccessoryMaxCounts[7] = 2113333333; // MAX

        AccessoryLevelProbilityValue_Common = new float[8];

        AccessoryLevelProbilityValue_Common[0] = 95.00f;      //  95.00%
        AccessoryLevelProbilityValue_Common[1] = 85.00f;      //  85.00%
        AccessoryLevelProbilityValue_Common[2] = 70.00f;      //  70.00%
        AccessoryLevelProbilityValue_Common[3] = 53.00f;      //  53.00%
        AccessoryLevelProbilityValue_Common[4] = 30.00f;      //  30.00%
        AccessoryLevelProbilityValue_Common[5] = 15.00f;      //  15.00%
        AccessoryLevelProbilityValue_Common[6] = 10.00f;      //  10.00%
        AccessoryLevelProbilityValue_Common[7] = 10.00f;      //  10.00%

        AccessoryLevelProbilityValue_Rare = new float[8];

        AccessoryLevelProbilityValue_Rare[0] = 4.80f;     //  99.80%
        AccessoryLevelProbilityValue_Rare[1] = 14.20f;    //  99.20%
        AccessoryLevelProbilityValue_Rare[2] = 28.00f;    //  98.00%
        AccessoryLevelProbilityValue_Rare[3] = 44.00f;    //  97.00%    
        AccessoryLevelProbilityValue_Rare[4] = 62.00f;    //  92.00%
        AccessoryLevelProbilityValue_Rare[5] = 71.18f;    //  86.18%
        AccessoryLevelProbilityValue_Rare[6] = 67.18f;    //  77.18%
        AccessoryLevelProbilityValue_Rare[7] = 67.18f;    //  77.18%

        AccessoryLevelProbilityValue_UnCommon = new float[8];

        AccessoryLevelProbilityValue_UnCommon[0] = 0.19f;         //  99.990%
        AccessoryLevelProbilityValue_UnCommon[1] = 0.75f;         //  99.950%
        AccessoryLevelProbilityValue_UnCommon[2] = 1.705f;        //  99.705%
        AccessoryLevelProbilityValue_UnCommon[3] = 2.40f;         //  99.400%
        AccessoryLevelProbilityValue_UnCommon[4] = 6.00f;         //  98.000%
        AccessoryLevelProbilityValue_UnCommon[5] = 10.80f;        //  96.980%
        AccessoryLevelProbilityValue_UnCommon[6] = 17.773f;       //  94.953%
        AccessoryLevelProbilityValue_UnCommon[7] = 17.773f;       //  94.953%

        AccessoryLevelProbilityValue_Artifact = new float[8];

        AccessoryLevelProbilityValue_Artifact[0] = 0.01f;     //  100%
        AccessoryLevelProbilityValue_Artifact[1] = 0.05f;     //  100%
        AccessoryLevelProbilityValue_Artifact[2] = 0.29f;     //  99.995%
        AccessoryLevelProbilityValue_Artifact[3] = 0.58f;     //  99.980%
        AccessoryLevelProbilityValue_Artifact[4] = 1.905f;    //  99.905%
        AccessoryLevelProbilityValue_Artifact[5] = 2.80f;     //  99.780%
        AccessoryLevelProbilityValue_Artifact[6] = 4.4f;      //  99.353%
        AccessoryLevelProbilityValue_Artifact[7] = 4.4f;      //  99.353%

        AccessoryLevelProbilityValue_Mythical = new float[8];

        AccessoryLevelProbilityValue_Mythical[0] = 0f;        //  100%
        AccessoryLevelProbilityValue_Mythical[1] = 0f;        //  100%
        AccessoryLevelProbilityValue_Mythical[2] = 0.004f;    //  99.999%
        AccessoryLevelProbilityValue_Mythical[3] = 0.018f;    //  99.998%
        AccessoryLevelProbilityValue_Mythical[4] = 0.09f;     //  99.995%
        AccessoryLevelProbilityValue_Mythical[5] = 0.21f;     //  99.990%
        AccessoryLevelProbilityValue_Mythical[6] = 0.63f;     //  99.983%
        AccessoryLevelProbilityValue_Mythical[7] = 0.63f;     //  99.983%

        AccessoryLevelProbilityValue_Legend = new float[8];

        AccessoryLevelProbilityValue_Legend[0] = 0f;          //  100%
        AccessoryLevelProbilityValue_Legend[1] = 0f;          //  100%
        AccessoryLevelProbilityValue_Legend[2] = 0.001f;      //  100%
        AccessoryLevelProbilityValue_Legend[3] = 0.002f;      //  100%
        AccessoryLevelProbilityValue_Legend[4] = 0.005f;      //  100%
        AccessoryLevelProbilityValue_Legend[5] = 0.01f;       //  100%
        AccessoryLevelProbilityValue_Legend[6] = 0.017f;      //  100%
        AccessoryLevelProbilityValue_Legend[7] = 0.017f;      //  100%


        // 악세서리 장착 경험치 증가
        AccessoryEquipExp[0] = 1;
        AccessoryEquipExp[1] = 2;
        AccessoryEquipExp[2] = 3;
        AccessoryEquipExp[3] = 4;

        AccessoryEquipExp[4] = 10;
        AccessoryEquipExp[5] = 15;
        AccessoryEquipExp[6] = 20;
        AccessoryEquipExp[7] = 30;

        AccessoryEquipExp[8] = 60;
        AccessoryEquipExp[9] = 80;
        AccessoryEquipExp[10] = 100;
        AccessoryEquipExp[11] = 120;

        AccessoryEquipExp[12] = 200;
        AccessoryEquipExp[13] = 250;
        AccessoryEquipExp[14] = 300;
        AccessoryEquipExp[15] = 350;

        AccessoryEquipExp[16] = 700;
        AccessoryEquipExp[17] = 1000;
        AccessoryEquipExp[18] = 1300;
        AccessoryEquipExp[19] = 1600;

        AccessoryEquipExp[20] = 3500;
        AccessoryEquipExp[21] = 5000;
        AccessoryEquipExp[22] = 6500;
        AccessoryEquipExp[23] = 8000;

        // 악세서리 보유 경험치 증가
        AccessoryOwnExp[0] = 0.1m;
        AccessoryOwnExp[1] = 0.2m;
        AccessoryOwnExp[2] = 0.3m;
        AccessoryOwnExp[3] = 0.4m;

        AccessoryOwnExp[4] = 1.0m;
        AccessoryOwnExp[5] = 1.5m;
        AccessoryOwnExp[6] = 2.0m;
        AccessoryOwnExp[7] = 2.5m;

        AccessoryOwnExp[8] = 5.0m;
        AccessoryOwnExp[9] = 7.0m;
        AccessoryOwnExp[10] = 9.0m;
        AccessoryOwnExp[11] = 11.0m;

        AccessoryOwnExp[12] = 33.0m;
        AccessoryOwnExp[13] = 43.0m;
        AccessoryOwnExp[14] = 48.0m;
        AccessoryOwnExp[15] = 53.0m;

        AccessoryOwnExp[16] = 150.0m;
        AccessoryOwnExp[17] = 170.0m;
        AccessoryOwnExp[18] = 190.0m;
        AccessoryOwnExp[19] = 210.0m;

        AccessoryOwnExp[20] = 600.0m;
        AccessoryOwnExp[21] = 750.0m;
        AccessoryOwnExp[22] = 900.0m;
        AccessoryOwnExp[23] = 1200.0m;

        // 악세서리 이름
        AccessoryTitle[0] = "<color=grey>황금 진주 반지</color>";
        AccessoryTitle[1] = "<color=grey>공허의 눈 반지</color>";
        AccessoryTitle[2] = "<color=grey>행운의 반지</color>";
        AccessoryTitle[3] = "<color=grey>빛나는 푸른 별</color>";
        AccessoryTitle[4] = "<color=orange>천사 반지</color>";
        AccessoryTitle[5] = "<color=orange>화염의 반지</color>";
        AccessoryTitle[6] = "<color=orange>태양의 반지</color>";
        AccessoryTitle[7] = "<color=orange>번개의 반지</color>";
        AccessoryTitle[8] = "<color=yellow>대지의 반지</color>";
        AccessoryTitle[9] = "<color=yellow>바람의 반지</color>";
        AccessoryTitle[10] = "<color=yellow>바다의 반지</color>";
        AccessoryTitle[11] = "<color=yellow>달빛의 반지</color>";
        AccessoryTitle[12] = "<color=lime>숲의 정령 반지</color>";
        AccessoryTitle[13] = "<color=lime>천둥의 반지</color>";
        AccessoryTitle[14] = "<color=lime>용암의 반지</color>";
        AccessoryTitle[15] = "<color=lime>서리의 반지</color>";
        AccessoryTitle[16] = "<color=darkblue>제우스의 반지</color>";
        AccessoryTitle[17] = "<color=darkblue>마드레드의 갈퀴</color>";
        AccessoryTitle[18] = "<color=darkblue>마드레드의 다중 갈퀴</color>";
        AccessoryTitle[19] = "<color=darkblue>강력 자석 반지</color>";
        AccessoryTitle[20] = "<color=red>미궁의 반지</color>";
        AccessoryTitle[21] = "<color=red>교황의 반지</color>";
        AccessoryTitle[22] = "<color=red>영웅의 반지</color>";
        AccessoryTitle[23] = "<color=red>악마의 반지</color>";


        // 권능
        Warrant_Power[0] = 2 + WarrantLevel[0];                    //  처치시 8% 확률로 경험치 (2 + N)배
        Warrant_Power[1] = 2 + WarrantLevel[1];                    //  공격시 2% 확률로 데미지 (2 + N)%
        Warrant_Power[2] = 2 + WarrantLevel[2] * 2;                //  공격시 (2 + N*2) % 확률로 데미지 2번더
        Warrant_Power[3] = 10 + WarrantLevel[3] * 2;               //  공격시 2% 확률로 최대체력 (10 * 2)%
        Warrant_Power[4] = 1 + WarrantLevel[4];                    //  유물 강화 파괴 확률 감소 (1 + N)%              
        Warrant_Power[5] = 20 + WarrantLevel[5] * 10;              //  공격시  (20 + N * 10)% 추가피해                
        Warrant_Power[6] = 5 + WarrantLevel[6] * 5;                //  적 체력 재생량 감소 (5 + N * 5)%               
        Warrant_Power[7] = 2 + WarrantLevel[7];                    //  채광시 2% 확률로 (2 + N)배 데미지              
        Warrant_Power[8] = 20 + WarrantLevel[8] * 10;              //  채광시 추가 피해  (20 + N * 10)%               
        Warrant_Power[9] = 10 + WarrantLevel[9] * 5;               //  채광시 광물 획득량이 (10 + N * 5)              
        Warrant_Power[10] = 3 + WarrantLevel[10] * 3;              //  무기 크리티컬 확률 증가 (3 + N * 3)%           
        Warrant_Power[11] = 2 + WarrantLevel[11];                  //  처치시 8% 확률로 골드 (2 + N)배                
        Warrant_Power[12] = 25 + WarrantLevel[12] * 25;            //  영구적으로 골드 (25 + N * 25)%   
        Warrant_Power[13] = 5 + WarrantLevel[13] * 3;              //  골드 상점 가격을 낮춰줌 (5 + N * 3)%           
        Warrant_Power[14] = 5 + WarrantLevel[14] * 3;              //  각종 뽑기 가격을 낮춰줌 (5 + N * 3)%           
        Warrant_Power[15] = 5 + WarrantLevel[15] * 3;              //  적 최대 체력 약화 (5 + N * 3)%                 
        Warrant_Power[16] = 2 + WarrantLevel[16];                  //  8% 확률로 강화석 획득 (2 + N)배                
        Warrant_Power[17] = 2 + WarrantLevel[17];                  //  8% 확률로 장비 획득 (2 + N)배                  
        Warrant_Power[18] = 3 + WarrantLevel[18] * 2;              //  플레이어가 레벨업 필요 경험치 하락 (2 + N * 2)%
        Warrant_Power[19] = 100 + WarrantLevel[19] * 50;            //  추가 경험치 획득량 (100 + N * 50)%              
        Warrant_Power[20] = 100 + WarrantLevel[20] * 50;            //  추가 골드 획득량 (100 + N * 50)%                
        Warrant_Power[21] = 1 + WarrantLevel[21];                  //  각종 뽑기를 공짜 (1 + N)%                      
        Warrant_Power[22] = 500 + WarrantLevel[22] * 500;          //  플레이어 공격력 증가 (500 + N * 500)           
        Warrant_Power[23] = 500 + WarrantLevel[23] * 500;          //  플레이어 크리티컬 데미지 증가 (500 + N * 500)  
        Warrant_Power[24] = 15 + WarrantLevel[24] * 5;             //  플레이어 공격력 증가 (15 + N * 5)%             
        Warrant_Power[25] = 5 + WarrantLevel[25];                  //  권능 구매 가격을 낮춰줌 (5 + N)%               
        Warrant_Power[26] = 1 + WarrantLevel[26];                  //  뽑기 광고 시청 제한이 (1 + N)%                 
        Warrant_Power[27] = 5 + WarrantLevel[27] * 5;              //  모든 데미지가 증가 (5 + N * 5)%                
        Warrant_Power[28] = 1 + WarrantLevel[28];                  //  처치시 (1 + N)% 확률로 강화스크롤 획득         



        InitializeDustRequirements();

        InitializeItemSprites();
    }
    

    private void InitializeDustRequirements()
    {
        
    }

    private void InitializeItemSprites()
    {
        itemSprites = new Sprite[GameConstants.WeaponNum];
        AccessorySprites = new Sprite[GameConstants.AccessoryNum];
        WarrantSprites = new Sprite[728];
        GemSprites = new Sprite[9];

        int nSpriteIndex = 0; // NWeapon 스프라이트는 15번째 인덱스부터 시작

        // 다른 스프라이트 로드
        Sprite[] nSpriteSheet = Resources.LoadAll<Sprite>("NWeapon");
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Weapon");
        Sprite[] AccessoryspriteSheet = Resources.LoadAll<Sprite>("Sample");
        Sprite[] warrantSheet = Resources.LoadAll<Sprite>("Warrant");
        Sprite[] gemSheet =
        {
            Resources.Load<Sprite>("Gem_01"),
            Resources.Load<Sprite>("Gem_02"),
            Resources.Load<Sprite>("Gem_03"),
            Resources.Load<Sprite>("Gem_04"),
            Resources.Load<Sprite>("Gem_05"),
            Resources.Load<Sprite>("Gem_06"),
            Resources.Load<Sprite>("Gem_07"),
            Resources.Load<Sprite>("Gem_08"),
            Resources.Load<Sprite>("Gem_09"),
        };

        // 스프라이트 추출
        for (int i = 0; i < GameConstants.WeaponNum - 9 && i < spriteSheet.Length; i++)
        {
            itemSprites[i] = spriteSheet[i];
            Debug.Log("로드된 스프라이트 경로: " + spriteSheet[i].name);
        }

        // 스프라이트 이어붙이기
        for (int i = 0; i < GameConstants.WeaponNum - 15 && nSpriteIndex < nSpriteSheet.Length; i++)
        {
            itemSprites[i + 15] = nSpriteSheet[nSpriteIndex];
            Debug.Log("로드된 스프라이트 경로: " + nSpriteSheet[nSpriteIndex].name);
            nSpriteIndex++;
        }

        for (int i = 0; i < GameConstants.AccessoryNum && i < AccessoryspriteSheet.Length; i++)
        {
            AccessorySprites[i] = AccessoryspriteSheet[i];
            Debug.Log("로드된 스프라이트 경로: " + AccessoryspriteSheet[i].name);
        }

        for(int i = 0; i < GameConstants.WarrantNum * 7; i++)
        {
            WarrantSprites[i] = warrantSheet[i];
        }

        for (int i = 0; i < 9; i++)
        {
            GemSprites[i] = gemSheet[i];
        }


        // GameManager.itemCounts 초기화
        itemCounts = new Dictionary<Sprite, int>();
        AccessoryCount = new Dictionary<Sprite, int>();

        foreach (Sprite itemSprite in itemSprites)
        {
            itemCounts[itemSprite] = 0;
        }

        foreach (Sprite itemSprite in AccessorySprites)
        {
            AccessoryCount[itemSprite] = 0;
        }

        if (itemCounts == null)
        {
            itemCounts = new Dictionary<Sprite, int>();

            // 24개의 항목을 추가
            for (int i = 0; i < GameConstants.WeaponNum; i++)
            {
                itemCounts.Add(itemSprites[i], 0);
            }
        }
    }
}
