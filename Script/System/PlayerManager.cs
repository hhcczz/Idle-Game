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

    private string[] PlayerNameString;

    public Text PlayerName;
    public Text BossPlayerName;

    [SerializeField] //외부스크립트에서 수정을 하지 못하게 하는 변수(insfactor에서는 접근가능)

    private void Start()
    {

        // 플레이어의 애니메이터 컴포넌트 배열 초기화
        playerAnimators = GetComponent<Animator>();

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        PlayerNameString = new string[28];

        PlayerNameString[0] = "<color=#95F8CC>초급 검사</color>";
        PlayerNameString[1] = "<color=cyan>중급 검사</color>";
        PlayerNameString[2] = "<color=yellow>상급 검사</color>";
        PlayerNameString[3] = "<color=#FFA6A6>최상급 검사</color>";

        PlayerNameString[4] = "<color=#95F8CC>초급 용사</color>";
        PlayerNameString[5] = "<color=cyan>중급 용사</color>";
        PlayerNameString[6] = "<color=yellow>상급 용사</color>";
        PlayerNameString[7] = "<color=#FFA6A6>최상급 용사</color>";

        PlayerNameString[8] = "<color=#95F8CC>초급 용검사</color>";
        PlayerNameString[9] = "<color=cyan>중급 용검사</color>";
        PlayerNameString[10] = "<color=yellow>상급 용검사</color>";
        PlayerNameString[11] = "<color=#FFA6A6>최상급 용검사</color>";

        PlayerNameString[12] = "<color=#95F8CC>초급 전설</color>";
        PlayerNameString[13] = "<color=cyan>중급 전설</color>";
        PlayerNameString[14] = "<color=yellow>상급 전설</color>";
        PlayerNameString[15] = "<color=#FFA6A6>최상급 전설</color>";

        PlayerNameString[16] = "<color=#95F8CC>초급 영웅</color>";
        PlayerNameString[17] = "<color=cyan>중급 영웅</color>";
        PlayerNameString[18] = "<color=yellow>상급 영웅</color>";
        PlayerNameString[19] = "<color=#FFA6A6>최상급 영웅</color>";

        PlayerNameString[20] = "<color=#95F8CC>초급 초월자</color>";
        PlayerNameString[21] = "<color=cyan>중급 초월자</color>";
        PlayerNameString[22] = "<color=yellow>상급 초월자</color>";
        PlayerNameString[23] = "<color=#FFA6A6>최상급 초월자</color>";

        PlayerNameString[24] = "<color=#95F8CC>초급 파괴신</color>";
        PlayerNameString[25] = "<color=cyan>중급 파괴신</color>";
        PlayerNameString[26] = "<color=yellow>상급 파괴신</color>";
        PlayerNameString[27] = "<color=#FFA6A6>최상급 파괴신</color>";

        PlayerName.text = PlayerNameValue(GameManager.Player_Level);
        BossPlayerName.text = PlayerNameValue(GameManager.Player_Level);

    }

    private bool canAttack = true;

    private string PlayerNameValue(int index)
    {

        if (index < 10) return PlayerNameString[0];
        else if (index >= 10 && index < 20) return PlayerNameString[1];
        else if (index >= 20 && index < 30) return PlayerNameString[2];
        else if (index >= 30 && index < 40) return PlayerNameString[3];

        else if (index >= 40 && index < 80) return PlayerNameString[4];
        else if (index >= 800 && index < 120) return PlayerNameString[5];
        else if (index >= 120 && index < 160) return PlayerNameString[6];
        else if (index >= 160 && index < 200) return PlayerNameString[7];

        else if (index >= 200 && index < 280) return PlayerNameString[8];
        else if (index >= 280 && index < 360) return PlayerNameString[9];
        else if (index >= 360 && index < 440) return PlayerNameString[10];
        else if (index >= 440 && index < 520) return PlayerNameString[11];

        else if (index >= 520 && index < 680) return PlayerNameString[8];
        else if (index >= 680 && index < 840) return PlayerNameString[9];
        else if (index >= 840 && index < 1000) return PlayerNameString[10];
        else if (index >= 1000 && index < 1160) return PlayerNameString[11];

        else if (index >= 1160 && index < 1460) return PlayerNameString[12];
        else if (index >= 1460 && index < 1760) return PlayerNameString[13];
        else if (index >= 1760 && index < 2060) return PlayerNameString[14];
        else if (index >= 2060 && index < 2500) return PlayerNameString[15];

        else if (index >= 2500 && index < 3500) return PlayerNameString[16];
        else if (index >= 3500 && index < 4500) return PlayerNameString[17];
        else if (index >= 4500 && index < 5500) return PlayerNameString[18];
        else if (index >= 5500 && index < 7000) return PlayerNameString[19];

        else if (index >= 7000 && index < 10000) return PlayerNameString[20];
        else if (index >= 10000 && index < 13000) return PlayerNameString[21];
        else if (index >= 13000 && index < 16000) return PlayerNameString[22];
        else if (index >= 16000 && index < 20000) return PlayerNameString[23];

        else if (index >= 20000 && index < 50000) return PlayerNameString[24];
        else if (index >= 50000 && index < 80000) return PlayerNameString[25];
        else if (index >= 80000 && index < 100000) return PlayerNameString[26];
        else return PlayerNameString[27];

    }

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

        PlayerName.text = PlayerNameValue(GameManager.Player_Level);
        BossPlayerName.text = PlayerNameValue(GameManager.Player_Level);
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
