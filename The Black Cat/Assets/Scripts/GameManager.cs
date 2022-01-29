using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("CheckPoint Variables")]
    private CheckPoint[] checkPoints;
    public Vector3 spawnPoint;

    [Header("Soul Collecing System Variables")]
    public int maxSouls = 100;
    public int currentSouls;

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

    public void Respawn()
    {
        PlayerHealthController.instance.TakeDamage(1);
        PlayerController.instance.transform.position = spawnPoint;
    }
}
