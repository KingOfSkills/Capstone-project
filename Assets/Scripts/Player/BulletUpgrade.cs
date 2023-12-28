using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgrade : MonoBehaviour
{
    //private int baseCost = 15;
    private int baseDamage = 1;
    private int baseCost = 10;
    private int incrementCost = 10;
    private int upgradedDamage = 0;

    public void BuyUpgrade()
    {
        // Apply Upgrade
        upgradedDamage++;
        // Increase Cost
        baseCost += incrementCost;
    }

    public int GetUpgradedDamage()
    {
        return upgradedDamage;
    }

    public int GetUpgradeCost()
    {
        return baseCost; // + incrementCostCost * upgradesBought;
    }

    public int GetTotalDamage()
    {
        return baseDamage + upgradedDamage;
    }
}
