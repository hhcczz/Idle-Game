using UnityEngine;

/*
 * File :   TextFormatter.cs
 * Desc :   숫자 콤마 반환 함수 모음
 *         
 *
 & Functions
 &  [Public]
 &  : GetThousandCommaText(long data)
 &  : GetDecimalPointCommaText_00(decimal data)
 &  : GetDecimalPointCommaText_0(decimal data)
 &  : GetFloatPointCommaText_00(float data)
 &  : GetFloatPointCommaText_0(float data)
 &  : GetFloatPointCommaText(float data)
 &  : GetThousandCommaText00(decimal data)
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

public class TextFormatter : MonoBehaviour
{
    public static string GetThousandCommaText(long data)
    {
        if (data == 0) return "0"; // 0이면 빈 문자열 반환
        else return string.Format("{0:#,###}", data);
    }

    public static string GetDecimalPointCommaText(decimal data)
    {
        return string.Format("{0:#,###}", data);
    }

    public static string GetDecimalPointCommaText_00(decimal data)
    {
        return string.Format("{0:#,##0.00}", data);
    }

    public static string GetDecimalPointCommaText_0(decimal data)
    {
        return string.Format("{0:#,##0.0}", data);
    }
    public static string GetFloatPointCommaText_00(float data)
    {
        return string.Format("{0:#,##0.00}", data);
    }

    public static string GetFloatPointCommaText_0(float data)
    {
        return string.Format("{0:#,##0.0}", data);
    }

    public static string GetFloatPointCommaText(float data)
    {
        if (data > 10f) return string.Format("{0:##,##}", data);
        else return string.Format("{0:#,##0.0}", data);
    }
    public static string GetThousandCommaText00(decimal data)
    {
        return string.Format("{0:#,###.##}", data);
    }
}
