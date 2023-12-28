using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static bool isGamePaused = false;
    public static bool isGameOver = false;
    public static bool isShopOpen = false;
    public static bool isBossFight = false;
    public static bool didPlayerWin;
    //public int money = 0;

    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button readyButton; // not yet made

    private PlayerUI playerUI;

    private enum State
    {
        WaitingToStart,
        WaveInProgress,
        ReliefTime,
    }

    private State gameState;

    //public void UpdateMoney(int amount)
    //{
    //    //money += amount;
    //    //FindObjectOfType<PlayerUI>().UpdateMoneyText(money);
    //}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
        isBossFight = false;
        gameState = State.WaitingToStart;
    }

    private void Update()
    {
        switch (gameState)
        {
            case State.WaitingToStart:
                WaveManager.Instance.SetWaveState(WaveManager.State.WaitingToStart);
                gameState = State.WaveInProgress;
                break;
            case State.WaveInProgress:
                break;
            case State.ReliefTime:
                break;
        }
        Shop();
        PauseInput();
    }

    private void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                Pause();

            }
            else
            {
                Resume();
            }
        }
    }

    private void Pause()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    private void Shop()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isShopOpen == false)
            {
                shop.SetActive(true);
                isShopOpen = true;
            }
            else
            {
                shop.SetActive(false);
                isShopOpen = false;
            }
        }
    }

    public void GameOver()
    {
        didPlayerWin = false;
        SceneManager.LoadScene("Credits");
    }

    public void WinGame()
    {
        didPlayerWin = true;
        SceneManager.LoadScene("Credits");
    }
}
