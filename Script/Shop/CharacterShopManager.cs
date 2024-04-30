using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterShopManager : MonoBehaviour
{
    public Button Player_Upgrade_DamageLevelBtn;
    public Button Player_Upgrade_AttackSpeedLevelBtn;
    public Button Player_Upgrade_Critical_PercentLevelBtn;
    public Button Player_Upgrade_Critical_DamageLevelBtn;
    public Button Player_Upgrade_ArmorPenetrationLevelBtn;

    public Text Player_CurPassivePoint_Text;

    public Text Player_PPUP_Damage_Level;
    public Text Player_PPUP_Damage_Left;
    public Text Player_PPUP_Damage_Right;

    public Text Player_PPUP_AttackSpeed_Level;
    public Text Player_PPUP_AttackSpeed_Left;
    public Text Player_PPUP_AttackSpeed_Right;

    public Text Player_PPUP_CriticalChance_Level;
    public Text Player_PPUP_CriticalChance_Left;
    public Text Player_PPUP_CriticalChance_Right;

    public Text Player_PPUP_CriticalDamage_Level;
    public Text Player_PPUP_CriticalDamage_Left;
    public Text Player_PPUP_CriticalDamage_Right;

    public Text Player_PPUP_ArmorPenetration_Level;
    public Text Player_PPUP_ArmorPenetration_Left;
    public Text Player_PPUP_ArmorPenetration_Right;

    public GameObject lackBG;
    public Button LackOutBtn;

    private bool holding = false;
    private bool holdingplay = false;
    private int holdIndex = -1;
    private float holdDuration = 0.5f; // 버튼을 누른 상태로 유지할 시

    private decimal[] IncreaseValue;

    private void Start()
    {

        // PP 사용 업그레이드 버튼 리스너
        Player_Upgrade_DamageLevelBtn.onClick.AddListener(() => PPUpgrade(0));

        Player_Upgrade_AttackSpeedLevelBtn.onClick.AddListener(() => PPUpgrade(1));

        Player_Upgrade_Critical_PercentLevelBtn.onClick.AddListener(() => PPUpgrade(2));

        Player_Upgrade_Critical_DamageLevelBtn.onClick.AddListener(() => PPUpgrade(3));

        Player_Upgrade_ArmorPenetrationLevelBtn.onClick.AddListener(() => PPUpgrade(4));

        LackOutBtn.onClick.AddListener(LackOut);


        // 증가값
        IncreaseValue = new decimal[5]
        {
            1.5m,
            0.01m,
            0.2m,
            0.5m,
            1m,
        };

        for(int i = 0; i < 5; i++)
        {
            int index = i;

            TextUpdate(index);
        }


    }

    private void Update()
    {
        if (holding)
        {
            holdDuration -= Time.deltaTime;
            if (holdDuration <= 0)
            {
                holdingplay = true;
            }
            if(holdingplay == true) PPUpgrade(holdIndex);
        }
    }

    private void TextUpdate(int index)
    {
        if(index == 0)
        {
            Player_PPUP_Damage_Level.text = "Lv. " + GameManager.Player_PPUP_DamageLevel;
            Player_PPUP_Damage_Left.text = GameManager.Player_Damage + "";
            Player_PPUP_Damage_Right.text = GameManager.Player_Damage + IncreaseValue[index] + "";
        }
        else if (index == 1)
        {
            Player_PPUP_AttackSpeed_Level.text = "Lv. " + GameManager.Player_PPUP_AttackSpeedLevel;
            Player_PPUP_AttackSpeed_Left.text = GameManager.Player_AttackSpeed + "%";
            Player_PPUP_AttackSpeed_Right.text = GameManager.Player_AttackSpeed + IncreaseValue[index] + "%";

        }
        else if(index == 2)
        {
            Player_PPUP_CriticalChance_Level.text = "Lv. " + GameManager.Player_PPUP_CriticalChanceLevel;
            Player_PPUP_CriticalChance_Left.text = GameManager.Player_CriticalChance + "%";
            Player_PPUP_CriticalChance_Right.text = GameManager.Player_CriticalChance + IncreaseValue[index] + "%";
        }
        else if (index == 3)
        {
            Player_PPUP_CriticalDamage_Level.text = "Lv. " + GameManager.Player_PPUP_CriticalDamageLevel;
            Player_PPUP_CriticalDamage_Left.text = GameManager.Player_CriticalDamage + "";
            Player_PPUP_CriticalDamage_Right.text = GameManager.Player_CriticalDamage + IncreaseValue[index] + "";
        }
        else if (index == 4)
        {
            Player_PPUP_ArmorPenetration_Level.text = "Lv. " + GameManager.Player_PPUP_ArmorPenetrationLevel;
            Player_PPUP_ArmorPenetration_Left.text = GameManager.Player_ArmorPenetration + "";
            Player_PPUP_ArmorPenetration_Right.text = GameManager.Player_ArmorPenetration + (int)IncreaseValue[index] + "";
        }
        Player_CurPassivePoint_Text.text = "PP  :  " + GameManager.Player_PassivePoint;
    }

    private void PPUpgrade(int index)
    {
        holdIndex = index;
        if (GameManager.Player_PassivePoint >= 1)
        {
            GameManager.Player_PassivePoint -= 1;
            //  플레이어 데미지 업그레이드 | PP 사용
            if (index == 0) 
            {
                GameManager.Player_PPUP_DamageLevel += 1;
                GameManager.Player_Damage += IncreaseValue[index];
            }
            //  플레이어 공격속도 업그레이드 | PP 사용
            else if (index == 1)
            {
                GameManager.Player_PPUP_AttackSpeedLevel += 1;
                GameManager.Player_AttackSpeed += IncreaseValue[index];
            }
            //  플레이어 치명타 확률 업그레이드 | PP 사용
            else if (index == 2)
            {
                GameManager.Player_PPUP_CriticalChanceLevel += 1;
                GameManager.Player_CriticalChance += IncreaseValue[index];
            }
            //  플레이어 치명타 데미지 업그레이드 | PP 사용
            else if (index == 3)
            {
                GameManager.Player_PPUP_CriticalDamageLevel += 1;
                GameManager.Player_CriticalDamage += IncreaseValue[index];
            }
            //  플레이어 방어력 관통 업그레이드 | PP 사용
            else if (index == 4)
            {
                GameManager.Player_PPUP_ArmorPenetrationLevel += 1;
                GameManager.Player_ArmorPenetration += (int)IncreaseValue[index];
            }
            TextUpdate(index);
        }
        else LackOpen();
    }

    //  PP 부족 패널 띄우기
    private void LackOpen()
    {
        lackBG.SetActive(true);

        return;
    }

    //  PP 부족 패널 닫기
    private void LackOut()
    {
        lackBG.SetActive(false);
    }

    public void StartHold(int index)
    {
        holdDuration = 0.5f;
        holdIndex = index;
        holding = true;
    }

    public void EndHold()
    {
        holding = false;
        holdingplay = false;
    }
}
