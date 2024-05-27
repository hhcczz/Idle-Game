using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMoneyShopManager : MonoBehaviour
{
    private static CharacterMoneyShopManager instance;

    public static CharacterMoneyShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterMoneyShopManager>();
                if (instance == null)
                {
                    Debug.LogError("BasicCharacterInfo 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }

    public GameObject lackBG;
    public Button lackoutBtn;

    public Button[] MoneyUpBtn;
    public Text[] MoneyUpBtnText;
    private long SuperNeedMoney = 2;

    public Text Player_MoneyUp_EarnMoneyLevel;
    public Text Player_MoneyUp_EarnMoneyTextLeft;
    public Text Player_MoneyUp_EarnMoneyTextRight;

    public Text Player_MoneyUp_DamageLevel;
    public Text Player_MoneyUp_DamageTextLeft;
    public Text Player_MoneyUp_DamageTextRight;

    public Text Player_MoneyUp_AttackSpeedLevel;
    public Text Player_MoneyUp_AttackSpeedTextLeft;
    public Text Player_MoneyUp_AttackSpeedTextRight;

    public Text Player_MoneyUp_CriticalChanceLevel;
    public Text Player_MoneyUp_CriticalChanceTextLeft;
    public Text Player_MoneyUp_CriticalChanceTextRight;

    public Text Player_MoneyUp_CriticalDamageLevel;
    public Text Player_MoneyUp_CriticalDamageTextLeft;
    public Text Player_MoneyUp_CriticalDamageTextRight;

    public Text Player_MoneyUp_ArmorPenetrationLevel;
    public Text Player_MoneyUp_ArmorPenetrationTextLeft;
    public Text Player_MoneyUp_ArmorPenetrationTextRight;

    public Text[] Player_MoneyUp_MaxLevel;

    public long needMoney = 0;

    private bool holding = false;
    private bool holdingplay = false;
    private int holdIndex = -1;
    private float holdDuration = 0.5f; // 버튼을 누른 상태로 유지할 시

    private decimal[] IncreaseValue;
    private int[] MaxValue;

    // Start is called before the first frame update
    void Start()
    {

        lackoutBtn.onClick.AddListener(LackOut);



        // 증가값
        IncreaseValue = new decimal[6]
        {
            2.3m,
            1.1m,
            0.005m,
            0.02m,
            0.25m,
            0.5m,
        };

        MaxValue = new int[6]
        {
            99999,
            99999,
            200,
            2000,
            99999,
            500,
        };

        for(int i = 0; i < Player_MoneyUp_MaxLevel.Length; i++)
        {
            int index = i;

            Player_MoneyUp_MaxLevel[index].text = "MAX : Lv. " + TextFormatter.GetThousandCommaText(MaxValue[index]);
        }

        for (int i = 0; i < MoneyUpBtn.Length; i++)
        {
            int index = i;

            MoneyUpBtn[index].onClick.AddListener(() => MoneyUp(index));
        }

        for (int i = 0; i < GameManager.NeedMoney.Length; i++)
        {
            int index = i;

            if (index == 0)
            {
                GameManager.NeedMoney[index] = 1;
                SuperNeedMoney = 2; // 초기화
            }
            else
            {
                GameManager.NeedMoney[index] = (GameManager.NeedMoney[index - 1] + 2 * index) + SuperNeedMoney;
                if (index % 10 == 0)
                {
                    // 10의 배수일 때 SuperNeedMoney를 증가시키는데, 너무 크게 증가하지 않도록 수정
                    SuperNeedMoney += index / 8; // 예시로 증가폭을 index/5로 설정
                }
            }
        }

        for (int i = 0; i < MoneyUpBtn.Length; i++)
        {
            int index = i;
            
            MoneyUpTextUpdate(index);
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
            if (holdIndex == 0 && GameManager.Player_MoneyUp_EarnMoneyLevel >= MaxValue[holdIndex] + 1) EndHold();
            else if (holdIndex == 1 && GameManager.Player_MoneyUp_DamageLevel >= MaxValue[holdIndex] + 1) EndHold();
            else if (holdIndex == 2 && GameManager.Player_MoneyUp_AttackSpeedLevel >= MaxValue[holdIndex] + 1) EndHold();
            else if (holdIndex == 3 && GameManager.Player_MoneyUp_CriticalChanceLevel >= MaxValue[holdIndex] + 1) EndHold();
            else if (holdIndex == 4 && GameManager.Player_MoneyUp_CriticalDamageLevel >= MaxValue[holdIndex] + 1) EndHold();
            else if (holdIndex == 5 && GameManager.Player_MoneyUp_ArmorPenetrationLevel >= MaxValue[holdIndex] + 1) EndHold();

            if (holdingplay == true) MoneyUp(holdIndex);
        }
    }

    private void LackOut()
    {
        lackBG.SetActive(false);
    }

    private void LackMoney()
    {

        lackBG.SetActive(true);
        return;
    }

    private void MoneyUp(int index)
    {
        holdIndex = index;
        if (GameManager.WarrantLevel[13] >= 1) needMoney = (long)(GameManager.NeedMoney[GameManager.NeedMoney_Level[index]] * (float)(1 - GameManager.Warrant_Power[13] / 100f));
        else                                needMoney = GameManager.NeedMoney[GameManager.NeedMoney_Level[index]];

        // 골드 획득량 증가
        if (index == 0 && needMoney <= GameManager.Player_Money)
        { 
            GameManager.Player_MoneyUp_EarnMoneyLevel++;                                                                                            //  골드 획득량 레벨 증가 Value
            GameManager.Player_MoneyUp_EarnMoney += (long)IncreaseValue[index];                                                                     //  골드 획득량 증가 Value
            if (GameManager.Player_MoneyUp_EarnMoneyLevel >= MaxValue[index]) MoneyUpBtn[index].interactable = false;
        }
        // 공격력 증가
        else if (index == 1 && needMoney <= GameManager.Player_Money)
        {                                                                                                                                           
            GameManager.Player_MoneyUp_DamageLevel++;                                                                                               //  공격력 레벨 증가 Value
            GameManager.Player_MoneyUp_Damage += IncreaseValue[index];                                                                              //  공격력 증가 Value
            if (GameManager.Player_MoneyUp_DamageLevel >= MaxValue[index]) MoneyUpBtn[index].interactable = false;
        }
        // 공격속도 증가
        else if (index == 2 && needMoney <= GameManager.Player_Money)
        {                                                                                                                                           
            GameManager.Player_MoneyUp_AttackSpeedLevel++;                                                                                          //  공격 속도 레벨 증가 Value
            GameManager.Player_MoneyUp_AttackSpeed += IncreaseValue[index];                                                                         //  공격 속도 증가 Value
            if (GameManager.Player_MoneyUp_AttackSpeedLevel >= MaxValue[index]) MoneyUpBtn[index].interactable = false;
        }
        // 크리티컬 확률 증가
        else if (index == 3 && needMoney <= GameManager.Player_Money)
        {                                                                                                                                           
            GameManager.Player_MoneyUp_CriticalChanceLevel++;                                                                                       //  크리티컬 확률 레벨 증가 Value
            GameManager.Player_MoneyUp_CriticalChance += IncreaseValue[index];                                                                      //  크리티컬 확률 증가 Value
            if (GameManager.Player_MoneyUp_CriticalChanceLevel >= MaxValue[index]) MoneyUpBtn[index].interactable = false;
        }
        // 크리티컬 데미지 증가
        else if (index == 4 && needMoney <= GameManager.Player_Money)
        {                                                                                                                                           
            GameManager.Player_MoneyUp_CriticalDamageLevel++;                                                                                       //  크리티컬 데미지 레벨 증가 Value
            GameManager.Player_MoneyUp_CriticalDamage += IncreaseValue[index];                                                                      //  크리티컬 데미지 증가 Value
            if (GameManager.Player_MoneyUp_CriticalDamageLevel >= MaxValue[index]) MoneyUpBtn[index].interactable = false;
        }
        // 방어구 관통력 증가
        else if (index == 5 && needMoney <= GameManager.Player_Money)
        {                                                                                                                                           
            GameManager.Player_MoneyUp_ArmorPenetrationLevel++;                                                                                     //  방어력 관통 레벨 증가 Value
            GameManager.Player_MoneyUp_ArmorPenetration += IncreaseValue[index];                                                                    //  방어력 관통 증가 Value
            if (GameManager.Player_MoneyUp_ArmorPenetrationLevel >= MaxValue[index]) MoneyUpBtn[index].interactable = false;
        }
        else
        {
            LackMoney();
            return;
        }
        GameManager.NeedMoney_Level[index]++;
        GameManager.Player_Money -= needMoney;       //  골드 차감 Value

        MoneyUpTextUpdate(index);
    }

    private int ReturnFontSize(decimal Value)
    {
        if (Value <= 1000) return 43;
        else if (Value <= 10000) return 40;
        else if (Value <= 100000) return 38;
        else if (Value <= 1000000) return 36;
        else if (Value <= 10000000) return 34;
        else  return 32;
    }


    public void MoneyUpTextUpdate(int index)
    {
        if (GameManager.WarrantLevel[13] >= 1) needMoney = (long)(GameManager.NeedMoney[GameManager.NeedMoney_Level[index]] * (float)(1 - GameManager.Warrant_Power[13] / 100f));
        else needMoney = GameManager.NeedMoney[GameManager.NeedMoney_Level[index]];

        if (index == 0)
        {
            if (GameManager.Player_MoneyUp_EarnMoneyLevel >= MaxValue[index])
            {
                Player_MoneyUp_EarnMoneyLevel.text = "<color=cyan>Lv. MAX</color>";                                                                                         //  골드 획득량 레벨 Text
                Player_MoneyUp_EarnMoneyTextLeft.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_EarnMoney) + "%";                                     //  골드 획득량 증가 Value Left
                Player_MoneyUp_EarnMoneyTextRight.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_EarnMoney) + "%";                                    //  골드 획득량 증가 Value Right

                Player_MoneyUp_EarnMoneyTextLeft.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_EarnMoney);
                Player_MoneyUp_EarnMoneyTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_EarnMoney);
            }
            else
            {
                Player_MoneyUp_EarnMoneyLevel.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_MoneyUp_EarnMoneyLevel);                                 //  골드 획득량 레벨 Text
                Player_MoneyUp_EarnMoneyTextLeft.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_EarnMoney) + "%";                                     //  골드 획득량 증가 Value Left
                Player_MoneyUp_EarnMoneyTextRight.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_EarnMoney + (long)IncreaseValue[index]) + "%";       //  골드 획득량 증가 Value Right

                Player_MoneyUp_EarnMoneyTextLeft.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_EarnMoney);
                Player_MoneyUp_EarnMoneyTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_EarnMoney);
            }
        }
        else if(index == 1)
        {
            if (GameManager.Player_MoneyUp_DamageLevel >= MaxValue[index])
            {
                Player_MoneyUp_DamageLevel.text = "<color=cyan>Lv. MAX</color>";                                //  공격력 레벨 Text
                Player_MoneyUp_DamageTextLeft.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_Damage) + "";                                      //  공격력 증가 Value Left
                Player_MoneyUp_DamageTextRight.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_Damage) + "";                                     //  공격력 증가 Value Right

                Player_MoneyUp_DamageTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_Damage);
                Player_MoneyUp_DamageTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_Damage);
            }
            else
            {
                Player_MoneyUp_DamageLevel.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_MoneyUp_DamageLevel);                                       //  공격력 레벨 Text
                Player_MoneyUp_DamageTextLeft.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_Damage) + "";                                      //  공격력 증가 Value Left
                Player_MoneyUp_DamageTextRight.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_Damage + IncreaseValue[index]) + "";              //  공격력 증가 Value Right

                Player_MoneyUp_DamageTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_Damage);
                Player_MoneyUp_DamageTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_Damage);
            }
        }
        else if (index == 2)
        {
            if (GameManager.Player_MoneyUp_AttackSpeedLevel >= MaxValue[index])
            {
                Player_MoneyUp_AttackSpeedLevel.text = "<color=cyan>Lv. MAX</color>";                        //  공격 속도 레벨 Text
                Player_MoneyUp_AttackSpeedTextLeft.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_AttackSpeed) + "%";                          //  공격 속도 증가 Value Left
                Player_MoneyUp_AttackSpeedTextRight.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_AttackSpeed) + "%";                         //  공격 속도 증가 Value Right

                Player_MoneyUp_AttackSpeedTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_AttackSpeed);
                Player_MoneyUp_AttackSpeedTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_AttackSpeed);
            }
            else
            {
                Player_MoneyUp_AttackSpeedLevel.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_MoneyUp_AttackSpeedLevel);                             //  공격 속도 레벨 Text
                Player_MoneyUp_AttackSpeedTextLeft.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_AttackSpeed) + "%";                          //  공격 속도 증가 Value Left
                Player_MoneyUp_AttackSpeedTextRight.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_AttackSpeed + IncreaseValue[index]) + "%";  //  공격 속도 증가 Value Right

                Player_MoneyUp_AttackSpeedTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_AttackSpeed);
                Player_MoneyUp_AttackSpeedTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_AttackSpeed);
            }
        }
        else if (index == 3)
        {
            if (GameManager.Player_MoneyUp_CriticalChanceLevel >= MaxValue[index])
            {
                Player_MoneyUp_CriticalChanceLevel.text = "<color=cyan>Lv. MAX</color>";                 //  크리티컬 확률 레벨 Text
                Player_MoneyUp_CriticalChanceTextLeft.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_CriticalChance) + "%";                             //  크리티컬 확률 증가 Value Left
                Player_MoneyUp_CriticalChanceTextRight.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_CriticalChance) + "%";                            //  크리티컬 확률 증가 Value Right

                Player_MoneyUp_CriticalChanceTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_CriticalChance);
                Player_MoneyUp_CriticalChanceTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_CriticalChance);
            }
            else
            {
                Player_MoneyUp_CriticalChanceLevel.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_MoneyUp_CriticalChanceLevel);                               //  크리티컬 확률 레벨 Text
                Player_MoneyUp_CriticalChanceTextLeft.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_CriticalChance) + "%";                             //  크리티컬 확률 증가 Value Left
                Player_MoneyUp_CriticalChanceTextRight.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_CriticalChance + IncreaseValue[index]) + "%";     //  크리티컬 확률 증가 Value Right

                Player_MoneyUp_CriticalChanceTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_CriticalChance);
                Player_MoneyUp_CriticalChanceTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_CriticalChance);
            }
        }
        else if (index == 4)
        {
            if (GameManager.Player_MoneyUp_CriticalDamageLevel >= MaxValue[index])
            {
                Player_MoneyUp_CriticalDamageLevel.text = "<color=cyan>Lv. MAX</color>";                  //  크리티컬 데미지 레벨 Text
                Player_MoneyUp_CriticalDamageTextLeft.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_CriticalDamage) + "";                              //  크리티컬 데미지 증가 Value Left
                Player_MoneyUp_CriticalDamageTextRight.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_CriticalDamage + IncreaseValue[index]) + "";      //  크리티컬 데미지 증가 Value Right

                Player_MoneyUp_CriticalDamageTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_CriticalDamage);
                Player_MoneyUp_CriticalDamageTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_CriticalDamage);
            }
            else
            {
                Player_MoneyUp_CriticalDamageLevel.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_MoneyUp_CriticalDamageLevel);                               //  크리티컬 데미지 레벨 Text
                Player_MoneyUp_CriticalDamageTextLeft.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_CriticalDamage) + "";                              //  크리티컬 데미지 증가 Value Left
                Player_MoneyUp_CriticalDamageTextRight.text = TextFormatter.GetDecimalPointCommaText_00(GameManager.Player_MoneyUp_CriticalDamage + IncreaseValue[index]) + "";      //  크리티컬 데미지 증가 Value Right

                Player_MoneyUp_CriticalDamageTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_CriticalDamage);
                Player_MoneyUp_CriticalDamageTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_CriticalDamage);
            }
        }
        else if (index == 5)
        {
            if (GameManager.Player_MoneyUp_ArmorPenetrationLevel >= MaxValue[index])
            {
                Player_MoneyUp_ArmorPenetrationLevel.text = "<color=cyan>Lv. MAX</color>";               //  방어력 관통 레벨 Text
                Player_MoneyUp_ArmorPenetrationTextLeft.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_ArmorPenetration) + "";                                //  방어력 관통 증가 Value Left
                Player_MoneyUp_ArmorPenetrationTextRight.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_ArmorPenetration + (int)IncreaseValue[index]) + "";   //  방어력 관통 증가 Value Right

                Player_MoneyUp_ArmorPenetrationTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_ArmorPenetration);
                Player_MoneyUp_ArmorPenetrationTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_ArmorPenetration);
            }
            else
            {
                Player_MoneyUp_ArmorPenetrationLevel.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_MoneyUp_ArmorPenetrationLevel);                           //  방어력 관통 레벨 Text
                Player_MoneyUp_ArmorPenetrationTextLeft.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_ArmorPenetration) + "";                                //  방어력 관통 증가 Value Left
                Player_MoneyUp_ArmorPenetrationTextRight.text = TextFormatter.GetDecimalPointCommaText_0(GameManager.Player_MoneyUp_ArmorPenetration + (int)IncreaseValue[index]) + "";   //  방어력 관통 증가 Value Right

                Player_MoneyUp_ArmorPenetrationTextLeft.fontSize  = ReturnFontSize(GameManager.Player_MoneyUp_ArmorPenetration);
                Player_MoneyUp_ArmorPenetrationTextRight.fontSize = ReturnFontSize(GameManager.Player_MoneyUp_ArmorPenetration);
            }
        }
        MoneyUpBtnText[index].text = TextFormatter.GetThousandCommaText(needMoney);                                                                                                 //  골드 획득량 증가 Text
    }

    public void StartHold(int index)
    {
        holdDuration = 0.5f;
        holdIndex = index;
        if (index == 0 && GameManager.Player_MoneyUp_EarnMoneyLevel < MaxValue[index] + 1) holding = true;
        else if (index == 1 && GameManager.Player_MoneyUp_DamageLevel < MaxValue[index] + 1) holding = true;
        else if (index == 2 && GameManager.Player_MoneyUp_AttackSpeedLevel < MaxValue[index] + 1) holding = true;
        else if (index == 3 && GameManager.Player_MoneyUp_CriticalChanceLevel < MaxValue[index] + 1) holding = true;
        else if (index == 4 && GameManager.Player_MoneyUp_CriticalDamageLevel < MaxValue[index] + 1) holding = true;
        else if (index == 5 && GameManager.Player_MoneyUp_ArmorPenetrationLevel < MaxValue[index] + 1) holding = true;
        else EndHold();
    }

    public void EndHold()
    {
        holding = false;
        holdingplay = false;
    }
}
