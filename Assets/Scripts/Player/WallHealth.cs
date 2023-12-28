using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallHealth : MonoBehaviour
{
    private WallHealthUI wallHealthUI;

    private int currentHealth;
    private int maxHealth = 25;

    private int upgradeCost = 50;
    private int upgradeCostIncrement = 25;
    private int healthUpgradeAmount = 25;
    private int repairAmount = 10;

    private void Start()
    {
        wallHealthUI = FindObjectOfType<WallHealthUI>();
        currentHealth = maxHealth;
        wallHealthUI.UpdateMaxHealth(maxHealth);
        wallHealthUI.UpdateCurrentHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        wallHealthUI.UpdateCurrentHealth(currentHealth);
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.TryGetComponent(out Zombie enemy))
    //    {
    //        TakeDamage(enemy.damage);
    //        //enemies.Add(enemy);
    //    }
    //}

    public void IncreaseMaxHealth()
    {
        maxHealth += healthUpgradeAmount;
        currentHealth += healthUpgradeAmount;
        upgradeCost += upgradeCostIncrement;
    }

    public void Repair()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += repairAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        } else
        {
            Debug.Log("Already at max durability");
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }
}
