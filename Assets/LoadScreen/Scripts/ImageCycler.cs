using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCycler : MonoBehaviour
{
    // Have to manually list these for now
    public List<Sprite> imagesToShow;
    public float timeBetweenUpdates;
    public string logosFolder;

    // Images we put the sprites on
    private UnityEngine.UI.Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        // Don't distort images, start the image cycling
        image.preserveAspect = true;
        StartCoroutine(UpdateImageCoroutine());
    }

    // Every timeBetweenUpdates seconds, it just sets a new image in the list
    IEnumerator UpdateImageCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenUpdates);
        int currentImage = 0;
        // Runs this until a new scene is loaded (or the coroutine is stopped for some reason)
        while(true)
        {
            // Set next image, will show on the canvas
            image.sprite = imagesToShow[currentImage];
            currentImage = (currentImage + 1) % imagesToShow.Count;
            yield return wait;
        }
    }
}
