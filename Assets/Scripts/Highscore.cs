using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text ScoreTxt;
    ButtonSc BtnScript;
    void Start()
    {
        BtnScript = new ButtonSc();
        LoadScore();
    }
    public void LoadScore()
    {
        if (ScoreTxt != null)
        {
            ScoreTxt.text = PlayerPrefs.GetInt("Score", 0).ToString();
        }
    }
    public void SetScore(int score)
    {
        PlayerPrefs.SetInt("Score", score);
    }
    public void ResetHScore()
    {
        PlayerPrefs.SetInt("Score", 0);
        LoadScore();
    }
    public void BtnPlaySc()
    {
        BtnScript.ToPlayScene();
    }
}
