using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int health, soulsToAdd;
    public GameObject enemyObject;
    public GameObject deathEffect;
    public bool isMonster, isSlime, isBat;

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
            Destroy(enemyObject);
            GameManager.instance.UpdateSoulsCount(soulsToAdd);

            if (isBat)
            {
                AudioManager.instance.PlaySFXAdjusted(1);
            }
            if(isMonster)
            {
                AudioManager.instance.PlaySFXAdjusted(2);
            }
            if(isSlime)
            {
                AudioManager.instance.PlaySFXAdjusted(5);
            }

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
        }
    }
}
