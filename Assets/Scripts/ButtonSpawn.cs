using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSpawn : MonoBehaviour
{
    //when player clicks on button, spawn a new object
    public GameObject prefab;
    public Transform spawnPoint;
    public AudioClip audioSource;
    private int spawnCount = 0;
    private void FixedUpdate()
    {
        //Get "inRange" from PlayerController2 script
        if (PickUpController2.inRange == true && spawnCount < 7)

        {
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            PickUpController2.inRange = false;
            spawnCount++;
            AudioSource.PlayClipAtPoint(audioSource, transform.position);
        }

    }

}
