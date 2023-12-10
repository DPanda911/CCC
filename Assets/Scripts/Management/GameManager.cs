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

    [Header("Door Spawn Positions")]
    public Vector3 spawnPos;
    public float spawnOrientation = 999999;

    [Header("Battery")]
    public float battery = 1;
    [SerializeField] private float batt_rate = 0.0075f;
    private bool batt_audPiss = false;

    [Header("View Count")]
    public float viewerCount = 250;
    private int viewerCountInt = 250;
    private float timeSinceLastUpdate = 0;
    [SerializeField] private float vc_rate = 0.01f;
    [SerializeField] private float vc_grav = -0.000004f;

    [SerializeField] private float averageViewCount = 0;
    private int averageVCChecks = 0;
    [SerializeField] private int peakViewCount = 0;

    [Header("Save Tags")]
    [SerializeField] private List<string> visitedRooms = new List<string>();
    [SerializeField] private List<string> dialogueTags = new List<string>();


    [Header("Sounds")]
    [SerializeField] AudioClip[] basicDoorSounds;
    [SerializeField] AudioClip bigDoorSound;
    [SerializeField] AudioClip ladderSound;
    AudioSource src;


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

        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.volume = 1f;
        src.spatialBlend = 0f;
    }

    void Update()
    {

        viewerCount += vc_rate;
        vc_rate += vc_grav;

        //Debug.Log("Real Viewer Count: " + viewerCount + "\nView Count Change per Frame: " + vc_rate);

        if (Time.time > (timeSinceLastUpdate + 2.0f))
        {
            if (viewerCount <= 0) {
                SceneManager.LoadScene("NoMoreViewers");
            }
            timeSinceLastUpdate = Time.time;
            UpdateViewerCount();
        }

        battery -= batt_rate * Time.deltaTime;
        battery = Mathf.Clamp(battery, 0f, 1f);
    }

    public void UpdateViewerCount()
    {
        TMP_Text textObj;
        textObj = GameObject.Find("ViewCount").GetComponent<TextMeshProUGUI>();
        if (textObj != null)
        {
            viewerCountInt = Mathf.CeilToInt(viewerCount);
            int bweg = Mathf.Max(viewerCountInt, 0);
            textObj.text = bweg.ToString("#,##0");
        }
        UIPhone pUI;
        pUI = GameObject.Find("Phone UI").GetComponent<UIPhone>();
        if (pUI != null) {
            pUI.UpdatedVC(viewerCountInt);
        }

        if (averageVCChecks > 0) {
            averageViewCount = (viewerCountInt + (averageViewCount * averageVCChecks)) / (averageVCChecks + 1);
        } else {
            averageViewCount = viewerCountInt;
        }
        averageVCChecks++;
        peakViewCount = Mathf.Max(viewerCountInt, peakViewCount);
        Debug.Log(averageViewCount);
        Debug.Log(peakViewCount);
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

    public float GetBattery()
    {
        if (battery <= 0 && !batt_audPiss) {
            batt_audPiss = true;
            AudienceWoo(0, -0.1f);
        }
        if (battery > 0 && batt_audPiss) {
            batt_audPiss = false;
            AudienceWoo(0, 0.1f);
        }
        return battery;
    }

    public void RechargeBattery()
    {
        battery += 0.85f;
        battery = Mathf.Clamp(battery, 0f, 1f);
    }

    public void DialogueMessage(string message, string tag = null, int mood = 0)
    {
        if (tag != null && tag != "IGNORE") {
            if ((dialogueTags.Contains(tag))) {
                return;
            }
            dialogueTags.Add(tag);
        }
        RendAndUI rau = GameObject.Find("Renderer").GetComponent<RendAndUI>();
        if (rau != null)
        {
            rau.SpawnDialogue(message, mood);
        }
    }

    public void PlaySound(SoundTypes snd)
    {
        switch (snd)
        {
            case SoundTypes.BasicDoor:
                int clipToUse = UnityEngine.Random.Range(0, basicDoorSounds.Length);
                src.clip = basicDoorSounds[clipToUse];
                break;
            case SoundTypes.BigDoor:
                src.clip = bigDoorSound;
                break;
            case SoundTypes.Ladder:
                src.clip = ladderSound;
                break;
        }
        src.Play();
    }

    public enum SoundTypes {
        BasicDoor = 0,
        BigDoor = 1,
        Ladder = 2
    }
   


}
