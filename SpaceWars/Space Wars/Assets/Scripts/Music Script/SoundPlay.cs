using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour {
    //Script Author: Roberto Di Biase
    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("soundEffect");
        if (obj.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
