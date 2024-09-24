using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI waveText;

    public static WaveManager Instance;

    int currentWave = 0;
    int currentWaveTime;
    bool waveRunning;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        StartNewWave();
    }

    private void StartNewWave()
    {
        StopAllCoroutines();
        timeText.color = Color.white;
        currentWave++;
        waveRunning = true;
        currentWaveTime = 30;
        waveText.text = "Wave: " + currentWave;
        StartCoroutine(WaveTimer());
    }

    IEnumerator WaveTimer()
    {
        while(waveRunning)
        {
            yield return new WaitForSeconds(1f);
            currentWaveTime--;

            timeText.text = currentWaveTime.ToString();

            if (currentWaveTime <= 0)
            {
                WaveComplete();
            }
        }
        yield return null;
    }

    private void WaveComplete()
    {
        StopAllCoroutines();
        EnemyManager.Instance.DestroyAllEnemies();

        waveRunning = false;
        currentWaveTime = 30;

        timeText.text = currentWaveTime.ToString();
        timeText.color = Color.red;
    }
    public bool WaveRunning() => waveRunning;
}
