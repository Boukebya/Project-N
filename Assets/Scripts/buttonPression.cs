using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPression : MonoBehaviour
{
    // pressure button, if an object is on it, it will be pressed
    // if the object is not on it, it will be released
    
    public static bool isPressed = false;
    Animator anim;
    public GameObject _gameObject;
    public void Start()
    {
        anim = _gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            //change the color of the button
            GetComponent<Renderer>().material.color = Color.red;
            anim.SetBool("isReceiving", true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
      
        if (collision.gameObject)
        {
            GetComponent<Renderer>().material.color = Color.blue;
            anim.SetBool("isReceiving", false);
        }
        
    }
}
