using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private Image img;
    [Space]
    [TextArea]
    [SerializeField] private string[] dialogues;
    [SerializeField] private int[] moods;
    [Space]
    [SerializeField] private AudioClip[] dialogueSounds;
    private AudioSource src;

    private float fadeTransit = 0;
    private bool startedDialogue = false;
    private bool fadeDir = true;

    private float spring = 0f;

    private int currentLine = 0;

    // Start is called before the first frame update
    void Start()
    {
        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f;
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
        txt.gameObject.transform.localPosition = new Vector3(0f, springAmount * 10f, 0f);

        if (startedDialogue) {
            if (currentLine < dialogues.Length - 1)
            {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    currentLine++;
                    PlayMessage(currentLine);
                }
            } else {
                Debug.Log("end");
            }
        }
    }

    void PlayMessage(int msgID) {
        txt.text = dialogues[msgID];
        src.clip = dialogueSounds[moods[msgID]];
        src.pitch = Random.Range(0.97f, 1.03f);
        src.Play();
        spring = 0f;
    }
}
