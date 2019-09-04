using System.Collections;
using UnityEngine;

public class LoadLevelAsync : MonoBehaviour
{
    // Publish the progress so other objects can know
    public float progress { get; private set; }
    // Name of the scene to load. Must be in the exported scenes or something in Project Settings
    public string SceneToLoad;
    // Need fader just to tell it when to start fading
    public Fader fader;

    void Start()
    {
        // Check on the load scene progress every scene
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        // Loads async and just returns nothing until it hits 89%
        var sceneLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneToLoad);
        sceneLoad.allowSceneActivation = false;

        // >89% means it's just waiting for allowSceneActivation to be true
        while (!sceneLoad.isDone && sceneLoad.progress < 0.89)
        {
            progress = sceneLoad.progress;
            yield return null;
        }

        // Start fading out
        fader.fadingOut = true;
        while(fader.fadingOut)
        {
            yield return null;
        }

        // Once it's faded out, start fading in (the fader persists across 
        // scenes) and then activate the new scene so Unity swaps them out
        progress = 1f;
        fader.fadingIn = true;
        sceneLoad.allowSceneActivation = true;
    }
}
