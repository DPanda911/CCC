using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public bool statueRight = false;
    public float posX;
    public float posY;
    public float posZ;
    Rigidbody rb;

    Vector3 newPos;

    [SerializeField] string statueTag;

    GameManager gm;
    AudioSource src;
    [SerializeField] AudioClip soundToMake;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        newPos = new Vector3(posX, posY - 0.4f, posZ);
        
        gm = GameManager.instance;

        if (gm.CheckForMiscTag(statueTag)) {
            statueRight = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = newPos;
        }

        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.volume = 1f;
        src.spatialBlend = 1f;
        src.maxDistance = 250f;
        src.clip = soundToMake;
    }

    // Update is called once per frame
    void Update()
    {
        if (statueRight) {
            transform.position = Vector3.Lerp(transform.position, newPos, 0.02f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Void") && (!statueRight))
        {
            statueRight = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            gm.NewMiscTag(statueTag);
            src.Play();
        }
    }

}
