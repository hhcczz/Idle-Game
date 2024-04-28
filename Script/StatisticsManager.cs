using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * File :   StatisticsManager.cs
 * Desc :   통계 관리
 *         
 *
 & Functions
 &  [Public]
 &  : WeaponHaveNum() - 무기 몇개 가지고 있는지
 &  : AccessoryHaveNum() - 악세서리 몇개 가지고 있는지
 &  : WarrantHaveNum(int level) - 레벨에 따른 가지고 있는 권능 개수 파악
 &  : TrandHaveNum() - 초월 장비 몇개 가지고 있는지
 &  [Protected]
 &  : 
 &  : 
 &  : 
 &  : 
 &
 &  [Private]
 &  : 
 *
 */

public class StatisticsManager : MonoBehaviour
{
    public static int ImmutabilityAccessoryCount;                           //  악세서리 뽑은 횟수
    public static int ImmutabilityWeaponCount;                              //  무기 뽑은 횟수

    public static int ImmutabilityAccessoryReinforceCount;                  //  악세러리 강화 시도 횟수
    public static int ImmutabilityWeaponReinforceCount;                     //  무기 강화 시도 횟수

    public static int ImmutabilityPickaxeUpgradeCount;                      //  곡괭이 강화 시도 횟수
    public static int ImmutabilityMineralUpgradeCount;                      //  곡괭이 강화 시도 횟수
    public static int ImmutabilityOptionUpgradeCount;                       //  곡괭이 강화 시도 횟수
    public static int ImmutabilityMineClickCount;                           //  광산 클릭 횟수
    public static int ImmutabilityMineBreakCount;                           //  광물 클리어 횟수

    public static int RelicsTry;                                            //  유물 강화 시도 횟수
  
    
    public static int WeaponHaveNum()
    {
        int num = 0;

        for(int i = 0; i < GameConstants.WeaponNum; i++)
        {
            int index = i;

            if (GameManager.WeaponDrawn[index] == true) num++;
        }

        return num;
    }

    public static int AccessoryHaveNum()
    {
        int num = 0;

        for (int i = 0; i < GameConstants.AccessoryNum; i++)
        {
            int index = i;

            if (GameManager.AccessoryDrawn[index] == true) num++;
        }

        return num;
    }

    public static int WarrantHaveNum(int level)
    {
        int num = 0;

        for(int i = 0; i < GameConstants.WarrantNum; i++)
        {
            int index = i;

            if (GameManager.WarrantLevel[index] >= level) num++;
        }

        return num;
    }
    public static int TrandHaveNum()
    {
        int num = 0;

        for (int i = 0; i < 8; i++)
        {
            int index = i;

            if (GameManager.TrandOwned[index] == true) num++;
        }

        return num;
    }
}

