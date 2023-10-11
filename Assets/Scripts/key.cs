using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    [SerializeField] door doorStatus;
    // Start is called before the first frame update
    [SerializeField] InventoryManager.AllItems itemType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(itemType);
            Destroy(gameObject);
            doorStatus.doorLockedStatus();
        }
               
    }
}
