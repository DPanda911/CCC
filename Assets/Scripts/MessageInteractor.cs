using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageInteractor : MonoBehaviour, IInteractable
{
    [TextArea]
    [SerializeField] private string message;
    [Tooltip("This tag ensures the dialogue won't repeat.")]
    [SerializeField] private string repeatTag;

    GameManager gm;
    MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
    }

    public void Interact() {
        gm.DialogueMessage(message, repeatTag);
    }
}
