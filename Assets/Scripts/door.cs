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

    [SerializeField] InventoryManager.AllItems requiredItem;

    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    public void Interact()
    {
        if (hasRequiredItem(requiredItem))
        {
            if (!doorLockedStatus())
            {
                Debug.Log("Hi");
                SceneManager.LoadScene(sceneLoad);
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

    public bool doorLockedStatus()
    {
        return true; 
    }
}
