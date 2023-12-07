using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public bool statueRight = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Void")
        {
            statueRight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Void")
        {
            statueRight = false;
        }
    }

}
