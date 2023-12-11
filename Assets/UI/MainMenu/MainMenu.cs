using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image logo;
    [SerializeField] private Image staticImage;

    [SerializeField] private Button startButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button soundButton;

    private Vector2 logoInitPos;

    [SerializeField] private AudioClip pingNoise;
    [SerializeField] private AudioClip staticNoise;
    private AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        logoInitPos = logo.rectTransform.anchoredPosition;

        Button btnA = startButton.GetComponent<Button>();
        Button btnB = skipButton.GetComponent<Button>();
        Button btnC = quitButton.GetComponent<Button>();
        Button btnD = soundButton.GetComponent<Button>();

        // don't ask me what delegate does
        btnA.onClick.AddListener(delegate{buttonClicked(0);});
        btnB.onClick.AddListener(delegate{buttonClicked(1);});
        btnC.onClick.AddListener(delegate{buttonClicked(2);});

        btnD.onClick.AddListener(soundTest);

        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        logo.rectTransform.anchoredPosition = new Vector2(logoInitPos.x, logoInitPos.y + Mathf.Sin(Time.time*1.75f));
        staticImage.rectTransform.anchoredPosition = new Vector3(Random.Range(-272, 272), Random.Range(-377, 377), 0);
    }

    void buttonClicked(int whatOne) {
        IEnumerator crt = ButtonSelect(whatOne);
        StartCoroutine(crt);
    }

    void soundTest() {
        src.clip = pingNoise;
        src.pitch = Random.Range(0.97f, 1.03f);
        src.Play();
    }

    private IEnumerator ButtonSelect(int choice) {
        staticImage.enabled = true;
        src.clip = staticNoise;
        src.pitch = 1f;
        src.loop = true;
        src.Play();
        yield return new WaitForSeconds(0.75f);
        switch (choice)
        {
            case 0:
                SceneManager.LoadScene("IntroScene");
                break;
            case 1:
                SceneManager.LoadScene("TutorialRoom");
                break;
            case 2:
                Application.Quit();
                break;
        }
    }
}
