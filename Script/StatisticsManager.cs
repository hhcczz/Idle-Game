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

public class StatisticsManager : MonoBehaviour
{
    public static int ImmutabilityAccessoryCount;                           //  악세서리 뽑은 횟수
    public static int ImmutabilityWeaponCount;                              //  무기 뽑은 횟수

    public static int ImmutabilityAccessoryReinforceCount;                  //  악세러리 강화 시도 횟수
    public static int ImmutabilityWeaponReinforceCount;                     //  무기 강화 시도 횟수

    public static int RelicsTry;                                            //  유물 강화 시도 횟수
    
}
