using System;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    //get the gameObject to get the animator
    [SerializeField] public GameObject objectToControl;
    private Animator _anim;
    public bool hitted;
    //audio clip
    public AudioClip sound;
    
    bool HavePlayed = false;
    private int counter = 0;
    public void Awake()
    {
        //get the animator
        _anim = objectToControl.GetComponent<Animator>();
    }

    public void Update()
    {
        if (hitted)
        {
            _anim.SetBool("isReceiving", true);
            
            if (!HavePlayed)
            {
                AudioSource.PlayClipAtPoint(sound, transform.position,0.2f);
                HavePlayed = true;
            }
            
        }
        else
        {
            _anim.SetBool("isReceiving", false);
            HavePlayed = false;
        }

        counter++;
        if (counter > 20)
        {
            hitted = false;
            counter = 0;
        }
    }
}

