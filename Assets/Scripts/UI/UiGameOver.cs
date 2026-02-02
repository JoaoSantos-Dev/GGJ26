using System;
using Core;
using GameplaySystem;
using GameplaySystem.Spawning;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class UiGameOver : UiSwitch
{
    [SerializeField]private GameplayController gameplayController;

    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        
        SetState(false);
    }

    private void OnEnable()
    {
        gameplayController.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        gameplayController.GameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        timerText.SetText(timer.TotalTimeFormated());
    }


    public void GoToMenu()
    {
        SceneLoader.LoadMenuSceneAsync();
    }
    
    

    public void Restart()
    {
        SceneLoader.LoadGameplaySceneAsync();
    }
    
    
    
    
    


}
