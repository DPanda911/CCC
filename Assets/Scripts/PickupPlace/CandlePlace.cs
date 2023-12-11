using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePlace : MonoBehaviour, IInteractable
{
    [SerializeField] InventoryManager.AllItems itemType;
    [SerializeField] string itemTag;
    public GameObject spawnPrefab;
    public float xPos;
    public float yPos;
    public float zPos;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.CheckForMiscTag(itemTag)) {
            Instantiate(spawnPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Interact()
    {
        

            if (hasRequiredItem(itemType))
            {
                Debug.Log("HasRequired");
                InventoryManager.Instance.RemoveItem(itemType);

                Instantiate(spawnPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);

                GameManager.instance.NewMiscTag(itemTag);
            }
            else
            {
                Debug.Log("doesntRequireItem");
            }
        
  
    }

    public bool hasRequiredItem(InventoryManager.AllItems itemRequired)
    {
        if (InventoryManager.Instance.inventoryItems.Contains(itemRequired))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
