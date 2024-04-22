using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   AdManager.cs
 * Desc :   광고 관리
 *         
 *
 & Functions
 &  [Public]
 &  : 
 &  : 
 &
 &  [Protected]
 &  : 
 &  : 
 &  : 
 &  : 
 &
 &  [Private]
 &  : AdOpen() - 광고 패널 열기
 &  : AdClose() - 광고 패널 닫기
 &  : AdPlay(int index) - 광고 진행
 &  : DecreaseAdDuration(int index, int duration) - 광고 남은 시간 표시 및 초기화 관리
 *
 */

public class AdManager : MonoBehaviour
{
    public Button AdOpenBtn;
    public Button AdCloseBtn;
    public Button[] AdSelBtn;

    public GameObject AdBox;

    public Text[] AdLevelText;
    public Text[] AdValue;
    public Text[] AdSelText;

    public Image[] AdPlayingImg;

    public static int[] AdPowerValue;

    public static bool[] AdPlaying;
    private int[] AdLevel;

    // 광고 지속 시간을 나타내는 변수를 설정
    int[] adDurationSeconds; // 3분

    // Start is called before the first frame update
    void Start()
    {
        AdOpenBtn.onClick.AddListener(AdOpen);
        AdCloseBtn.onClick.AddListener(AdClose);

        AdSelText[0].text = "<size=24>광고 보기</size>";
        AdSelText[1].text = "<size=24>광고 보기</size>";
        AdSelText[2].text = "<size=24>광고 보기</size>";

        for (int i = 0; i < AdSelBtn.Length; i++)
        {
            int index = i;

            AdSelBtn[index].onClick.AddListener(() => AdPlay(index));
        }


        AdPowerValue = new int[3]
        {
            100,
            100,
            100,
        };

        AdLevel = new int[3]
        {
            1,
            1,
            1,
        };

        AdPlaying = new bool[3];

        AdPowerValue[0] = 100 + 10 * (AdLevel[0] - 1);
        AdPowerValue[1] = 100 + 10 * (AdLevel[1] - 1);
        AdPowerValue[2] = 100 + 10 * (AdLevel[2] - 1);

        adDurationSeconds = new int[3]
        {
            180,
            180,
            180,
        };

        for(int i = 0; i < AdPlayingImg.Length; i++)
        {
            int index = i;

            if (AdPlaying[index] == true) AdPlayingImg[index].color = ColorManager.ColorChange("하얀색");
            else AdPlayingImg[index].color = ColorManager.ColorChange("검정색");
        }
        

    }

    private void AdOpen()
    {
        AdValue[0].text = "<color=red>공격력</color> +" + AdPowerValue[0] + "%";
        AdValue[1].text = "<color=yellow>골드</color> +" + AdPowerValue[1] + "%";
        AdValue[2].text = "<color=cyan>경험치</color> +" + AdPowerValue[2] + "%";

        AdLevelText[0].text = "<color=lime>Lv. </color>" + AdLevel[0];
        AdLevelText[1].text = "<color=lime>Lv. </color>" + AdLevel[1];
        AdLevelText[2].text = "<color=lime>Lv. </color>" + AdLevel[2];

        AdBox.SetActive(true);
    }

    private void AdClose()
    {
        AdBox.SetActive(false);
    }

    private void AdPlay(int index)
    {
        Image btnimg = AdSelBtn[index].GetComponent<Image>();
        if (index == 0 && !AdPlaying[index]) // 광고를 보지 않았을 때만 실행되도록 체크
        {
            AdSelBtn[index].interactable = false;
            AdPlaying[index] = true;
            btnimg.color = ColorManager.ColorChange("민트색");
            AdSelText[index].text = "남은 시간 : " + adDurationSeconds[index] + "초";
            AdLevel[index]++;
            AdPowerValue[index] = 100 + 10 * (AdLevel[index] - 1);
            AdValue[index].text = "<color=red>공격력</color> +" + AdPowerValue[index] + "%";
            AdLevelText[index].text = "<color=lime>Lv. </color>" + AdLevel[index];

            // 1초마다 광고 지속 시간을 감소시키는 코루틴 시작
            StartCoroutine(DecreaseAdDuration(index, adDurationSeconds[index]));
        }
        if (index == 1 && !AdPlaying[index]) // 광고를 보지 않았을 때만 실행되도록 체크
        {
            AdSelBtn[index].interactable = false;
            AdPlaying[index] = true;
            btnimg.color = ColorManager.ColorChange("민트색");
            AdSelText[index].text = "남은 시간 : " + adDurationSeconds[index] + "초";
            AdLevel[index]++;
            AdPowerValue[index] = 100 + 10 * (AdLevel[index] - 1);
            AdValue[index].text = "<color=yellow>골드</color> +" + AdPowerValue[index] + "%";
            AdLevelText[index].text = "<color=lime>Lv. </color>" + AdLevel[index];

            // 1초마다 광고 지속 시간을 감소시키는 코루틴 시작
            StartCoroutine(DecreaseAdDuration(index, adDurationSeconds[index]));
        }
        if (index == 2 && !AdPlaying[index]) // 광고를 보지 않았을 때만 실행되도록 체크
        {
            AdSelBtn[index].interactable = false;
            AdPlaying[index] = true;
            btnimg.color = ColorManager.ColorChange("민트색");
            AdSelText[index].text = "남은 시간 : " + adDurationSeconds[index] + "초";
            AdLevel[index]++;
            AdPowerValue[index] = 100 + 10 * (AdLevel[index] - 1);
            AdValue[index].text = "<color=cyan>경험치</color> +" + AdPowerValue[index] + "%";
            AdLevelText[index].text = "<color=lime>Lv. </color>" + AdLevel[index];

            // 1초마다 광고 지속 시간을 감소시키는 코루틴 시작
            StartCoroutine(DecreaseAdDuration(index, adDurationSeconds[index]));
        }
    }

    private IEnumerator DecreaseAdDuration(int index, int duration)
    {
        Image btnimg = AdSelBtn[index].GetComponent<Image>();
        AdPlayingImg[index].color = ColorManager.ColorChange("하얀색");

        // 광고 지속 시간이 0보다 큰 동안 반복
        while (duration > 0)
        {
            // 1초 대기
            yield return new WaitForSeconds(1f);
            duration--; // 광고 지속 시간 감소
                        // 텍스트 업데이트
            AdSelText[index].text = "남은 지속 시간 : " + duration + "초";
        }

        // 광고 종료 시 버튼 색상과 텍스트 복구
        btnimg.color = ColorManager.ColorChange("하얀색");
        AdSelBtn[index].interactable = true;
        AdSelText[index].text = "<size=24>광고 보기</size>";
        adDurationSeconds[index] = 180;
        // AdPlaying을 false로 설정
        AdPlaying[index] = false;
        AdPlayingImg[index].color = ColorManager.ColorChange("검정색");
    }
}
