using TMPro;
using UnityEngine;
using GameplaySystem.Spawning;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private WaveSpawnerArea waveSpawner;

    private void Update()
    {
        if (waveSpawner == null || timerText == null) return;

        if (!waveSpawner.EmIntervaloEntreHordas)
        {
            timerText.text = "";
            return;
        }

        float tempo = Mathf.Ceil(waveSpawner.TempoAteProximaHorda);
        timerText.text = $"Próxima horda em: {tempo}s";
    }
}