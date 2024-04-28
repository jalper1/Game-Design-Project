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
    Collider2D playerZone;

    bool isAttacking = false; // Flag to track if enemy is attacking

    PlayerCharacter playerCharacter;

    public AudioSource AudioSource;
    public AudioClip playerHurtSound;
    public AudioClip playerHurtSound2;


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

        playerZone = Physics2D.OverlapCircle(transform.position, attackRange, playerHitLayers);
        if (playerZone != null)
        {
            animator.SetFloat("Speed", 0);

            if (!isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("die")) // Only check for player range if not already attacking
            {
                Attack();
            }
        }
        else
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    private async void FinishAttack()
    {
        int hurt = Random.Range(0, 2);
        playerZone = Physics2D.OverlapCircle(transform.position, attackRange, playerHitLayers);
        if (playerCharacter.health.Value <= 0 || animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") || playerZone == null)
        {
            isAttacking = false;
            return;
        }
        if (hurt == 0)
        {
            AudioSource.PlayOneShot(playerHurtSound2);
        }
        else
        {
            AudioSource.PlayOneShot(playerHurtSound);
        }

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
        playerZone = Physics2D.OverlapCircle(transform.position, attackRange, playerHitLayers);
        if (playerZone != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            animator.SetTrigger("Attack");
            Invoke("FinishAttack", 0.275f);
        }
        else
        {
            isAttacking = false;
        }

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
