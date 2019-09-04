using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public string GameScreen;
   public string OptionScreen;
   public void PlayGame()
    {
        SceneManager.LoadScene(GameScreen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene(OptionScreen);
    }

}
