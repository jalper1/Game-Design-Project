using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vitals;
using XEntity.InventoryItemSystem;

namespace Custom.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Animator animator;
        public int maxHealth = 100;
        int currentHealth;
        public Collider2D hitBox;
        private Health health; // health component
        public GameObject healthBar;
        public CanvasGroup thiefHealthBar; // healthBar's component

        private ResourceManage resourceManager;
        public int harvestStrength = 1;
        public Item harvestItem;

        Interactor interactor;

        public AudioSource AudioSource;
        public AudioClip enemyDeathSound;
        // Start is called before the first frame update
        void Start()
        {
            interactor = GetComponent<Interactor>();
            resourceManager = GameManager.Instance.playerResources;
            currentHealth = maxHealth;
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
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(enemyDeathSound);
            }
            animator.SetBool("IsDead", true);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            hitBox.enabled = false;
            enabled = false;
            GetComponent<NavMeshAgent>().isStopped = true;

            //(harvestItem, resourceGained.resourcesGained) = transform.GetComponent<ResourceCalc>().CollectResource(harvestStrength);
            //resourceManager.AddToResourceTotal(resourceGained.resourcesGained, harvestItem.name);

            // jen's changes
            // get the itemList and add the husk item to it
            ItemContainer itemContainer;
            List<Item> itemList = ItemManager.Instance.itemList;
            itemList.Add(harvestItem);
            Debug.Log(gameObject.name);
            if(gameObject.name == "Enemy2(Clone)")
            {
                itemList.Add(harvestItem);
            }
            resourceManager.AddToResourceTotal(harvestStrength, harvestItem.name);

            GameObject playerInventory = GameObject.Find("Player Inventory");
            if (playerInventory != null)
            {
                // Get the ItemContainer component
                itemContainer = playerInventory.GetComponent<ItemContainer>();

                if (itemContainer != null)
                {
                    itemContainer.clearInv();
                    itemContainer.populateInv();
                }
                else
                {
                    Debug.LogError("ItemContainer component not found on 'Player Inventory' GameObject.");
                }
            }
            else
            {
                Debug.LogError("GameObject 'Player Inventory' not found.");
            }
        }

    }
}