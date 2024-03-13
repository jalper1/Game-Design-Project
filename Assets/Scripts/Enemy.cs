using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public Collider2D hitBox;
    int resourceAmount = 0;
    public int maxResourceAmount = 50;
    public int minResourceAmount = 10;
    //public AIPath aiPath;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        resourceAmount = Random.Range(minResourceAmount, maxResourceAmount);
    }

    //private void Update()
    //{
     //   if(aiPath.desiredVelocity.x >= 0.01f)
     //   {
     //       transform.localScale = new Vector3(-1f, 1f, 1f);
      //  } else if (aiPath.desiredVelocity.x <= -0.01f)
      //  {
      //      transform.localScale = new Vector3(1f, 1f, 1f);
     //   }
    //}

    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        GameManager.Instance.playerResources.AddToResourceTotal(resourceAmount, ResourceType.Husk);
        GetComponent<Collider2D>().enabled = false;
        hitBox.enabled = false;
        this.enabled = false;
    }

}
