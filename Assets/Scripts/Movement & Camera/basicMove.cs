using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMove : MonoBehaviour
{
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, 4), transform.position.y, transform.position.z);
    }
}
