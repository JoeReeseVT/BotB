using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseSong : MonoBehaviour
{
    //public static int choosesong;
    public static SongMetadata choosesong;
    public void songPick(SongMetadata songpick)
    {
        choosesong = songpick;
        
        SceneManager.LoadScene(1);
    }
}
