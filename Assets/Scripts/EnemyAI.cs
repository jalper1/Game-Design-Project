using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;

    private Transform target;
    public float speed = 5f;
    public float nextWaypointDistance = 3f;

    public LayerMask playerHitLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool isAttacking = false; // Flag to track if enemy is attacking

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && !animator.GetCurrentAnimatorStateInfo(0).IsName("die"))
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (!isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("die")) // Only check for player range if not already attacking
        {
            Collider2D playerZone = Physics2D.OverlapCircle(transform.position, attackRange, playerHitLayers);

            if (playerZone != null)
            {
                animator.SetFloat("Speed", 0);
                Debug.Log("Player in range");
                StartCoroutine(AttackCoroutine());
            } else
            {
                animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        // Wait for the duration of attack animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        isAttacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (!isAttacking)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
