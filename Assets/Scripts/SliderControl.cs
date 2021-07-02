using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderControl : MonoBehaviour
{
    public Scrollbar scr;
    private float oldVolume;
    // Start is called before the first frame update
    void Start()
    {
        oldVolume = scr.value;
        if (!PlayerPrefs.HasKey("volume")) scr.value = 1;
        else scr.value = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        if (oldVolume != scr.value){
            PlayerPrefs.SetFloat("volume", scr.value);
            PlayerPrefs.Save();
            oldVolume = scr.value;        }
    }
}
