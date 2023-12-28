using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    
    [SerializeField] private TMP_Text currentHealth;
    [SerializeField] private TMP_Text maxHealth;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Text lives;
    //[SerializeField] private TMP_Text versionNumberText;

    private void Start()
    {
        UpdateMaxHealth(PlayerController.Instance.GetMaxHealth());
        UpdateCurrentHealth(PlayerController.Instance.GetCurrentHealth());
        UpdateLives(PlayerController.Instance.GetLivesRemaining());
        //versionNumberText.text = Application.version;
    }

    public void UpdateMaxHealth(int max)
    {
        maxHealth.text = max.ToString();
        healthBar.GetComponent<Slider>().maxValue = max;
    }

    public void UpdateCurrentHealth(int current)
    {
        currentHealth.text = current.ToString();
        healthBar.GetComponent<Slider>().value = current;
    }

    public void UpdateLives(int livesLeft)
    {
        lives.text = livesLeft.ToString();
    }
}
