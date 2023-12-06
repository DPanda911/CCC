using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionBool : MonoBehaviour
{
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
            checkCollisionBool();
        }

    }

    public void checkCollisionBool()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        bool allTrue = true;
        Debug.Log(taggedObjects.Length);
        if (taggedObjects.Length == amountOfObj)
        {

            foreach (GameObject obj in taggedObjects)
            {
                if (!obj.GetComponent<StatueCheck>().statueRight)
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

            }

        }
    }
}
