using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSheen : MonoBehaviour
{
    public Transform rP;
    public Transform rPO;

    private float mirrorAxis = 90f;

    void Update()
    {
        Vector3 euRot = rPO.localRotation.eulerAngles;

        float rotV = (euRot.y * -1f) + mirrorAxis;

        float offs = Mathf.Clamp(Mathf.Sin(rotV * Mathf.Deg2Rad), -1000, 1000);

        float posX = rP.position.x + (offs * rP.position.z * .4f);

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
