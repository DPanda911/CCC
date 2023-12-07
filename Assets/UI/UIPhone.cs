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

    [Header("Sprites")]
    [SerializeField] private Sprite s_battery_full;
    [SerializeField] private Sprite s_battery_mid;
    [SerializeField] private Sprite s_battery_low;

    Interactor intr;

    GameManager gm;

    private float battLevel;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        intr = GameObject.Find("Main Camera").GetComponent<Interactor>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            gm.DialogueMessage("If my viewer count hits zero, I'll have no choice but to <color=#f77>stop the stream</color>.", "ViewCountExplanation");
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
            } else {
                deadBatteryImg.color = new Color(0.8f, 0.2f, 0.2f, 1f);
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
}
