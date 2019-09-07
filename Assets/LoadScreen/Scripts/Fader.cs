using UnityEngine;
using UnityEngine.UI;

public class Fader: MonoBehaviour
{
    // Just a blank blue image to cover the screen
    public Image fadeImage;
    public float fadeSpeed = 0.01f;

    [HideInInspector]
    public bool fadingOut = false;
    [HideInInspector]
    public bool fadingIn = false;

    // Start is called before the first frame update
    void Start()
    {
        // We need this object to persist across the next scene to fade back in
        DontDestroyOnLoad(gameObject);
        // Set the alpha to be 0 (idk what the alpha starts as)
        UpdateAlpha(fadeImage, -fadeImage.color.a);
    }

    private void Update()
    {
        // If we're fading out, increase alpha until the screen is covered
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
        // If we're fading in, make it transparent again and then destroy it
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

    // Updates alpha, clamped between 0 and 1
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
