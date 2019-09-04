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
    public UnityEngine.UI.Text loadingText;

    private bool isFading = false;
    private AsyncOperation sceneLoad;

    private void Start()
    {
        progress = 0;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        // Loads async and just returns nothing until it hits 89%
        sceneLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneToLoad);
        sceneLoad.allowSceneActivation = false;

        // >89% means it's just waiting for allowSceneActivation to be true
        while (!sceneLoad.isDone && sceneLoad.progress < 0.89)
        {
            Debug.Log(sceneLoad.progress);
            progress = sceneLoad.progress;
            yield return null;
        }

        progress = 1f;
        loadingText.text = "Press Space to Begin";
    }

    IEnumerator StartFade()
    {
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

    private void Update()
    {
        // If the scene is ready and we're not already fading out, then
        // start the fade out and activation when the player hits space
        if(sceneLoad != null && sceneLoad.progress >= .89f
            && !isFading && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fading");
            isFading = true;
            StartCoroutine(StartFade());
        }
    }
}
