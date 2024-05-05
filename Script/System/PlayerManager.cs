using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Text[] Money_text;
    public Text[] Diamond_text;
    public GameObject myPanel;
    private Animator playerAnimators; // 적의 애니메이터 컴포넌트 배열

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip AttackSoundClip; // AudioClip 변수 선언

    [SerializeField] //외부스크립트에서 수정을 하지 못하게 하는 변수(insfactor에서는 접근가능)

    private void Start()
    {

        // 플레이어의 애니메이터 컴포넌트 배열 초기화
        playerAnimators = GetComponent<Animator>();

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
    }

    private bool canAttack = true;

    private void Update()
    {
        //  발사체 관리
        if (canAttack && GameManager.Enemy_alive == true)
        {
            playerAnimators.SetTrigger("Attack");
            StartCoroutine(CoolTime((float)AttackSpeedManager.AttackSpeed()));
            Instantiate(bulletPrefab, myPanel.GetComponent<RectTransform>());

            if ((ChangeManager.InPanel == 2 || ChangeManager.InPanel == 3) && BossManager.fighting == false) audioSource.PlayOneShot(AttackSoundClip, 0.35f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
            
        }

        if (GameManager.Enemy_alive == false)
        {
            playerAnimators.ResetTrigger("Attack");
            playerAnimators.SetTrigger("Idle");
        } 

        // 다이아몬드 TEXT
        if (GameManager.Player_Diamond == 0)
        {
            Diamond_text[0].text = "0";
            Diamond_text[1].text = "0";
        }
        else
        {
            Diamond_text[0].text = TextFormatter.GetThousandCommaText(GameManager.Player_Diamond).ToString();
            Diamond_text[1].text = TextFormatter.GetThousandCommaText(GameManager.Player_Diamond).ToString();
        }

        //  돈 TEXT
        if (GameManager.Player_Money == 0)
        {
            Money_text[0].text = "0";
            Money_text[1].text = "0";
        }
        else
        {
            Money_text[0].text = TextFormatter.GetThousandCommaText(GameManager.Player_Money).ToString();
            Money_text[1].text = TextFormatter.GetThousandCommaText(GameManager.Player_Money).ToString();
        }
    }


    IEnumerator CoolTime(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f / cooldown);
        playerAnimators.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f / cooldown);
        canAttack = true;
    }
}
