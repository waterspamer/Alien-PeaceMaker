using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{
    [Header("Start stats")]
    public int StartHP = 100;

    public int StartEnergy = 100;

    public int MaxEnergy = 100;

    public int FieldCost = 100;


    [HideInInspector]
    public float _currentHP { get; private set; }
    [HideInInspector]
    public float _currentEnergy { get; private set; }

    

    [Header("UI Settings")]

    public Canvas UICanvas;

    public Image MainHB;

    public Image MainEB;

    public Image WhiteHBPart;

    public Text HealthText;

    public static PlayerStatsController instance;

    public delegate void OnDeath();
    public static event OnDeath DeathSender;

    public bool Forced
    {
        get;
        private set;
    }

    void Awake()
    {
        if (instance != null) return;
        instance = this;
        Forced = false;
    }

    void Start()
    {
        _currentHP = StartHP;
        _currentEnergy = StartEnergy;
        MainEB.fillAmount = (float)_currentEnergy / (float)MaxEnergy;
    }

    public void SetForceField(bool a)
    {
        if (a) ChangeEnergy(-FieldCost);
        Forced = a;
    }


    public void ChangeHP(float value)
    {
        _currentHP += value;
        if (_currentHP <= 0)
        {
            Die();
            return;
        }
        var coeff = (float)_currentHP / StartHP;
        MainHB.color = new Color(1 - coeff, coeff, 0);
        MainHB.fillAmount = (float)_currentHP / (float)StartHP;
    }

    public void Die()
    {
       DeathSender();
    }

    public void ChangeEnergy(float value)
    {
        _currentEnergy += value;
        _currentEnergy = _currentEnergy > MaxEnergy ? MaxEnergy : _currentEnergy;
        MainEB.fillAmount = (float)_currentEnergy / (float)MaxEnergy;
    }

    public bool IsEnoughEnergy(int value)
    {
        return _currentEnergy >= value;
    }
}
