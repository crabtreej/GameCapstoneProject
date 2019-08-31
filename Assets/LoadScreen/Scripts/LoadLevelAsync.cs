using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelAsync : MonoBehaviour
{

    public float progress { get; private set; }
    public string SceneToLoad;
    public Fader fader;
    
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        fader.fadingOut = true;
        var sceneLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneToLoad);
        sceneLoad.allowSceneActivation = false;

        while((!sceneLoad.isDone && sceneLoad.progress < 0.89) || fader.fadingOut)
        {
            progress = sceneLoad.progress;
            yield return null;
        }

        progress = 1f;
        fader.fadingIn = true;
        sceneLoad.allowSceneActivation = true;
    }
}
