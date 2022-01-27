using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("Fullscreen Toggle Variables")]
    public Toggle fullScreenToggle;
    [HideInInspector] public bool isFullScreenOn;
    [HideInInspector] public int screenInt;

    void Start()
    {
        //FullScreen Toggle Save
        screenInt = PlayerPrefs.GetInt("FullScreenState");
        if (screenInt == 1)
        {
            fullScreenToggle.isOn = false;
        }
        else
        {
            isFullScreenOn = true;
            fullScreenToggle.isOn = true;
        }
    }

    void Update()
    {

    }

    public void FullScreenMode(bool isFullScreenOn)
    {
        Screen.fullScreen = isFullScreenOn;

        if (isFullScreenOn == false)
        {
            isFullScreenOn = false;
            PlayerPrefs.SetInt("FullScreenState", 1);
        }
        else
        {
            isFullScreenOn = true;
            PlayerPrefs.SetInt("FullScreenState", 0);
            Debug.Log("Changing to FullScreen");
        }
    }
}
