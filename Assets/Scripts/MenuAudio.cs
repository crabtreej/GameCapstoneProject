﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudio : MonoBehaviour
{
    public Slider slider;
    public AudioSource MenuMusic;

    void Update()
    {
        MenuMusic.volume = slider.value;
    }
}