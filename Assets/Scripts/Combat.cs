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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) // Check if the current animation is the attack animation
        { 
            isAttacking = false; // Set attacking to true when attacking
        }

    }


    void Attack()
    {
        isAttacking = true; // Set attacking to true when attacking
        animator.SetTrigger("Attack");
    }

    // Method to be called when the attack animation finishes
    public void FinishAttack()
    {
        isAttacking = false; // Set attacking to false when attack animation finishes
    }
}

