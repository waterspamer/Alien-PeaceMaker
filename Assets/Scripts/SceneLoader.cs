using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.UnloadSceneAsync("MainScene");
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
    public void AppQuit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
