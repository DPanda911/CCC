using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlane : MonoBehaviour
{
    [SerializeField] private float driveSpeed = 3f;

    public float drivenDistance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        drivenDistance += driveSpeed * Time.deltaTime;
        transform.position = new Vector3(-1f * (drivenDistance % 10f), 0, -50f);
    }
}
