using UnityEngine;
using UnityEngine.UI;

public class ShowProgress : MonoBehaviour
{
    // Not in use right now, but this is needed for setting the progress bar
    public LoadLevelAsync levelLoader;
    private Image progressBar;

    void Start()
    {
        progressBar = GetComponent<Image>();
    }

    void Update()
    {
        progressBar.fillAmount = levelLoader.progress;
    }
}
