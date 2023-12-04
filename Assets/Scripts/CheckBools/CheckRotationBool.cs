using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRotationBool : MonoBehaviour
{
    public GameObject prefab;
    public bool check = false;
    public bool check2 = false;
    public string tag;
    public int amountOfObj;
   
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!check2)
        {
            checkRotationBool();
        }
        if (check2 && !check)
        {
            GetComponent<CheckPosBool>().checkPosBool();
            check = true; 

        }
        

    }

    private void checkRotationBool()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Pentagram");
        bool allTrue = true;
        Debug.Log(taggedObjects.Length);
        if (taggedObjects.Length == amountOfObj)
        {

            foreach (GameObject obj in taggedObjects)
            {
                if (!obj.GetComponent<CheckRotation>().rotateCorrect)
                {
                    allTrue = false;
                }
                else
                {
                    allTrue = true;
                }
            }
            if (allTrue)
            {
                check2 = true;
                
                

            }

        }
    }


}