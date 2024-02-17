using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;

    private bool isAttacking = false; // New variable to track attack state

    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    public bool IsAttacking()
    {
        return isAttacking;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime) // Check if not attacking
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    private void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack")) // Check if the current animation is the attack animation
        {
            isAttacking = true; // Set attacking to true when attacking
        } else
        {
            isAttacking = false; // Set attacking to false when not attacking
        }
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
}

