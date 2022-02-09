using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("CheckPoint Variables")]
    private CheckPoint[] checkPoints;
    public Vector3 spawnPoint;

    [Header("Soul Collecing System Variables")]
    public int maxSouls = 50;
    public int currentSouls;
    public GameObject soulParticleEffect;

    [Header("Loading Next Level")]
    public string nextLevel;
    public bool levelIsEnding;

    [Header("Respawning")]
    public float waitToRespawn;
    public CinemachineBrain theCM;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
        spawnPoint = PlayerController.instance.transform.position;
    }

    void Update()
    {

    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public void DeactiveCheckPoints()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].ResetCheckPoint();
        }
    }

    public void UpdateSoulsCount(int soulsToAdd)
    {
        currentSouls += soulsToAdd;

        if (currentSouls >= maxSouls)
        {
            currentSouls = maxSouls;
        }

        UIController.instance.UpdateSoulUI();
    }

    public void SubtractSouls()
    {
        currentSouls = 0;
        AudioManager.instance.PlaySFXAdjusted(10);
        Instantiate(soulParticleEffect, PlayerController.instance.transform.position, Quaternion.identity);

        UIController.instance.UpdateSoulUI();
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCO());
    }

    private IEnumerator RespawnCO()
    {
        PlayerHealthController.instance.theSR.enabled = false;
        PauseMenu.instance.canPause = false;
        UIController.instance.isDead = true;
        theCM.enabled = false;

        yield return new WaitForSeconds(waitToRespawn - (1 / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 1f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.transform.position = spawnPoint;
        PlayerHealthController.instance.theSR.enabled = true;
        PlayerController.instance.currentStamina = PlayerController.instance.maxStamina;
        UIController.instance.UpdateStaminaUI();
        PauseMenu.instance.canPause = true;
        UIController.instance.isDead = false;
        theCM.enabled = true;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCO());
    }

    private IEnumerator EndLevelCO()
    {
        PauseMenu.instance.canPause = false;
        UIController.instance.isDead = true;
        levelIsEnding = true;
        PlayerController.instance.theRB.velocity = new Vector2(5f, PlayerController.instance.theRB.velocity.y);

        yield return new WaitForSeconds(2f);
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 1f);

        SceneManager.LoadScene(nextLevel);
    }
}
