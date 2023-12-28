using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    private SpawnManager spawnManager;

    private int waveNumber = 0;
    private int bossWave = 4;
    private float nextWaveStart;
    public static bool isWaveInProgress = false;
    public static bool isBossWave = false;

    [SerializeField] private Text nextWaveText;
    [SerializeField] private Text nextWaveTimerText;
    [SerializeField] private Text waveNumberText;

    private float waveTimeIncrement = 1.5f;

    public enum State
    {
        Idle,
        Spawning,
        WaitingToEnd,
        WaitingToStart,
    }
    public State waveState;

    private void Start()
    {
        Instance = this;
        spawnManager = FindObjectOfType<SpawnManager>();
        //bossWave = 1;
        waveNumber = 0;
        nextWaveStart = 1f;
        isWaveInProgress = false;
        waveState = State.Idle;
    }

    private void Update()
    {
        switch (waveState)
        {
            case State.Idle:
                //waveState = State.WaitingToStart;
                break;
            case State.Spawning:
                break;
            case State.WaitingToEnd:
                break;
            case State.WaitingToStart:
                //CountDownTimer(); // Count down the Timer for next wave to start
                break;
        }
        if (!isWaveInProgress)
        {
            CountDownTimer();
        }
    }

    private void CountDownTimer()
    {
        if (nextWaveStart >= 0) // Countdown to next wave spawn
        {
            nextWaveText.gameObject.SetActive(true);
            nextWaveText.text = "Wave " + (waveNumber + 1);
            nextWaveTimerText.gameObject.SetActive(true);
            nextWaveStart -= Time.deltaTime;
            nextWaveTimerText.text = nextWaveStart.ToString("F2");
        }
        else // Timer reachers zero this happens
        {
            isWaveInProgress = true;
            waveNumber++;
            if (waveNumber == bossWave)
            {
                isBossWave = true;
            }
            else
            {
                isBossWave = false;
            }
            AddToTimer();
            //if (isBossWave)
            //{
            //    spawnManager.SpawnBoss();
            //}
            //else
            //{
            //    spawnManager.SpawnNextWave(waveNumber);
            //}
            spawnManager.SpawnNextWave(waveNumber);
            waveNumberText.text = waveNumber.ToString();
            nextWaveText.gameObject.SetActive(false);
            nextWaveTimerText.gameObject.SetActive(false);
            waveState = State.WaitingToEnd;
        }
    }

    private void AddToTimer()
    {
        nextWaveStart = (waveNumber + 1) * waveTimeIncrement; // Adds waveTimeIncrement seconds to time per wave after timer reaches zero
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public State GetWaveState()
    {
        return waveState;
    }

    public void SetWaveState(State newState)
    {
        waveState = newState;
    }
}
