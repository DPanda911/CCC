using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] private float alpha = 1f;
    private bool isLoaded = false;
    public bool leaving = false;
    Image img;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        isLoaded = true;

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoaded) {
            if ((alpha > 0) && !leaving) {
                alpha -= 2.5f * Time.deltaTime;
            }
            if (leaving && (alpha < 1)) {
                alpha += 3f * Time.deltaTime;
                cam.fieldOfView -= Time.deltaTime * 25f;
            }
            img.color = new Color(0f, 0f, 0f, Mathf.Clamp(alpha, 0f, 1f));
        }
    }
}
