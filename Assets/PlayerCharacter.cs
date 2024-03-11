using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Vitals;

public class PlayerCharacter : MonoBehaviour
{
    public Health health;
    public Stamina stamina;

    private void Awake()
    {
        health = GetComponent<Health>();
        stamina = GetComponent<Stamina>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        stamina.Regeneration.StartRegeneration();
    }

    public void ConsumeStamina(float amount) => stamina.Decrease(amount);

    public bool EnoughStaminaAttack()
    {
        if (stamina.Value > 15)
        {
            return true;
        }
        else { return false; }
    }

    public bool EnoughStaminaDash()
    {
        if (stamina.Value > 10)
        {
            return true;
        }
        else { return false; }
    }

}
