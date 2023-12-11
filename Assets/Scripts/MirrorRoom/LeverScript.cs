using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, IInteractable
{
    private Animator anim;

    [SerializeField] private bool flipped = false;
    [SerializeField] private bool canFlipManually = true;

    public GameObject linkedLever;

    public GameObject door;

    private GameManager gm;

    [SerializeField] string leverTag;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gm.CheckForMiscTag(leverTag))
        {
            flipped = true;
            anim.Play("Lev_IsFlipped");
            if (door)
            {
                door.GetComponent<Animator>().Play("MRDoor_IsOpened");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (canFlipManually)
        {
            Debug.Log("Flipping");
            if (!flipped)
            {
                FlipLever();
                gm.AudienceWoo(0.667f, 0.00025f);
                if (linkedLever)
                {
                    linkedLever.GetComponent<LeverScript>().FlipLever();
                }
            }
        }
    }

    public void FlipLever()
    {
        flipped = true;
        anim.Play("Lev_Flip");
        if (door)
        {
            door.GetComponent<Animator>().Play("MRDoor_Open");
        }

        gm.NewMiscTag(leverTag);
    }
}
