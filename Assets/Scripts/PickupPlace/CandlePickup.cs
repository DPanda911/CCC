using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePickup : MonoBehaviour, IInteractable
{
    [SerializeField] InventoryManager.AllItems itemType;
    public GameObject candle;
    public bool isActive;

    [Header("Pickup Message")]
    [SerializeField] private bool useMessage;
    [TextArea][SerializeField] private string pickupMessage;
    [SerializeField] private string pickupTag;
    [SerializeField] private int pickupMood;

    [Header("Audience Sway")]
    [SerializeField] private bool useAudienceSway;
    [SerializeField] private float pickupVCAddition;
    [SerializeField] private float pickupRateAddition;
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

        InventoryManager.Instance.AddItem(itemType);
        Debug.Log("HI");
        if (isActive)
        {
            InventoryManager.Instance.AddItem(itemType);
            candle.SetActive(false);
            Debug.Log("Hi");
            if (useMessage) {
                GameManager.instance.DialogueMessage(pickupMessage, pickupTag, pickupMood);
            }
            if (useAudienceSway) {
                GameManager.instance.AudienceWoo(pickupVCAddition, pickupRateAddition);
            }
        }

        if (!isActive)
        {
            InventoryManager.Instance.RemoveItem(itemType);
            candle.SetActive(true);
        }
    }
}

    
