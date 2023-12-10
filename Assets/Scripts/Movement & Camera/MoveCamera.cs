using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    [SerializeField] float bobAmount = 0.01f;
    [SerializeField] float bobRate = 5f;
    [SerializeField] Rigidbody playerBody;

    float bobSet = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 plrMovement = new Vector2(playerBody.velocity.x, playerBody.velocity.z);
        float bobMult = plrMovement.magnitude;

        bobSet += bobMult * Time.deltaTime;

        float bobOffset = Mathf.Sin(bobSet * bobRate) * bobAmount * bobMult;

        transform.position = new Vector3(cameraPosition.position.x, cameraPosition.position.y + bobOffset, cameraPosition.position.z);
    }
}
