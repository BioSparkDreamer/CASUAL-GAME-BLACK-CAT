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
    [HideInInspector] public int screenInt;

    [Header("FPS Counter Variables")]
    public Toggle fpsToggle;
    public TMP_Text fpsText;
    public GameObject fpsCounterObject;
    [HideInInspector] public int fpsInt;

    [Header("Vsync Toggle Variables")]
    public Toggle syncToggle;
    [HideInInspector] public int syncInt;

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
            fullScreenToggle.isOn = true;
        }

        //FPS Counter Toggle Save
        fpsInt = PlayerPrefs.GetInt("FpsToggleState");
        if (fpsInt == 1)
        {
            fpsToggle.isOn = false;
            fpsCounterObject.SetActive(false);
        }
        else
        {
            fpsToggle.isOn = true;
            fpsCounterObject.SetActive(true);
        }

        //Vsync Toggle Save
        syncInt = PlayerPrefs.GetInt("SyncToggleState");
        if (syncInt == 1)
        {
            syncToggle.isOn = false;
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            syncToggle.isOn = true;
            QualitySettings.vSyncCount = 1;
        }
    }

    void Update()
    {
        //Calculating FPS
        float fps = 1 / Time.unscaledDeltaTime;
        fpsText.text = "FPS: " + fps.ToString("F2");
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

    public void FpsToggleMode(bool isFpsOn)
    {
        if (isFpsOn == false)
        {
            PlayerPrefs.SetInt("FpsToggleState", 1);
            fpsCounterObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("FpsToggleState", 0);
            fpsCounterObject.SetActive(true);
            Debug.Log("FPS Counter is Off");
        }
    }

    public void VsyncToggleMode(bool isSyncOn)
    {
        if (isSyncOn == false)
        {
            PlayerPrefs.SetInt("SyncToggleState", 1);
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            PlayerPrefs.SetInt("SyncToggleState", 0);
            QualitySettings.vSyncCount = 1;
            Debug.Log("Vsync is On");
        }
    }
}
