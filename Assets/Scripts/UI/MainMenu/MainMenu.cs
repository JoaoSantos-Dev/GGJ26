using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private ModularWindow creditsWindow;
        [SerializeField] private ModularWindow controlsWindow;
        


        public void ShowCredits()
        {
            creditsWindow.ActiveWindow();
        }

        public void ShowControls()
        {
            controlsWindow.ActiveWindow();
        }

        public void QuitGame()
        {
            Application.Quit();
            
        }
            
        
        
        
        
    }




    
    
}
