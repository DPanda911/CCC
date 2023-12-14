using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public bool statueRight = false;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.constraints = RigidbodyConstraints.FreezeAll;
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
