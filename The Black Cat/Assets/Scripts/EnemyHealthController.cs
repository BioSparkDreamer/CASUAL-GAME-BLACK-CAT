using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int health, soulsToAdd;
    public GameObject enemyObject;

    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(int damageToDeal)
    {
        health -= damageToDeal;

        if (health <= 0)
        {
            health = 0;
            enemyObject.SetActive(false);
            GameManager.instance.UpdateSoulsCount(soulsToAdd);
        }
    }
}
