using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class door : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    public string sceneLoad;
    public Vector3 playerPosition;
    public float playerRotation;
    public bool requiresItem;
    public bool requiresMultipleItems;
    public bool isDoorLocked;

    GameManager gm;
    Interactor intrac;
    Fader fd;

    private IEnumerator crt;

    [SerializeField] InventoryManager.AllItems requiredItem;
    [SerializeField] InventoryManager.AllItems requiredItem2;
    [SerializeField] InventoryManager.AllItems requiredItem3;
    [SerializeField] InventoryManager.AllItems requiredItem4;

    [Header("Open Fail Message")]
    [TextArea][SerializeField] string failMessage = "It won't budge.";
    [SerializeField] int failMood = 2;
    
    [Space]
    [SerializeField] GameManager.SoundTypes openSound = GameManager.SoundTypes.BasicDoor;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        intrac = GameObject.Find("Main Camera").GetComponent<Interactor>();
        fd = GameObject.Find("Black Fader").GetComponent<Fader>();
        crt = GoInDoor();
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    public void Interact()
    {
        bool succeeded = false;
        if (requiresItem)
        {
            Debug.Log("requiresItem check");
            if (hasRequiredItem(requiredItem))
            {
                Debug.Log("itemHadAndDoorLoced");
                if (!isDoorLocked)
                {
                    Debug.Log("Hi");

                    StartCoroutine(crt);
                    succeeded = true;
                }
            }
        }
        else if (requiresMultipleItems)
        {
            if(hasRequiredItems(requiredItem, requiredItem2, requiredItem3, requiredItem4))
            {
                Debug.Log("itemHadAndDoorLoced");
                if (!isDoorLocked)
                {
                    Debug.Log("Hi");

                    StartCoroutine(crt);
                    succeeded = true;
                }
            }
        }
        else
        {
            Debug.Log("doesntRequireItem");
            if (!isDoorLocked)
            {
                Debug.Log("Hi");
                StartCoroutine(crt);
                    succeeded = true;
            }
        }

        if (!succeeded) {
            gm.DialogueMessage(failMessage, null, failMood);
        }

    }

    public bool hasRequiredItem(InventoryManager.AllItems itemRequired)
    {
        if (InventoryManager.Instance.inventoryItems.Contains(itemRequired)){
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool hasRequiredItems(InventoryManager.AllItems itemRequired, InventoryManager.AllItems itemRequired2, InventoryManager.AllItems itemRequired3, InventoryManager.AllItems itemRequired4)
    {
        if (InventoryManager.Instance.inventoryItems.Contains(itemRequired) && InventoryManager.Instance.inventoryItems.Contains(itemRequired2) && InventoryManager.Instance.inventoryItems.Contains(itemRequired3) && InventoryManager.Instance.inventoryItems.Contains(itemRequired4))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator GoInDoor()
    {
        gm.SetSpawnPos(playerPosition, playerRotation);
        gm.EZFreeze();
        gm.PlaySound(openSound);
        intrac.canInteract = false;
        fd.leaving = true;

        if (openSound == GameManager.SoundTypes.Ladder) {
            gm.StopUpdating();
            RendAndUI rau = GameObject.Find("Renderer").GetComponent<RendAndUI>();
            rau.EndGameFade();
            yield return new WaitForSeconds(4f);
        } else {
            yield return new WaitForSeconds(.35f);
        }
        SceneManager.LoadScene(sceneLoad);
    }

    
}
