using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string NextScreen;
   public void PlayGame()
    {
        SceneManager.LoadScene(NextScreen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
