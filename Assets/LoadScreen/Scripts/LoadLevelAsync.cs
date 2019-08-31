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
        var sceneLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneToLoad);
        sceneLoad.allowSceneActivation = false;

        while (!sceneLoad.isDone && sceneLoad.progress < 0.89)
        {
            progress = sceneLoad.progress;
            yield return null;
        }

        fader.fadingOut = true;
        while(fader.fadingOut)
        {
            yield return null;
        }

        progress = 1f;
        fader.fadingIn = true;
        sceneLoad.allowSceneActivation = true;
    }
}
