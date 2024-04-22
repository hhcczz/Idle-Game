using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   MineTipBox.cs
 * Desc :   광산 팁 공지 띄우기
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
 &  : AnimateText() 시간마다 공지 띄우고 무한반복 시키기
 *
 */


public class MineTipBox : MonoBehaviour
{
    private float moveSpeed = 110f; // 텍스트 이동 속도

    public RectTransform rectTransform;
    public Text text;

    private string[] AnnounceText;
    private int AnnounceOrder;

    void OnEnable()
    {

        AnnounceOrder = 0;

        AnnounceText = new string[3];
        AnnounceText[0] = "현재 스테이지 광물이 단단하면 더 약한 광물을 소환해서 채광해보세요!";
        AnnounceText[1] = "광산 컨텐츠를 진행하다보면 자연스럽게 캐릭터가 스펙업 됩니다! 열심히 해보세요.";
        AnnounceText[2] = "광산은 클릭 쿨타임이 있습니다. 업그레이드를 통해서 높혀보세요";

        // 애니메이션 실행
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        text.text = AnnounceText[AnnounceOrder];
        rectTransform.anchoredPosition = new Vector2(Screen.width, 0);  // 텍스트가 오른쪽에서 시작하도록 초기 위치 설정

        while (rectTransform.anchoredPosition.x > -1100)
        {
            // 텍스트 이동
            rectTransform.anchoredPosition -= new Vector2(moveSpeed * Time.deltaTime, 0);

            yield return null;
        }
        if (AnnounceOrder == 2) AnnounceOrder = 0;
        else AnnounceOrder++;

        // 애니메이션이 끝나면 게임 오브젝트 비활성화
        StartCoroutine(AnimateText());
    }
}
