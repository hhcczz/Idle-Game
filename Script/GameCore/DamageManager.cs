using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static decimal Damage(string name)
    {
        decimal Damage = GameManager.Player_Damage + GameManager.Player_MoneyUp_Damage + GameManager.WeaponEquipDamage + MobScrollManager.MS_UpPower + GameManager.Warrant_Power[22] - 500;
        decimal PlusDamage = GameManager.WeaponOwnDamage + GameManager.Player_RelicsDamageAmplification;
        decimal CriticalDamage = GameManager.Player_CriticalDamage + GameManager.Player_MoneyUp_CriticalDamage + GameManager.Warrant_Power[23] - 500;

        if (GameManager.WarrantLevel[24] >= 1) Damage *= (decimal)(1 + (float)GameManager.Warrant_Power[24] / 100f);

        if (name == "일반") Damage *= 1 + (PlusDamage / 100m);
        else if (name == "크리티컬")
        {
            Damage *= 1.5m * CriticalDamage;
            Damage *= 1 + (PlusDamage / 100m);
        }

        return Damage;
    }
}
