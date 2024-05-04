using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RelicsSlider : MonoBehaviour
{

    public Slider ReinforceSlider;

    public GameObject ingBG;
    public GameObject EndBG;

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip RelicsSoundClip; // AudioClip 변수 선언

    private void Awake()
    {

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

        ReinforceSlider.value = 0f;
        // 애니메이션 이벤트가 호출될 때의 리스너를 추가합니다.
        AnimationClip animationClip = GetComponent<Animation>()?.clip;
        if (animationClip != null)
        {
            AnimationEvent animationEvent = new();
            animationEvent.functionName = "IncreaseSliderValue";
            animationEvent.time = animationClip.length; // 애니메이션의 종료 지점에 이벤트를 추가합니다.
            animationClip.AddEvent(animationEvent);
        }
    }

    private void OnEnable()
    {

        audioSource.PlayOneShot(RelicsSoundClip, 0.8f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
    }

    // 애니메이션 이벤트에 의해 호출될 함수
    public void IncreaseSliderValue()
    {
        if (ReinforceSlider != null)
        {
            audioSource.PlayOneShot(RelicsSoundClip, 0.8f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
            ReinforceSlider.value += 0.125f;
            //ReinforceSlider.value += 0.4f;
        }
        if (ReinforceSlider.value >= 1f)
        {
            EndBG.SetActive(true);
            ingBG.SetActive(false);
            ReinforceSlider.value = 0f;
        }
    }
}
