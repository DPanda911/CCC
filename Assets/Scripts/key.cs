using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour, IInteractable
{
    [SerializeField] door doorStatus;
    [SerializeField] InventoryManager.AllItems itemType;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
   
            InventoryManager.Instance.AddItem(itemType);
            Destroy(gameObject);
            doorStatus.isDoorLocked = false;
               
    }
}
