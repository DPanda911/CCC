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
    public bool isDoorLocked;

    GameManager gm;

    [SerializeField] InventoryManager.AllItems requiredItem;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                    GoIntoDoor();
                }
            }
        }
        else
        {
            Debug.Log("doesntRequireItem");
            if (!isDoorLocked)
            {
                Debug.Log("Hi");
                GoIntoDoor();
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

    private void GoIntoDoor()
    {
        gm.SetSpawnPos(playerPosition, playerRotation);
        SceneManager.LoadScene(sceneLoad);
    }

    
}
