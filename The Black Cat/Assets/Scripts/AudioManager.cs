using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source Variables")]
    public AudioSource[] sfx;
    public AudioSource levelMusic, menuMusic;
    public bool isLevel, isMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if (isLevel)
        {
            levelMusic.Play();
        }

        if (isMenu)
        {
            menuMusic.Play();
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(int sfxToPlay)
    {
        sfx[sfxToPlay].pitch = Random.Range(.8f, 1.2f);
        sfx[sfxToPlay].Play();
    }

    public void StopLevelMusic()
    {
        levelMusic.Pause();
        menuMusic.Play();
    }

    public void ResumeLevelMusic()
    {
        levelMusic.Play();
        menuMusic.Stop();
    }
}
