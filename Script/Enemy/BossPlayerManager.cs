using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPlayerManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bossBox;
    private Animator playerAnimators; // 적의 애니메이터 컴포넌트 배열

    private bool bosscanAttack = true;

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip AttackSoundClip; // AudioClip 변수 선언
    public AudioClip BossSummonsSoundClip; // AudioClip 변수 선언
    public AudioClip BossClearSoundClip;

    ChangeManager changemanager;

    // Start is called before the first frame update=
    private void Awake()
    {
        changemanager = ChangeManager.Instance;

        // 플레이어의 애니메이터 컴포넌트 배열 초기화
        playerAnimators = GetComponent<Animator>();

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        bosscanAttack = true;
        BossManager.bossHit = true;
        playerAnimators.ResetTrigger("Attack");
        audioSource.PlayOneShot(BossSummonsSoundClip, 1f);
        for (int i = 0; i < changemanager.MainObject.Length; i++) changemanager.MainObject[i].SetActive(false);

        if (ChangeManager.InPanel == 2) changemanager.CharacterScroll.SetActive(false);
        else if (ChangeManager.InPanel == 3) changemanager.EquipmentScroll.SetActive(false);
    }

    private void OnDisable()
    {
        for (int i = 0; i < changemanager.MainObject.Length; i++) changemanager.MainObject[i].SetActive(true);

        if (ChangeManager.InPanel == 2) changemanager.CharacterScroll.SetActive(true);
        else if (ChangeManager.InPanel == 3) changemanager.EquipmentScroll.SetActive(true);
        audioSource.PlayOneShot(BossClearSoundClip, 1f);
    }

    // Update is called once per frame
    private void Update()
    {
        //  발사체 관리
        
        if (bosscanAttack == true && BossManager.fighting == true)
        {
            playerAnimators.SetTrigger("Attack");
            StartCoroutine(BossCoolTime((float)(GameManager.Player_AttackSpeed + GameManager.Player_MoneyUp_AttackSpeed + (decimal)MobScrollManager.MS_UpAttackSpeed)));
            Instantiate(bulletPrefab, bossBox.GetComponent<RectTransform>());
            audioSource.PlayOneShot(AttackSoundClip, 0.35f);
        }

    }
    IEnumerator BossCoolTime(float cooldown)
    {
        bosscanAttack = false;
        yield return new WaitForSeconds(0.5f / cooldown);
        playerAnimators.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f / cooldown);
        bosscanAttack = true;

    }

}
