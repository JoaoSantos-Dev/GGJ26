using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInputConfirmation : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    private Image backgroundImage;
    private TMP_Text confirmationText;
    private  string ConfirmMessage => $"P{playerData.Id} OK";


    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        confirmationText = GetComponentInChildren<TMP_Text>();
    }

    public void ConfirmInput()
    {
        confirmationText.SetText(ConfirmMessage);
        backgroundImage.color = playerData.Color;
    }
    
}
