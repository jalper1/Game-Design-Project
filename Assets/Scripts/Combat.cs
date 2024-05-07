using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vitals;
using XEntity.InventoryItemSystem;
using Input = UnityEngine.Input;

namespace Custom.Scripts
{
    public class Combat : MonoBehaviour
    {
        public Animator animator;

        private bool isAttacking = false; // New variable to track attack state
        private Resource resourceGained;
        private ResourceManage resourceManager; private Item harvestItem;

        public float attackRate = 2f;
        public float nextAttackTime = 0f;
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask hitLayers;
        public LayerMask resourceLayers;

        PlayerCharacter playerCharacter;
        public bool canAttack = true;

        public AudioSource AudioSource;
        public AudioClip hitPerson;
        public AudioClip hitResource;
        public AudioClip hitNothing;

        public bool IsAttacking()
        {
            return isAttacking;
        }
        void Start()
        {
            resourceManager = GameManager.Instance.playerResources;
            playerCharacter = GetComponent<PlayerCharacter>();
        }

        void Update()
        {
            //Debug.Log(playerCharacter.stamina.Value);
            if (playerCharacter.EnoughStaminaAttack())
            {
                canAttack = true;

            }
            else
            {
                canAttack = false;
            }

            if (Time.time >= nextAttackTime && canAttack) // Check if not attacking
            {
                if (Input.GetButtonDown("Attack"))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            if (playerCharacter.health.Value <= 0)
            {
                animator.SetTrigger("Death");
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

        private void PlayAudio()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);
            Collider2D[] hitResources = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, resourceLayers);

            if (hitEnemies.Length > 0)
            {
                AudioSource.PlayOneShot(hitPerson);
            }
            else if (hitResources.Length > 0)
            {
                AudioSource.PlayOneShot(hitResource);
            }
            else
            {
                AudioSource.PlayOneShot(hitNothing);
            }
        }
        // called by animation event
        private void AttackHit()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);
            Collider2D[] hitResources = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, resourceLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.transform.root.GetComponent<Enemy>().ReceiveDamage(GameManager.Instance.attackStrength);
            }
            foreach (Collider2D resource in hitResources)
            {
                (harvestItem, resourceGained.resourcesGained) = resource.transform.GetComponent<ResourceCalc>().CollectResource(GameManager.Instance.harvestStrength);
                resourceManager.AddToResourceTotal(resourceGained.resourcesGained, harvestItem.name);

            }

            playerCharacter.ConsumeStamina(10);
            VitalsUIBind bindComponent = playerCharacter.staminaBar.GetComponent<VitalsUIBind>();
            bindComponent.UpdateImage(playerCharacter.stamina.Value, playerCharacter.stamina.MaxValue, false);
        }

        void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

}