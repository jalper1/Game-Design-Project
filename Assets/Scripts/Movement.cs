using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    // Update is called once per frame
    void Update()
    {

        horMove = Input.GetAxisRaw("Horizontal") * (runSpeed / 4);
        vertMove = Input.GetAxisRaw("Vertical") * (runSpeed / 4);
        animator.SetFloat("Speed", Mathf.Abs(horMove) + Mathf.Abs(vertMove));


        if (!combat.IsAttacking() && Time.time >= nextDashTime)
        {
            if (Input.GetButtonDown("Dash"))
            {
                dash = true;
                nextDashTime = Time.time + 1f / dashRate;
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
