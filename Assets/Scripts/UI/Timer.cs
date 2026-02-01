using System;
using TMPro;
using UnityEngine;
using GameplaySystem.Spawning;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text waveTimer;
    [SerializeField] private WaveSpawnerArea waveSpawner;

    private void Update()
    {
        if (waveSpawner == null || waveTimer == null) return;

        if (!waveSpawner.EmIntervaloEntreHordas)
        {
            waveTimer.text = "00:00";
            return;
        }

        float tempo = Mathf.Ceil(waveSpawner.TempoAteProximaHorda);
        waveTimer.SetText(string.Format("{0:00}:{1:00}", tempo / 60, tempo % 60));
        float tempoTotal = Mathf.Ceil(waveSpawner.TempoTotal);
        timerText.SetText(string.Format("{0:00}:{1:00}", tempoTotal / 60, tempoTotal % 60));
    }
}