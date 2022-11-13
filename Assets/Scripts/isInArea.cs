using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isInArea : MonoBehaviour
{
    public bool isInBox;
    [SerializeField] private Material metal;
    void Update(){
        if(isInBox){
            Debug.Log("Found in box!");
            metal.color = Color.Lerp(metal.color, Color.red,  Mathf.PingPong(Time.time, 0.01f));
        }
        else
        {
            metal.color = Color.Lerp(metal.color, Color.black,  Mathf.PingPong(Time.time, 0.02f));

        }
    }
 
    void OnTriggerStay(Collider other){
        if(other.CompareTag("PickUp")){
            isInBox = true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.CompareTag("PickUp")){
            isInBox = false;
        }
    }
}
