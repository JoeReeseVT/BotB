using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseSong : MonoBehaviour
{
    public int SongNumber;
    public void changeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}