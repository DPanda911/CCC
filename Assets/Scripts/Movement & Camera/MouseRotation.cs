using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public float delay = 0.1f;
    [Range(0.0f, 90.0f)]public float maxYLook = 90f;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse Input

        //rotate the camera and orientation
        if (Time.time > startTime + delay) {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -maxYLook, maxYLook);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        } else {
            xRotation = 0;
            yRotation = 0;
        }

    }
}
