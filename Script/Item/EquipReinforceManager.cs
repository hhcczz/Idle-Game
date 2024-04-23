using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   EquipReinforceManager.cs
 * Desc :   광고 관리
 *         
 *
 & Functions
 &  [Public]
 &  : ReinforceItem(int currentindex, int equipindex) - 장비 강화 값 리턴 시키기
 &  : ReinforceNeedTextUpdate(int currentindex, int equipindex) - 장비 강화 필요 수치 리턴 시키기
 &
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
public class EquipReinforceManager : MonoBehaviour
{
    public static int[] E_ClassNeedReinStone;          // 아이템 강화 필요 빨간 보석
    public static int[] D_ClassNeedReinStone;          // 아이템 강화 필요 빨간 보석
    public static int[] C_ClassNeedReinStone;          // 아이템 강화 필요 빨간 보석
    public static int[] B_ClassNeedReinStone;          // 아이템 강화 필요 빨간 보석
    public static int[] A_ClassNeedReinStone;          // 아이템 강화 필요 빨간 보석
    public static int[] S_ClassNeedReinStone;          // 아이템 강화 필요 빨간 보석

    public static int[] WeaponReinValue = new int[GameConstants.WeaponNum];             // 무기 강화 레벨
    public static int[] AccessoryReinValue = new int[GameConstants.AccessoryNum];          // 악세서리 강화 레벨

    public static int NeedReinStoneValue;       // 필요 빨간 보석 : 2

    public static int[] IncreaseWeaponValue = new int[GameConstants.WeaponNum];
    public static int[] IncreaseAccessoryValue = new int[GameConstants.AccessoryNum];

    private static int returnNum;
    private static int NeedTextReturn;

    // Start is called before the first frame update
    void Start()
    {   //  5   10 15 25  35  50  65  75  95  115 135 160 185 210 
        E_ClassNeedReinStone = new int[100];
        D_ClassNeedReinStone = new int[100];
        C_ClassNeedReinStone = new int[100];
        B_ClassNeedReinStone = new int[100];
        A_ClassNeedReinStone = new int[100];
        S_ClassNeedReinStone = new int[100];

        NeedReinStoneValue = 2;
        for (int i = 1; i < 101; i++)
        {
            if (i % 3 == 1) NeedReinStoneValue += 2;

            E_ClassNeedReinStone[i - 1] = NeedReinStoneValue * i;
            D_ClassNeedReinStone[i - 1] = NeedReinStoneValue * i * 2;
            C_ClassNeedReinStone[i - 1] = (int)((float)NeedReinStoneValue * i * 2.5);
            B_ClassNeedReinStone[i - 1] = (int)((float)NeedReinStoneValue * i * 3.5);
            A_ClassNeedReinStone[i - 1] = NeedReinStoneValue * i * 4;
            S_ClassNeedReinStone[i - 1] = NeedReinStoneValue * i * 5;
        }

        IncreaseWeaponValue[0] = 1;
        IncreaseWeaponValue[1] = 2;
        IncreaseWeaponValue[2] = 3;
        IncreaseWeaponValue[3] = 4;

        IncreaseWeaponValue[4] = 5;
        IncreaseWeaponValue[5] = 6;
        IncreaseWeaponValue[6] = 7;
        IncreaseWeaponValue[7] = 8;

        IncreaseWeaponValue[8] = 10;
        IncreaseWeaponValue[9] = 12;
        IncreaseWeaponValue[10] = 15;
        IncreaseWeaponValue[11] = 18;

        IncreaseWeaponValue[12] = 25;
        IncreaseWeaponValue[13] = 28;
        IncreaseWeaponValue[14] = 33;
        IncreaseWeaponValue[15] = 38;

        IncreaseWeaponValue[16] = 130;
        IncreaseWeaponValue[17] = 160;
        IncreaseWeaponValue[18] = 190;
        IncreaseWeaponValue[19] = 220;

        IncreaseWeaponValue[20] = 650;
        IncreaseWeaponValue[21] = 750;
        IncreaseWeaponValue[22] = 850;
        IncreaseWeaponValue[23] = 1500;

        IncreaseAccessoryValue[0] = 1;
        IncreaseAccessoryValue[1] = 2;
        IncreaseAccessoryValue[2] = 3;
        IncreaseAccessoryValue[3] = 4;
           
        IncreaseAccessoryValue[4] = 5;
        IncreaseAccessoryValue[5] = 6;
        IncreaseAccessoryValue[6] = 7;
        IncreaseAccessoryValue[7] = 8;
       
        IncreaseAccessoryValue[8] = 9;
        IncreaseAccessoryValue[9] = 10;
        IncreaseAccessoryValue[10] = 11;
        IncreaseAccessoryValue[11] = 12;
  
        IncreaseAccessoryValue[12] = 14;
        IncreaseAccessoryValue[13] = 16;
        IncreaseAccessoryValue[14] = 18;
        IncreaseAccessoryValue[15] = 20;
 
        IncreaseAccessoryValue[16] = 35;
        IncreaseAccessoryValue[17] = 40;
        IncreaseAccessoryValue[18] = 55;
        IncreaseAccessoryValue[19] = 60;

        IncreaseAccessoryValue[20] = 200;
        IncreaseAccessoryValue[21] = 350;
        IncreaseAccessoryValue[22] = 500;
        IncreaseAccessoryValue[23] = 800;

        

        returnNum = 0;
        NeedTextReturn = 0;
    }

    public static int ReinforceItem(int currentindex, int equipindex)
    {
        if (equipindex == 1)
        {
            if(currentindex >= 0 && currentindex <= 3)
            {
                if (GameManager.Player_RedStone >= E_ClassNeedReinStone[WeaponReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= E_ClassNeedReinStone[WeaponReinValue[currentindex]];
                    WeaponReinValue[currentindex]++;
                    GameManager.WeaponOwnDmg[currentindex] += (decimal)IncreaseWeaponValue[currentindex] / 10;
                    GameManager.WeaponEquipDmg[currentindex] += IncreaseWeaponValue[currentindex];
                    StatisticsManager.ImmutabilityWeaponReinforceCount++;


                    returnNum = WeaponReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 4 && currentindex <= 7)
            {
                if (GameManager.Player_RedStone >= D_ClassNeedReinStone[WeaponReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= D_ClassNeedReinStone[WeaponReinValue[currentindex]];
                    WeaponReinValue[currentindex]++;
                    GameManager.WeaponOwnDmg[currentindex] += (decimal)IncreaseWeaponValue[currentindex] / 10;
                    GameManager.WeaponEquipDmg[currentindex] += IncreaseWeaponValue[currentindex];
                    StatisticsManager.ImmutabilityWeaponReinforceCount++;

                    returnNum = WeaponReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 8 && currentindex <= 11)
            {
                if (GameManager.Player_RedStone >= C_ClassNeedReinStone[WeaponReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= C_ClassNeedReinStone[WeaponReinValue[currentindex]];
                    WeaponReinValue[currentindex]++;
                    GameManager.WeaponOwnDmg[currentindex] += (decimal)IncreaseWeaponValue[currentindex] / 10;
                    GameManager.WeaponEquipDmg[currentindex] += IncreaseWeaponValue[currentindex];
                    StatisticsManager.ImmutabilityWeaponReinforceCount++;

                    returnNum = WeaponReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 12 && currentindex <= 15)
            {
                if (GameManager.Player_RedStone >= B_ClassNeedReinStone[WeaponReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= B_ClassNeedReinStone[WeaponReinValue[currentindex]];
                    WeaponReinValue[currentindex]++;
                    GameManager.WeaponOwnDmg[currentindex] += (decimal)IncreaseWeaponValue[currentindex] / 10;
                    GameManager.WeaponEquipDmg[currentindex] += IncreaseWeaponValue[currentindex];
                    StatisticsManager.ImmutabilityWeaponReinforceCount++;

                    returnNum = WeaponReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 16 && currentindex <= 19)
            {
                if (GameManager.Player_RedStone >= A_ClassNeedReinStone[WeaponReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= A_ClassNeedReinStone[WeaponReinValue[currentindex]];
                    WeaponReinValue[currentindex]++;
                    GameManager.WeaponOwnDmg[currentindex] += (decimal)IncreaseWeaponValue[currentindex] / 10;
                    GameManager.WeaponEquipDmg[currentindex] += IncreaseWeaponValue[currentindex];
                    StatisticsManager.ImmutabilityWeaponReinforceCount++;

                    returnNum = WeaponReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 20 && currentindex <= 23)
            {
                if (GameManager.Player_RedStone >= S_ClassNeedReinStone[WeaponReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= S_ClassNeedReinStone[WeaponReinValue[currentindex]];
                    WeaponReinValue[currentindex]++;
                    GameManager.WeaponOwnDmg[currentindex] += (decimal)IncreaseWeaponValue[currentindex] / 10;
                    GameManager.WeaponEquipDmg[currentindex] += IncreaseWeaponValue[currentindex];
                    StatisticsManager.ImmutabilityWeaponReinforceCount++;

                    returnNum = WeaponReinValue[currentindex];
                }
                else return -1;
            }


        }
        else if (equipindex == 2)
        {
            if(currentindex >= 0 && currentindex <= 3)
            {
                if (GameManager.Player_RedStone >= E_ClassNeedReinStone[AccessoryReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= E_ClassNeedReinStone[AccessoryReinValue[currentindex]];
                    AccessoryReinValue[currentindex]++;
                    GameManager.AccessoryOwnExp[currentindex] += (decimal)IncreaseAccessoryValue[currentindex] / 10;
                    GameManager.AccessoryEquipExp[currentindex] += IncreaseAccessoryValue[currentindex];
                    StatisticsManager.ImmutabilityAccessoryReinforceCount++;

                    returnNum = AccessoryReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 4 && currentindex <= 7)
            {
                if (GameManager.Player_RedStone >= D_ClassNeedReinStone[AccessoryReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= D_ClassNeedReinStone[AccessoryReinValue[currentindex]];
                    AccessoryReinValue[currentindex]++;
                    GameManager.AccessoryOwnExp[currentindex] += (decimal)IncreaseAccessoryValue[currentindex] / 10;
                    GameManager.AccessoryEquipExp[currentindex] += IncreaseAccessoryValue[currentindex];
                    StatisticsManager.ImmutabilityAccessoryReinforceCount++;

                    returnNum = AccessoryReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 8 && currentindex <= 11)
            {
                if (GameManager.Player_RedStone >= C_ClassNeedReinStone[AccessoryReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= C_ClassNeedReinStone[AccessoryReinValue[currentindex]];
                    AccessoryReinValue[currentindex]++;
                    GameManager.AccessoryOwnExp[currentindex] += (decimal)IncreaseAccessoryValue[currentindex] / 10;
                    GameManager.AccessoryEquipExp[currentindex] += IncreaseAccessoryValue[currentindex];
                    StatisticsManager.ImmutabilityAccessoryReinforceCount++;

                    returnNum = AccessoryReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 12 && currentindex <= 15)
            {
                if (GameManager.Player_RedStone >= B_ClassNeedReinStone[AccessoryReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= B_ClassNeedReinStone[AccessoryReinValue[currentindex]];
                    AccessoryReinValue[currentindex]++;
                    GameManager.AccessoryOwnExp[currentindex] += (decimal)IncreaseAccessoryValue[currentindex] / 10;
                    GameManager.AccessoryEquipExp[currentindex] += IncreaseAccessoryValue[currentindex];
                    StatisticsManager.ImmutabilityAccessoryReinforceCount++;

                    returnNum = AccessoryReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 16 && currentindex <= 19)
            {
                if (GameManager.Player_RedStone >= A_ClassNeedReinStone[AccessoryReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= A_ClassNeedReinStone[AccessoryReinValue[currentindex]];
                    AccessoryReinValue[currentindex]++;
                    GameManager.AccessoryOwnExp[currentindex] += (decimal)IncreaseAccessoryValue[currentindex] / 10;
                    GameManager.AccessoryEquipExp[currentindex] += IncreaseAccessoryValue[currentindex];
                    StatisticsManager.ImmutabilityAccessoryReinforceCount++;

                    returnNum = AccessoryReinValue[currentindex];
                }
                else return -1;
            }
            if (currentindex >= 20 && currentindex <= 23)
            {
                if (GameManager.Player_RedStone >= S_ClassNeedReinStone[AccessoryReinValue[currentindex]])
                {
                    GameManager.Player_RedStone -= S_ClassNeedReinStone[AccessoryReinValue[currentindex]];
                    AccessoryReinValue[currentindex]++;
                    GameManager.AccessoryOwnExp[currentindex] += (decimal)IncreaseAccessoryValue[currentindex] / 10;
                    GameManager.AccessoryEquipExp[currentindex] += IncreaseAccessoryValue[currentindex];
                    StatisticsManager.ImmutabilityAccessoryReinforceCount++;

                    returnNum = AccessoryReinValue[currentindex];
                }
                else return -1;
            }
        }
        
        return returnNum;
    }


    public static int ReinforceNeedTextUpdate(int currentindex, int equipindex)
    {
        if (equipindex == 1)
        {
            if(currentindex >= 0 && currentindex <= 3) NeedTextReturn =     E_ClassNeedReinStone[WeaponReinValue[currentindex]];
            if (currentindex >= 4 && currentindex <= 7) NeedTextReturn =    D_ClassNeedReinStone[WeaponReinValue[currentindex]];
            if (currentindex >= 8 && currentindex <= 11) NeedTextReturn =   C_ClassNeedReinStone[WeaponReinValue[currentindex]];
            if (currentindex >= 12 && currentindex <= 15) NeedTextReturn =  B_ClassNeedReinStone[WeaponReinValue[currentindex]];
            if (currentindex >= 16 && currentindex <= 19) NeedTextReturn =  A_ClassNeedReinStone[WeaponReinValue[currentindex]];
            if (currentindex >= 20 && currentindex <= 23) NeedTextReturn =  S_ClassNeedReinStone[WeaponReinValue[currentindex]];

        }
        else if (equipindex == 2)
        {
            if (currentindex >= 0 && currentindex <= 3) NeedTextReturn =    E_ClassNeedReinStone[AccessoryReinValue[currentindex]];
            if (currentindex >= 4 && currentindex <= 7) NeedTextReturn =    D_ClassNeedReinStone[AccessoryReinValue[currentindex]];
            if (currentindex >= 8 && currentindex <= 11) NeedTextReturn =   C_ClassNeedReinStone[AccessoryReinValue[currentindex]];
            if (currentindex >= 12 && currentindex <= 15) NeedTextReturn =  B_ClassNeedReinStone[AccessoryReinValue[currentindex]];
            if (currentindex >= 16 && currentindex <= 19) NeedTextReturn =  A_ClassNeedReinStone[AccessoryReinValue[currentindex]];
            if (currentindex >= 20 && currentindex <= 23) NeedTextReturn =  S_ClassNeedReinStone[AccessoryReinValue[currentindex]];
        }

        return NeedTextReturn;
    }

}
