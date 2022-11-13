using System.Collections;
using TMPro;
using UnityEngine;

public class final : MonoBehaviour
{
    [SerializeField] private EndDialog txt;
    [SerializeField] private TMP_Text dialog;
    [SerializeField] private TMP_Text protagonist;

    void Start()
    {
        StartCoroutine(LaunchDialog());
    }
    

    IEnumerator LaunchDialog()
    {
        protagonist.text = "";
        dialog.text = "";
        yield return new WaitForSeconds(5);
        for (int index = 0; index <= 16; index++)
        {
            protagonist.text = txt.names[index % 2];
            dialog.text = "";
            for (int letter = 0; letter < txt.text[index].Length; letter++)
            {
                dialog.text += txt.text[index][letter];
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
