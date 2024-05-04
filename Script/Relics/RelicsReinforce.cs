using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicsReinforce : MonoBehaviour
{
    
    public Text RankText;
    public Text UpText;
    public Button ReinforceNormalBtn;
    public Button ReinforceSpecialBtn;
    public Text[] ReinforceChanceText;                  //  0 성공, 1 실패, 2 파괴
    public Text RelicsEndLeftText;
    public Text RelicsEndRightText;
    public Sprite[] RelicsImgArr;
    public Image RelicsImg;  // UI 이미지를 표현하는 Image 컴포넌트
    public Image RelicsingImg;  // UI 이미지를 표현하는 Image 컴포넌트
    public Image RelicsEndLeftImg;  // UI 이미지를 표현하는 Image 컴포넌트
    public Image RelicsEndRightImg;  // UI 이미지를 표현하는 Image 컴포넌트
    public Button RelicsEndOutBtn;

    public Text OwnRelicsReinforceScroll;

    public GameObject BlackSmith;
    public GameObject ReinforceBtnBox;

    public Text RelicsEndTitle;
    public Text RelicsRankText;
    public GameObject ingBG;
    public GameObject EndBG;

    public GameObject lackBG;
    public GameObject lackPanel;
    public GameObject lackImg;
    public Text lackText;
    private float fadeSpeed = 0.3f; // 서서히 사라지는 속도
    private Image panelImage;

    private int LastRank = -1;

    float breakPercent = 0f;

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip[] RelicsSoundClip; // AudioClip 변수 선언

    private float speed = 14f; // 패널 이동 속도
    private float maxDistance = 300f; // 최대 이동 거리
    private bool movingForward = false; // 패널이 끝에 도달했다고 알려주는 변수

    RectTransform newpanel;

    private int RelicsReinforceCheck = 0;   //  1 = 성공, 2 = 실패, 3 = 파괴
    float SaveRelicsRank = 0f;

    void Start()
    {

        // AudioSource 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();

        TextUpdate();
        UpText.text = GameManager.Relics_Rank * 5f + "% <color=lime>▶</color> " + (GameManager.Relics_Rank * 5f + 5f) + "%";
        ReinforceNormalBtn.onClick.AddListener(() => Reinforce(1));
        ReinforceSpecialBtn.onClick.AddListener(() => Reinforce(2));
        ChangeRankText(GameManager.Relics_Rank);
        RelicsEndOutBtn.onClick.AddListener(RelicsEndOutBtnClick);
        panelImage = lackPanel.GetComponent<Image>(); // 패널의 이미지 컴포넌트 가져오기

        // RectTransform 컴포넌트 가져오기
        newpanel = lackPanel.GetComponent<RectTransform>();

        OwnRelicsReinforceScroll.text = GameManager.RelicsReinforceScroll + "";
        movingForward = false;
    }

    public IEnumerator PanelMove()
    {
        // RectTransform 컴포넌트 가져오기
        RectTransform newpanel = lackPanel.GetComponent<RectTransform>();

        // 패널을 이동 방향으로 이동
        if (!movingForward)
        {
            // 현재 위치와 목표 위치 사이의 거리 계산
            float distanceToTarget = maxDistance - newpanel.anchoredPosition.y;

            // 이동 속도를 거리에 따라 서서히 줄이기
            float currentSpeed = speed * (distanceToTarget / maxDistance);
            if(currentSpeed < 6.5) currentSpeed = 5f;

            newpanel.Translate(currentSpeed * Time.deltaTime * Vector2.up);

            // 최대 이동 거리에 도달하면 도착했다 알림
            if (newpanel.anchoredPosition.y >= maxDistance)
            {
                movingForward = true;
            }
        }
        yield return null;
    }

    IEnumerator FadeOut(int index)
    {
        float alpha = 1f; // 최대 알파값

        if (index == 0) lackText.text = "일반 강화는 강화 스크롤 <color=yellow><size=43>1</size></color>개가 필요합니다.";
        else if (index == 1) lackText.text = "특수 강화는 강화 스크롤 <color=yellow><size=43>3</size></color>개가 필요합니다.";
        else if (index == 2)
        {
            lackText.text = "튜토리얼 <color=yellow><size=43>11</size></color>단계를 클리어 하셔야합니다.";
            lackImg.SetActive(false);
        }

        Color newColor = panelImage.color;
        newColor.a = alpha;

        lackBG.SetActive(true); // 패널 이동 전에 활성화


        // 코루틴을 반복하여 호출
        while (movingForward == false)
        {
            yield return StartCoroutine(PanelMove()); // PanelMove() 코루틴을 호출하고 완료될 때까지 대기
        }


        // 패널 이동이 완료되면 페이드 아웃 시작
        while (alpha > 180f / 255f)
        {
            alpha -= fadeSpeed * Time.deltaTime; // 알파값 감소

            newColor.a = alpha; // 새로운 알파값 설정
            panelImage.color = newColor; // 패널의 색상 업데이트
            yield return null;
        }
        if (index == 2) lackImg.SetActive(true);

        // 알파값이 0 이하가 되면 패널 비활성화
        newColor.a = 1f; // 알파값을 초기화합니다.
        panelImage.color = newColor; // 패널의 색상 업데이트
        newpanel.anchoredPosition = Vector2.zero; // 패널의 위치를 초기 위치로 설정합니다.
        movingForward = false;
        lackBG.SetActive(false);
    }

    private void OnEnable()
    {
        OwnRelicsReinforceScroll.text = GameManager.RelicsReinforceScroll + "";
    }

    void RelicsEndOutBtnClick()
    {
        EndBG.SetActive(false);
        BlackSmith.SetActive(true);
        ReinforceBtnBox.SetActive(true);
    }

    void ChangeRankText(float index)
    {
        int Num = (int)GameManager.Relics_Rank - 1;

        RelicsImg.sprite = RelicsImgArr[Num];
        RelicsRankText.text = "+" + (Num + 1);
        if (RelicsReinforceCheck == 1)
        {
            RelicsingImg.sprite = RelicsImgArr[Num - 1];
            RelicsEndLeftImg.sprite = RelicsImgArr[Num - 1];
            RelicsEndRightImg.sprite = RelicsImgArr[Num];
        }
        else if (RelicsReinforceCheck == 2)
        {
            RelicsingImg.sprite = RelicsImgArr[Num + 1];
            RelicsEndLeftImg.sprite = RelicsImgArr[Num + 1];
            RelicsEndRightImg.sprite = RelicsImgArr[Num];
        }
        else if (RelicsReinforceCheck == 3)
        {
            RelicsingImg.sprite = RelicsImgArr[LastRank + 1];
            RelicsEndLeftImg.sprite = RelicsImgArr[(int)SaveRelicsRank];
            RelicsEndRightImg.sprite = RelicsImgArr[Num];
        }
        switch (index)
        {
            case 1f:
                RankText.text = "하급 나무 활";
                break;
            case 2f:
                RankText.text = "중급 나무 활";
                break;
            case 3f:
                RankText.text = "고급 나무 활";
                break;
            case 4f:
                RankText.text = "하급 실버 활";
                break;
            case 5f:
                RankText.text = "중급 실버 활";
                break;
            case 6f:
                RankText.text = "고급 실버 활";
                break;
            case 7f:
                RankText.text = "하급 티타늄 활";
                break;
            case 8f:
                RankText.text = "중급 티타늄 활";
                break;
            case 9f:
                RankText.text = "상급 티타늄 활";
                break;
            case 10f:
                RankText.text = "하급 황금 활";
                break;
            case 11f:
                RankText.text = "중급 황금 활";
                break;
            case 12f:
                RankText.text = "상급 황금 활";
                break;
            case 13f:
                RankText.text = "하급 절망 활";
                break;
            case 14f:
                RankText.text = "중급 절망 활";
                break;
            case 15f:
                RankText.text = "상급 절망 활";
                break;
            case 16f:
                RankText.text = "엘프의 나무 신발";
                break;
            case 17f:
                RankText.text = "엘프의 실버 신발";
                break;
            case 18f:
                RankText.text = "엘프의 빙결 신발";
                break;
            case 19f:
                RankText.text = "엘프의 황금 신발";
                break;
            case 20f:
                RankText.text = "엘프의 절망 신발";
                break;
            case 21f:
                RankText.text = "엘프의 나무 갑옷";
                break;
            case 22f:
                RankText.text = "엘프의 실버 갑옷";
                break;
            case 23f:
                RankText.text = "엘프의 빙결 갑옷";
                break;
            case 24f:
                RankText.text = "엘프의 황금 갑옷";
                break;
            case 25f:
                RankText.text = "엘프의 절망 갑옷";
                break;
            case 26f:
                RankText.text = "엘프의 나무 장갑";
                break;
            case 27f:
                RankText.text = "엘프의 실버 장갑";
                break;
            case 28f:
                RankText.text = "엘프의 빙결 장갑";
                break;
            case 29f:
                RankText.text = "엘프의 황금 장갑";
                break;
            case 30f:
                RankText.text = "엘프의 절망 장갑";
                break;
            case 31f:
                RankText.text = "엘프의 나무 모자";
                break;
            case 32f:
                RankText.text = "엘프의 실버 모자";
                break;
            case 33f:
                RankText.text = "엘프의 빙결 모자";
                break;
            case 34f:
                RankText.text = "엘프의 황금 모자";
                break;
            case 35f:
                RankText.text = "엘프의 절망 모자";
                break;
            case 36f:
                RankText.text = "엘프의 티타늄 모자";
                break;
            case 37f:
                RankText.text = "하급 나무 목걸이";
                break;
            case 38f:
                RankText.text = "중급 나무 목걸이";
                break;
            case 39f:
                RankText.text = "고급 나무 목걸이";
                break;
            case 40f:
                RankText.text = "하급 실버 목걸이";
                break;
            case 41f:
                RankText.text = "중급 실버 목걸이";
                break;
            case 42f:
                RankText.text = "고급 실버 목걸이";
                break;
            case 43f:
                RankText.text = "하급 티타늄 목걸이";
                break;
            case 44f:
                RankText.text = "중급 티타늄 목걸이";
                break;
            case 45f:
                RankText.text = "상급 티타늄 목걸이";
                break;
            case 46f:
                RankText.text = "하급 황금 목걸이";
                break;
            case 47f:
                RankText.text = "중급 황금 목걸이";
                break;
            case 48f:
                RankText.text = "상급 황금 목걸이";
                break;
            case 49f:
                RankText.text = "하급 절망 목걸이";
                break;
            case 50f:
                RankText.text = "중급 절망 목걸이";
                break;
            case 51f:
                RankText.text = "상급 절망 목걸이";
                break;
        }
        if (index <= 9f)
        {
            RankText.color = ColorManager.ColorChange("무기1번색");
            RelicsRankText.color = ColorManager.ColorChange("무기1번색");
        }
        else if (index >= 10f && index <= 19f)
        {
            RankText.color = ColorManager.ColorChange("무기2번색");
            RelicsRankText.color = ColorManager.ColorChange("무기2번색");
        }
        else if (index >= 20f && index <= 29f)
        {
            RankText.color = ColorManager.ColorChange("노랑색");
            RelicsRankText.color = ColorManager.ColorChange("노랑색");
        }
        else if (index >= 30f && index <= 39f)
        {
            RankText.color = ColorManager.ColorChange("초록색");
            RelicsRankText.color = ColorManager.ColorChange("초록색");
        }
        else if (index >= 40f && index <= 49f)
        {
            RankText.color = ColorManager.ColorChange("파란색");
            RelicsRankText.color = ColorManager.ColorChange("파란색");
        }
        else if (index >= 50f && index <= 59f)
        {
            RankText.color = ColorManager.ColorChange("빨간색");
            RelicsRankText.color = ColorManager.ColorChange("빨간색");
        }
    }

    private void Reinforce(int index)
    {
        LastRank = (int)GameManager.Relics_Rank;
        if(index == 1)
        {
            if(GameManager.RelicsReinforceScroll < 1)
            {
                StartCoroutine(FadeOut(0));
                return;
            }
            ingBG.SetActive(true);
            BlackSmith.SetActive(false);
            ReinforceBtnBox.SetActive(false);
            float MI = Random.value;

            GameManager.RelicsReinforceScroll -= 1;
            StatisticsManager.RelicsTry += 1;
            // 성공, 실패, 파괴 변수 관리
            /*
             * 파괴확률 0.85 보다 크면 파괴
             * MI가 성공확률보다 작으면 성공
             * 현재 성공 확률 0.5
             * 만약 MI = 0.83 
             * 파괴검사 시작
             * 0.83 > 0.85 파괴 X 실패로 처리
             * 만약 MI = 0.86
             * 0.86 > 0.85 파괴로처리
             */
            if (MI <= (101f - GameManager.Relics_Rank) * 0.01f) RelicsReinforceCheck = 1;
            else if (GameManager.Relics_Rank >= 20f && MI > 1 - ((breakPercent - GameManager.Warrant_Power[4]) * 0.01f)) RelicsReinforceCheck = 3;
            else RelicsReinforceCheck = 2;

            // 성공
            if (RelicsReinforceCheck == 1)
            {
                GameManager.Relics_Rank += 1f;
                RelicsEndLeftText.text = "데미지 : <color=lime>" + (GameManager.Relics_Rank * 5f - 5f) + "</color>%";
                RelicsEndRightText.text = "데미지 : <color=lime>" + (GameManager.Relics_Rank * 5f) + "</color>%";
                RelicsEndTitle.text = "강화 <color=lime>성공!</color>";

                Debug.Log("성공 MI 값 : " + MI);
                Debug.Log("성공 변수 값 : " + ((101f - GameManager.Relics_Rank) * 0.01f));
                GameManager.Player_RelicsDamageAmplification += 5;
            }

            // 실패
            else if (RelicsReinforceCheck == 2)
            {
                GameManager.Relics_Rank -= 1f;
                RelicsEndLeftText.text = "데미지 : <color=red>" + (GameManager.Relics_Rank * 5f) + "</color>%";
                RelicsEndRightText.text = "데미지 : <color=red>" + (GameManager.Relics_Rank * 5f - 5f) + "</color>%";
                RelicsEndTitle.text = "강화 <color=red>실패..</color>";

                Debug.Log("실패 MI 값 : " + MI);
                Debug.Log("실패 변수 값 : " + ((101f - GameManager.Relics_Rank) * 0.01f));
                GameManager.Player_RelicsDamageAmplification -= 5;
            }

            // 파괴
            else if (RelicsReinforceCheck == 3)
            {
                RelicsEndLeftText.text = "데미지 : <color=red>" + (GameManager.Relics_Rank * 5f) + "</color>%";
                RelicsEndRightText.text = "데미지 : <color=red>5</color>%";
                RelicsEndTitle.text = "강화 <color=red>파괴....</color>";
                SaveRelicsRank = GameManager.Relics_Rank;
                GameManager.Relics_Rank = 1f;

                Debug.Log("파괴 MI 값 : " + MI);
                Debug.Log("파괴 변수 값 : " + (1 - ((breakPercent - GameManager.Warrant_Power[4]) * 0.01f)));
                GameManager.Player_RelicsDamageAmplification = 5;
            }

            ChangeRankText(GameManager.Relics_Rank);
            TextUpdate();
            StartCoroutine(PlaySound());

        }
        else if(index == 2)
        {
            if(GameManager.RelicsReinforceScroll < 3)
            {
                lackBG.SetActive(true);
                StartCoroutine(FadeOut(1));
                return;
            }
            if (TutorialManager.PlayerTutorialLevel <= 11)
            {
                lackBG.SetActive(true);
                StartCoroutine(FadeOut(2));
                return;
            }
            ingBG.SetActive(true);
            BlackSmith.SetActive(false);
            ReinforceBtnBox.SetActive(false);
            float MI = Random.value;


            GameManager.RelicsReinforceScroll -= 3;

            // 성공, 실패, 파괴 변수 관리
            /*
             * 파괴확률 0.85 보다 크면 파괴
             * MI가 성공확률보다 작으면 성공
             * 현재 성공 확률 0.5
             * 만약 MI = 0.83 
             * 파괴검사 시작
             * 0.83 > 0.85 파괴 X 실패로 처리
             * 만약 MI = 0.86
             * 0.86 > 0.85 파괴로처리
             */
            if (MI <= (101f - GameManager.Relics_Rank) * 0.01f) RelicsReinforceCheck = 1;
            else RelicsReinforceCheck = 2;

            // 성공
            if (RelicsReinforceCheck == 1)
            {
                GameManager.Relics_Rank += 1f;
                RelicsEndLeftText.text = "데미지 : <color=lime>" + (GameManager.Relics_Rank * 5f - 5f) + "</color>%";
                RelicsEndRightText.text = "데미지 : <color=lime>" + (GameManager.Relics_Rank * 5f) + "</color>%";
                RelicsEndTitle.text = "강화 <color=lime>성공!</color>";

                Debug.Log("성공 MI 값 : " + MI);
                Debug.Log("성공 변수 값 : " + ((101f - GameManager.Relics_Rank) * 0.01f));
                GameManager.Player_RelicsDamageAmplification += 5;
            }

            // 실패
            else if (RelicsReinforceCheck == 2)
            {
                GameManager.Relics_Rank -= 1f;
                RelicsEndLeftText.text = "데미지 : <color=red>" + (GameManager.Relics_Rank * 5f) + "</color>%";
                RelicsEndRightText.text = "데미지 : <color=red>" + (GameManager.Relics_Rank * 5f - 5f) + "</color>%";
                RelicsEndTitle.text = "강화 <color=red>실패..</color>";

                Debug.Log("실패 MI 값 : " + MI);
                Debug.Log("실패 변수 값 : " + ((101f - GameManager.Relics_Rank) * 0.01f));
                GameManager.Player_RelicsDamageAmplification -= 5;
            }

            ChangeRankText(GameManager.Relics_Rank);
            TextUpdate();
            StartCoroutine(PlaySound());
        }
        
    }

    IEnumerator PlaySound()
    {
        if(RelicsReinforceCheck == 1) yield return new WaitForSeconds(2f);
        else if (RelicsReinforceCheck == 2) yield return new WaitForSeconds(2.5f);
        else if (RelicsReinforceCheck == 3) yield return new WaitForSeconds(2.5f);

        audioSource.PlayOneShot(RelicsSoundClip[RelicsReinforceCheck - 1], 0.8f); // soundClip은 AudioClip 변수, volume은 소리의 크기 조절값입니다.
    }

    private void TextUpdate()
    {
        OwnRelicsReinforceScroll.text = GameManager.RelicsReinforceScroll + "";

        if (GameManager.Relics_Rank >= 20 && GameManager.Relics_Rank < 30) breakPercent = 5;
        else if (GameManager.Relics_Rank >= 30 && GameManager.Relics_Rank < 35) breakPercent = 10;
        else if (GameManager.Relics_Rank >= 35 && GameManager.Relics_Rank < 40) breakPercent = 15;
        else if (GameManager.Relics_Rank >= 40 && GameManager.Relics_Rank < 45) breakPercent = 20;

        ReinforceChanceText[0].text = 101f - GameManager.Relics_Rank + " %";
        if (GameManager.WarrantLevel[4] >= 1 && GameManager.Relics_Rank >= 20)
        {
            ReinforceChanceText[0].text = 101f - GameManager.Relics_Rank + GameManager.Warrant_Power[4] + " %";
            ReinforceChanceText[1].text = GameManager.Relics_Rank - breakPercent - GameManager.Warrant_Power[4] - 1f + " %";
            ReinforceChanceText[2].text = breakPercent - GameManager.Warrant_Power[4] + " %";
        }
        else if (GameManager.Relics_Rank >= 20)
        {
            ReinforceChanceText[1].text = GameManager.Relics_Rank - breakPercent - 1f + " %";
            ReinforceChanceText[2].text = breakPercent + " %";
        }
        else
        {
            ReinforceChanceText[1].text = GameManager.Relics_Rank - 1f + " %";
            ReinforceChanceText[2].text = 0 + " %";
        }


        UpText.text = GameManager.Relics_Rank * 5f + "% <color=lime>▶</color> " + (GameManager.Relics_Rank * 5f + 5f) + "%";
    }
}
