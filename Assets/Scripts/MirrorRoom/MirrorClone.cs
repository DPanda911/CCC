using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorClone : MonoBehaviour
{
    public Transform realPlayer;
    public Transform realPlayerOrientation;

    public float mirrorAxis = 90f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(realPlayer.position.x, realPlayer.position.y, realPlayer.position.z * -1f);

        Vector3 euRot = realPlayerOrientation.localRotation.eulerAngles;

        float rotV = (euRot.y * -1f) + mirrorAxis;

        transform.localRotation = Quaternion.Euler(0, rotV, 0);
    }
}
