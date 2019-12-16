using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnVolume : MonoBehaviour {

    public SettingSC settingsc;
    public float Thisvol;
    public void SetVolume()
    {
        settingsc.vol = Thisvol;
        PlayerPrefs.SetFloat("Volume", Thisvol);
    }
    void Update()
    {
        if (settingsc.vol >= Thisvol)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = new Color(0.26f, 0.26f, 0.26f);
        }
    }
}
