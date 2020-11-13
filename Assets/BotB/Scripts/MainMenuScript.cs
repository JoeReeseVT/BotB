using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    //menu states
    public enum MenuStates { Main, Controls};
    public MenuStates currentState;

    public GameObject MainMenu;
    public GameObject ControlsMenu;

    void Awake()
    {
        currentState = MenuStates.Main;
    }

    void Update()
    {

        if (currentState == MenuStates.Main)
        {
            MainMenu.SetActive(true);
            ControlsMenu.SetActive(false);
        }
        else
        {
            ControlsMenu.SetActive(true);
            MainMenu.SetActive(false);
        }
    }

    public void OnControls()
    {
        UnityEngine.Debug.Log("Controls button pressed");
        currentState = MenuStates.Controls;
    }

    public void OnMainMenu()
    {
        UnityEngine.Debug.Log("Menu button pressed");
        currentState = MenuStates.Main;
    }
}
