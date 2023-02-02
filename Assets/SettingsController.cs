using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public GameObject buttonMutedSound, buttonNormalSound;

    public AudioStateController audio;

    public GameObject settingsPanel;
    void Start()
    {
        updateSound();
    }

    public void muteSound(){
        PlayerPrefs.SetInt("!sound",1);
        updateSound();
        audio.updateSound();

        //GetComponent<AudioSource>().enabled=false;
    }

    public void unmuteSound(){
        PlayerPrefs.SetInt("!sound",0);
        updateSound();
        audio.updateSound();

        //GetComponent<AudioSource>().enabled=true;
    }

    void updateSound(){
        if(PlayerPrefs.GetInt("!sound")==0){

            buttonMutedSound.SetActive(false);
            buttonNormalSound.SetActive(true);

        }
        else{
            buttonMutedSound.SetActive(true);
            buttonNormalSound.SetActive(false);
        }
        //audio.updateSound();
    }

    public void closePanel(){
        settingsPanel.SetActive(false);
    }

    public void openPanel(){
        settingsPanel.SetActive(true);
    }

    public void rate(){
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Vertex+Studio+Games");
    }
}
