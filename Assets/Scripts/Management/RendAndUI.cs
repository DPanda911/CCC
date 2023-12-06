using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RendAndUI : MonoBehaviour
{
    /*[[public static RendAndUI instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }*/


    [Header("Configurables")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject itemListObject;
    [SerializeField] private List<Sprite> itemImages = new List<Sprite>(); 

    [Header("Internal Stuff")]
    [SerializeField] private List<GameObject> inventorySlots = new List<GameObject>();

    InventoryManager im;

    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.Find("GameManager").GetComponent<InventoryManager>();

        RefreshInventoryUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Destroy(inventorySlots[i]);
        }
        inventorySlots.Clear();
        List<int> inventy = im.GetInventoryAsInt();
        
        for (int i = 0; (i < 5) && (i < inventy.Count); i++)
        {
            GameObject newSlot = Instantiate(slotPrefab);
            Image slotImage = newSlot.transform.Find("ItemImage").GetComponent<Image>();
            slotImage.sprite = itemImages[inventy[i]];
            newSlot.transform.SetParent(itemListObject.transform);
            newSlot.transform.localPosition = new Vector3(0, 0, 0);
            inventorySlots.Add(newSlot);
        }
    }
}
