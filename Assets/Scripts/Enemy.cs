using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public Collider2D hitBox;
    public int resourceAmount = 33;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

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
