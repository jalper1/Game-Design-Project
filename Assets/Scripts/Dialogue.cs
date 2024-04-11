using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // UI References
    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private TMP_Text speakerText;

    [SerializeField]
    private TMP_Text dialogueText;

    // Dialogue Content
    [SerializeField]
    private string[] speaker;

    [SerializeField]
    [TextArea]
    private string[] dialogueWords;

    private bool dialogueActivated;
    private int step; // keep track of dialgoue conversation


    // TODO: add in disabling dialogue interaction when npc dies or no longer need it

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && dialogueActivated == true)
        {
            if (step >= dialogueWords.Length)
            {
                dialogueCanvas.SetActive(false);
                step = 0;
            }
            else
            {
                dialogueCanvas.SetActive(true);
                speakerText.text = speaker[step];
                dialogueText.text = dialogueWords[step];
                step += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueActivated = false;
        dialogueCanvas.SetActive(false);
        step = 0;
    }

}
