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


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateHealthUI();
        UpdateStaminaUI();
    }

    void Update()
    {

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

        staminaText.text = "Stamina: " + PlayerController.instance.currentStamina.ToString("F0");

    }

    public void GameOverScreen()
    {
        isDead = true;
        PlayerHealthController.instance.theSR.enabled = false;
        AudioManager.instance.StopLevelMusic();
        StartCoroutine(ShowGameOverScreenCO());

    }

    public IEnumerator ShowGameOverScreenCO()
    {
        yield return new WaitForSeconds(1f);
        gameOverScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restartButton);
        Time.timeScale = 0;
    }
}
