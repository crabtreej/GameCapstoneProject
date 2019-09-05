using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuMasterScript : MonoBehaviour {


    public static bool GameIsPaused = false;
    public GameObject PauseMenu;
    public GameObject OptionsMenu;

    private void Start()
    {
    }
        protected void Update()
        {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        print(Time.timeScale);

        AudioMaster.instance.unPauseSong();
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        AudioMaster.instance.PauseSong();
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitMM()
    {
        Time.timeScale = 0f;
        AudioMaster.instance.StopSong();
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitDesktop()
    {
        Application.Quit();
    }
}
