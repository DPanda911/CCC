using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool rotateCorrect;
    public int correctYRotation = 0;
    private int rotation; 
    

    [SerializeField]
    float eulerAngY;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        eulerAngY = transform.localEulerAngles.y;
        
        Mathf.Round(eulerAngY);
        int check = (int)eulerAngY;
        if (check == correctYRotation)
        {
            rotateCorrect = true;
            
        }
    }
}
