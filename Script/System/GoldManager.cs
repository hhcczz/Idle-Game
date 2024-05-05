using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{


    [System.Obsolete]
    public static long Gold(int MonsterValue)
    {
        int Warrant_Random = Random.Range(0, 100);  // 골드 N배 확률

        long gold = (long)(MonsterValue * (1 + ((float)GameManager.Player_MoneyUp_EarnMoney + MobScrollManager.MS_UpEarnGold) / 100f));

        if (GameManager.WarrantLevel[20] >= 1) gold *= (long)(1 + (float)GameManager.Warrant_Power[12] / 100f);

        if (AdManager.AdPlaying[1] == true) gold *= (long)((AdManager.AdPowerValue[1] + 100) / 100f);

        if (GameManager.WarrantLevel[11] >= 1 && Warrant_Random < 8) gold *= (int)(1 + (float)GameManager.Warrant_Power[11] / 100);

        return gold;
    }
}
