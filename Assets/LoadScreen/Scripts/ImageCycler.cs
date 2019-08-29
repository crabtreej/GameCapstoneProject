using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageCycler : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public List<Sprite> imagesToShow;
    public float timeBetweenUpdates;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateImageCoroutine());
    }

    IEnumerator UpdateImageCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenUpdates);
        int currentImage = 0;
        while(true)
        {
            image.sprite = imagesToShow[currentImage];
            currentImage = (currentImage + 1) % imagesToShow.Count;
            yield return wait;
        }
    }
}
