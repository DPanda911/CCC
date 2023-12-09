using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMessage : MonoBehaviour
{
    TMP_Text textObj;

    private bool isFading = false;
    private float fadeTime = 3f;
    private float springTime = 0f;

    [SerializeField] private float textDurationPerCharacter = 0.055f;
    [SerializeField] private float textDurationAddition = 2f;
    // Start is called before the first frame update
    void Start()
    {
        textObj = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        springTime += Time.deltaTime;
        float springAmount = Mathf.Sin(springTime * 25f) * Mathf.Pow(2, -5 * springTime);
        transform.localPosition = new Vector3(-0.4f, -0.8f + (springAmount * 0.1f), 0f);

        if (isFading)
        {
            fadeTime -= Time.deltaTime;
            if (fadeTime < 0) {
                if (fadeTime <= -1) {
                    Destroy(gameObject);
                }
                textObj.color = new Color(1f, 1f, 1f, fadeTime + 1f);
            }
        } else {
            CalcFadeTime();
        }
    }

    public void CalcFadeTime()
    {
        fadeTime = (textObj.text.Length * textDurationPerCharacter) + textDurationAddition;
        isFading = true;
        Debug.Log(fadeTime);
    }


}
