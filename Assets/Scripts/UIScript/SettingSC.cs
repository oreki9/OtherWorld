using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSC : MonoBehaviour {

    public float vol;
    public ButtonSc SceneCh;
    void Start()
    {
        vol = PlayerPrefs.GetFloat("Volume", 0);
    }
    public void GetBackToMenu()
    {
        SceneCh.ToMenuScene();
    }
}
