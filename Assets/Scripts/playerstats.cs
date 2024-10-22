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

    private GameManager controller;

    void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

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
        controller.gameOver = true;
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
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseHealItem();
        }

        if (health <= 0)
        {
            Die();
        }
    }
}