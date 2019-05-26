using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    private float _timeScaleDelta;

    private float _bTimeMultiplier = 0.2f;

    [SerializeField] private Canvas _uiCanvas;

    private void Awake()
    {
        instance = this;
        _timeScaleDelta = Time.timeScale / _bTimeMultiplier;
        Time.timeScale = 1;
    }

    IEnumerator TimeScaleSwapper(float time)
    {
        _uiCanvas.enabled = false;
        EnableBulletTime();
        yield return new WaitForSeconds(time / _timeScaleDelta);
        _uiCanvas.enabled = true;
        DisableBulletTime();
    }

    private float _timeScale;

    public void PauseTime()
    {
        _timeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void UnpauseTime()
    {
        Time.timeScale = _timeScale;
    }

    public void EnableBulletTime(float time)
    {
        StartCoroutine(TimeScaleSwapper(time));
    }

    public void EnableBulletTime()
    {
        Time.timeScale = _bTimeMultiplier;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void DisableBulletTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

}
