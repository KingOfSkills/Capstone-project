using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int money = 0;

    public AudioSource[] audioSources;// = new AudioSource[];

    private WallHealth wallHealth;
    private BulletUpgrade bulletUpgrade;
    private PlayerController playerController;
    //[SerializeField] private GameObject bulletPrefab;

    private ShopUI shopUI;
    private int wallUpgradeCost = 50;
    //private int wallUpgradeCostIncrement = 25;
    //private int playerhealthUpgradeCost = 50;
    //private int wallUpgradeCostIncrement = 25;
    //private int wallUpgradeCost = 50;
    //private int wallUpgradeCostIncrement = 25;

    private void Start()
    {
        money = 0;
        wallHealth = FindObjectOfType<WallHealth>();
        bulletUpgrade = FindObjectOfType<BulletUpgrade>();
        playerController = FindObjectOfType<PlayerController>();
        //bulletUpgradeCost = bulletUpgrade.GetUpgradeCost();
        //bullet = bulletPrefab.GetComponent<Bullet>();
        shopUI = FindObjectOfType<ShopUI>();
        shopUI.UpdateMoneyText(money);
        shopUI.UpdateBulletText(bulletUpgrade.GetUpgradeCost(), bulletUpgrade.GetTotalDamage());
        shopUI.UpdateWallHealthText(wallUpgradeCost, wallHealth.GetMaxHealth(), wallHealth.GetCurrentHealth());
        //shopUI.UpdateBulletText(bulletUpgrade.GetUpgradeCost(), bulletUpgrade.GetBulletDamage());
    }

    public void BuyBulletUpgrade()
    {
        if (money >= bulletUpgrade.GetUpgradeCost()) //bulletUpgradeCost)
        {
            money -= bulletUpgrade.GetUpgradeCost();
            bulletUpgrade.BuyUpgrade();
            //Bullet.IncreaseDamage();
            //bulletUpgradeCost += 5;
            shopUI.UpdateMoneyText(money);
            shopUI.UpdateBulletText(bulletUpgrade.GetUpgradeCost(), bulletUpgrade.GetTotalDamage());
            audioSources[0].PlayOneShot(audioSources[0].clip);
        } else
        {
            //Debug.Log("Not enough money");
            audioSources[1].PlayOneShot(audioSources[1].clip);
        }
    }

    public void BuyWallUpgrade()
    {
        if (money >= wallHealth.GetUpgradeCost())
        {
            money -= wallHealth.GetUpgradeCost();
            //Bullet.IncreaseDamage();
            wallHealth.IncreaseMaxHealth();
            shopUI.UpdateMoneyText(money);
            shopUI.UpdateWallHealthText(wallUpgradeCost, wallHealth.GetMaxHealth(), wallHealth.GetCurrentHealth());
            
            //shopUI.UpdateWallHealthText();
            audioSources[0].PlayOneShot(audioSources[0].clip);
        }
        else
        {
            //Debug.Log("Not enough money");
            audioSources[1].PlayOneShot(audioSources[1].clip);
        }
    }

    public void BuyHealth()
    {
        if (money >= 30)
        {
            playerController.IncreaseHealth();
            money -= 30;
            shopUI.UpdateMoneyText(money);
            audioSources[0].PlayOneShot(audioSources[0].clip);
        }
        else
        {
            audioSources[1].PlayOneShot(audioSources[1].clip);
        }
    }

    public void UpdateMoney(int amount)
    {
        money += amount;
        shopUI.UpdateMoneyText(money);
    }
}
