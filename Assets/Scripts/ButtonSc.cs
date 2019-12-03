using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSc{

    // Use this for initialization
    public void ToPlayScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ToMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
