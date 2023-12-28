using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallHealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text maxHealth;
    [SerializeField] private TMP_Text currentHealth;
    [SerializeField] private GameObject healthBar;

    public void UpdateCurrentHealth(int current)
    {
        currentHealth.text = current.ToString();
        healthBar.GetComponent<Slider>().value = current;
    }

    public void UpdateMaxHealth(int max)
    {
        maxHealth.text = max.ToString();
        healthBar.GetComponent<Slider>().maxValue = max;
    }
}
