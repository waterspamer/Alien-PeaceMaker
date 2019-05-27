using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("Sound Presets")]
    public AudioClip DoubleKill;
    public AudioClip TripleKill;
    public AudioClip UltraKill;
    public AudioClip Rampage;
    public AudioClip MachineGun;


    private AudioSource _player;

    public AudioSource BGMusicPlayer;

    private AudioSource _mGPlayer;

    public static AudioManager instance;

    void Awake()
    {
        if (instance != null) return;
        instance = this;
        _player = GetComponent<AudioSource>();
        _mGPlayer = gameObject.AddComponent<AudioSource>();
        _mGPlayer.clip = MachineGun;
        _mGPlayer.loop = true;
    }

    public void PlayMGSound()
    {
        _mGPlayer.Play();
    }
    public void StopMGSound()
    {
        _mGPlayer.Stop();
    }
    public void PlayBGMusic()
    {
        BGMusicPlayer.Play();
    }

    public void ContinueBGMusic()
    {
        BGMusicPlayer.UnPause();
    }

    public void StopBGMusic()
    {
        BGMusicPlayer.Pause();
    }
    
    public void PlayStreak(int killcount)
    {
        switch (killcount)
        {
            case 2:
                {
                    _player.clip = DoubleKill;
                    _player.Play();
                    break;
                }
            case 3:
                {
                    _player.clip = TripleKill;
                    _player.Play();
                    break;
                }
            case 4:
                {
                    _player.clip = UltraKill;
                    _player.Play();
                    break;
                }
            case 5:
                {
                    _player.clip = Rampage;
                    _player.Play();
                    break;
                }
        }
    }
}
