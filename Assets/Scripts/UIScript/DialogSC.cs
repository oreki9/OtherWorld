using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSC : MonoBehaviour {

    List<string> text = new List<string>() {"Musuh datang dari kanan pilih tombol","dibawah untuk mengeluarkan sihir"};
    public float time = 0;
    int timeIntBanding = 0;
    int textListNow = 0;
    public Text TextBox;
    string StrNow = "";
    // Update is called once per frame
    void Update () {
        if (textListNow < text.Count)
        {
            if ((int)time == timeIntBanding)
            {
                if (time <= text[textListNow].Length)
                {
                    StrNow += text[textListNow][timeIntBanding];
                    timeIntBanding += 1;
                }
            }
            if(time>= text[textListNow].Length+5)
            {
                textListNow += 1;
                time = 0;
                timeIntBanding = 0;
                StrNow = "";
            }
            TextBox.text = StrNow;
            time += 0.2f;
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
