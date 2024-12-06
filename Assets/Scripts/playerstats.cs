using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerstats : MonoBehaviour
{
    public float HealItems = 3f;
    public int health = 100;
    public int stealthValue = 5;
    public int maxHealth = 100;
    public int healAmount = 35;

    private GameManager controller;

    // Power-up related variables
    private bool isPowerUpActive = false;
    private float powerUpDuration = 60f;
    private int originalHealth;
    private int originalStealth;
    public string levelName;
    void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        Debug.Log("Player healed! Current health: " + health);
    }

    void Die()
    {
        Debug.Log("Player has died.");
        controller.gameOver = true;
        SceneManager.LoadScene(levelName);
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

    // Coroutine for the power-up effect
    private IEnumerator PowerUpCoroutine()
    {
        if (isPowerUpActive) yield break;

        isPowerUpActive = true;

        // Store original values
        originalHealth = health;
        originalStealth = stealthValue;

        // Increase health and stealth value by 50%
        health += originalHealth / 2;
        stealthValue += originalStealth / 2;

        Debug.Log("Power-up activated! Increased health and stealth.");

        float timer = powerUpDuration;
        while (timer > 0)
        {
            Debug.Log("Power-up time left: " + Mathf.CeilToInt(timer) + " seconds");
            timer -= Time.deltaTime;
            yield return null;
        }

        // Revert to original values after the duration
        health = originalHealth;
        stealthValue = originalStealth;
        isPowerUpActive = false;

        Debug.Log("Power-up expired. Stats reverted.");
    }

    // Trigger for the power-up pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            StartCoroutine(PowerUpCoroutine());
            Destroy(other.gameObject);
        }
    }
}