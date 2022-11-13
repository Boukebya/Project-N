using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera: MonoBehaviour
{

    public Transform cameraPosition;

    // La camera prend la position du parent
    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
