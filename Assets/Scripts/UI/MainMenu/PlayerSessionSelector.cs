using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace UI
{
    public class PlayerSessionSelector : MonoBehaviour
    {
        [FormerlySerializedAs("playerSessionData")] [SerializeField] private GameSessionSO gameSessionData;
        [SerializeField, Range(2, 4)] private int playerSessionTarget = 2;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ChoosePlayerSession);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        private void ChoosePlayerSession()
        {
            gameSessionData.MaxPlayer = playerSessionTarget;
            SceneLoader.LoadGameplaySceneAsync();
        }
        
    }
}
