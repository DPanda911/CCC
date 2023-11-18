using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPhone : MonoBehaviour
{
    private Animator anim;
    private string curState = "Phone_Start";
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(curState);
        if (Input.GetButtonDown("Fire1"))
        {
            PlayAnimation("Phone_Lift");
        }
        if (Input.GetButtonUp("Fire1") && (curState != "PhoneStart"))
        {
            PlayAnimation("Phone_Rest");
        }
    }

    private void PlayAnimation(string newState)
    {
        if (newState == curState) return;

        anim.Play(newState);

        curState = newState;
    }
}
