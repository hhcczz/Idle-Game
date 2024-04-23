using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * File :   InventoryManager.cs
 * Desc :   인벤토리 관리
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
 &  : InventoryClose() - 인벤토리 닫기
 *
 */
public class InventoryManager : MonoBehaviour
{
    public Button OpenInventoryBtn;
    public GameObject Inventory;
    public Button CloseInventoryBtn;
    public Text[] InventoryItemNum;

    // Start is called before the first frame update
    void Start()
    {
        OpenInventoryBtn.onClick.AddListener(InventoryOpen);
        CloseInventoryBtn.onClick.AddListener(InventoryClose);
    }

    void InventoryOpen()
    {
        InventoryItemNum[0].text = TextFormatter.GetThousandCommaText(GameManager.Player_Money);
        InventoryItemNum[1].text = TextFormatter.GetThousandCommaText(GameManager.Player_Diamond);
        InventoryItemNum[2].text = TextFormatter.GetThousandCommaText(GameManager.Player_RedStone);
        InventoryItemNum[3].text = TextFormatter.GetThousandCommaText(GameManager.RelicsReinforceScroll);
        InventoryItemNum[4].text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[0]);
        InventoryItemNum[5].text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[1]);
        InventoryItemNum[6].text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[2]);
        InventoryItemNum[7].text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[3]);
        InventoryItemNum[8].text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[4]);
        InventoryItemNum[9].text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[5]);
        InventoryItemNum[10].text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[6]);
        InventoryItemNum[11].text = TextFormatter.GetThousandCommaText(GameManager.PlayerHaveMobScroll[7]);
        InventoryItemNum[12].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarGrey);
        InventoryItemNum[13].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarBrown);
        InventoryItemNum[14].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarBlue);
        InventoryItemNum[15].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarGreen);
        InventoryItemNum[16].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarRed);
        InventoryItemNum[17].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarYellow);
        InventoryItemNum[18].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarPurple);
        InventoryItemNum[19].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarOrange);
        InventoryItemNum[20].text = TextFormatter.GetThousandCommaText(GameManager.HaveStarDark);

        Inventory.SetActive(true);
    }

    void InventoryClose()
    {
        Inventory.SetActive(false);
    }
}
