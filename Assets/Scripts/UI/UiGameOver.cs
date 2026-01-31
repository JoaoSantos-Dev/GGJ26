using Core;
using UI;
using UnityEngine;

public class UiGameOver : MonoBehaviour, ISwitchable
{

    public void GoToMenu()
    {
        SceneLoader.LoadMenuSceneAsync();
    }

    public void SetState(bool state)
    {
        
    }
}
