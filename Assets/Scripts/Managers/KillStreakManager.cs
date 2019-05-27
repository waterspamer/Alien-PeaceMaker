using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillStreakManager : MonoBehaviour
{

    private int _killCount;

    public Text StreakText;

    public static KillStreakManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        EnemyHealthController.OnKill += AddKillCounter;
        _killCount = 0;
    }

    void OnDisable()
    {
        EnemyHealthController.OnKill -= AddKillCounter; 
    }

    private void AddKillCounter()
    {
        StopCoroutine(Countdown());
        _killCount++;
        _killCount = _killCount > 5 ? 5 : _killCount;
        if (_killCount > 1)
        {
            KillStreakLogic(_killCount);
            StartCoroutine(Countdown());
        }      
    }

    private void KillStreakLogic (int killcount)
    {
        AudioManager.instance.PlayStreak(killcount);
        StartCoroutine(ShowKillText(killcount));
    }

    IEnumerator ShowKillText(int killcount)
    {
        switch (killcount)
        {
            case 2:
                StreakText.text = "DOUBLE KILL !";
                break;
            case 3:
                StreakText.text = "TRIPLE KILL !";
                break;
            case 4:
                StreakText.text = "ULTRA KILL !";
                break;
            case 5:
                StreakText.text = "RAMPAGE !";
                break;
        }
        yield return new WaitForSeconds(2f);
        StreakText.text = "";
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(10);
        _killCount = 0;
    }
}
