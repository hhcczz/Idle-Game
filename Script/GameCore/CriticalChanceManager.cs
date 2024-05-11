using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalChanceManager : MonoBehaviour
{

    public static decimal CriticalChance()
    {
        decimal criticalchance = GameManager.Player_CriticalChance + GameManager.Player_MoneyUp_CriticalChance;

        if (GameManager.WarrantLevel[10] >= 1) criticalchance += GameManager.Warrant_Power[10];

        return criticalchance;
    }
}
