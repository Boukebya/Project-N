using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    [Header("Mouvement")]
    public float moveSpeed;
    // J'ai essay� de faire une touche de sprint mais j'ai arr�t� ca marchait pas bien
    public float runspeed;

    public float groundDrag;
    [Header("Ground check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Touches")]
    public KeyCode ToucheSaut = KeyCode.Space;
    public KeyCode Run = KeyCode.LeftShift;

    public float jumpForce;
    public float jumpCd;
    public float airMultiplier;
    bool ready = true;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;
   
    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // On recup�re le rb de notre personnage, et on l'empeche de tourner sur lui meme
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        // Envoie un raycast vers le bas de la moiti� de la taille du perso + 1, si cela touche un layer "whatisground" alors grounded = true
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 1f, whatIsGround);
        MyInput();
        SpeedControl();

        // Si on est sur le sol on applique un drag pr�d�finit, permet d'arreter de glisser sur le sol
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0; 
    }

    private void FixedUpdate()
    {
        if (Keypad.IsLock ==false)
        {
            Moveplayer();
            // la gravit� n'est pas assez forte sur le personnage, quand on saute il flotte alors j'ai rajout� de la force
            rb.AddForce(Vector3.down * 40f);
        }
    }

    private void MyInput()
    {
        // recup�re les input horizontaux et verticaux, on peut els param�trer dans unity
        //devant derriere
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Gauche et droite
        verticalInput = Input.GetAxisRaw("Vertical");
        
        // On saute si la touche de saut est appuy�, que le cd du saut est pass�, et qu'on est au sol
        if(Input.GetKey(ToucheSaut) && ready && grounded)
        {
            ready = false;
            Jump();
            //G�re le cd
            Invoke(nameof(ResetJump), jumpCd);
        }

    }

    // Fonction des mouvements du joueur
    private void Moveplayer()
    {
        //calcul la direction de d�placement
        //Prends le mouvement sur un axe avec les mouvements avant/arriere
        //et sur un autre pour gauche droite en fonction du parent "orientation" qui d�pend de ou on regarde avec la camera
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //si on est au sol on ajoute une force  qui d�pend de la movespeed, movedirection est normalis� pour pas aller trop vite, le mode de force correspond a une force
        if(grounded)
             rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); 
        // On diminue la vitesse quand on saute pour �viter d'etre trop mobile dans les airs
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    //Empeche de d�passer la vitesse "movespeed", mais ca n'a pas l'air de bien fonctionner car la vitesse d�passe :(   
    private void SpeedControl()
    {
        // On r�cup�re la vitesse sur x et z et on set � 0 la vitesse en hauteur
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // Si la magnitude d�passe movespeed alors on limite la vitesse qui ne dois pas d�passer movespeed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            // On applique la force limit�
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    private void Jump()
    {
        // On r�cup�re la vitesse sur x et z et on set � 0 la vitesse en hauteur
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // on ajoute une force sur l'axe y �quivalent a jumpforce, sous forme d'impulsion
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
   
    private void ResetJump()
    {
        ready = true;
    }
}
