using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageInteractor : MonoBehaviour, IInteractable
{
    [TextArea]
    [SerializeField] private string[] message = {"Your message goes here!"};
    [Tooltip("This tag ensures the dialogue won't repeat.")]
    [SerializeField] private string repeatTag;
    [Tooltip("The sound made by the player.")]
    [SerializeField] private int mood = 0;
    [Tooltip("How to loop through multiple messages if there are multiple.")]
    [SerializeField] private SequenceType loopMethod = 0;

    private int curMessage = 0;

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
        if (loopMethod == SequenceType.Random) {
            int lastMsg = curMessage;
            while (curMessage == lastMsg)
            {
                curMessage = Random.Range(0, message.Length - 1);
            }
        }
        gm.DialogueMessage(message[curMessage], repeatTag, mood);
        curMessage += 1;
        if (curMessage >= message.Length) {
            if (loopMethod == SequenceType.Sequenced) {
                curMessage -= 1;
            } else {
                curMessage = 0;
            }
        }
    }

    private enum SequenceType {
        Sequenced = 0,
        Looping = 1,
        Random = 2
    }
}
