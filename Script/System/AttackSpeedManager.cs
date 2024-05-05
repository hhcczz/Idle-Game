using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedManager : MonoBehaviour
{
    public static decimal AttackSpeed()
    {
        decimal attackspeed = GameManager.Player_AttackSpeed + GameManager.Player_MoneyUp_AttackSpeed + (decimal)MobScrollManager.MS_UpAttackSpeed;


        return attackspeed;
    }
}
