using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelChange : MonoBehaviour
{
    private static CharacterPanelChange instance;

    public static CharacterPanelChange Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterPanelChange>();
                if (instance == null)
                {
                    Debug.LogError("CharacterPanelChange 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }

    public Button Ability;
    public Button LevelUp;
    public Button MoneyUp;

    public GameObject AblityPanel;
    public GameObject LevelUpPanel;
    public GameObject MoneyUpPanel;

    int InPanel = -1;       //  1 내정보    2 레벨업   3 골드업 

    // Start is called before the first frame update
    void Start()
    {
        if (AblityPanel.activeSelf == true) InPanel = 1;
        else if (LevelUpPanel.activeSelf == true) InPanel = 2;
        else if (MoneyUpPanel.activeSelf == true) InPanel = 3;
        // Panel 변경
        Ability.onClick.AddListener(() => CharacherChangePanel(1));
        LevelUp.onClick.AddListener(() => CharacherChangePanel(2));
        MoneyUp.onClick.AddListener(() => CharacherChangePanel(3));

        CharacherChangePanel(InPanel);
    }

    //  Panel 변경
    public void CharacherChangePanel(int index)
    {

        if (index == 1) OnButtonClick(AblityPanel, Ability.GetComponent<Image>());
        else if (index == 2) OnButtonClick(LevelUpPanel, LevelUp.GetComponent<Image>());
        else if (index == 3) OnButtonClick(MoneyUpPanel, MoneyUp.GetComponent<Image>());

    }
    private void SetPanelActive(GameObject panel)
    {
        AblityPanel.SetActive(panel == AblityPanel);
        LevelUpPanel.SetActive(panel == LevelUpPanel);
        MoneyUpPanel.SetActive(panel == MoneyUpPanel);
    }
    private void OnButtonClick(GameObject panel, Image clickedButtonImage)
    {
        SetPanelActive(panel);
        SetButtonsAlpha(clickedButtonImage);
    }
    private void SetButtonsAlpha(Image clickedButtonImage)
    {
        float disableAlpha = GameManager.disable_ButtonAlpha;
        float enableAlpha = GameManager.enable_ButtonAlpha;

        Image[] buttons = { Ability.GetComponent<Image>(), LevelUp.GetComponent<Image>(), MoneyUp.GetComponent<Image>() };

        foreach (Image buttonImage in buttons)
        {
            Color buttonColor = buttonImage.color;
            buttonColor.a = buttonImage == clickedButtonImage ? enableAlpha : disableAlpha;
            buttonImage.color = buttonColor;
        }

    }
}
