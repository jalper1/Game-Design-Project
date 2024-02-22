using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;

    private bool isAttacking = false; // New variable to track attack state

    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask hitLayers;
    public int attackStrength = 40;

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
        }
        else
        {
            isAttacking = false; // Set attacking to false when not attacking
        }
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void AttackHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.transform.root.GetComponent<Enemy>().ReceiveDamage(attackStrength);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

