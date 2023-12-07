using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSpawner : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string message;
    [Tooltip("This tag ensures the dialogue won't repeat.")]
    [SerializeField] private string repeatTag;

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider ot) {
        Debug.Log(ot.gameObject.name);
        if (ot.gameObject.name == "Player")
        {
            gm.DialogueMessage(message, repeatTag);
        }
    }
}
