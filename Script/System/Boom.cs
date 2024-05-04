using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * File :   Boom.cs
 * Desc :   땅에 떨어진 entity 제거
 *         
 *
 & Functions
 &  [Public]
 &  : 
 &
 &  [Protected]
 &  : 
 &  : 
 &  : 
 &  : 
 &
 &  [Private]
 &  : Destroy() - 파괴
 *
 */


public class Boom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f); // 0.5초 후에 실행됨

        Destroy(gameObject); // 자기 자신을 삭제
    }
}
