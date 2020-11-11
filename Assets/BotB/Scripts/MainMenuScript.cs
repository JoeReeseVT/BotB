using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    //menu states
    public enum MenuStates { Main, Controls};
    public MenuStates currentState;

    void Awake()
    {
        currentState = MenuStates.Main;
    }

    void Update()
    {
        switch (currentState)
        {
            case MenuStates.Main:
                break;

            case MenuStates.Controls:
                break;


        }
    }

    public void OnControls()
    {
        
    }

    public void OnMainMenu()
    {
        
    }
}
