using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Vitals;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;

    private Transform target;
    NavMeshAgent agent;

    public LayerMask playerHitLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackStrength = 10f;
    float newHealth;

    bool isAttacking = false; // Flag to track if enemy is attacking

    PlayerCharacter playerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        playerCharacter = target.GetComponent<PlayerCharacter>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("die"))
        {
            agent.isStopped = true;
            return;
        }
        agent.SetDestination(target.position);
        if (agent.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (agent.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {

        }

        Collider2D playerZone = Physics2D.OverlapCircle(transform.position, attackRange, playerHitLayers);
        if (playerZone != null)
        {
            animator.SetFloat("Speed", 0);
            agent.isStopped = true;

            if (!isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("die")) // Only check for player range if not already attacking
            {
                Attack();
            }
        }
        else
        {
            agent.isStopped = false;
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    private async void FinishAttack()
    {
        playerCharacter.health.Decrease(attackStrength);
        VitalsUIBind bindComponent = playerCharacter.healthBar.GetComponent<VitalsUIBind>();
        bindComponent.UpdateImage(playerCharacter.health.Value, playerCharacter.health.MaxValue, false);
        RespawnManager.Instance.playerLife = (int)playerCharacter.health.Value;
        await Task.Delay(1000);
        isAttacking = false;
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        FinishAttack();
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
