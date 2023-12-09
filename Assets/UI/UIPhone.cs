using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPhone : MonoBehaviour
{
    private Animator anim;
    private string curState = "Phone_Start";
    public bool canPhone = true;

    [SerializeField] private Image batteryImg;
    [SerializeField] private Image deadBatteryImg;

    [SerializeField] private Image viewerImg;
    [SerializeField] private Sprite[] viewerTiers;

    [Header("Sprites")]
    [SerializeField] private Sprite s_battery_full;
    [SerializeField] private Sprite s_battery_mid;
    [SerializeField] private Sprite s_battery_low;

    Interactor intr;

    GameManager gm;
    InventoryManager im;

    private bool hasPanicked = false;

    private float battLevel;

    [Space]
    AudioSource src;
    [SerializeField] private AudioClip deadBeep;
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip phoneImpactSound;
    [SerializeField] InventoryManager.AllItems chargerItem;
    private bool hasBeeped = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        intr = GameObject.Find("Main Camera").GetComponent<Interactor>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        im = GameObject.Find("GameManager").GetComponent<InventoryManager>();

        src = gameObject.AddComponent<AudioSource>();

        src.playOnAwake = false;
        src.volume = .8f;
        src.spatialBlend = .1f;
        src.dopplerLevel = 0f;
        src.minDistance = 11.25f;
        src.maxDistance = 50f;

        src.clip = deadBeep;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && (canPhone))
        {
            PlayAnimation("Phone_Lift");
            intr.canInteract = false;
        }
        if (Input.GetButtonUp("Fire1") && (curState == "Phone_Lift"))
        {
            PlayAnimation("Phone_Rest");
            intr.canInteract = true;
            gm.DialogueMessage("It's important I check on that often. If my viewer count hits zero, I'll have no choice but to <color=#f77>stop the stream</color>.", "ViewCountExplanation", 3);
        }
        if (Input.GetKeyDown(KeyCode.R) && intr.canInteract && im.HasItem(chargerItem))
        {
            PlayAnimation("Phone_Charge");
            im.RemoveItem(chargerItem);
            canPhone = false;
            intr.canInteract = false;
        }

        if ((curState == "Phone_Lift") && !hasPanicked && (gm.GetBattery() <= 0)) {
            if (gm.GetBattery() <= 0) {
                IEnumerator crt = ImOutOfBattery();
                StartCoroutine(crt);
            }
        }

        battLevel = gm.GetBattery();
        UpdateBatteryUI(battLevel);
    }

    private void PlayAnimation(string newState)
    {
        if (newState == curState) return;

        anim.Play(newState);

        curState = newState;
    }

    private void UpdateBatteryUI(float level)
    {
        if (level <= 0) {
            deadBatteryImg.enabled = true;
            if (Mathf.Sin(Time.time*25) > 0) {
                deadBatteryImg.color = new Color(1f, 1f, 1f, 1f);
                if (!hasBeeped && (curState == "Phone_Lift")) {
                    hasBeeped = true;
                    src.clip = deadBeep;
                    src.Play();
                }
            } else {
                deadBatteryImg.color = new Color(0.8f, 0.2f, 0.2f, 1f);
                hasBeeped = false;
            }
        } else {
            deadBatteryImg.enabled = false;
            if (level < 0.3) {
                batteryImg.sprite = s_battery_low;
            } else if (level < 0.6) {
                batteryImg.sprite = s_battery_mid;
            } else {
                batteryImg.sprite = s_battery_full;
            }
        }
    }

    private IEnumerator ImOutOfBattery() {
        yield return new WaitForSeconds(.35f);
        gm.DialogueMessage("Oh crap, out of battery?!? I'M <color=#f77>NOT BROADCASTING</color>! My viewer count is probably plummeting! Why didn't I bring my own <color=#fa7>battery pack</color>?!?", "WaitImOutOfBattery", 4);
    }

    private void PhoneImpact() {
        src.clip = phoneImpactSound;
        src.Play();
        gm.AudienceWoo(0, 0.0015f);
    }

    private void BatteryPackCharge() {
        gm.RechargeBattery();
        src.clip = chargeSound;
        src.Play();
    }

    private void EndCharge() {
        intr.canInteract = true;
        canPhone = true;
    }

    public void UpdatedVC(int viewerCount) {
        if (viewerCount >= 100) {
            viewerImg.sprite = viewerTiers[0];
        } else if (viewerCount >= 75) {
            viewerImg.sprite = viewerTiers[1];
        } else if (viewerCount >= 50) {
            viewerImg.sprite = viewerTiers[2];
        } else if (viewerCount >= 25) {
            viewerImg.sprite = viewerTiers[3];
        } else {
            viewerImg.sprite = viewerTiers[4];
        }
    }
}
