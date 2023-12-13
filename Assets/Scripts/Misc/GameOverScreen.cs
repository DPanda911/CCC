using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private Image gameOverImage;
    [SerializeField] private Button quitBtn;

    private float spring = 0f;
    private float springMult = 1f;

    [TextArea]
    [SerializeField] private string[] dialogues;
    [SerializeField] private int[] moods;
    [Space]
    [SerializeField] private AudioClip[] dialogueSounds;
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource src;

    private bool startedDialogue = false;
    private int currentLine = 0;

    private bool ended = false;

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

        
        if (startedDialogue) {
            if (currentLine < dialogues.Length - 1)
            {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    currentLine++;
                    PlayMessage(currentLine);
                }
            } else if (!ended) {
                ended = true;
                IEnumerator crt = EndCutscene();
                StartCoroutine(crt);
            }
        }
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1.25f);
        startedDialogue = true;
        PlayMessage(0);
    }

    private IEnumerator EndCutscene()
    {
        yield return new WaitForSeconds(1.25f);
        gameOverImage.enabled = true;
        src.clip = gameOverSound;
        src.pitch = 0.2f;
        src.Play();
        yield return new WaitForSeconds(1f);
        quitBtn.gameObject.SetActive(true);
        src.volume = .5f;
        src.pitch = 1f;
        src.Play();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void PlayMessage(int msgID) {
        txt.text = dialogues[msgID];
        src.clip = dialogueSounds[moods[msgID]];
        src.pitch = Random.Range(0.97f, 1.03f);
        src.Play();
        spring = 0f;
    }

    void ExitScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
