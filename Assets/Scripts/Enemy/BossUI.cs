using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    //[SerializeField] private TMP_Text maxHealthText;
    //[SerializeField] private TMP_Text currentHealthText;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Slider healhtBarSlider;

    public void InitializeHealth(int maxHealth)
    {
        healhtBarSlider.maxValue = maxHealth;
        healhtBarSlider.value = maxHealth;
    }

    public void UpdateCurrentHealth(int current)
    {
        //currentHealthText.text = current.ToString();
        healhtBarSlider.value = current;
    }

    public void UpdateMaxHealth(int max)
    {
        //maxHealthText.text = max.ToString();
        healhtBarSlider.maxValue = max;
    }
}
