using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static InventoryManager Instance;

    //Inventory Items
    public List<AllItems> inventoryItems = new List<AllItems>();


    private void Start()
    {
        Instance = this;
    }

    public void AddItem(AllItems item) //Add Items to the inventory
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
        }

        UpdateUI();
    }

    public void RemoveItem(AllItems item) //Remove Items to the inventory
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
        }
        UpdateUI();
    }

    public List<int> GetInventoryAsInt()
    {
        Debug.Log("Getting Inventory as Integers...");
        List<int> inv = new List<int>();
        
        for (int i = 0; (i < 5) && (i < inventoryItems.Count); i++)
        {
            inv.Add((int) inventoryItems[i]);
            Debug.Log("Adding " + inventoryItems[i] + " ( " + (int) inventoryItems[i] + " )");
        }
        Debug.Log("Done!");

        return inv;
    }

    private void UpdateUI()
    {
        RendAndUI rau = GameObject.Find("Renderer").GetComponent<RendAndUI>();
        if (rau != null)
        {
            rau.RefreshInventoryUI();
        }
    }


    public enum AllItems //All Items in the game
    {
        KeyBlue = 0, 
        KeyPurple = 1,
        KeyGreen = 2,
        KeyOrange = 3,
        CandleOrange = 4,
        CandleYellow = 5,
        CandleBlue = 6,
        CandlePink = 7,
        CandleGreen = 8,
        TutorialKey = 9,
        BatteryPack = 10
    }


}
