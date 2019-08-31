using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowProgress : MonoBehaviour
{
    public LoadLevelAsync levelLoader;

    private Image progressBar;
    void Start()
    {
        progressBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //progressBar.fillAmount = levelLoader.progress;
    }
}
