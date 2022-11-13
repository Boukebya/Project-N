using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyManager : MonoBehaviour
{


  public int Healthp;
  public GameObject enemy;

    // L'ennemi est détruit quand ses hp ateignent 0
    private void Update()
    {
        if (Healthp<=0) Destroy(enemy);
    }

}

