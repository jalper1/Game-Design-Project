using Custom.Scripts;
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

    private bool dialogueActivated;
    private int step; // keep track of dialgoue conversation

    private GameObject player;
    PlayerCharacter playerCharacter;

    // TODO: add in disabling dialogue interaction when npc dies or no longer need it

    // Update is called once per frame
    private void Start()
    {
        speaker[0] = "Core";
    }
    void Update()
    {
        player = GameObject.Find("Player");
        playerCharacter = player.GetComponent<PlayerCharacter>();
        if (Input.GetButtonDown("Interact") && dialogueActivated == true)
        {
            if (step >= GameManager.Instance.dialogueWords.Length || GameManager.Instance.dialogueWords[step] == "")
            {
                dialogueCanvas.SetActive(false);
                step = 0;
                if (GameManager.Instance.levelup)
                {
                    switch (GameManager.Instance.coreLevel)
                    {
                        case 1:
                            GameManager.Instance.harvestStrength = 2;
                            RespawnManager.Instance.playerLife = 100;
                            break;
                        case 2:
                            GameManager.Instance.harvestStrength = 3;
                            RespawnManager.Instance.playerLife = 120;
                            break;
                        case 3:
                            GameManager.Instance.harvestStrength = 4;
                            RespawnManager.Instance.playerLife = 140;
                            break;
                        case 4:
                            GameManager.Instance.harvestStrength = 5;
                            RespawnManager.Instance.playerLife = 160;
                            break;
                        default:
                            GameManager.Instance.harvestStrength = 2;
                            RespawnManager.Instance.playerLife = 100;
                            break;
                    }
                    playerCharacter.health.Set(RespawnManager.Instance.playerLife);
                    playerCharacter.health.SetMax(RespawnManager.Instance.playerLife);
                }
                GameManager.Instance.levelup = false;
            }
            else
            {
                dialogueCanvas.SetActive(true);
                speakerText.text = speaker[0];
                dialogueText.text = GameManager.Instance.dialogueWords[step];
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
        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }

        step = 0;
    }

}
