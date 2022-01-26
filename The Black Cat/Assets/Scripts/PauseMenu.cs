using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [Header("Pausing Variables")]
    public GameObject pauseMenu;
    public bool isPaused;
    public GameObject resumeButton;

    [Header("Loading to Main Menu")]
    public string loadToMenu;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            ResumeandUnPause();
        }
    }

    public void ResumeandUnPause()
    {
        //Do if Player is Pausing the Game
        if (!pauseMenu.activeInHierarchy && !isPaused && !UIController.instance.isDead)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
            AudioManager.instance.StopLevelMusic();

            //Make EventSystem select resume button when pausing game
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }

        //Do if player is UnPausing the Game
        else
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
            AudioManager.instance.ResumeLevelMusic();
        }
    }

    public void QuitToMenu()
    {
        ChangeTimeScale();
        SceneManager.LoadScene(loadToMenu);
    }

    public void RestartScene()
    {
        ChangeTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeTimeScale()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFXAdjusted(0);
    }
}
