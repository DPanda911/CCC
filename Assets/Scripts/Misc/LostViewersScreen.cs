using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LostViewersScreen : MonoBehaviour
{
    [SerializeField] private Image viewerImg;
    [SerializeField] private Sprite[] imagesNGL;
    [SerializeField] private GameObject viewerText;
    [SerializeField] private Image staticImage;
    [SerializeField] private AudioClip[] staticNoises;
    private AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.spatialBlend = 0f;
        src.clip = staticNoises[0];
        src.loop = true;
        src.Play();

        IEnumerator crt = DoThing();
        StartCoroutine(crt);
    }

    // Update is called once per frame
    void Update()
    {
        staticImage.rectTransform.anchoredPosition = new Vector3(Random.Range(-272, 272), Random.Range(-377, 377), 0);
    }

    private IEnumerator DoThing() {
        yield return new WaitForSeconds(.1f);
        staticImage.enabled = false;
        src.Stop();
        src.clip = staticNoises[1];
        src.Play();
        yield return new WaitForSeconds(3.2f);
        src.pitch = 0.3f;
        viewerImg.rectTransform.sizeDelta = new Vector2(28*2, 30*2);
        viewerImg.color = new Color(0.8117647f, 0.1647059f, 0.1647059f, 1f);
        yield return new WaitForSeconds(5f);
        viewerText.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        staticImage.enabled = true;
        src.Stop();
        src.pitch = 0.9f;
        src.clip = staticNoises[0];
        src.Play();
        viewerText.SetActive(false);
        viewerImg.sprite = imagesNGL[1];
        yield return new WaitForSeconds(.06f);
        staticImage.enabled = false;
        src.Stop();
        yield return new WaitForSeconds(.5f);
        staticImage.enabled = true;
        src.pitch = 1f;
        src.Play();
        yield return new WaitForSeconds(.2f);
        staticImage.enabled = false;
        viewerImg.enabled = false;
        src.Stop();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");

    }
}
