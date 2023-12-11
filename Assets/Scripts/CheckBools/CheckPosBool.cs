using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosBool : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public bool check = false;
    
    public string tag;
    public int amountOfObj;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!check)
        {
            checkPosBool();
        }

    }

    public void checkPosBool()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        bool allTrue = true;
        Debug.Log(taggedObjects.Length);
        if (taggedObjects.Length == amountOfObj)
        {
            
            foreach (GameObject obj in taggedObjects)
            {
                if (!obj.GetComponent<CheckPos>().posCorrect)
                {
                    allTrue = false;
                    break;
                }
                else
                {
                    allTrue = true;
                }
            }
            if (allTrue)
            {
                check = true;
                Instantiate(prefab);

                GameManager.instance.DialogueMessage("That looks about right. Seems like it spawned <color=#f7f>something</color> in the middle of the room, too.", "CandleRoomSomethingSpawned", 0);

            }

        }
    }


}
