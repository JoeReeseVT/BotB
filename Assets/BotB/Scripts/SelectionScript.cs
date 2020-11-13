using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    //menu states
    public enum MenuStates { Player1, Player2, Song };
    public MenuStates currentState;

    public GameObject P1Menu;
    public GameObject P2Menu;
    public GameObject SongMenu;

    void Awake()
    {
        currentState = MenuStates.Player1;
    }

    void Update()
    {

        if (currentState == MenuStates.Player1)
        {
            P1Menu.SetActive(true);
            P2Menu.SetActive(false);
            SongMenu.SetActive(false);
        }
        else if (currentState == MenuStates.Player2)
        {
            P2Menu.SetActive(true);
            P1Menu.SetActive(false);
            SongMenu.SetActive(false);
        }
        else
        {
            P2Menu.SetActive(false);
            P1Menu.SetActive(false);
            SongMenu.SetActive(true);
        }
    }

    /*
    public void OnP1()
    {
        UnityEngine.Debug.Log("Controls button pressed");
        currentState = MenuStates.Player1;
    }
    */

    public void OnP2()
    {
        UnityEngine.Debug.Log("Menu button pressed");
        currentState = MenuStates.Player2;
    }

    public void OnSong()
    {
        UnityEngine.Debug.Log("Menu button pressed");
        currentState = MenuStates.Song;
    }
}
