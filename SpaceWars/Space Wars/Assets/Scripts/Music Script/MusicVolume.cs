using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicVolume : MonoBehaviour {
    //Script Author: Roberto Di Biase
    public Slider volume;
    public AudioSource music;
	
    void Start()
    {
        volume.value = PlayerPrefs.GetFloat("musicVolume");
        if (volume.value == 0)
        {
            volume.value = 1;
        }
    }
	// Update is called once per frame
	void Update () {
        if(music != null)
        {
            music.volume = volume.value;
            PlayerPrefs.SetFloat("musicVolume", volume.value);
        }
    }
}
