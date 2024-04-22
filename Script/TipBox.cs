using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   TipBox.cs
 * Desc :   팁 공지 띄우기
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


public class TipBox : MonoBehaviour
{
    public float moveSpeed = 100f; // 텍스트 이동 속도

    public RectTransform rectTransform;
    public Text text;

    private string[] AnnounceText;
    private int AnnounceOrder;

    void Start()
    {
        AnnounceOrder = 0;

        AnnounceText = new string[5];
        AnnounceText[0] = "현재 스테이지 몬스터가 너무 강력하면 더 약한 몬스터를 소환해서 잡아보세요!";
        AnnounceText[1] = "광산 컨텐츠를 열심히 하면 스펙업이 따라옵니다! 권능을 획득해보세요!";
        AnnounceText[2] = "스테이지를 금방 넘어가기보다는 비급서를 획득해 장비를 얻은 뒤 넘어가주세요!";
        AnnounceText[3] = "다이아몬드는 다양한 곳에서 획득 가능합니다! 포기하지말고 스펙업을 시도해주세요!";
        AnnounceText[4] = "방어력 관통은 보스를 처치하는데 가장 중요한 포인트입니다!";

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
        if (AnnounceOrder == 4) AnnounceOrder = 0;
        else AnnounceOrder++;

        // 애니메이션이 끝나면 게임 오브젝트 비활성화
        StartCoroutine(AnimateText());
    }
}
