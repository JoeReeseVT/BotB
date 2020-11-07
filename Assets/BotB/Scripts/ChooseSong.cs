using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseSong : MonoBehaviour
{
    public static int songint;
    public SongMetadata Song;
    public void songPick(int songnum)
    {
        songint = songnum;
        
        SceneManager.LoadScene(1);
    }
}
