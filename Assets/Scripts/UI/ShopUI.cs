using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private Text bulletCostText;
    [SerializeField] private Text bulletDMGText;

    [SerializeField] private Text wallCostText;
    [SerializeField] private Text wallCurrentHealthShopText;
    [SerializeField] private TMP_Text wallMaxHealthText;
    [SerializeField] private TMP_Text wallCurrentHealthText;

    public void UpdateMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }

    public void UpdateBulletText(int bulletCost, int bulletDMG)
    {
        bulletCostText.text = "$" + bulletCost.ToString();
        bulletDMGText.text = bulletDMG.ToString();
    }

    public void UpdateWallHealthText(int cost, int maxHealth, int currentHealth)
    {
        wallCostText.text = "$" + cost.ToString();
        wallCurrentHealthShopText.text = maxHealth.ToString();
        wallMaxHealthText.text = maxHealth.ToString();
        wallCurrentHealthText.text = currentHealth.ToString();
    }
}
