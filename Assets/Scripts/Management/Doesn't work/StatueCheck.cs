using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public bool statueRight = false;
    public float posX;
    public float posY;
    public float posZ;
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
            transform.position = new Vector3(posX, posY, posZ);
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
