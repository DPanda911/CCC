using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationAroundPt2 : MonoBehaviour
{
    public GameObject Planet;
    // Start is called before the first frame update
    void Start()
    {

        Planet = GameObject.Find("Planet");
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Planet.transform.position, new Vector3(0f, -1f, 0f), 45 * Time.deltaTime);
    }
}
