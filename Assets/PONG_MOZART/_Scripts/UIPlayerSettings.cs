using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIPlayerSettings : MonoBehaviour
{
    [SerializeField] private UIGameMenu mainMenu;
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private TMP_Dropdown mode;
    [SerializeField] private TMP_Dropdown inputs;
    [SerializeField] private Slider reaction;



    private void Start()
    {
        mainMenu.OnGameStart += Set;
    }


    public void OnValueModify()
    {
        inputs.interactable = mode.value == 0;
    }


    public void Set()
    {
        PlayerSettings.Mode playerMode = mode.value == 0 ? PlayerSettings.Mode.Human : PlayerSettings.Mode.Computer;
        PlayerSettings.Inputs playerInput;

        switch (inputs.value)
        {
            case 0:
                playerInput = PlayerSettings.Inputs.Mouse_Y;
                break;
            case 1:
                playerInput = PlayerSettings.Inputs.ArrowsKey;
                break;
            case 2:
                playerInput = PlayerSettings.Inputs.WASD;
                break;
            case 3:
                playerInput = PlayerSettings.Inputs.Joystick;
                break;
            case 4:
                playerInput = PlayerSettings.Inputs.Joystick2;
                break;
            default:
                playerInput = PlayerSettings.Inputs.Mouse_Y;
                break;
        }

        settings.Preferences = new PlayerSettings.Setup(playerMode, playerInput, reaction.value);
    }


    public void OnDestroy()
    {
        mainMenu.OnGameStart -= Set;
    }


}
