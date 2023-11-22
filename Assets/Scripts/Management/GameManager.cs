using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 spawnPos;
    public float spawnOrientation = 999999;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetSpawnPos(Vector3 newSpawn, float newOrient)
    {
        spawnPos = newSpawn;
        spawnOrientation = newOrient;
    }

    public void GetSpawnPos(out Vector3 spawn, out float ori)
    {
        spawn = spawnPos;
        ori = spawnOrientation;
    }

    public void EZFreeze()
    {
        Rigidbody plr = GameObject.Find("Player").GetComponent<Rigidbody>();
        MouseRotation mr = GameObject.Find("Main Camera").GetComponent<MouseRotation>();
        UIPhone uip = GameObject.Find("Phone UI").GetComponent<UIPhone>();

        plr.constraints = RigidbodyConstraints.FreezeAll;
        mr.sensX = 0;
        mr.sensY = 0;
        uip.canPhone = false;
    }
}
