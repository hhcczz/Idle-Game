using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   ColorManager.cs
 * Desc :   버튼 및 텍스트 색생 변경
 *         
 *
 & Functions
 &  [Public]
 &  : ColorChange(string ColorName) - 색상 이름 넣으면 변경 가능
 &  : 
 &
 &  [Protected]
 &  : 
 &  : 
 &  : 
 &  : 
 &
 &  [Private]
 &  : 
 *
 */
public class ColorManager : MonoBehaviour
{
    static Color NormalColor = new(27f / 255f, 27f / 255f, 28f / 255f);             // 일반 버튼색
    static Color SpecialColor = new(0f, 1f, 143f / 255f);                           // 민트색
    static Color VColor282829 = new(28f / 255f, 28f / 255f, 29f / 255f, 98f / 255f); // 투명도
    static Color RelicsColor = new(228f / 255f, 99f / 255f, 87f / 255f);
    static Color LimeColor = new(0f, 1f, 0f);
    static Color Color144 = new(144f / 255f, 0f, 0f);
    static Color[] WeaponColor = 
    {
        new(75f / 255f, 75f / 255f, 75f / 255f),
        new(255f / 255f, 127f / 255f, 0f),
        new(254f / 255f, 253f / 255f, 0f),
    };

public static Color ColorChange(string ColorName)
    {
        if (ColorName == "기본색") return NormalColor;
        else if (ColorName == "구매가능") return SpecialColor;
        else if (ColorName == "투명기본색") return VColor282829;
        else if (ColorName == "빨간색") return Color.red;
        else if (ColorName == "노랑색") return Color.yellow;
        else if (ColorName == "파란색") return Color.blue;
        else if (ColorName == "초록색") return Color.green;
        else if (ColorName == "하늘색") return Color.cyan;
        else if (ColorName == "하얀색") return Color.white;
        else if (ColorName == "검정색") return Color.black;
        else if (ColorName == "라임색") return LimeColor;
        else if (ColorName == "적홍색") return Color144;
        else if (ColorName == "무기1번색") return WeaponColor[0];
        else if (ColorName == "무기2번색") return WeaponColor[1];
        else if (ColorName == "무기3번색") return WeaponColor[2];
        else if (ColorName == "밝은주황색") return RelicsColor;

        else return NormalColor;
    }

}
