using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text ScoreTxt;
    public ButtonSc BtnScript;
    public GameObject LoadingPnl;
    void Start()
    {
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
        PlayerPrefs.SetString("Magic", "0 7");
        PlayerPrefs.SetInt("Point", 0);
        LoadScore();
    }
    public void BtnPlaySc()
    {
        LoadingPnl.SetActive(true);
        BtnScript.ToPlayScene();
    }
    public void BtnCredits()
    {
        LoadingPnl.SetActive(true);
        BtnScript.ToCreditScene();
    }
    public void BtnSetting()
    {
        LoadingPnl.SetActive(true);
        BtnScript.ToSetingScene();
    }
}
