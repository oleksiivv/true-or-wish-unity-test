using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStateController : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;

    void Start(){
        this.updateSound();
    }

    public void updateSound(){
        source.enabled=PlayerPrefs.GetInt("!sound",0)==0;
        source.clip=clips[Random.Range(0,clips.Length)];
        source.Play();
    }
}
