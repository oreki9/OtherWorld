using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSc : MonoBehaviour{

    // Use this for initialization
    public void ToPlayScene()
    {
        StartCoroutine((LoadAsyncScene("MainScene")));
    }
    public void ToMenuScene()
    {
        StartCoroutine(LoadAsyncScene("MenuScene"));
    }
    public void ToCreditScene()
    {
        StartCoroutine((LoadAsyncScene("Credits")));
    }
    public void ToSetingScene()
    {
        StartCoroutine((LoadAsyncScene("Setting")));
    }
    IEnumerator LoadAsyncScene(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
