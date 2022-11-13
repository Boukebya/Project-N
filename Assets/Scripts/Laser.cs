using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer _lr;
    public Transform firePoint;
    public GameObject particle;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }
    void FixedUpdate()
    {
       SetUpLaser();
    }
    
    public void SetUpLaser()
    {
        //set a line between the firePoint the first collision
        _lr.SetPosition(0, firePoint.position);
        //draw a 3D raycast from the firePoint that interact with trigger
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward*1.05f, out hit, 1000f))
        {
            //debug raycast
            Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance*1.05f, Color.red);
            
            _lr.SetPosition(1, hit.point+0.2F*firePoint.forward);
            //instantiate a particle at the hit point
            var part= Instantiate(particle, hit.point, Quaternion.identity);
            Destroy(part,0.1f);
            
            //if raycast hit receiver
            if (hit.collider.CompareTag("Receiver"))
            {
                //set value of receiver to true
                hit.collider.GetComponent<Receiver>().hitted = true;
                Debug.Log(hit.collider.GetComponent<Receiver>());
            }

            
        }
        else
        {
            _lr.SetPosition(1, firePoint.position + firePoint.forward * 1000f);
        }
    }
    
    
}
