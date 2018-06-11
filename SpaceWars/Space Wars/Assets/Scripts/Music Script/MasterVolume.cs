using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour {
    //Script Author: Roberto Di Biase
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        if(volumeSlider.value == 0)
        {
            volumeSlider.value = 1;
        }
    }
    void Update()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
