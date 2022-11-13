using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (buttonPression.isPressed)
        {
            anim.SetBool("isPressed", true);
        }
        else
        {
            anim.SetBool("isPressed", false);
        }
    }
}
