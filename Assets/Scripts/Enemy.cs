using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vitals;

namespace Custom.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Animator animator;
        public int maxHealth = 100;
        int currentHealth;
        public Collider2D hitBox;
        int resourceAmount = 0;
        public int maxResourceAmount = 50;
        public int minResourceAmount = 10;
        private Health health; // health component
        public GameObject healthBar;
        public CanvasGroup thiefHealthBar; // healthBar's component

        //public AIPath aiPath;
        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            resourceAmount = Random.Range(minResourceAmount, maxResourceAmount);
            if (healthBar == null)
            {
                Debug.Log("ThiefHealthBar GameObject not found.");
            }
        }

        // sets up the Vitals Health Asset
        private void Awake()
        {
            health = GetComponent<Health>();
        }

        public void ReceiveDamage(int damage)
        {
            currentHealth -= damage;
            health.Decrease(40f);
            // update ui animation bar
            VitalsUIBind bindComponent = healthBar.GetComponent<VitalsUIBind>();
            bindComponent.UpdateImage(health.Value, health.MaxValue, false);
            animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                Die();
                HideHealthBar(); // clear UI
            }
        }

        private void HideHealthBar()
        {
            //thiefHealthBar = healthBar.GetComponent<CanvasGroup>();
            if (thiefHealthBar != null)
            {
                thiefHealthBar.alpha = 0f;
                thiefHealthBar.interactable = false;
                thiefHealthBar.blocksRaycasts = false;
            }
            else
            {
                Debug.Log("CanvasGroup component not found on ThiefHealthBar GameObject.");
            }
        }

        void Die()
        {
            animator.SetBool("IsDead", true);
            GameManager.Instance.playerResources.AddToResourceTotal(resourceAmount, "Husk");
            GetComponent<Collider2D>().enabled = false;
            hitBox.enabled = false;
            this.enabled = false;
        }

    }
}