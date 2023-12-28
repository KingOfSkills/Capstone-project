using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private int baseCost;
    private int incrementCost;
    private int totalCost;
    private int upgradesBought;
    private int baseValue;
    private int incrementValue;
    private int totalValue;

    //public Upgrade()
    //{
    //    baseCost = 10;
    //    incrementCost = 10;
    //    upgradesBought = 0;
    //}

    public Upgrade(int initVaue, int initCost, int increaseValue, int increaseCost)
    {
        baseCost = initCost;
        totalCost = baseCost;
        incrementCost = increaseCost;
        baseValue = initVaue;
        totalValue = baseValue;
        incrementValue = increaseValue;
        upgradesBought = 0;
    }

    public void BuyUpgrade()
    {
        // Apply upgrade
        totalValue = baseValue + (incrementValue * upgradesBought);
        // Increase upgradesBought
        upgradesBought++;
    }

    public void IncreaseCost()
    {
        //Increase Cost
        totalCost = baseCost + (incrementCost * upgradesBought);
    }

    public int GetValue()
    {
        return totalValue; // baseValue + incrementValue * upgradesBought;
    }

    public int GetCost()
    {
        return totalCost; // baseCost + incrementCost * upgradesBought;
    }
}
