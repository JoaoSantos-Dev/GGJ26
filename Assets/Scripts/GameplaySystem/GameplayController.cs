using System;
using System.Collections.Generic;
using GameplaySystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameplaySystem
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager  playerInputManager;
        [SerializeField] private PlayersLifeCycle playersLifeCycle;
        private VictoryCondition victoryCondition;

        private void Awake()
        {
            victoryCondition = new (playersLifeCycle);
        }
        
        
        
        
        
    }
}

