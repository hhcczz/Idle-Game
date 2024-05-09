using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineRainbow : MonoBehaviour
{
    public float speed = 1f; // 색상 변경 속도

    private Outline[] outlines; // 아웃라인 컴포넌트 배열
    private float hue; // HSV 색상 공간의 Hue 값

    void Start()
    {
        // 해당 오브젝트에 붙어있는 모든 Outline 컴포넌트를 찾습니다.
        outlines = GetComponentsInChildren<Outline>();

        // 색상 변경 코루틴을 시작합니다.
        StartCoroutine(ChangeRainbowColor());
    }

    IEnumerator ChangeRainbowColor()
    {
        while (true)
        {
            // Hue 값을 변경하여 무지개 색상을 만듭니다.
            hue += Time.deltaTime * speed;
            if (hue > 1f)
                hue -= 1f;

            // 모든 아웃라인의 색상을 설정합니다.
            foreach (Outline outline in outlines)
            {
                outline.effectColor = Color.HSVToRGB(hue, 1f, 1f);
            }

            yield return null;
        }
    }
}
