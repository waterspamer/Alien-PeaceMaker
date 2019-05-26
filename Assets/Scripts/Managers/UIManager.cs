using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UISettings")]

    public Canvas UICanvas;

    public Button ContinueBut;

    public Text ResultText;

    public Canvas MainCanvas;

    private bool _isPaused;

    private bool _isEndOfGame;

    public static UIManager instance;

    void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ResultText.gameObject.SetActive(false);
        _isEndOfGame = false;

        UICanvas.enabled = false;
        
        _isPaused = false;
    }

    void PauseGame()
    {
        AudioManager.instance.StopBGMusic();
        TimeController.instance.PauseTime();
        UICanvas.enabled = true;
        MainCanvas.enabled = false;
    }
    public void UnpauseGame()
    {
        if (_isEndOfGame) return;
        AudioManager.instance.ContinueBGMusic();
        TimeController.instance.UnpauseTime();
        UICanvas.enabled = false;
        MainCanvas.enabled = true;
    }

    public void ShowEndUI(bool won)
    {
        _isEndOfGame = true;
        PauseGame();
        ResultText.gameObject.SetActive(true);
        if (won)
        {
            ResultText.text = "YOU WON !";
            ResultText.color = Color.green;
        }
        else
        {
            ResultText.text = "YOU LOST !";
            ResultText.color = Color.red;
        }
        ContinueBut.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                UnpauseGame();
            }
            else
            PauseGame();

            _isPaused = !_isPaused;
            
        }
    }
}
