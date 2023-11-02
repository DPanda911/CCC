using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static InventoryManager Instance;

    //Inventory Items
    public List<AllItems> inventoryItems = new List<AllItems>();


    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(AllItems item) //Add Items to the inventory
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
        }
    }

    public void RemoveItem(AllItems item) //Remove Items to the inventory
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
        }
    }


    public enum AllItems //All Items in the game
    {
        KeyBlue, 
        KeyPurple
    }


}
