using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateInteract : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        CheckRotation CR = gameObject.GetComponent<CheckRotation>();
        if (!CR.rotateCorrect)
        {
           transform.Rotate(0, 72, 0);
        }
        

        
    }
}
