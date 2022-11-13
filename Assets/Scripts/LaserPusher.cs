using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPusher : MonoBehaviour
{
    private LineRenderer _lr;
    public Transform firePoint;
    public GameObject particle;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        SetUpLaser();
    }

    private void SetUpLaser()
    {
        //set a line between the firepoint the first colision
        _lr.SetPosition(0, firePoint.position);
        //draw a 3D raycast from the firepoint
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit))
        {
            _lr.SetPosition(1, hit.point);
            //particle effect
            var part =Instantiate(particle, hit.point, Quaternion.identity);
            Destroy(part,0.1f);
            //push the object
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition(firePoint.forward * 100f, hit.point);
            }


        }
        else
        {
            _lr.SetPosition(1, firePoint.position + firePoint.forward * 100);
            var part =Instantiate(particle, hit.point, Quaternion.identity);
            //destroy the particle after 1 second
            Destroy(part,0.5f);
        }
    }
    
}
