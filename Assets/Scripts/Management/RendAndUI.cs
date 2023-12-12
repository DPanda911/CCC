using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [Space]
    [SerializeField] private GameObject messagePrefab;

    [Header("Internal Stuff")]
    [SerializeField] private List<GameObject> inventorySlots = new List<GameObject>();

    InventoryManager im;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] dialogueSounds;

    private AudioSource src;

    private Image endingFader;
    private SpriteRenderer fullStreamLayout;
    private bool endingFading = false;
    private float endFadeAmnt = 0f;

    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.Find("GameManager").GetComponent<InventoryManager>();

        RefreshInventoryUI();

        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (endingFading)
        {
            endFadeAmnt += 0.4f * Time.deltaTime;
            endFadeAmnt = Mathf.Clamp(endFadeAmnt, 0f, 1f);

            endingFader.color = new Color(0f, 0f, 0f, endFadeAmnt);
            fullStreamLayout.color = new Color(1f - endFadeAmnt, 1f - endFadeAmnt, 1f - endFadeAmnt, 1f);
        }
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

    public void SpawnDialogue(string message, int mood) {
        Transform peskyCurrentMessage = gameObject.transform.Find("Canvas/UI Message(Clone)");
        if (peskyCurrentMessage != null)
        {
            Destroy(peskyCurrentMessage.gameObject);
        }

        GameObject newMessage = Instantiate(messagePrefab);
        TMP_Text msgText = newMessage.GetComponent<TextMeshProUGUI>();
        msgText.text = message;
        newMessage.transform.SetParent(gameObject.transform.Find("Canvas"));
        newMessage.transform.SetSiblingIndex(1);

        
        int pick;
        if ((mood < 0) || (mood > dialogueSounds.Length - 1)) {
            pick = 0;
            Debug.LogWarning("Dialogue Mood [" + mood + "] doesn't exist. Your mood should be between 0-" + (dialogueSounds.Length - 1));
        } else {
            pick = mood;
        }
        src.clip = dialogueSounds[pick];
        src.pitch = Random.Range(0.97f, 1.03f);
        src.Play();
    }

    public void EndGameFade() {
        endingFader = GameObject.Find("FullFade").GetComponent<Image>();
        fullStreamLayout = GameObject.Find("basicLayout").GetComponent<SpriteRenderer>();
        endingFading = true;
    }
}
