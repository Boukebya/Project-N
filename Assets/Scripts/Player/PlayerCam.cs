using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //Sensibilit�
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    private void Start()
    {
        // Pour pouvoir cliquer dessus, fait disparaitre le curseur
        Cursor.lockState = CursorLockMode.Locked;
        // Fais disparaitre le urseur malgr� la ligne du dessus, c'est une s�curit�
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {

        if (PickUpController2.isLock == false && Keypad.IsLock == false)
        {
            //On r�cup�re l'axe de la souris, qu'on multiplie avec la sensibilit� et la diff de temps
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            // On ajoute la rotation de la souris
            yRotation += mouseX;
            // -= sinon c'est dans le sens inverse
            xRotation -= mouseY;

            // Limite x rotation entre -90 et 90 pour �viter de se faire le coup du lapin quand on l�ve la t�te
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Orientation de la camera et de orientation
            // Mouvement en x quand on l�ve la tete et y sur les cot�s
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            // orientation servira pour les mouvements pour pouvoir courir en fonction de orientation et ainsi toujours courir en fonction d'ou on regarde
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        
    }
}
