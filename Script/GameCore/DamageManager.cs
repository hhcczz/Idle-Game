using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static decimal Damage(string name)
    {
        decimal Damage;

        if(GameManager.TrandOwned[0] == true) Damage = (GameManager.Player_Damage + GameManager.Player_MoneyUp_Damage) * 3 + GameManager.WeaponEquipDamage + MobScrollManager.MS_UpPower + GameManager.Warrant_Power[22] - 500;
        else Damage = GameManager.Player_Damage + GameManager.Player_MoneyUp_Damage + GameManager.WeaponEquipDamage + MobScrollManager.MS_UpPower + GameManager.Warrant_Power[22] - 500;

        if (GameManager.TrandOwned[6] == true) Damage += 5000;

        decimal PlusDamage = GameManager.WeaponOwnDamage + GameManager.Player_RelicsDamageAmplification;
        decimal CriticalDamage = GameManager.Player_CriticalDamage + GameManager.Player_MoneyUp_CriticalDamage + GameManager.Warrant_Power[23] - 500;

        if (AdManager.AdPlaying[0] == true && AdManager.AdPlaying != null) Damage *= (AdManager.AdPowerValue[0] + 100) / 100m;
        if (GameManager.WarrantLevel[24] >= 1) Damage *= (decimal)(1 + (float)GameManager.Warrant_Power[24] / 100f);

        if (name == "일반") Damage *= 1 + (PlusDamage / 100m);
        else if (name == "크리티컬")
        {
            Damage *= 1.5m * CriticalDamage;
            Damage *= 1 + (PlusDamage / 100m);
        }
        else if (name == "크리티컬데미지") Damage = CriticalDamage;

        decimal[] multipliers = { 0.5m, 1.0m, 2.0m, 4.0m };
        decimal sum = 0;
        for (int i = 0; i < multipliers.Length; i++)
        {
            if (GameManager.PackageBuyCheck[i])
            {
                sum += multipliers[i];
            }
        }

        Damage *= 1 + sum;

        if (GameManager.TrandOwned[1] == true) Damage *= 2;

        return Damage;
    }
}
