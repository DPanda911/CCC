using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePickup : MonoBehaviour, IInteractable
{
    [SerializeField] InventoryManager.AllItems itemType;
    // Start is called before the first frame update
    public GameObject candle;
    public bool isActive;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {

        InventoryManager.Instance.AddItem(itemType);
        Debug.Log("HI");
        if (isActive)
        {
            InventoryManager.Instance.AddItem(itemType);
            candle.SetActive(false);
            Debug.Log("Hi");
        }

        if (!isActive)
        {
            InventoryManager.Instance.RemoveItem(itemType);
            candle.SetActive(true);
        }
    }
}

    
