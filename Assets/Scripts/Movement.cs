using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Vitals;
using Input = UnityEngine.Input;

public class Movement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Combat combat; // Reference to the Combat script

    float horMove = 0f;
    float vertMove = 0f;
    public float runSpeed = 40f;

    bool dash = false;

    public float dashRate = 2f;
    public float nextDashTime = 0f;

    PlayerCharacter playerCharacter;
    public bool canDash = true;

    private void Start()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {

        horMove = Input.GetAxisRaw("Horizontal") * (runSpeed / 4);
        vertMove = Input.GetAxisRaw("Vertical") * (runSpeed / 4);
        animator.SetFloat("Speed", Mathf.Abs(horMove) + Mathf.Abs(vertMove));

        if (playerCharacter != null )
        {
            if (playerCharacter.EnoughStaminaDash())
            {
                canDash = true;
            }
            else
            {
                canDash = false;
            }
        }
        else
        {
            Debug.Log("PLAYER CHARACTER NOT INSTANTIATED");
        }

        if (!combat.IsAttacking() && Time.time >= nextDashTime && canDash)
        {
            if (Input.GetButtonDown("Dash"))
            {
                dash = true;
                nextDashTime = Time.time + 1f / dashRate;

                playerCharacter.ConsumeStamina(10);
                VitalsUIBind bindComponent = playerCharacter.staminaBar.GetComponent<VitalsUIBind>();
                bindComponent.UpdateImage(playerCharacter.stamina.Value, playerCharacter.stamina.MaxValue, false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (combat.IsAttacking()) // Check if not attacking
        {
            horMove = 0f; vertMove = 0f; // Reset movement if attacking
        }
        controller.Move(horMove, vertMove, dash);
        dash = false;
    }
}
