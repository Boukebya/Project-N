using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morse : MonoBehaviour
{
    [SerializeField] public Light lt;
    
    void Start()
    {
        lt = GetComponent<Light>();
        StartCoroutine(MorseCode());
    }
    
    
    IEnumerator MorseCode()
    {
        lt.range = 20;
        yield return new WaitForSeconds(5);
        
        // start morse code 
        // long 
        lt.range = 40;
        yield return new WaitForSeconds(3);
        // pause
        lt.range = 20;
        yield return new WaitForSeconds(1);
        // long 
        lt.range = 40;
        yield return new WaitForSeconds(3);
        // pause
        lt.range = 20;
        yield return new WaitForSeconds(1);
        // short 
        lt.range = 40;
        yield return new WaitForSeconds(1);
        // pause
        lt.range = 20;
        yield return new WaitForSeconds(1);
        // short 
        lt.range = 40;
        yield return new WaitForSeconds(1);
        // pause
        lt.range = 20;
        yield return new WaitForSeconds(1);
        // short 
        lt.range = 40;
        yield return new WaitForSeconds(1);
        // pause
        lt.range = 20;
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        StartCoroutine(MorseCode());
    }
}
