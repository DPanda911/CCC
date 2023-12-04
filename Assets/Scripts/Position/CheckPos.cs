using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPos : MonoBehaviour
{
    // Start is called before the first frame update

    public bool posCorrect = false;
    public float correctXpos = 0;
    public float correctYpos = 0;
    public float correctZpos = 0;

    public GameObject Pos;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        
        if (correctXpos == Pos.transform.position.x)
        {
            if (correctYpos == Pos.transform.position.y)
            {
                if (correctZpos == Pos.transform.position.z)
                {
                    posCorrect = true;
                }
            }

        }
    }
}
