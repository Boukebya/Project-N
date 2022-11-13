using UnityEngine;

public class PickUpController2 : MonoBehaviour
{
        [Header("Pick Up Settings")]
        [SerializeField] private Transform holdArea;
        private GameObject heldObject;
        private Rigidbody heldObjectRb;
        
        [Header("Physics Settings")]
        [SerializeField]private float pickupDistance;
        [SerializeField]private float pickupForce;

        public static bool inRange;
        public static bool isLock;
        public static bool keypadBool = false;
        

        private void Update()
        {
                
               isLock = false;
                if (Input.GetKeyDown(KeyCode.E))
                {
                        if (heldObject == null)
                        {
                               
                                if (Physics.Raycast(transform.position, transform.forward, out var hit, pickupDistance)){
                                        //Show raycast in scene view
                                        Debug.DrawRay(transform.position, transform.forward * pickupDistance, Color.blue, 1f);
                                        if (hit.collider.CompareTag("PickUp"))
                                        {
                                                PickupObject(hit.transform.gameObject);
                                        }
                                        if (hit.collider.CompareTag("Keypad"))
                                        {
                                                keypadBool = true;
                                        }
                                }
                        }  
                        else
                        {
                                print("drop item");
                                DropObject();
                        }
                }
                if(heldObject != null)
                {
                        MoveObject();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),
                                    out var hit2, pickupDistance)) return;
                        if (!hit2.collider.CompareTag("Button")) return;
                        inRange= true;
                }

                if (heldObject)
                {
                        if (Input.GetMouseButton(1))
                        {
                                //rotate object with mouse input base on world
                                heldObject.transform.Rotate(-Vector3.up * Input.GetAxis("Mouse X") * 5f, Space.World);
                                heldObject.transform.Rotate(-Vector3.right * Input.GetAxis("Mouse Y") * 5f, Space.World);

                                //freeze rotation off camera
                                isLock = true;

                        }
                        else{
                                isLock = false;
                        }
                        //if (Input.GetMouseButton(0)) throw object
                        if (Input.GetMouseButtonDown(0))
                        {
                                heldObjectRb.AddForce(transform.forward * pickupForce/7, ForceMode.Impulse);
                                DropObject();
                        }
                        
                }
                
                
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void PickupObject(GameObject pickObj)
        {
                if(pickObj.GetComponent<Rigidbody>())
                {
                        heldObjectRb = pickObj.GetComponent<Rigidbody>();
                        heldObjectRb.useGravity= false;
                        heldObjectRb.drag = 10;
                        heldObjectRb.constraints = RigidbodyConstraints.FreezeRotation;
                                
                        heldObjectRb.transform.parent = holdArea;
                        heldObject = pickObj;
                }
        }
        void DropObject()
        {
                heldObjectRb.useGravity= true;
                heldObjectRb.drag = 1;
                heldObjectRb.constraints = RigidbodyConstraints.None;
                heldObjectRb.transform.parent = null;
                heldObject = null;
        }

        void MoveObject()
        {
                if(Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
                {
                        Vector3 moveDirection = (holdArea.position - heldObject.transform.position);
                        heldObjectRb.AddForce(moveDirection * pickupForce);
                }
        }
}
