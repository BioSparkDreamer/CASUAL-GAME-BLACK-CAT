using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Health Variables")]
    public Slider healthSlider;
    public TMP_Text healthText;

    [Header("Stamina Variables")]
    public Slider staminaSlider;
    public TMP_Text staminaText;

    [Header("Game Over Screen")]
    public GameObject gameOverScreen;
    public bool isDead;
    public GameObject restartButton;

    [Header("Soul Collect Variables")]
    public Slider soulSlider;
    public TMP_Text soulText;

    [Header("Fade Screen Variables")]
    public Image fadeScreen;
    public float fadeSpeed;
    public bool shouldFadeToBlack, shouldFadeFromBlack;

    [Header("Time In Level Variables")]
    public float timerInLevel = 150f;
    public TMP_Text timerText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateHealthUI();
        UpdateStaminaUI();
        UpdateSoulUI();
        FadeFromBlack();
    }

    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.r, fadeScreen.color.b,
            Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }

        if (timerInLevel > 0)
        {
            timerInLevel -= Time.deltaTime;
            timerText.text = "Time Left " + timerInLevel.ToString("F0");
        }
        else if (timerInLevel <= 0)
        {
            timerText.text = "Out of Time!";
            GameOverScreen();
        }
    }

    public void UpdateHealthUI()
    {
        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = PlayerHealthController.instance.currentHealth;

        healthText.text = "Life Force: " + PlayerHealthController.instance.currentHealth.ToString()
        + "/" + PlayerHealthController.instance.maxHealth.ToString();
    }

    public void UpdateStaminaUI()
    {
        staminaSlider.maxValue = PlayerController.instance.maxStamina;
        staminaSlider.value = PlayerController.instance.currentStamina;

        if (PlayerController.instance.currentStamina >= PlayerController.instance.maxStamina)
        {
            PlayerController.instance.currentStamina = PlayerController.instance.maxStamina;
        }

        staminaText.text = "Stamina: " + PlayerController.instance.currentStamina.ToString("F0");

    }

    public void UpdateSoulUI()
    {
        soulSlider.maxValue = GameManager.instance.maxSouls;
        soulSlider.value = GameManager.instance.currentSouls;

        soulText.text = "Souls: " + GameManager.instance.currentSouls.ToString() + "/" + GameManager.instance.maxSouls.ToString();
    }

    public void GameOverScreen()
    {
        isDead = true;
        PauseMenu.instance.canPause = false;
        AudioManager.instance.StopLevelMusic();
        StartCoroutine(ShowGameOverScreenCO());

    }

    public IEnumerator ShowGameOverScreenCO()
    {
        yield return new WaitForSeconds(0.8f);
        gameOverScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restartButton);
        Time.timeScale = 0;
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}
