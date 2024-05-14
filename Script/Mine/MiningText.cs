using UnityEngine;
using UnityEngine.UI;

/*
 * File :   MiningText.cs
 * Desc :   광산 데미지 표시 하는거 위로 올리기
 *
 *
 & Functions
 &  [Public]
 &  :
 &  :
 &
 &  [Protected]
 &  : DestroyObject() - 시간되면 부수기
 &  :
 &  :
 &  :
 &
 &  [Private]
 &
 */
public class MiningText : MonoBehaviour
{
    public float moveSpeed = 1.1f;     // 텍스트 이동속도
    public float alphaSpeed = 3.5f;    // 투명도 변화속도
    public float destroyTime = 1.2f;   // 텍스트가 사라지기 전 대기 시간

    private Text textComponent;
    private Color alphaColor;

    private void Start()
    {
        textComponent = GetComponent<Text>();
        alphaColor = textComponent.color;

        Invoke(nameof(DestroyObject), destroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        if (alphaColor.a > 50f / 255f)  // 알파값이 50/255보다 크다면 계속해서 진행
        {
            alphaColor.a = Mathf.Lerp(alphaColor.a, 0, Time.deltaTime * alphaSpeed);  // 투명도 조절
            textComponent.color = alphaColor; // 변경된 알파값을 설정합니다.
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
