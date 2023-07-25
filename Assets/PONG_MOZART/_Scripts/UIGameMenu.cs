using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class UIGameMenu : MonoBehaviour
{
    public Action OnGameStart;

    [SerializeField] private FieldSettings settings;
    [SerializeField] private TMP_Dropdown diceLevel;


    public void StartGame()
    {
        OnGameStart?.Invoke();
        settings.diceLevel = diceLevel.value == 0 ? FieldSettings.Level.Mozart : FieldSettings.Level.Stadler;
        SceneManager.LoadScene(1);
    }


}
