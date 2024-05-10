using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    
    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip[] BGS_SoundClip; // AudioClip 변수 선언

    public Button SettingOpenBtn;             //  설정 리스트 오픈 버튼
    public Button SettingReturnBtn;           //  설정에서 되돌아가는 버튼

    public GameObject SettingPanel;   //  설정 리스트 열기

    public Text BGS_ChangeText;

    public Button BGS_NextBtn;
    public Button BGS_ReturnBtn;

    private string[] BGS_Text;
    private void Awake()
    {

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
    }

    // 버튼 리스너 추가
    private void Start()
    {
        BGS_Text = new string[6]
        {
            "Sound 1",
            "Sound 2",
            "Sound 3",
            "Sound 4",
            "Sound 5",
            "Sound 6",
        };
        MusicUpdate(GameManager.BGS_Number);

        SettingOpenBtn.onClick.AddListener(SettingOpen);
        SettingReturnBtn.onClick.AddListener(Return);

        BGS_NextBtn.onClick.AddListener(BGSNext);
        BGS_ReturnBtn.onClick.AddListener(BGSReturn);
    }

    private void BGSNext()
    {
        if (GameManager.BGS_Number == 5) GameManager.BGS_Number = 0;
        else GameManager.BGS_Number++;

        MusicUpdate(GameManager.BGS_Number);
    }

    private void BGSReturn()
    {
        if (GameManager.BGS_Number == 0) GameManager.BGS_Number = 5;
        else GameManager.BGS_Number--;

        MusicUpdate(GameManager.BGS_Number);
    }

    private void MusicUpdate(int index)
    {
        BGS_ChangeText.text = BGS_Text[index];

        audioSource.clip = BGS_SoundClip[index];
        audioSource.loop = true;
        audioSource.Play();
    }

    //  설정 열기
    private void SettingOpen()
    {
        SettingPanel.SetActive(true);
    }

    //  설정에서 되돌아가기
    private void Return()
    {
        SettingPanel.SetActive(false);
    }

    public void SetMusicVoulme(float Voulme)
    {
        audioSource.volume = Voulme;
    }
}
