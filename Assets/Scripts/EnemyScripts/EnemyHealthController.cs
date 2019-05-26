using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [Header("UI Settings")]

    public Canvas Canvas;

    public Image MainHB;

    public Image WhiteHBPart;

    public Text HealthText;

    [Range(0f, 30f)]
    public float DamageOnDeath = 20f;

    public int EnergyCost = 10;

    [Range(0.5f, 3f)]
    public float DamageOnDeathRadius = 1f;

    [Header("HP Settings")]
    public int StartHealth = 100;

    [Header("ExplosionFX")]
    public GameObject ExplosionEffect;

    [Range(0.5f, 3f)]
    public float UIAnimSpeed = 2;

    private int _currentHealth;  

    public string PoolTag = "EnemyPrefabs";

    void Start()
    {
        Canvas.enabled = false;
        _currentHealth = StartHealth;
        HealthText.text = "HEALTH : " + _currentHealth.ToString();
    }

    public delegate void OnSendKill();
    public static event OnSendKill OnKill;

    private bool _isSwapping = false;

    public void ReceiveDamage(int damage)
    {       
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
            return;
        }
        var coeff = (float)_currentHealth / StartHealth;
        MainHB.color = new Color(1 - coeff, coeff, 0);
        HealthText.GetComponent<Text>().text = "HEALTH : " + _currentHealth.ToString();
        MainHB.fillAmount = (float)_currentHealth / (float)StartHealth;
    }

    void Die()
    {
        OnKill();
        var a = Instantiate(ExplosionEffect, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(a, 1.7f);

        TimeController.instance.EnableBulletTime(1);
        CameraController.instance.MoveToBulletTime(transform);
        PlayerStatsController.instance.ChangeEnergy(EnergyCost);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(Canvas.transform.position, Camera.main.transform.position) < 30) Canvas.enabled = true;
        WhiteHBPart.fillAmount = Mathf.Lerp(WhiteHBPart.fillAmount, MainHB.fillAmount, UIAnimSpeed * Time.deltaTime);
    }

}
