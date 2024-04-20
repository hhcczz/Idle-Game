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
 &  : 
 *
 */
public class MiningText : MonoBehaviour
{
    public float moveSpeed;     // 텍스트 이동속도
    public float AlphaSpeed;    // 투명도 변화속도
    public float destroyTime;

    private Color Alpha;

    private void Start()
    {
        moveSpeed = 1.5f;
        AlphaSpeed = 0.4f;
        Alpha = GetComponent<Text>().color; // 알파값을 초기화합니다.

        Invoke(nameof(DestroyObject), destroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        if (Alpha.a > 50f / 255f)  // 알파값이 N보다 크다면 계속해서 진행
        {
            Alpha.a = Mathf.Lerp(Alpha.a, 0, Time.deltaTime * AlphaSpeed);  // 투명도 조절
            GetComponent<Text>().color = Alpha; // 변경된 알파값을 설정합니다.
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
