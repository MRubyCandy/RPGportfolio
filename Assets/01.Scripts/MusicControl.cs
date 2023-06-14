using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicslider;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetMusicValue()
    {
        mixer.SetFloat("Music", Mathf.Log10(musicslider.value) * 20);
    }
    public void SetSFXValue()
    {
        mixer.SetFloat("SFX", Mathf.Log10(musicslider.value) * 20);
    }
}
