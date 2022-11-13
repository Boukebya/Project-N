using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Keypad : MonoBehaviour
{
    //get player
    public GameObject player;
    public GameObject control;
    Animator _anim;
    public TextMeshPro gt;
    public AudioClip keypadSound;
    
    public static bool IsLock = false;

    [SerializeField] bool whichOne;
    [SerializeField] Light roomLight;
    [SerializeField] Light playerLight;

    
    private bool _isTyping;
    public void Start()
    {
        //get animator
        _anim = control.GetComponent<Animator>();
    }

    void Update()
    {
        // if e is pressed down, change bool
        if (PickUpController2.keypadBool && _isTyping ==false )
        {
            gt.text = "";
            _isTyping = true;
            IsLock = true;
        }
        // if pressed E
        if (_isTyping)
        {
            // if player is in range of keypad
            if (Vector3.Distance(transform.position, player.transform.position) < 4)
            {
                KeypadFunction();
            }
        }
        
    }
    
    //Keypad function
    private void KeypadFunction()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (gt.text.Length != 0)
                {
                    gt.text = gt.text.Substring(0, gt.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                _isTyping = false;
                PickUpController2.keypadBool = false;
                IsLock = false;
                
            }
            else
            {
                gt.text += c;
            }
        }

        // if input is 3724
        if (gt.text == "3724" & !whichOne)
        {
            _isTyping = false;
            PickUpController2.keypadBool = false;
            IsLock = false;
            _anim.SetBool("isReceiving",true);
            AudioSource.PlayClipAtPoint(keypadSound, transform.position,0.2f);
            gt.text = "";
        }
        if (gt.text == "954" & whichOne)
        {
            AudioSource.PlayClipAtPoint(keypadSound, transform.position,0.2f);
            _isTyping = false;
            PickUpController2.keypadBool = false;
            IsLock = false;
            StartCoroutine(ending());
        }
    }

    IEnumerator ending()
    {
        yield return new WaitForSeconds(2);
        roomLight.range = 0;
        yield return new WaitForSeconds(2);
        playerLight.range = 0;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Ending");
    }
}
