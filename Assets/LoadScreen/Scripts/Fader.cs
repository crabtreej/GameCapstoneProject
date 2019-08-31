using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader: MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 0.01f;

    public bool fadingOut = false;
    public bool fadingIn = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UpdateAlpha(fadeImage, -fadeImage.color.a);
    }

    private void Update()
    {
        if(fadingOut)
        {
            if(fadeImage.color.a < 1)
            {
                UpdateAlpha(fadeImage, fadeSpeed);
            }
            else
            {
                fadingOut = false;
            }
        }
        else if(fadingIn)
        {
            if(fadeImage.color.a > 0)
            {
                UpdateAlpha(fadeImage, -fadeSpeed);
            }
            else
            {
                fadingIn = false;
                Destroy(gameObject);
            }
        }
    }

    void UpdateAlpha(Image i, float val)
    {
        var color = i.color;
        if(color.a + val > 1)
        {
            color.a = 1;
        }
        else if(color.a + val < 0)
        {
            color.a = 0;
        }
        else
        {
            color.a += val;
        }

        i.color = color;
    }
}
