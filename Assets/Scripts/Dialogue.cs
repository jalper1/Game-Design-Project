using Custom.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vitals;

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

    public AudioSource AudioSource;
    public AudioClip levelupAudio;
    public AudioClip dialogueAudio;

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
        if (UnityEngine.Input.GetButtonDown("Interact") && dialogueActivated == true)
        {
            if (step >= GameManager.Instance.dialogueWords.Length || GameManager.Instance.dialogueWords[step] == "")
            {
                dialogueCanvas.SetActive(false);
                step = 0;
                if (GameManager.Instance.levelup)
                {
                    AudioSource.PlayOneShot(levelupAudio);
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
                        case 5:
                            GameManager.Instance.harvestStrength = 6;
                            RespawnManager.Instance.playerLife = 180;
                            break;
                        case 6:
                            GameManager.Instance.win = true;
                            break;
                        default:
                            GameManager.Instance.harvestStrength = 6;
                            RespawnManager.Instance.playerLife = 180;
                            break;
                    }
                    playerCharacter.health.SetMax(RespawnManager.Instance.playerLife);
                    playerCharacter.health.Set(RespawnManager.Instance.playerLife);
                    VitalsUIBind bindComponent = playerCharacter.healthBar.GetComponent<VitalsUIBind>();
                    bindComponent.UpdateImage(playerCharacter.health.Value, playerCharacter.health.MaxValue, false);
                }
                GameManager.Instance.levelup = false;
            }
            else
            {
                dialogueCanvas.SetActive(true);
                speakerText.text = speaker[0];
                dialogueText.text = GameManager.Instance.dialogueWords[step];
                step += 1;
                AudioSource.PlayOneShot(dialogueAudio);
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
