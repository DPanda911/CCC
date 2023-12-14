using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TooMuchScreen : MonoBehaviour
{
    [SerializeField] private Image staticImage;
    [SerializeField] private AudioClip[] staticNoises;
    [SerializeField] private GameObject textA;
    [SerializeField] private GameObject textB;
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
        yield return new WaitForSeconds(6f);
        staticImage.enabled = true;
        src.Stop();
        src.clip = staticNoises[0];
        src.Play();
        yield return new WaitForSeconds(.2f);
        staticImage.enabled = false;
        textA.SetActive(false);
        textB.SetActive(false);
        src.Stop();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");

    }
}
