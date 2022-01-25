using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Header("Player Health Variables")]
    public int maxHealth = 9;
    public int currentHealth;

    [Header("Invincibility Vartiables")]
    public float invincibleLength;
    private float invincibleCounter;

    [Header("Player Related Variables")]
    public SpriteRenderer theSR;
    public float transparentPerHit, spriteAlphaValue, startAlphaValue;
    public Color hurtColor, defaultColor;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentHealth = maxHealth;
        defaultColor = theSR.color;
    }

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        startAlphaValue = spriteAlphaValue;
    }

    void Update()
    {
        //Count down the invincible Counter when above 0
        if (invincibleCounter > 0)
        {
            theSR.color = new Color(hurtColor.r, hurtColor.g, hurtColor.b, spriteAlphaValue);
            invincibleCounter -= Time.deltaTime;
        }
        else
        {
            theSR.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, spriteAlphaValue);
        }
    }

    public void TakeDamage(int damageToDeal)
    {
        if (invincibleCounter <= 0)
        {
            currentHealth -= damageToDeal;

            if (damageToDeal <= 1)
            {
                theSR.color = new Color(1f, 1f, 1f, spriteAlphaValue - transparentPerHit);
                spriteAlphaValue = theSR.color.a;
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                UIController.instance.GameOverScreen();
            }

            invincibleCounter = invincibleLength;
            UIController.instance.UpdateHealthUI();
        }
    }

    public void RestoreHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;

        if (healthToAdd > 1)
        {
            theSR.color = new Color(1f, 1f, 1f, startAlphaValue);
            spriteAlphaValue = theSR.color.a;
        }
        else if (healthToAdd <= 1)
        {
            theSR.color = new Color(1f, 1f, 1f, spriteAlphaValue + transparentPerHit);
            spriteAlphaValue = theSR.color.a;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthUI();
    }
}
