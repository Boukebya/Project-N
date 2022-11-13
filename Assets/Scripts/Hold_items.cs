using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold_items : MonoBehaviour
{
    //Hold items
    public GameObject item;
    public Rigidbody itemRb;
    
    float hold_range = 2f;
    float throw_force = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (item == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, hold_range))
                {
                    if (hit.collider.CompareTag("Item"))
                    {
                        item = hit.collider.gameObject;
                        itemRb = item.GetComponent<Rigidbody>();
                        itemRb.useGravity = false;
                        itemRb.isKinematic = true;
                        item.transform.parent = transform;
                    }
                }
            }
            else
            {
                item.transform.parent = null;
                itemRb.useGravity = true;
                itemRb.isKinematic = false;
                itemRb.AddForce(transform.forward * throw_force, ForceMode.Impulse);
                item = null;
            }
        }
    }
}
