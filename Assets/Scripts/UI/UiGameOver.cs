using System;
using Core;
using GameplaySystem;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class UiGameOver : UiSwitch
{
    [SerializeField]private GameplayController gameplayController;

    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text winnerText;

    void Start()
    {
        
        SetState(false);

        gameplayController.GameOver += OnGameOver;
        gameplayController.victoryCondition.PlayerWin += OnPlayerWin;
    }

    

    private void OnDestroy()
    {
        gameplayController.GameOver -= OnGameOver;
        gameplayController.victoryCondition.PlayerWin -= OnPlayerWin;

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
    
    private void OnPlayerWin(PlayerController controller)
    {
        winnerText.SetText("Player "+controller.CharacterConfig.ID.ToString());
        winnerText.color = controller.CharacterConfig.Color;
    }

    
    
    


}
