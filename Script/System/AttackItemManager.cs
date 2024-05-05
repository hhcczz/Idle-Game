using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackItemManager : MonoBehaviour
{
    public GameObject hudDamageText;    // 데미지 pref
    public GameObject BosshudDamageText;    // 데미지 pref

    public RectTransform EnemyBox;
    public RectTransform BossBox;


    [System.Obsolete]
    private void Update()
    {
        if (GameManager.Enemy_Hit == true && GameManager.Enemy_alive == true && EnemyManager.currentHP >= 0.01)
        {
            int Warrant = Random.RandomRange(0, 100); // 권능
            int Random_Critical = Random.RandomRange(0, 100); // 크리티컬
            int Random_10MaxHP = Random.RandomRange(0,100);

            decimal NormalDamage = DamageManager.Damage("일반");

            decimal CriticalDamage = DamageManager.Damage("크리티컬");

            decimal AddCriticalChance = CriticalChanceManager.CriticalChance();

            decimal damageAmount = Random_Critical < AddCriticalChance ? CriticalDamage : NormalDamage;

            if (AdManager.AdPlaying[0] == true) damageAmount *= AdManager.AdPowerValue[0] / 50m;

            if (GameManager.WarrantLevel[5] >= 1) damageAmount *= 1 + (decimal)GameManager.Warrant_Power[5] / 100;
            if (GameManager.WarrantLevel[27] >= 1) damageAmount *= 1 + (decimal)GameManager.Warrant_Power[27] / 100;

            if (Random_10MaxHP <= 1 && GameManager.WarrantLevel[3] >= 1) damageAmount += (decimal)EnemyManager.maxHP / GameManager.Warrant_Power[3]; // 최대 체력 10 + N * 2 

            decimal[] multipliers = { 0.5m, 1.0m, 2.0m, 4.0m };
            decimal sum = 0;
            for (int i = 0; i < multipliers.Length; i++)
            {
                if (GameManager.PackageBuyCheck[i])
                {
                    sum += multipliers[i];
                }
            }

            damageAmount *= 1 + sum;


            if (Warrant <= GameManager.Warrant_Power[2] && GameManager.WarrantLevel[2] >= 1) StartCoroutine(WarrantAttack(Warrant, Random_Critical, AddCriticalChance, damageAmount));
            else // 2번더 공격 안터졌음
            {
                GameObject hudText = Instantiate(hudDamageText, EnemyBox);
                hudText.transform.position = new Vector3(EnemyBox.position.x, EnemyBox.position.y + 4.5f, EnemyBox.position.z); // EnemyBox의 위치에서 y 좌표에 3을 더한 위치에 hudText 배치

                if (Warrant <= 1 && GameManager.WarrantLevel[1] >= 1)                 // 권능 터짐
                {
                    hudText.GetComponent<UIManager>().damage = damageAmount * GameManager.Warrant_Power[1];

                    if (Random_Critical < AddCriticalChance) FontColorText(ColorManager.ColorChange("노란색"), 55, hudText.GetComponent<Text>()); // 권능 터졌는데 크리티컬 터짐
                    else FontColorText(ColorManager.ColorChange("빨간색"), 55, hudText.GetComponent<Text>());                                        // 권능 터졌는데 크리티컬 안터짐
                }
                else
                {
                    if (Random_Critical < AddCriticalChance)        // 권능은 안터졌는데 크티티컬은 터짐
                    {
                        FontColorText(ColorManager.ColorChange("하늘색"), 55, hudText.GetComponent<Text>());
                    }
                    else FontColorText(ColorManager.ColorChange("하얀색"), 45, hudText.GetComponent<Text>());
                    hudText.GetComponent<UIManager>().damage = damageAmount;
                }
            }
            // 데미지 적용
            EnemyManager.currentHP -= (float)damageAmount;
            //GameManager.Enemy_Hit = false;
        }

        if(BossManager.fighting == true && BossManager.bossHit == true)
        {
            BossManager.bossHit = false;

            if(BossManager.BossAttackStart == false)
            {
                BossManager.BossAttackStart = true;
                return;
            }

            int Warrant = Random.RandomRange(0, 100); // 권능
            int Random_Critical = Random.RandomRange(0, 100); // 크리티컬
            int Random_10MaxHP = Random.RandomRange(0, 100);

            decimal NormalDamage = DamageManager.Damage("일반");

            decimal CriticalDamage = DamageManager.Damage("크리티컬");

            decimal AddCriticalChance = CriticalChanceManager.CriticalChance();

            decimal damageAmount = Random_Critical < AddCriticalChance ? CriticalDamage : NormalDamage;

            if (AdManager.AdPlaying[0] == true) damageAmount *= AdManager.AdPowerValue[0] / 50m;

            if (GameManager.WarrantLevel[5] >= 1) damageAmount *= 1 + (decimal)GameManager.Warrant_Power[5] / 100;
            if (GameManager.WarrantLevel[27] >= 1) damageAmount *= 1 + (decimal)GameManager.Warrant_Power[27] / 100;


            if (Random_10MaxHP <= 1 && GameManager.WarrantLevel[3] >= 1) damageAmount += (decimal)EnemyManager.maxHP / GameManager.Warrant_Power[3]; // 최대 체력 10 + N * 2 

            if (GameManager.Player_ArmorPenetration + GameManager.Player_MoneyUp_ArmorPenetration + MobScrollManager.MS_UpArmorPenetration < BossManager.bossArmor) damageAmount = 0;

            if (Warrant <= GameManager.Warrant_Power[2] && GameManager.WarrantLevel[2] >= 1) StartCoroutine(BossWarrantAttack(Warrant, Random_Critical, AddCriticalChance, damageAmount));
            else // 2번더 공격 안터졌음
            {
                GameObject hudText = Instantiate(BosshudDamageText, BossBox);
                hudText.transform.position = new Vector3(BossBox.position.x, BossBox.position.y + 4.5f, BossBox.position.z); // EnemyBox의 위치에서 y 좌표에 3을 더한 위치에 hudText 배치

                if (Warrant <= 1 && GameManager.WarrantLevel[1] >= 1)                 // 권능 터짐
                {
                    hudText.GetComponent<UIManager>().damage = damageAmount * GameManager.Warrant_Power[1];

                    if (Random_Critical < AddCriticalChance) FontColorText(ColorManager.ColorChange("노란색"), 55, hudText.GetComponent<Text>()); // 권능 터졌는데 크리티컬 터짐
                    else FontColorText(ColorManager.ColorChange("빨간색"), 55, hudText.GetComponent<Text>());                                        // 권능 터졌는데 크리티컬 안터짐
                }
                else
                {
                    if (Random_Critical < AddCriticalChance)        // 권능은 안터졌는데 크티티컬은 터짐
                    {
                        FontColorText(ColorManager.ColorChange("하늘색"), 55, hudText.GetComponent<Text>());
                    }
                    else FontColorText(ColorManager.ColorChange("하얀색"), 45, hudText.GetComponent<Text>());
                    hudText.GetComponent<UIManager>().damage = damageAmount;
                }
            }
            Debug.Log("damageAmount : " + damageAmount);
            // 데미지 적용
            BossManager.bossCurHealth -= (float)damageAmount;

        }
    }

    private void FontColorText(Color color, int fontSize, Text textComponent)
    {
        textComponent.color = color;
        textComponent.fontSize = fontSize;
    }

    IEnumerator WarrantAttack(int Warrant, int Random_Critical, decimal AddCriticalChance, decimal damageAmount)
    {

        if (Warrant <= 1 && GameManager.WarrantLevel[1] >= 1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GameManager.Enemy_alive == false) break;
                Debug.Log("3번 권능 공격!");
                GameObject hudText = Instantiate(hudDamageText, EnemyBox);

                hudText.GetComponent<UIManager>().damage = damageAmount * GameManager.Warrant_Power[1];
                 
                if (Random_Critical < AddCriticalChance) hudText.GetComponent<Text>().color = ColorManager.ColorChange("노란색");
                else hudText.GetComponent<Text>().color = ColorManager.ColorChange("빨간색");

                hudText.GetComponent<Text>().fontSize = 55;

                hudText.transform.position = new Vector3(EnemyBox.position.x, EnemyBox.position.y + 4.5f, EnemyBox.position.z); // EnemyBox의 위치에서 y 좌표에 3을 더한 위치에 hudText 배치
                EnemyManager.currentHP -= (float)damageAmount;
                yield return new WaitForSeconds(0.15f); // 0.5초 후에 실행됨
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (GameManager.Enemy_alive == false) break;
                Debug.Log("3번 일반 공격!");
                GameObject hudText = Instantiate(hudDamageText, EnemyBox);

                hudText.GetComponent<UIManager>().damage = damageAmount;
                if (Random_Critical < AddCriticalChance)
                {
                    hudText.GetComponent<Text>().color = ColorManager.ColorChange("하늘색");
                    hudText.GetComponent<Text>().fontSize = 55;
                }
                else hudText.GetComponent<Text>().color = ColorManager.ColorChange("하얀색");

                hudText.transform.position = new Vector3(EnemyBox.position.x, EnemyBox.position.y + 4.5f, EnemyBox.position.z); // EnemyBox의 위치에서 y 좌표에 3을 더한 위치에 hudText 배치
                
                EnemyManager.currentHP -= (float)damageAmount;
                yield return new WaitForSeconds(0.15f); // 0.5초 후에 실행됨
            }
        }
        
    }
    IEnumerator BossWarrantAttack(int Warrant, int Random_Critical, decimal AddCriticalChance, decimal damageAmount)
    {

        if (Warrant <= 1 && GameManager.WarrantLevel[1] >= 1)
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("3번 권능 공격!");
                GameObject hudText = Instantiate(BosshudDamageText, BossBox);

                hudText.GetComponent<UIManager>().damage = damageAmount * GameManager.Warrant_Power[1];

                if (Random_Critical < AddCriticalChance) hudText.GetComponent<Text>().color = ColorManager.ColorChange("노란색");
                else hudText.GetComponent<Text>().color = ColorManager.ColorChange("빨간색");

                hudText.GetComponent<Text>().fontSize = 55;

                hudText.transform.position = new Vector3(BossBox.position.x, BossBox.position.y + 4.5f, BossBox.position.z); // EnemyBox의 위치에서 y 좌표에 3을 더한 위치에 hudText 배치
                BossManager.bossCurHealth -= (float)damageAmount;
                yield return new WaitForSeconds(0.15f); // 0.5초 후에 실행됨
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("3번 일반 공격!");
                GameObject hudText = Instantiate(BosshudDamageText, BossBox);

                hudText.GetComponent<UIManager>().damage = damageAmount;
                if (Random_Critical < AddCriticalChance)
                {
                    hudText.GetComponent<Text>().color = ColorManager.ColorChange("하늘색");
                    hudText.GetComponent<Text>().fontSize = 55;
                }
                else hudText.GetComponent<Text>().color = ColorManager.ColorChange("하얀색");

                hudText.transform.position = new Vector3(BossBox.position.x, BossBox.position.y + 4.5f, BossBox.position.z); // EnemyBox의 위치에서 y 좌표에 3을 더한 위치에 hudText 배치

                BossManager.bossCurHealth -= (float)damageAmount;
                yield return new WaitForSeconds(0.15f); // 0.5초 후에 실행됨
            }
        }

    }
}
