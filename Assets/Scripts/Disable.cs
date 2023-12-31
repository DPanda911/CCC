using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField] InventoryManager.AllItems itemType;

    [SerializeField] bool destroyItem = false;

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

            Destroy(gameObject);

           

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
            if (destroyItem)
            {
                InventoryManager.Instance.RemoveItem(itemRequired);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
