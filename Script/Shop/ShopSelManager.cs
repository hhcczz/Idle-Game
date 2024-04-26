using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelManager : MonoBehaviour
{
    public Button[] ShopSelBtn;

    public GameObject[] ShopPanel;

    private void Start()
    {
        for(int i = 0; i < ShopSelBtn.Length; i++)
        {
            int index = i;

            ShopSelBtn[index].onClick.AddListener(() => ShopSel(index));
        }

    }

    private void OnEnable()
    {
        for (int i = 0; i < ShopPanel.Length; i++)
        {
            Image image = ShopSelBtn[i].GetComponent<Image>();
            if (ShopPanel[i].activeSelf == true) image.color = ColorManager.ColorChange("민트색");
            else image.color = ColorManager.ColorChange("기본색");
        }
    }

    private void ShopSel(int index)
    {
        ShopPanel[index].SetActive(true);
        if (index == 0)
        {
            
            ShopPanel[1].SetActive(false);
            ShopPanel[2].SetActive(false);
        }
        if (index == 1)
        {
            ShopPanel[0].SetActive(false);
            ShopPanel[2].SetActive(false);
        }
        else if(index == 2)
        {
            ShopPanel[0].SetActive(false);
            ShopPanel[1].SetActive(false);
        }

        for(int i = 0; i < ShopPanel.Length; i++)
        {
            Image image = ShopSelBtn[i].GetComponent<Image>();
            if(ShopPanel[i].activeSelf == true) image.color = ColorManager.ColorChange("민트색");
            else image.color = ColorManager.ColorChange("기본색");
        }
    }


}
