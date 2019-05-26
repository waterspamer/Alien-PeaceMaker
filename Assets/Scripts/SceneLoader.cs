using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
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
