using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer mixer;
    public static float volumeLevel = 1.0f;
    public Slider sliderVolumeCtrl;
    private Conductor conductor;

    void Awake()
    {
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseMenuUI.SetActive(false);

        SetLevel(volumeLevel);
        conductor = GameObject.Find("Conductor").GetComponent(typeof(Conductor)) as Conductor;
        sliderVolumeCtrl.value = volumeLevel;
    }

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
    }

    public void TogglePauseMenu() {
        if (GameisPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        //restart the game:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("GameAudioVolume", Mathf.Log10(sliderValue) * 20);
        volumeLevel = sliderValue;
    }
}
