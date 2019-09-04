using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public string GameScreen;
   public void PlayGame()
    {
        SceneManager.LoadScene(GameScreen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
