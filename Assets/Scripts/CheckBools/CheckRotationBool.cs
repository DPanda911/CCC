using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRotationBool : MonoBehaviour
{
    public GameObject prefab;
    
    public bool check2 = false;
    public string tag;
    public int amountOfObj;
    public bool check;

    public GameObject boo;
    


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
        if (check2 && !GetComponent<CheckPosBool>().check)
        {
            GetComponent<CheckPosBool>().checkPosBool();

            Debug.Log("Bitch what?");

        }



    }

    private void checkRotationBool()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Pentagram");
        bool allTrue = true;
        //Debug.Log(taggedObjects.Length);
        if (taggedObjects.Length == amountOfObj)
        {

            foreach (GameObject obj in taggedObjects)
            {
                if (!obj.GetComponent<CheckRotation>().rotateCorrect)
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
                check2 = true;
                
                

            }

        }
    }


}