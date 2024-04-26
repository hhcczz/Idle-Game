using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    private EnemyManager enemyManager;


    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip HitSoundClip; // AudioClip 변수 선언

    public AudioClip[] BossSoundClip; // AudioClip 변수 선언

    private void Awake()
    {
        enemyManager = EnemyManager.Instance;
    }
    void Start()
    {

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        // 일정 시간 후에 자동으로 삭제되도록 설정
        StartCoroutine(DestroyAfterDelay());
    }

    void Update()
    {
        transform.Translate(new Vector2(10, 0) * Time.deltaTime);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if((ChangeManager.InPanel == 2 || ChangeManager.InPanel == 3) && BossManager.fighting == false) audioSource.PlayOneShot(HitSoundClip, 0.15f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
            GameManager.Enemy_Hit = true;
            enemyManager.enemyAnimators[enemyManager.FixDifficulty].SetTrigger("Hit");
            Image Img = GetComponent<Image>();
            Img.color = new Color(0, 0, 0, 0);
        }
        if (collision.CompareTag("Boss"))
        {
            BossManager.bossHit = true;
            if (GameManager.Player_ArmorPenetration + GameManager.Player_MoneyUp_ArmorPenetration + MobScrollManager.MS_UpArmorPenetration < BossManager.bossArmor) audioSource.PlayOneShot(BossSoundClip[0], 1f);
            else audioSource.PlayOneShot(BossSoundClip[1], 1f);

            Image Img = GetComponent<Image>();
            Img.color = new Color(0, 0, 0, 0);
        }
    }

    // 일정 시간 후에 자동으로 삭제하는 코루틴
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(4f); // 0.5초 후에 실행됨
        Destroy(gameObject); // 자기 자신을 삭제
    }

}
