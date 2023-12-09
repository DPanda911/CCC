using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameManager.instance.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
