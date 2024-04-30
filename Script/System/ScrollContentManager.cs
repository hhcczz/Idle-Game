using UnityEngine;
using UnityEngine.UI;

public class ScrollContentManager : MonoBehaviour
{
    public RectTransform content; // Scroll View의 Content
    public RectTransform[] objectsInContent; // Content 안에 들어있는 객체들
    public GameObject[] objectBox;
    private void UpdateObjectState()
    {
        for(int i = 0; i < objectsInContent.Length; i++)
        {
            if (content.localPosition.y > Mathf.Abs(objectsInContent[i].localPosition.y) + 400 || content.localPosition.y + objectsInContent[i].localPosition.y < -1000) objectBox[i].SetActive(false);
            else objectBox[i].SetActive(true);
        }
    }

    private void OnEnable()
    {
        // 주기적으로 상태를 업데이트하기 위해 InvokeRepeating 호출
        InvokeRepeating(nameof(UpdateObjectState), 0f, 0.25f); // 0.25초마다 호출하도록 설정
    }

    private void OnDisable()
    {
        // InvokeRepeating 중지
        CancelInvoke(nameof(UpdateObjectState));
    }
}
