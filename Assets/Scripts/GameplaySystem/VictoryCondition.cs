using System;
using UnityEngine;

namespace GameplaySystem
{
    
    public class VictoryCondition : IDisposable
    {
        private readonly PlayersLifeCycle playersLifeCycle;

        public event Action<PlayerController> PlayerWin;

        public VictoryCondition(PlayersLifeCycle playersLifeCycle)
        {
            this.playersLifeCycle = playersLifeCycle;
            playersLifeCycle.OnPlayerExit += OnPlayerExit;
        }
        public void Dispose()
        {
            playersLifeCycle.OnPlayerExit -= OnPlayerExit;
        }
        private void OnPlayerExit(PlayerController obj)
        {
            CheckVictory();
        }

        

        private void CheckVictory()
        {
            if (playersLifeCycle.PlayerCount == 1)
            {
                Debug.Log("fim de jogo : ");
                PlayerWin?.Invoke(playersLifeCycle.Players[0]);
            }
        }



    }
}