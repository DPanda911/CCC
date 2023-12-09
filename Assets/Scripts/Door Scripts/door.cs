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

    public int itemRequiredCount = 0;

    GameManager gm;
    Interactor intrac;
    Fader fd;

    private IEnumerator crt;

    [SerializeField] InventoryManager.AllItems requiredItem;
    
    

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
            }
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

    

    private IEnumerator GoInDoor()
    {
        gm.SetSpawnPos(playerPosition, playerRotation);
        gm.EZFreeze();
        intrac.canInteract = false;
        fd.leaving = true;
        yield return new WaitForSeconds(.35f);
        SceneManager.LoadScene(sceneLoad);
    }

    
}
