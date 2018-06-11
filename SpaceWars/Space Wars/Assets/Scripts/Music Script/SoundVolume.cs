using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour {
    //Script Author: Roberto Di Biase
    public static Slider volume;
    public static AudioSource music;

    void Start()
    {
        volume.value = PlayerPrefs.GetFloat("soundVolume");
        if (volume.value == 0)
        {
            volume.value = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (music != null)
        {
            music.volume = volume.value;
            PlayerPrefs.SetFloat("soundVolume", volume.value);
        }
    }
}
