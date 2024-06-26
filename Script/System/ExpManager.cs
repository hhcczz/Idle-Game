using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * File :   Expmanager.cs
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

    AdManager adManager;

    void Start()
    {
        SaveLoadManager.Instance.LoadShop();
        adManager = FindObjectOfType<AdManager>();

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
            if (GameManager.WarrantLevel[18] >= 1) GameManager.Player_MaxExp += GameManager.Player_Level * 6.25f * (1 - GameManager.Warrant_Power[18] / 100f);
            else GameManager.Player_MaxExp = GameManager.Player_Level * 6.25f;
            GameManager.Player_Level++;
            GameManager.Player_PassivePoint++;

            if (GameManager.Player_Level % 10 == 0 && GameManager.Player_Level % 100 == 0) GameManager.Player_MaxExp *= 1.3f;
            if (GameManager.Player_Level % 100 == 0) GameManager.Player_MaxExp *= 1.75f;

            UpdateText();

            PlayerSave saveData = new();
            SaveLoadManager.Instance.SavePlayer(saveData);

        }
    }

    private void UpdateText()
    {
        Level_text.text = "Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_Level);

        if(GameManager.Player_Level < 10) LeftLevel.text = "<size=30>Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_Level) + "</size>";
        else if(GameManager.Player_Level >= 10 && GameManager.Player_Level < 100) LeftLevel.text = "<size=24>Lv. " + TextFormatter.GetThousandCommaText(GameManager.Player_Level) + "</size>";
        else LeftLevel.text = "<size=20>Lv." + TextFormatter.GetThousandCommaText(GameManager.Player_Level) + "</size>";

        Player_CurPassivePoint_Text.text = "PP  :  " + TextFormatter.GetThousandCommaText(GameManager.Player_PassivePoint);
    }

    private void Handle()
    {
        expbar.value = GameManager.Player_CurExp / GameManager.Player_MaxExp; //Handle의 값 0/100
    }

    private void Exp_Text()
    {
        exp_text.text = "Exp : " + TextFormatter.GetFloatPointCommaText_00(GameManager.Player_CurExp) + " / " + TextFormatter.GetFloatPointCommaText_00(GameManager.Player_MaxExp);

    }

    [System.Obsolete]
    public static float ReturnExp(float MonsterValue)
    {
        Debug.Log("AdPlaying[2] = " + AdManager.AdPlaying[2]);
        Debug.Log("AdPowerValue[2] = " + AdManager.AdPowerValue[2]);
        int Warrant_Random = Random.Range(0, 100);  // 경험치 N배 확률

        float exp;
        exp = MonsterValue * (1 + ((float)GameManager.AccessoryEquipExperience + (float)MobScrollManager.MS_UpExp + (float)GameManager.AccessoryOwnExperience) / 100);
        if (GameManager.WarrantLevel[19] >= 1) exp *= 1 + (float)GameManager.Warrant_Power[19] / 100;
        if (AdManager.AdPlaying[2] == true) exp *= (AdManager.AdPowerValue[2] + 100) / 100f;

        if (GameManager.WarrantLevel[0] >= 1 && Warrant_Random < 8) exp *= 1 + (float)GameManager.Warrant_Power[0] / 100;        // 경험치 2배
        if (GameManager.TrandOwned[3] == true) exp *= 6;

        return exp;
    }
}
