using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Awake()
    {
        PlayerStatsController.DeathSender += LoseLogic;
        TankController.WinSender += WinLogic;
    }

    public void LoseLogic()
    {
        UIManager.instance.ShowEndUI(false);
    }
    public void WinLogic()
    {
        UIManager.instance.ShowEndUI(true);
    }
}
