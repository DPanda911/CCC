using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private Image fader;

    [Space]

    [SerializeField] GameObject clearMsg;
    [SerializeField] TextMeshProUGUI t_average;
    [SerializeField] TextMeshProUGUI t_peak;
    [SerializeField] TextMeshProUGUI t_dur;
    [SerializeField] TextMeshProUGUI t_final;
    [SerializeField] Image rank;
    [SerializeField] Button quitBtn;

    [SerializeField] Sprite[] imgRanks;

    int finalRank = 0;

    [Space]

    private float spring = 0f;
    private float springMult = 1f;

    [TextArea]
    [SerializeField] private string[] dialogues;
    [SerializeField] private int[] moods;
    [SerializeField] private Vector3[] angles;
    [SerializeField] private EndingBG endbg;
    [Space]
    [SerializeField] private AudioClip[] dialogueSounds;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip[] rankSounds;
    private AudioSource src;

    private bool canText = false;
    private int currentLine = 0;

    private bool fadeText = false;
    private float txtAlpha = 1f;

    private int bgMode = 0;
    private float bgAlpha = 1f;

    [SerializeField] private int averageViewCount;
    [SerializeField] private int peakViewCount;

    private int avgDisplay;
    private int peakDisplay;

    // Start is called before the first frame update
    void Start()
    {
        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f;

        IEnumerator crt = StartScene();
        StartCoroutine(crt);

        quitBtn.onClick.AddListener(ExitScene);
    }

    // Update is called once per frame
    void Update()
    {
        spring += Time.deltaTime;
        float springAmount = Mathf.Sin(spring * 25f) * Mathf.Pow(2, -5 * spring);
        txt.rectTransform.anchoredPosition = new Vector3(0f, springAmount * 10f * springMult, 0f);

        if (!fadeText) {
            txt.color = new Color(1f, 1f, 1f, 1f);
            txtAlpha = 1f;
        } else {
            txtAlpha -= 0.2f * Time.deltaTime;
            txtAlpha = Mathf.Max(txtAlpha, 0f);
            txt.color = new Color(1f, 1f, 1f, txtAlpha);
        }

        
        if (canText && (currentLine < dialogues.Length - 1)) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                currentLine++;
                PlayMessage(currentLine);
            }
        }

        switch (bgMode) {
            case 0:
                break;
            case 1:
                bgAlpha -= 0.15f * Time.deltaTime;
                bgAlpha = Mathf.Max(bgAlpha, 0f);
                fader.color = new Color(0f, 0f, 0f, bgAlpha);
                break;
            case 2:
                bgAlpha += 0.175f * Time.deltaTime;
                bgAlpha = Mathf.Clamp(bgAlpha, 0f, 0.75f);
                fader.color = new Color(0f, 0f, 0f, bgAlpha);
                break;
        }
    }

    void PlayMessage(int msgID) {
        txt.text = dialogues[msgID];
        src.clip = dialogueSounds[moods[msgID]];
        src.pitch = Random.Range(0.97f, 1.03f);
        src.Play();
        spring = 0f;
        endbg.targetCamAngle = angles[msgID];

        if (currentLine != msgID) {currentLine = msgID;}

        if (msgID == 1) {
            SetStatLines();
        }

        if (msgID == 3) {
            canText = false;
            IEnumerator crt = CarTime();
            StartCoroutine(crt);
        }

        if (msgID == dialogues.Length - 1) {
            canText = false;
            IEnumerator crt = Ending();
            StartCoroutine(crt);
        }
    }


    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1.25f);
        canText = true;
        PlayMessage(0);
    }
    private IEnumerator CarTime()
    {
        yield return new WaitForSeconds(.5f);
        GameObject.Find("ExtraSound").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(6.5f);
        fadeText = true;
        yield return new WaitForSeconds(4f);
        bgMode = 1;
        yield return new WaitForSeconds(6f);
        PlayMessage(4);
        fadeText = false;
        canText = true;
    }


    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(0.5f);
        bgMode = 2;
        yield return new WaitForSeconds(0.5f);
        fadeText = true;
        yield return new WaitForSeconds(2f);
        src.clip = hitSound;
        src.pitch = 0.4f;
        src.Play();
        clearMsg.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        t_average.text = "Average Viewer Count: <color=#ff7>" + avgDisplay.ToString("#,##0") + "";
        t_average.enabled = true;
        src.pitch = 1f;
        src.volume = 0.4f;
        src.Play();
        yield return new WaitForSeconds(0.6f);

        t_peak.text = "Peak Viewer Count: <color=#f7f>" + peakDisplay.ToString("#,##0") + "";
        t_peak.enabled = true;
        src.pitch = 1.05f;
        src.Play();
        yield return new WaitForSeconds(0.6f);

        t_dur.text = "Stream Runtime: <color=#7af>" + GameManager.instance.GetDuration() + "";
        t_dur.enabled = true;
        src.pitch = 1.1f;
        src.Play();
        yield return new WaitForSeconds(1.15f);

        t_final.enabled = true;
        src.pitch = 0.7f;
        src.volume = 0.65f;
        src.Play();
        yield return new WaitForSeconds(0.9f);
        
        
        GameObject.Find("ExtraSound").GetComponent<AudioSource>().clip = rankSounds[finalRank];
        GameObject.Find("ExtraSound").GetComponent<AudioSource>().Play();
        rank.sprite = imgRanks[finalRank];
        rank.enabled = true;
        src.pitch = 0.5f;
        src.volume = 1f;
        src.Play();
        yield return new WaitForSeconds(2.2f);

        quitBtn.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }



    private void SetStatLines() {
        if (GameManager.instance == null) {
            return;
        }

        int howPleased = 0;
        averageViewCount = GameManager.instance.GetAverageViews();
        peakViewCount = GameManager.instance.GetPeakViews();

        avgDisplay = GameManager.instance.ConvertViews(averageViewCount);
        peakDisplay = GameManager.instance.ConvertViews(peakViewCount);

        if (averageViewCount >= 325) {
            dialogues[6] = "I could hardly believe it, I somehow got a whopping <color=#ff7>" + avgDisplay.ToString("#,##0") + "</color> average viewer count! How the hell did I pull that off???";
            moods[6] = 1;
            howPleased = 6;
        } else if (averageViewCount >= 312) {
            dialogues[6] = "My average view count of this stream hit <color=#ff7>" + avgDisplay.ToString("#,##0") + "</color>! That's a record for me!";
            moods[6] = 1;
            howPleased = 5;
        } else if (averageViewCount >= 300) {
            dialogues[6] = "I managed to rack up an average viewer count of <color=#ff7>" + avgDisplay.ToString("#,##0") + "</color>! Way more than I usually get!";
            moods[6] = 1;
            howPleased = 4;
        } else if (averageViewCount >= 280) {
            dialogues[6] = "I got an impressive count of <color=#ff7>" + avgDisplay.ToString("#,##0") + "</color> average viewers.";
            moods[6] = 0;
            howPleased = 3;
        } else if (averageViewCount >= 260) {
            dialogues[6] = "I had <color=#ff7>" + avgDisplay.ToString("#,##0") + "</color> people watching on average.";
            moods[6] = 2;
            howPleased = 2;
        } else if (averageViewCount >= 240) {
            dialogues[6] = "I had like <color=#ff7>" + avgDisplay.ToString("#,##0") + "</color> people watching on average.";
            moods[6] = 2;
            howPleased = 1;
            angles[7] = new Vector3(-3, 0, -2);
        } else {
            dialogues[6] = "I only managed to get <color=#ff7>" + avgDisplay.ToString("#,##0") + "</color> people watching on average.";
            moods[6] = 3;
            angles[7] = new Vector3(3, -2, 0);
        }

        if (peakViewCount >= 390) {
            dialogues[7] = "...and a peak count of <color=#ff7>" + peakDisplay.ToString("#,##0") + "</color>?!? That's insane!";
            moods[7] = 5;
            angles[7] = new Vector3(-5, -2, 0);
            howPleased += 4;
        } else if (peakViewCount >= 360) {
            dialogues[7] = "I also peaked at  an impressive <color=#ff7>" + peakDisplay.ToString("#,##0") + "</color> viewers!";
            moods[7] = 1;
            howPleased += 3;
        } else if (peakViewCount >= 320) {
            dialogues[7] = "I also peaked at <color=#ff7>" + peakDisplay.ToString("#,##0") + "</color> viewers!";
            moods[7] = 1;
            howPleased += 2;
        } else if (peakViewCount >= 275) {
            dialogues[7] = "The stream peaked at <color=#ff7>" + peakDisplay.ToString("#,##0") + "</color> viewers.";
            moods[7] = 0;
            howPleased += 1;
            angles[7] = new Vector3(0, 0, 2);
        } else {
            dialogues[7] = "It also said my view count got up to <color=#ff7>" + peakDisplay.ToString("#,##0") + "</color>. Probably just at the start, though.";
            moods[7] = 2;
            angles[7] = new Vector3(2, 6, 0);
        }


        switch (howPleased) {
            case 10:
                angles[4] = new Vector3(-8, 0, 0);
                SetMessage(4, "God, that went SO GOOD!!!!", 5);
                SetMessage(8, "Man, I am so good at streaming.", 0);
                SetMessage(9, "The guys over at <color=#7cf>T-FUEL</color> are lucky they picked someone<br>as good as me to promote their crappy drink.", 0);
                SetMessage(10, "I just know they'll be paying me a fat wad of cash. Goodbye, bills!", 1);
                SetMessage(11, "Oh, Nelle. You've truly outdone yourself.", 0);
                finalRank = 4;
                break;
            case 9:
            case 8:
            case 7:
                SetMessage(4, "That was such a good stream!", 1);
                SetMessage(9, "I'm really happy with how this went.", 0);
                SetMessage(9, "I'll let <color=#7cf>T-FUEL</color> know how I did. They should be happy.", 0);
                finalRank = 3;
                break;
            case 6:
            case 5:
            case 4:
                finalRank = 2;
                break;
            case 3:
            case 2:
                SetMessage(4, "Certainly not the worst stream I've had.", 1);
                SetMessage(8, "...I've had better.", 2);
                SetMessage(9, "At least <color=#7cf>T-FUEL</color> will still be paying me.", 0);
                finalRank = 1;
                break;
            case 1:
            case 0:
                SetMessage(4, "...", 6);
                angles[4] = new Vector3(5, 0, 0);
                angles[5] = new Vector3(2.5f, 0, 0);
                SetMessage(8, "Yeah, not my best work.", 3);
                SetMessage(9, "<color=#7cf>T-FUEL</color>'s still paying me, right?<br>I think that still qualifies as a \"successful\" stream.<br> Not like I died or anything.", 2);
                angles[8] = new Vector3(1, 0, 0);
                angles[9] = new Vector3(-2, -3, -4);
                SetMessage(10, "I think I just gotta cross my fingers and pray they still pay me.", 3);
                SetMessage(11, "...otherwise, still kinda <color=#f77>screwed</color>.", 4);
                SetMessage(13, "...there's always next stream, I guess.", 7);
                angles[12] = new Vector3(2, 0, 0);
                angles[13] = new Vector3(4, 8, 1);
                finalRank = 0;
                break;
            default:
                finalRank = 2;
                break;
        }
    }

    private void SetMessage(int id, string message, int mood)
    {
        dialogues[id] = message;
        moods[id] = mood;
    }

    void ExitScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
