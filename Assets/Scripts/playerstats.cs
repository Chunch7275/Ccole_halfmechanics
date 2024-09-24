using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstats : MonoBehaviour
{
    public float HealItems = 3f;     
    public int health = 100;         
    public int stealthValue = 5;      
    public int maxHealth = 100;       
    public int healAmount = 35;       
    void Start()
    {
    }
   // Maybe saturate could work rather than a manual clamp? ex: health = saturate(health);
   
    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);  
     

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, maxHealth);  
        Debug.Log("Player healed! Current health: " + health);
    }

    void Die()
    {
        Debug.Log("Player has died.");
    }

    void UseHealItem()
    {
        if (HealItems > 0)
        {
            Heal(healAmount);   
            HealItems--;        
            Debug.Log("Used a heal item. Remaining heal items: " + HealItems);
        }
        else
        {
            Debug.Log("No heal items left!");
        }
    }

    void Update() 
    {
    // maybe double-heal issue comes from healing input being stored in void update? Not certain though.
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseHealItem();
        }
    }
}
