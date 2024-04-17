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
    public Button[] btn;

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip ClickBtnSoundClip; // AudioClip 변수 선언

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
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(ClickBtnSoundClip, 0.5f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
    }
}
