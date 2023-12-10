using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static InventoryManager Instance;

    //Inventory Items
    public List<AllItems> inventoryItems = new List<AllItems>();

    private bool firstBatteryPack = false;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddItem(AllItems item) //Add Items to the inventory
    {
        if (!inventoryItems.Contains(item) || item == AllItems.BatteryPack)
        {
            inventoryItems.Add(item);
        }
        GameManager.instance.AudienceWoo(0, 0.00075f);
        UpdateUI();

        if (item == AllItems.BatteryPack)
        {
            if (firstBatteryPack)
            {
                int dialogueChoice = Random.Range(0, 4);
                switch (dialogueChoice)
                {
                    case 0:
                        GameManager.instance.DialogueMessage("A spare <color=#fa7>battery pack</color>? Don't mind if I do!", null, 1);
                        break;
                    case 1:
                        GameManager.instance.DialogueMessage("Who just leaves perfectly good <color=#fa7>battery packs</color> lying around like this?", null, 0);
                        break;
                    case 2:
                        GameManager.instance.DialogueMessage("Hope whoever owns this <color=#fa7>battery pack</color> doesn't mind me borrowing it for a bit.", null, 2);
                        break;
                    case 3:
                        GameManager.instance.DialogueMessage("Another <color=#fa7>battery pack</color>. Sweet.", null, 0);
                        break;
                }
            } else {
                firstBatteryPack = true;
                GameManager.instance.DialogueMessage("A <color=#fa7>battery pack</color>! What are you doing here? You should be helpful!<br>If I hit <color=#ff7>[R]</color> while holding one of these, I'll be able to charge my phone.", null, 1);
            }
        }
    }

    public void RemoveItem(AllItems item) //Remove Items to the inventory
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
        }
        GameManager.instance.AudienceWoo(0, -0.00075f);
        UpdateUI();
    }

    public bool HasItem(AllItems item)
    {
        return inventoryItems.Contains(item);
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
