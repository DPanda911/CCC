using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform playerTransform;

    float xRotation;
    float yRotation = 0;

    public float delay = 0.1f;
    [Range(0.0f, 90.0f)]public float maxYLook = 90f;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        startTime = Time.time;

        yRotation = playerTransform.eulerAngles.y * 0f;
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
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
        } else {
            yRotation = playerTransform.eulerAngles.y * 0f;
            xRotation = 0;
        }

    }
}
