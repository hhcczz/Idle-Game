using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * File :   UIManager.cs
 * Desc :   Mob + Boss 데미지 표시 하는거 위로 올리기
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

public class UIManager : MonoBehaviour
{
    public float moveSpeed;     // 텍스트 이동속도
    public float AlphaSpeed;    // 투명도 변화속도
    public float destroyTime;
    public decimal damage;
    private Text Damage_text;
    Color Alpha;

    private void Start()
    {
        Damage_text = GetComponent<Text>();
        Alpha = Damage_text.color;
        if(damage == 0) Damage_text.text = "방어함";
        else Damage_text.text = TextFormatter.GetThousandCommaText00(damage).ToString();


        moveSpeed = 1.5f;
        AlphaSpeed = 0.4f;

        Invoke(nameof(DestroyObject), destroyTime);

    }
    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        if (Alpha.a > 180f / 255f)  // 알파값이 N보다 크다면 계속해서 진행
        {
            Alpha.a = Mathf.Lerp(Alpha.a, 0, Time.deltaTime * AlphaSpeed);  // 투명도 조절
            Damage_text.color = Alpha;
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
