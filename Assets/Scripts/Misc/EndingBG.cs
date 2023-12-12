using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBG : MonoBehaviour
{
    [SerializeField] GameObject grassPlane;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject wheel;

    [Space]
    [SerializeField] float driveSpeed;
    float driveDistance;

    public Vector3 targetCamAngle = new Vector3(0f, 0f, 0f);
    private Vector3 curCamAngle = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        driveDistance += driveSpeed * Time.deltaTime;

        float planeOffset = Mathf.PerlinNoise(driveDistance * 0.2f, 0f);
        grassPlane.transform.localPosition = new Vector3(0f, -0.6f + (planeOffset * 0.075f), -1f * (driveDistance % 10f));

        float camYOffset = Mathf.PerlinNoise(driveDistance * 1f, 0f);
        cam.transform.localPosition = new Vector3(-0.1f, 0.75f + (camYOffset * 0.01f), 0f);

        float rotOffset = Mathf.PerlinNoise(0f, driveDistance * 0.2f);

        curCamAngle = Vector3.Lerp(curCamAngle, targetCamAngle, 0.05f);

        cam.transform.eulerAngles = new Vector3(2f, 0f, -1f + (rotOffset)) + curCamAngle;

        float steerWheelOffset = Mathf.PerlinNoise(driveDistance * 0.04f , driveDistance * 0.1f);
        wheel.transform.eulerAngles = new Vector3(5.5f, 0f, -4f + (steerWheelOffset * 10f));
    }
}
