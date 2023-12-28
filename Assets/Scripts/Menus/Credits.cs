using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private Text gameOver;

    private void Start()
    {
        if (GameManager.didPlayerWin == true)
        {
            gameOver.text = "You Win!";
        }
        else
        {
            gameOver.text = "Game Over!";
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
