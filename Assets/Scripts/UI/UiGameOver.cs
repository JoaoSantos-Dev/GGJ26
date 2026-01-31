using Core;
using UI;
using UnityEngine;

public class UiGameOver : UiSwitch
{

    public void GoToMenu()
    {
        SceneLoader.LoadMenuSceneAsync();
    }

    public void Restart()
    {
        SceneLoader.LoadGameplaySceneAsync();
    }


}
