using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   

    

    public Vector3 spawnPos;
    public float spawnOrientation = 999999;

    public float viewerCount = 250;
    private int viewerCountInt = 250;
    private float timeSinceLastUpdate = 0;

    [SerializeField] private float vc_rate = 0.01f;
    [SerializeField] private float vc_grav = -0.000004f;

    [SerializeField] private List<string> visitedRooms = new List<string>();


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
        timeSinceLastUpdate = Time.time;
    }

    void Update()
    {
        viewerCountInt = Mathf.CeilToInt(viewerCount);

        viewerCount += vc_rate;
        vc_rate += vc_grav;

        //Debug.Log("Real Viewer Count: " + viewerCount + "\nView Count Change per Frame: " + vc_rate);

        if (Time.time > (timeSinceLastUpdate + 2.0f))
        {
            timeSinceLastUpdate = Time.time;
            UpdateViewerCount();
        }
    }

    public void UpdateViewerCount()
    {
        TMP_Text textObj;
        textObj = GameObject.Find("ViewCount").GetComponent<TextMeshProUGUI>();
        if (textObj != null)
        {
            textObj.text = viewerCountInt.ToString("#,##0");
        }
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

    public void LogEnteredScene(string sceneName)
    {
        Debug.Log("Attempting to log scene: " + sceneName);
        if (visitedRooms.Contains(sceneName))
        {
            Debug.Log("We've already been here. Boring!");
            AudienceWoo(-1f, -0.00001f);
        } else {
            Debug.Log("New Room!!!! Adding " + sceneName + " to the visitedRooms list.");
            visitedRooms.Add(sceneName);
            AudienceWoo(3f, 0.002f);
        }
    }

    public void AudienceWoo(float memberAdd, float rateChange)
    {
        viewerCount += memberAdd;
        vc_rate += rateChange;

        Debug.Log("AUDIENCE WOO'D ----\nView Count Addition: " + memberAdd + "\nRate Change: " + rateChange);
    }

   


}
