using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelAsync : MonoBehaviour
{

    public float progress { get; private set; }
    
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        var sceneLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("ToonMaze");
        sceneLoad.allowSceneActivation = false;

        while(!sceneLoad.isDone && sceneLoad.progress < 0.89)
        {
            progress = sceneLoad.progress;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        sceneLoad.allowSceneActivation = true;
        progress = 1f;
    }
}
