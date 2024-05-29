using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using GoogleMobileAds.Common;
using UnityEngine.Advertisements;
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
    private static AdManager instance;

    public static AdManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AdManager>();
                if (instance == null)
                {
                    Debug.LogError("AdManager 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }

    public Button AdOpenBtn;
    public Button AdCloseBtn;
    public Button[] AdSelBtn;

    public GameObject AdBox;

    public Text[] AdLevelText;
    public Text[] AdValue;
    public Text[] AdSelText;
    public Text[] AdTimeText;

    public Image[] AdPlayingImg;

    public static int[] AdPowerValue;

    public static bool[] AdPlaying;
    public static int[] AdLevel;

    private RewardedAd rewardedAd;
    private string adUnitId;

    // 광고 지속 시간을 나타내는 변수를 설정
    int[] adDurationSeconds; // 3분

    private void Awake()
    {
        AdLevel = new int[3]
        {
            1,
            1,
            1,
        };

        AdPowerValue = new int[3]
        {
            100 + 10 * (AdLevel[0] - 1),
            100 + 10 * (AdLevel[1] - 1),
            100 + 10 * (AdLevel[2] - 1),
        };
        AdPlaying = new bool[3];

        AdPowerValue = new int[3]
        {
            100,
            100,
            100,
        };
        adDurationSeconds = new int[3]
        {
            180,
            180,
            180,
        };

    }

    // Start is called before the first frame update
    [Obsolete]
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
            
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                //초기화 완료
                AdSelBtn[index].onClick.AddListener(() => AdPlay(index));
            });
        }

        for (int i = 0; i < AdPlayingImg.Length; i++)
        {
            int index = i;

            if (AdPlaying[index] == true)
            {
                AdPlayingImg[index].color = ColorManager.ColorChange("하얀색");
                AdTimeText[index].gameObject.SetActive(true);
            }
            else
            {
                AdPlayingImg[index].color = ColorManager.ColorChange("검정색");
                AdTimeText[index].gameObject.SetActive(false);
            }
        }


#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif

        LoadRewardedAd();
    }
    [Obsolete]
    public void LoadRewardedAd() //광고 로드 하기
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }

    [Obsolete]
    public void ShowAd() //광고 보기
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                //보상 획득하기
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
        else
        {
            LoadRewardedAd();
        }
    }

    [Obsolete]
    private void RegisterReloadHandler(RewardedAd ad) //광고 재로드
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += (null);
        {
            Debug.Log("Rewarded Ad full screen content closed.");
    
            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
    
            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }

    public void AdOpen()
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

    [Obsolete]
    private void AdPlay(int index)
    {
        ShowAd();
        StatisticsManager.ImmutabilityMainAdCount += 1;
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
        LoadRewardedAd();
    }

    private IEnumerator DecreaseAdDuration(int index, int duration)
    {
        Image btnimg = AdSelBtn[index].GetComponent<Image>();
        AdPlayingImg[index].color = ColorManager.ColorChange("하얀색");

        AdTimeText[index].gameObject.SetActive(true);

        // 광고 지속 시간이 0보다 큰 동안 반복
        while (duration > 0)
        {
            // 1초 대기
            yield return new WaitForSeconds(1f);
            duration--; // 광고 지속 시간 감소
                        // 텍스트 업데이트
            if (duration >= 3600) AdTimeText[index].text = duration / 3600 + "h";
            else if (duration >= 60) AdTimeText[index].text = duration / 60 + "m";
            else AdTimeText[index].text = duration + "s";
            AdSelText[index].text = "남은 지속 시간 : " + duration + "초";
        }

        // 광고 종료 시 버튼 색상과 텍스트 복구
        AdTimeText[index].gameObject.SetActive(false);
        btnimg.color = ColorManager.ColorChange("하얀색");
        AdSelBtn[index].interactable = true;
        AdSelText[index].text = "<size=24>광고 보기</size>";
        adDurationSeconds[index] = 180;
        // AdPlaying을 false로 설정
        AdPlaying[index] = false;
        AdPlayingImg[index].color = ColorManager.ColorChange("검정색");
    }

}


