using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MusicPlay : MonoBehaviour {
    //Script Author: Roberto Di Biase

    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("music");
        if(obj.Length > 1)
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
