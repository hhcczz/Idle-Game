using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   BtnSoundManager.cs
 * Desc :   버튼 클릭 사운드 관리
 *         
 *
 & Functions
 &  [Public]
 &
 &  [Protected]
 &  : 
 &  : 
 &  : 
 &  : 
 &
 &  [Private]
 &  : PlaySound() 소리 재생
 *
 */
public class BtnSoundManager : MonoBehaviour
{
    private static BtnSoundManager instance;

    public static BtnSoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BtnSoundManager>();
                if (instance == null)
                {
                    Debug.LogError("BtnSoundManager 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }
    public Button[] btn;

    private AudioSource audioSource; // AudioSource 변수 추가

    // 0 Normal click | 1 Receive | 2 Reinforce | 3 Equip | 4 Tutorial Success | 5 Mob Summons
    public AudioClip[] BtnSoundClip; // AudioClip 변수 선언

    public Button[] ReceiveBtn;

    public Button ReinforceBtn;
    public Button[] EquipBtn;
    public Button TutorialSuccessBtn;
    public Button MobSummonsBtn;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < btn.Length; i++)
        {
            int index = i;

            btn[index].onClick.AddListener(PlaySound);
        }
        for(int i = 0; i < ReceiveBtn.Length; i++)
        {
            int index = i;

            ReceiveBtn[index].onClick.AddListener(() => audioSource.PlayOneShot(BtnSoundClip[1], 1.5f));
        }

        ReinforceBtn.onClick.AddListener(() => audioSource.PlayOneShot(BtnSoundClip[2], 1f));
        EquipBtn[0].onClick.AddListener(() => audioSource.PlayOneShot(BtnSoundClip[3], 1f));
        EquipBtn[1].onClick.AddListener(() => audioSource.PlayOneShot(BtnSoundClip[3], 1f));
        MobSummonsBtn.onClick.AddListener(() => audioSource.PlayOneShot(BtnSoundClip[5], 1f));
        TutorialSuccessBtn.onClick.AddListener(TutorialSuccessPlay);
    }

    public void TutorialSuccessPlay()
    {
        if(TutorialManager.tutorialclear == true) audioSource.PlayOneShot(BtnSoundClip[4], 0.6f);
        else audioSource.PlayOneShot(BtnSoundClip[0], 1.5f);

    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(BtnSoundClip[0], 1.5f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
    }
}
