using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePlace : MonoBehaviour, IInteractable
{
    [SerializeField] InventoryManager.AllItems itemType;
    public GameObject spawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
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

                Instantiate(spawnPrefab, transform.position, Quaternion.identity);

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
