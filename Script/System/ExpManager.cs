using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   ExpManager.cs
 * Desc :   경험치 관리
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
 &  : UpdateText()  - 레벨 텍스트 관리
 &  : Handle()      - 핸들 관리
 &  : Exp_Text()    - 경험치 텍스트 관리
 *
 */

public class ExpManager : MonoBehaviour
{
    public Text Level_text;

    [SerializeField] //외부스크립트에서 수정을 하지 못하게 하는 변수(insfactor에서는 접근가능)

    private Slider expbar;
    public Text exp_text;
    public Text Player_CurPassivePoint_Text;
    public Text LeftLevel;

    void Start()
    {

        expbar = GetComponent<Slider>();
        expbar.value = GameManager.Player_CurExp / GameManager.Player_MaxExp;

        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (expbar.value == 0) transform.Find("Fill Area").gameObject.SetActive(false);
        else transform.Find("Fill Area").gameObject.SetActive(true);

        Exp_Text();
        Handle();

        // 레벨업 조건 및 레벨업 관리
        if(GameManager.Player_CurExp >= GameManager.Player_MaxExp)
        {
            GameManager.Player_CurExp -= GameManager.Player_MaxExp;
            if (GameManager.WarrantLevel[18] >= 1) GameManager.Player_MaxExp = GameManager.Player_Level * 1.75f * (1 - GameManager.Warrant_Power[18] / 100f);
            else GameManager.Player_MaxExp = GameManager.Player_Level * 2.25f;
            GameManager.Player_Level++;
            GameManager.Player_PassivePoint++;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        Level_text.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_Level);
        LeftLevel.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_Level);
        Player_CurPassivePoint_Text.text = "PP  :  " + TextFormatter.GetThousandCommaText(GameManager.Player_PassivePoint);
    }

    private void Handle()
    {
        expbar.value = GameManager.Player_CurExp / GameManager.Player_MaxExp; //Handle의 값 0/100
    }

    private void Exp_Text()
    {
        exp_text.text = TextFormatter.GetFloatPointCommaText_00(GameManager.Player_CurExp) + " / " + TextFormatter.GetFloatPointCommaText_00(GameManager.Player_MaxExp);

    }
}
