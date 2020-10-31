using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseSong : MonoBehaviour
{

    public SongMetadata Song;
    public void changeToScene(int ScenetoChangeto)
    {
        infoStruct info = new infoStruct(ScenetoChangeto, Song);
        SceneManager.LoadScene(info.scene);
    }
}

struct infoStruct
{
    public int scene;
    public SongMetadata song;
    public infoStruct(int scenenum, SongMetadata whatsong)
    {
        scene = scenenum;
        song = whatsong;
    }
}