using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private Image img;
    [SerializeField] private Image spookyImage;
    [SerializeField] private GameObject staticImage;
    [SerializeField] private Sprite[] faceSprites;
    [Space]
    [TextArea]
    [SerializeField] private string[] dialogues;
    [SerializeField] private int[] moods;
    [Space]
    [SerializeField] private AudioClip[] dialogueSounds;
    [SerializeField] private AudioClip staticNoise;
    private AudioSource src;

    private float fadeTransit = 0;
    private bool startedDialogue = false;
    private bool fadeDir = true;
    private bool ending = false;

    private float spring = 0f;

    private int currentLine = 0;
    private float springMult = 1f;

    // Start is called before the first frame update
    void Start()
    {
        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f;

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeDir && (fadeTransit < 1)) {
            fadeTransit += 0.1f * Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, 1f - fadeTransit);
        }

        if ((fadeTransit > 0.5) && !startedDialogue) {
            startedDialogue = true;
            PlayMessage(0);
        }
        spring += Time.deltaTime;
        float springAmount = Mathf.Sin(spring * 25f) * Mathf.Pow(2, -5 * spring);
        txt.gameObject.transform.localPosition = new Vector3(0f, springAmount * 10f * springMult, 0f);

        if (startedDialogue) {
            if (currentLine < dialogues.Length - 1)
            {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    currentLine++;
                    PlayMessage(currentLine);
                }
            } else {
                if (!ending) {
                    ending = true;
                    fadeDir = false;
                    img.color = new Color(0f, 0f, 0f, 1f);
                    springMult = 0.2f;
                    spookyImage.enabled = true;
                    IEnumerator crt = EndScene();
                    StartCoroutine(crt);
                }
            }
        }

        staticImage.transform.localPosition = new Vector3(Random.Range(-272, 272), Random.Range(-377, 377), 0);
    }

    void PlayMessage(int msgID) {
        txt.text = dialogues[msgID];
        src.clip = dialogueSounds[moods[msgID]];
        src.pitch = Random.Range(0.97f, 1.03f);
        src.Play();
        spring = 0f;
    }

    private IEnumerator EndScene() {
        yield return new WaitForSeconds(.06f);
        spookyImage.sprite = faceSprites[1];
        yield return new WaitForSeconds(.06f);
        spookyImage.sprite = faceSprites[2];
        yield return new WaitForSeconds(.06f);
        spookyImage.sprite = faceSprites[3];
        yield return new WaitForSeconds(.06f);
        spookyImage.sprite = faceSprites[4];
        yield return new WaitForSeconds(.06f);
        staticImage.SetActive(true);
        src.clip = staticNoise;
        src.pitch = 1f;
        src.loop = true;
        src.Play();
        yield return new WaitForSeconds(.4f);
        SceneManager.LoadScene("TutorialRoom");
    }
}
